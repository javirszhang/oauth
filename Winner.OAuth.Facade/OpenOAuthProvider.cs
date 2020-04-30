using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.DataAccess.Interfaces;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 获取 OPEN_ID
    /// </summary>
    public class OpenOAuthProvider : FacadeBase
    {
        private string _appid;
        private string _secret;
        private string _auth_code;
        private string _grant_type;
        public OpenOAuthProvider(string appid, string secret, string auth_code, string grant_type)
        {
            this._appid = appid;
            this._secret = secret;
            this._auth_code = auth_code;
            this._grant_type = grant_type;
        }
        public UserOpenModel OAuthUser { get; private set; }
        public bool OAuthAccess()
        {
            var app = OAuthAppCache.Get(this._appid);
            if (app == null)
            {
                Alert("无效的应用编号");
                return false;
            }
            //Tauth_Code daCode = new Tauth_Code();
            var daCode = DaoFactory.Tauth_Code();
            if (!daCode.SelectByAppId_GrantCode(app.Id, this._auth_code))
            {
                Alert("无效的授权码");
                return false;
            }
            if (daCode.Status == 1)
            {
                Alert("该授权码已被使用，不能重复使用");
                return false;
            }
            if (daCode.Expire_Time < DateTime.Now)
            {
                Alert("授权码已过期");
                return false;
            }
            daCode.Status = 1;
            if (!daCode.Update())
            {
                Alert("授权码验证失败");
                return false;
            }
            var scope = ScopeCache.Get(daCode.Scope_Id);
            BeginTransaction();
            UserTokenProvider utp = new UserTokenProvider(app, daCode.User_Id, daCode, scope.Code);
            utp.ReferenceTransactionFrom(Transaction);
            if (!utp.GenerateUserToken())
            {
                Rollback();
                Alert(utp.PromptInfo);
                return false;
            }
            this.OAuthUser = utp.OAuthUser;
            if (!UpdateTokenRights(utp.TokenId, utp.Refresh_Timeout, daCode.Right_Json))
            {
                Rollback();
                return false;
            }
            Commit();
            return true;
        }
        private bool UpdateTokenRights(int tokenId, DateTime timeout, string rightJson)
        {
            if (string.IsNullOrEmpty(rightJson))
            {
                Log.Info("TokenId={0},无API授权信息", tokenId);
                return true;
            }
            try
            {
                List<CodePrivilege> rights = Javirs.Common.Json.JsonSerializer.Deserializer<List<CodePrivilege>>(rightJson);
                if (rights == null || rights.Count <= 0)
                {
                    return true;
                }
                List<int> apis = new List<int>();
                foreach (CodePrivilege gcr in rights)
                {
                    if (gcr.Type == 0)//Group
                    {
                        apis.AddRange(GetGroupApis(gcr.Id));
                    }
                    else//api-info
                    {
                        apis.Add(gcr.Id);
                    }
                }
                if (!AddOrUpdate(tokenId, timeout, apis))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("更新令牌权限失败", ex);
                Alert("更新令牌权限失败");
                return false;
            }
        }
        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="expireTime"></param>
        /// <param name="apis"></param>
        /// <returns></returns>
        public bool AddOrUpdate(int tokenId, DateTime expireTime, List<int> apis)
        {
            if (apis == null || apis.Count <= 0)
            {
                return true;
            }
            BeginTransaction();
            foreach (int api_id in apis)
            {
                //Tauth_Token_Right daTRight = new Tauth_Token_Right();
                var daTRight = DaoFactory.Tauth_Token_Right();
                daTRight.ReferenceTransactionFrom(Transaction);
                if (daTRight.SelectByTokenId_ApiId(tokenId, api_id))
                {
                    if (daTRight.Have_Right == 0 || daTRight.Expire_Time < expireTime)
                    {
                        daTRight.Have_Right = 1;
                        daTRight.Expire_Time = expireTime;
                        if (!daTRight.Update())
                        {
                            Rollback();
                            return false;
                        }
                    }
                }
                else
                {
                    daTRight.Api_Id = api_id;
                    daTRight.Expire_Time = expireTime;
                    daTRight.Have_Right = 1;
                    daTRight.Last_Modify_Time = DateTime.Now;
                    daTRight.Token_Id = tokenId;
                    if (!daTRight.Insert())
                    {
                        Rollback();
                        return false;
                    }
                }
            }
            Commit();
            return true;
        }
        private List<int> GetGroupApis(int groupId)
        {
            //Tauth_Group_RightCollection daRightsCollection = new Tauth_Group_RightCollection();
            var daRightsCollection = DaoFactory.Tauth_Group_RightCollection();
            daRightsCollection.ListByGroup_Id(groupId);
            List<int> result = new List<int>();
            foreach (ITauth_Group_Right right in daRightsCollection)
            {
                result.Add(right.Api_Id);
            }
            return result;
        }
    }
}
