using Javirs.Common.Json;
using Javirs.Common.Net;
using Javirs.Common.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;

namespace Winner.OAuth.Facade.Thirdparty
{
    public class GpuAuthLoginBehavior : ThirdpartyLoginBehavior
    {
        public GpuAuthLoginBehavior(ThirdpartyConf conf) : base(conf)
        {
        }

        public override string GetAuthenticationUrl(string state, string redirectUri)
        {
            string host = ConfigProvider.app("gpu", "domain");
            string apiUrl = string.Concat(host, "/openapi/oauth/authorize/", Config.AppId, "/", Config.Platform);
            var http = new HttpHelper(apiUrl);
            http.Get();
            if (http.StatusCode != 200)
            {
                return null;
            }
            var response = http.GetResponse();
            JsonObject JObject = JsonObject.Parse(response.Result);
            string resCode = JObject.GetString("resCode");
            string resMsg = JObject.GetString("resMsg");
            if (resCode != "0000")
            {
                return null;
            }
            JsonObject JIO = JObject.GetObject("data");
            string authenticationUrl = JIO.GetString("url");
            return authenticationUrl;
        }

        public override ThirdpartyJsApiTicket GetJsapiTicket()
        {
            throw new NotImplementedException();
        }

        public override ThirdpartyAccessToken GetPlatformAccessToken()
        {
            throw new NotImplementedException();
        }

        public override ThirdpartyUserInfo GetUserInfo(string openid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public override ThirdpartyUserToken GetUserToken(HttpContext ctx)
        {
            string cipher_openid = ctx.Request.Query["openid"];
            string cipher_token = ctx.Request.Query["token"];
            DesEncodeDecode des = new DesEncodeDecode(this.Config.Secret);
            byte[] bytes_openid = des.DesEncrypt(Base58.Decode(cipher_openid));
            byte[] bytes_token = des.DesEncrypt(Base58.Decode(cipher_token));
            ThirdpartyUserToken ut = new ThirdpartyUserToken
            {
                AccessToken = Encoding.UTF8.GetString(bytes_token),
                OpenID = Encoding.UTF8.GetString(bytes_openid)
            };
            return ut;
        }
    }
}
