using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class RegisterService : Register.RegisterBase
    {
        public override Task<UserRegisterResponseDTO> UserRegister(UserRegisterRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                UserRegisterResponseDTO response = new UserRegisterResponseDTO();
                var app = OAuthAppCache.Get(request.Appid);
                if (app == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的应用ID";
                    return response;
                }
                //先注册，再登录发放TOKEN
                RegisterProvider provider = new RegisterProvider(request.UserCode, request.Password, request.SmsCode, request.RefereeCode);
                if (!provider.Register())
                {
                    response.RetCode = xUtils.TransformFailRetCode(provider.PromptInfo.ResultType);
                    response.RetMsg = provider.PromptInfo.CustomMessage;
                    return response;
                }
                LoginProvider loginProvider = new LoginProvider(request.UserCode, request.Password, "basic");
                if (loginProvider.Login(request.Client.Type, request.Client.System, request.Client.DeviceId, request.Client.Ip, request.Client.SessionId, request.Client.Version, app.Id))
                {
                    response.Data = new UserRegisterResponseDTO.Types.Result
                    {
                        Expires = loginProvider.OAuthUser.Expire_In,
                        Openid = loginProvider.OAuthUser.Open_Id,
                        RefreshExpires = loginProvider.OAuthUser.Refresh_Expire_In,
                        RefreshToken = loginProvider.OAuthUser.Refresh_Token,
                        Token = loginProvider.OAuthUser.Token,
                        UserCode = loginProvider.User.UserCode
                    };
                }
                if (response.Data == null)
                {
                    Log.Info(string.Concat("注册成功，登录失败！", Environment.NewLine, loginProvider.PromptInfo.MessageStack));
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                return response;
            });
        }
    }
}
