﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade.Caches;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 授权登录提供程序
    /// </summary>
    public class GrantTokenPrivilegeProvider : FacadeBase
    {
        private string _appid, _scope, _device_id;
        private int _userId;
        /// <summary>
        /// 授权登录提供程序
        /// </summary>
        /// <param name="appid">app代码</param>
        /// <param name="secret">app密码</param>
        /// <param name="userId">用户账号</param>
        /// <param name="scope">授权范围</param>
        public GrantTokenPrivilegeProvider(string appid, int userId, string scope, string device_id)
        {
            this._appid = appid;
            this._userId = userId;
            this._scope = scope;
            this._device_id = device_id;
        }
        /// <summary>
        /// 执行授权
        /// </summary>
        /// <returns></returns>
        public bool Grant(bool takeAll, params CodePrivilege[] rights)
        {
            OAuthApp app = OAuthAppCache.Get(this._appid);
            if (app == null)
            {
                Alert("未注册的应用");
                return false;
            }
            string[] scopeCodes = this._scope.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var scope = ScopeCache.Get(scopeCodes);
            if (scope == null || scope.Count <= 0)
            {
                Alert("未定义的授权类型");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByID(this._userId);
            if (user == null)
            {
                Alert("用户信息加载失败");
                return false;
            }
            if (CheckAlreadyAuth(app.Id, user.UserId))
            {
                return true;
            }
            if (takeAll && (rights == null || rights.Length <= 0))
            {
                var temp = ScopeRightProvider.GetScopeRights(this._scope);
                rights = new CodePrivilege[temp.Count];
                for (int i = 0; i < rights.Length; i++)
                {
                    rights[i] = new CodePrivilege
                    {
                        Id = temp[i].Right_Id,
                        Type = temp[i].Right_Type
                    };
                }
            }
            this.Auth_Code = Guid.NewGuid().ToString("N");
            //Tauth_Code daCode = new Tauth_Code();
            var daCode = DaoFactory.Tauth_Code();
            daCode.App_Id = app.Id;
            daCode.Expire_Time = DateTime.Now.AddMinutes(5);
            daCode.Grant_Code = this.Auth_Code;
            daCode.Scope_Id = scope.FirstOrDefault().Id;
            daCode.User_Id = user.UserId;
            daCode.Device_Id = this._device_id;
            if (rights != null && rights.Length > 0)
            {
                daCode.Right_Json = Javirs.Common.Json.JsonSerializer.JsonSerialize(rights);
            }
            if (!daCode.Insert())
            {
                Alert("授权失败，请重试！");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查是否已经授权了
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool CheckAlreadyAuth(int appid, int userId)
        {
            //Tauth_Code daCode = new Tauth_Code();
            var daCode = DaoFactory.Tauth_Code();
            if (!daCode.SelectByAppid_UserId(appid, userId))
            {
                return false;
            }
            if (daCode.Expire_Time < DateTime.Now)
            {
                return false;
            }
            if (daCode.Status == 1)
            {
                return false;
            }
            this.Auth_Code = daCode.Grant_Code;
            return true;
        }
        /// <summary>
        /// 授权码
        /// </summary>
        public string Auth_Code { get; set; }
    }
}
