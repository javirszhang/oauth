using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Entities;
using Winner.OAuth.Api.Models;
using Winner.User.Interface;
using Winner.OAuth.Facade.Thirdparty;

namespace Winner.OAuth.Api.Controllers
{
    /// <summary>
    /// 第三方快速登陆
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ThirdpartyController : OAuthControllerBase
    {
        private readonly ThirdpartyLoginFactory Factory;
        /// <summary>
        /// 第三方快速登录
        /// </summary>
        /// <param name="factory"></param>
        public ThirdpartyController(ThirdpartyLoginFactory factory)
        {
            this.Factory = factory;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ResponseResult<ThirdPartyLoginResult> Login(ThirdpartyLoginRequestDTO model)
        {
            var app = OAuthAppCache.Get(model.Appid);
            if (app == null)
            {
                return Fail<ThirdPartyLoginResult>("无效的应用id", "0400");
            }
            string plainText;
            if (!xUtils.RsaDecrypt(model.AuthCode, out plainText))
            {
                return Fail<ThirdPartyLoginResult>("授权码解密失败");
            }
            int pos = plainText.IndexOf('_');
            string[] array = new string[2];
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                return Fail<ThirdPartyLoginResult>("授权码明文格式不正确", "0400");
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                return Fail<ThirdPartyLoginResult>("请求已过期", "0403");
            }
            string trueOpenID = array[1];
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByVoucher(trueOpenID, (UserVoucherType)model.PlatformID);
            var thirdLogin = new ThirdPartyLoginProvider(user);

            string csource = Request.Headers["clientsource"];
            int.TryParse(csource, out int clientSource);
            string clientSystem = Request.Headers["clientsystem"];
            string device_id = Request.Headers["device_id"];
            string userHostAddress = Request.Headers["X-FORWARD-FOR"];
            string sessionId = Request.Headers["sessionId"];
            string clientVersion = Request.Headers["clientversion"];
            //若登录失败，客户端需调用绑定手机号
            var result = thirdLogin.Login(clientSource, clientSystem, device_id, clientVersion, userHostAddress, sessionId, app.Id);
            if (!result.Success)
            {
                return Fail<ThirdPartyLoginResult>("首次使用第三方登录，请先绑定账号！", "0202");
            }
            return Success(result.Content);
        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("bind")]
        public ResponseResult<ThirdPartyLoginResult> Bind(ThirdPartyBindingModel model)
        {
            var app = OAuthAppCache.Get(model.Appid);
            if (app == null)
            {
                return Fail<ThirdPartyLoginResult>("无效的应用id","0400");
            }
            //先绑定手机号
            string plainText;
            if (!xUtils.RsaDecrypt(model.AuthCode, out plainText))
            {
                return Fail<ThirdPartyLoginResult>( "授权码解密失败");
            }
            int pos = plainText.IndexOf('_');
            string[] array = new string[2];
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                return Fail<ThirdPartyLoginResult>( "授权码明文格式不正确");
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                return Fail<ThirdPartyLoginResult>("请求已过期","0400");
            }
            ThirdPartyBindingProvider binding = new ThirdPartyBindingProvider(model);
            if (!binding.Register())
            {
                return Fail<ThirdPartyLoginResult>( binding.PromptInfo.CustomMessage);
            }
            //再调用登录
            int clientSource = 0;
            string csource = Request.Headers["clientsource"];
            int.TryParse(csource, out clientSource);
            string clientSystem = Request.Headers["clientsystem"];
            string device_id = Request.Headers["device_id"];
            string userHostAddress = Request.Headers["X-FORWARD-FOR"];
            string sessionId = Request.Headers["sessionId"];
            string clientVersion = Request.Headers["clientversion"];
            var thirdLogin = new ThirdPartyLoginProvider(binding.User);
            var result = thirdLogin.Login(clientSource, clientSystem, device_id, clientVersion, userHostAddress, sessionId, app.Id);
            if (!result.Success)
            {
                return Fail<ThirdPartyLoginResult>("第三方账号绑定成功，但登陆失败，请重新登录！","0202");
            }
            //return new ApiResult<ThirdPartyLoginResult> { retCode = "0000", retMsg = "ok", Data = result.Content };
            return Success(result.Content);
        }
        /// <summary>
        /// 获取第三方平台授权链接
        /// </summary>
        /// <returns></returns>
        [HttpGet("{platform}")]
        public ResponseResult GetAuthorizationUrl(string platform, string redirectUri, string state)
        {
            if (!Enum.TryParse(platform, true, out ThirdpartyLoginPlatform thirdparty))
            {
                return Fail("无效的第三方平台名称", "0400");
            }
            var provider = this.Factory.GetBehavior(platform);
            string url = provider.GetAuthenticationUrl(state, redirectUri);
            return Success(new { url });
        }
        /// <summary>
        /// 获取用户授权token
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        [HttpGet("{platform}/token")]
        public ResponseResult GetUserToken(string platform)
        {
            var provider = this.Factory.GetBehavior(platform);
            var ut = provider.GetUserToken(this.HttpContext);
            if (ut == null)
            {
                return Fail("获取用户授权token失败");
            }
            return Success(ut);
        }
    }
}
