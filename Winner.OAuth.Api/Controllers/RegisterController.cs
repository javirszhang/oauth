using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Api.Models;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.Api.Controllers
{
    /// <summary>
    /// 注册
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : OAuthControllerBase
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<UserRegisterResponseDTO> UserRegister(UserRegisterRequestDTO request)
        {
            UserRegisterResponseDTO response = new UserRegisterResponseDTO();
            var app = OAuthAppCache.Get(request.Appid);
            if (app == null)
            {
                return Fail<UserRegisterResponseDTO>("无效的应用ID", "0400");
            }
            //先注册，再登录发放TOKEN
            RegisterProvider provider = new RegisterProvider(request.UserCode, request.Password, request.SmsCode, request.RefereeCode);
            if (!provider.Register())
            {
                return Fail<UserRegisterResponseDTO>(provider.PromptInfo.CustomMessage);
            }
            LoginProvider loginProvider = new LoginProvider(request.UserCode, request.Password, "basic");
            if (loginProvider.Login(request.Client.Type, request.Client.System, request.Client.DeviceId, request.Client.IP, request.Client.SessionId, request.Client.Version, app.Id))
            {
                response.RegisterResult = true;
                response.Expires = loginProvider.OAuthUser.Expire_In;
                response.Openid = loginProvider.OAuthUser.Open_Id;
                response.RefreshExpires = loginProvider.OAuthUser.Refresh_Expire_In;
                response.RefreshToken = loginProvider.OAuthUser.Refresh_Token;
                response.Token = loginProvider.OAuthUser.Token;
                response.UserCode = loginProvider.User.UserCode;
            }
            if (!response.RegisterResult)
            {
                Log.Info(string.Concat("注册成功，登录失败！", Environment.NewLine, loginProvider.PromptInfo.MessageStack));
            }
            return Success(response);
        }
    }
}
