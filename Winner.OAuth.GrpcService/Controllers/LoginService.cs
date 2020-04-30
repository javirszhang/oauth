using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class LoginService : Login.LoginBase
    {
        public override Task<LoginResponseDTO> LoginByAccount(LoginRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                LoginResponseDTO response = new LoginResponseDTO();
                Log.Info($"deviceId={request.Client.DeviceId}&clientver={request.Client.Version}&source={request.Client.Type}&userCode={request.Usercode}&password={request.Password}");
                var app = OAuthAppCache.Get(request.Appid);
                if (app == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "未注册的应用";
                    return response;
                }

                LoginProvider loginProvider = new LoginProvider(request.Usercode, request.Password, request.Scopes, (LoginType)request.LoginType);
                if (!loginProvider.Login(request.Client.Type, request.Client.System, request.Client.DeviceId, request.Client.Ip, request.Client.SessionId, request.Client.Version, app.Id))
                {
                    response.RetCode = xUtils.TransformFailRetCode(loginProvider.PromptInfo.ResultType);
                    response.RetMsg = loginProvider.PromptInfo.CustomMessage;
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new LoginResponseDTO.Types.Result()
                {
                    Token = loginProvider.Token,
                    Expires = loginProvider.OAuthUser.Expire_In,
                    Openid = loginProvider.OAuthUser.Open_Id,
                    RefreshExpires = loginProvider.OAuthUser.Refresh_Expire_In,
                    RefreshToken = loginProvider.OAuthUser.Refresh_Token,
                };
                return response;
            });
        }
    }
}
