using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Api.Models;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : OAuthControllerBase
    {
        /// <summary>
        /// 使用账号密码直接登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<LoginResponseDTO> LoginByAccount(LoginRequestDTO request)
        {
            Log.Info($"deviceId={request.Client.DeviceId}&clientver={request.Client.Version}&source={request.Client.Type}&userCode={request.UserCode}&password={request.Password}");
            var app = OAuthAppCache.Get(request.Appid);
            if (app == null)
            {
                return Fail<LoginResponseDTO>("无效的应用id", "0400");
            }

            LoginProvider loginProvider = new LoginProvider(request.UserCode, request.Password, request.Scopes, (LoginType)request.LoginType);
            if (!loginProvider.Login(request.Client.Type, request.Client.System, request.Client.DeviceId, request.Client.IP, request.Client.SessionId, request.Client.Version, app.Id))
            {
                return Fail<LoginResponseDTO>(loginProvider.PromptInfo.CustomMessage);
            }
            LoginResponseDTO response = new LoginResponseDTO()
            {
                Token = loginProvider.Token,
                Expires = loginProvider.OAuthUser.Expire_In,
                Openid = loginProvider.OAuthUser.Open_Id,
                RefreshExpires = loginProvider.OAuthUser.Refresh_Expire_In,
                RefreshToken = loginProvider.OAuthUser.Refresh_Token,
            };
            return Success(response);
        }
    }
}
