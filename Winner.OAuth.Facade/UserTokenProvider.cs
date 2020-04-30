using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;
using Winner.OAuth.DataAccess.Interfaces;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Token;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 用户授权访问令牌管理功能
    /// </summary>
    public class UserTokenProvider : FacadeBase
    {
        private int _userid;
        private int? _appid, _authid;
        private ITauth_Code _daCode;
        private string _deviceid;
        private OAuthApp _app;
        private string _scope;
        /// <summary>
        /// 用户授权访问令牌管理功能
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <param name="authid"></param>
        /// <param name="deviceid"></param>
        public UserTokenProvider(int? appid, int userid, int? authid, string deviceid, string scope)
        {
            this._appid = appid;
            this._userid = userid;
            this._authid = authid;
            this._deviceid = deviceid;
            this.OAuthUser = new UserOpenModel();
            this._scope = scope;
            //this.OAuthUser.Expire_In = expireIn;//token过期时间，单位秒
            //this.OAuthUser.Refresh_Expire_In = reExpires;//refresh token过期时间，单位天
        }
        /// <summary>
        /// 用户授权访问令牌管理功能
        /// </summary>
        /// <param name="app"></param>
        /// <param name="userid"></param>
        /// <param name="dao"></param>
        /// <param name="deviceid"></param>
        public UserTokenProvider(OAuthApp app, int userid, ITauth_Code dao, string scope) : this(app?.Id, userid, dao?.Auth_Id, dao.Device_Id, scope)
        {
            this._daCode = dao;
            this._app = app;
        }
        /// <summary>
        /// 生成用户授权访问令牌
        /// </summary>
        /// <returns></returns>
        public bool GenerateUserToken()
        {
            try
            {
                if (_app == null && !_appid.HasValue)
                {
                    Alert(Winner.Framework.Utils.ResultType.无效数据类型, "无效的应用编号");
                    return false;
                }
                if (_app == null)
                {
                    _app = OAuthAppCache.Get(_appid.Value);
                }
                this.OAuthUser.Expire_In = _app.TokenExpireIn;
                this.OAuthUser.Refresh_Expire_In = 30;
                var fac = UserModuleFactory.GetUserModuleInstance();
                IUser user = fac?.GetUserByID(_userid);
                if (user == null)
                {
                    Alert(Winner.Framework.Utils.ResultType.数据库查不到数据, "用户不存在");
                    return false;
                }
                if (_daCode == null)
                {
                    //_daCode = new Tauth_Code();
                    _daCode = DaoFactory.Tauth_Code();
                    if (this._authid.HasValue)
                    {
                        if (!_daCode.SelectByPk(this._authid.Value))
                        {
                            Alert(Winner.Framework.Utils.ResultType.无效数据类型, "无效的授权码");
                            return false;
                        }
                    }
                    else
                    {
                        _daCode.App_Id = _app.Id;
                        _daCode.Expire_Time = DateTime.Now.AddMinutes(5);
                        _daCode.Grant_Code = Guid.NewGuid().ToString("N").ToLower();
                        _daCode.Scope_Id = ScopeCache.Get(this._scope).Id;
                        _daCode.User_Id = user.UserId;
                        _daCode.Device_Id = _deviceid;
                        _daCode.Remarks = "客户端登录自动授权";
                        _daCode.Status = 1;
                        if (!_daCode.Insert())
                        {
                            Alert(Winner.Framework.Utils.ResultType.非法操作, "登录授权失败");
                            return false;
                        }
                    }
                }
                int refresh_token_expire_in = this.OAuthUser.Refresh_Expire_In * 86400;
                string userCode = user.GetUserVoucher(UserVoucherType.自定义号码);
                string open_id = EncryptOpenId(_app.Id, user.UserId, userCode, _app.UidEncryptKey);
                this.OAuthUser.Open_Id = open_id;
                this.OAuthUser.Token = EncryptAccessToken(user.UserId, userCode, _app.Id, this.OAuthUser.Expire_In);
                this.OAuthUser.Refresh_Token = EncryptAccessToken(user.UserId, userCode, _app.Id, refresh_token_expire_in);
                BeginTransaction();
                //Tauth_Token daToken = new Tauth_Token();
                var daToken = DaoFactory.Tauth_Token();
                daToken.ReferenceTransactionFrom(Transaction);
                bool exist = daToken.SelectByAppId_UserId_DeviceId(_app.Id, this._userid, this._deviceid);
                daToken.App_Id = _app.Id;
                daToken.Expire_Time = DateTime.Now.AddSeconds(this.OAuthUser.Expire_In);
                daToken.Refresh_Timeout = DateTime.Now.AddDays(this.OAuthUser.Refresh_Expire_In);
                daToken.Refresh_Token = this.OAuthUser.Refresh_Token;
                daToken.Token_Code = this.OAuthUser.Token;
                daToken.Scope_Id = _daCode.Scope_Id;
                daToken.User_Id = _userid;
                daToken.Grant_Id = _daCode.Auth_Id;
                daToken.Device_Id = this._deviceid;
                if (exist)
                {
                    daToken.Last_Access_Time = DateTime.Now;
                    if (!daToken.Update())
                    {
                        Rollback();
                        Alert(Winner.Framework.Utils.ResultType.数据库更新失败, "TOKEN生成失败");
                        return false;
                    }
                }
                else
                {
                    if (!daToken.Insert())
                    {
                        Rollback();
                        Alert(Winner.Framework.Utils.ResultType.数据库更新失败, "TOKEN生成失败");
                        return false;
                    }
                }
                this.TokenId = daToken.Token_Id;
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("生成token失败", ex);
                Alert(ResultType.系统异常, "生成token失败");
                return false;
            }
        }

        /// <summary>
        /// 加密信息获得TOKEN
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <param name="userCode">会员账号</param>
        /// <param name="seconds">TOKEN有效期（秒），默认一天86400秒</param>
        /// <returns></returns>
        public static string EncryptAccessToken(int userId, string userCode, int appid, int seconds = 86400)
        {
            var token = new UserToken
            {
                UserCode = userCode,
                UserId = userId,
                Expire_Time = DateTime.Now.AddSeconds(seconds),
                AppId = appid,
                Verifiable = true
            };
            string cipherText = token.ToCipherToken();
            return cipherText;
        }
        public static string EncryptOpenId(int appid, int userId, string userCode, string key)
        {
            OAuthOpenID openObj = new OAuthOpenID
            {
                AppId = appid,
                UserId = userId,
                UserCode = userCode
            };
            return openObj.ToCipherString();
        }
        public static FuncResult<UserToken> DecryptAccessToken(string access_token)
        {
            var token = UserToken.FromCipherToken(access_token);
            return FuncResult.SuccessResult(token);
        }
        public UserOpenModel OAuthUser { get; protected set; }
        public int TokenId { get; protected set; }
        public DateTime Refresh_Timeout { get; protected set; }
    }
}
