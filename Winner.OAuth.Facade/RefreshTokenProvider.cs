using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.DataAccess.Interfaces;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Token;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    public class RefreshTokenProvider : FacadeBase
    {
        private string _refresh_token;
        private string _appid;
        public RefreshTokenProvider(string appid, string refresh_token)
        {
            this._refresh_token = refresh_token;
            this._appid = appid;
            this.OAuthUser = new UserOpenModel();
            this.OAuthUser.Expire_In = 7200;
        }
        public UserOpenModel OAuthUser { get; private set; }
        public bool Refresh()
        {
            var app = OAuthAppCache.Get(this._appid);
            if (app == null)
            {
                Alert(ResultType.非法操作, "未知的应用ID");
                return false;
            }
            var DecryptRes = UserTokenProvider.DecryptAccessToken(this._refresh_token);
            if (!DecryptRes.Success)
            {
                Alert(ResultType.非法操作, DecryptRes.Message);
                return false;
            }
            UserToken token = DecryptRes.Content;
            if (token.Expire_Time < DateTime.Now)
            {
                Alert(ResultType.需要登陆, "令牌已过期，请重新发起用户授权");
                return false;
            }
            //Tauth_Token daToken = new Tauth_Token();
            var daTokenCollection = DaoFactory.Tauth_TokenCollection();
            if (!daTokenCollection.ListByUserId_AppId(token.UserId, app.Id))
            {
                Alert(ResultType.无权限, "未找到授权记录，无效的刷新令牌");
                return false;
            }
            ITauth_Token daToken = null;
            foreach (ITauth_Token item in daTokenCollection)
            {
                if (item.Refresh_Token.Equals(this._refresh_token))
                {
                    daToken = item;
                }
            }
            if (daToken == null)
            {
                Alert(ResultType.非法操作, "无效的刷新令牌");
                return false;
            }
            if (daToken.Refresh_Timeout < DateTime.Now)
            {
                Alert(ResultType.无权限, "令牌已过期，请重新发起用户授权");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByID(daToken.User_Id);
            if (user == null)
            {
                Alert("用户不存在");
                return false;
            }
            string userCode = user.GetUserVoucher(UserVoucherType.自定义号码);
            string newToken = UserTokenProvider.EncryptAccessToken(token.UserId, userCode, app.Id);
            daToken.Token_Code = newToken;
            daToken.Expire_Time = DateTime.Now.AddSeconds(this.OAuthUser.Expire_In);
            if (!daToken.Update())
            {
                Alert(ResultType.系统异常, "Token刷新失败，请重试");
                return false;
            }
            this.OAuthUser.Open_Id = UserTokenProvider.EncryptOpenId(app.Id, token.UserId, userCode, app.UidEncryptKey);
            this.OAuthUser.Token = newToken;
            this.OAuthUser.Refresh_Token = this._refresh_token;
            this.OAuthUser.Refresh_Expire_In = (int)(daToken.Refresh_Timeout - DateTime.Now).TotalDays;
            return true;
        }
    }
}
