using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.OAuth.Facade.Thirdparty
{
    public abstract class ThirdpartyLoginBehavior
    {
        public ThirdpartyConf Config { get; protected set; }
        public ThirdpartyLoginBehavior(ThirdpartyConf conf)
        {
            this.Config = conf;
        }
        public abstract ThirdpartyUserToken GetUserToken(HttpContext ctx);
        public abstract ThirdpartyUserInfo GetUserInfo(string openid, string accessToken);
        public abstract ThirdpartyAccessToken GetPlatformAccessToken();
        public abstract ThirdpartyJsApiTicket GetJsapiTicket();
        public abstract string GetAuthenticationUrl(string state, string redirectUri);
    }
}
