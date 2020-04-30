using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    public class ThirdPartyLoginProvider : FacadeBase
    {
        private IUser _user;
        public ThirdPartyLoginProvider(IUser user)
        {
            this._user = user;
        }

        public FuncResult<ThirdPartyLoginResult> Login(int clientSource, string clientSys, string deviceId, string clientVer, string ipAddress, string session_id, int appId)
        {
            if (this._user == null)
            {
                return FuncResult.FailResult<ThirdPartyLoginResult>("未注册", 404);
            }
            LoginProvider localLogin = new LoginProvider(_user.UserCode, null, "basic");
            localLogin.IgnorePassword = true;
            if (!localLogin.Login(clientSource, clientSys, deviceId, ipAddress, session_id, clientVer, appId))
            {
                return FuncResult.FailResult<ThirdPartyLoginResult>(localLogin.PromptInfo.CustomMessage, (int)localLogin.PromptInfo.ResultType);
            }
            var data = new ThirdPartyLoginResult
            {
                Token = localLogin.Token,
                UserCode = _user.UserCode,
                Expires = localLogin.OAuthUser.Expire_In,
                RefreshExpires = localLogin.OAuthUser.Refresh_Expire_In,
                RefreshToken = localLogin.OAuthUser.Refresh_Token,
                Openid = localLogin.OAuthUser.Open_Id
            };
            return FuncResult.SuccessResult(data);
        }
    }
    public class ThirdPartyLoginResult
    {
        public string UserCode { get; set; }
        public string Token { get; set; }
        public int Expires { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshExpires { get; set; }
        public string Openid { get; set; }
    }
}
