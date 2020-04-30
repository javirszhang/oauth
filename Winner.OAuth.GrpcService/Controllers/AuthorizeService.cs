using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Token;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class AuthorizeService : Authorization.AuthorizationBase
    {
        public override Task<GrantResponseDTO> GrantByAccount(GrantByAccountRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var response = new GrantResponseDTO();
                OAuthApp app = OAuthAppCache.Get(request.Appid);
                List<Scope> scope = ScopeCache.Get(request.Scopes.Split(','));
                if (app == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的应用id";
                    return response;
                }
                string ip = context.GetHttpContext().Request.Headers["X-FORWARD-IP"];
                LoginProvider login = new LoginProvider(request.Account, request.Password, request.Scopes, LoginType.LOGIN_BY_PASSWORD);
                if (!login.Login(request.Client.Type, request.Client.System, request.Client.DeviceId, ip, request.Client.SessionId, request.Client.Version, app.Id))
                {
                    response.RetCode = "0500";
                    response.RetMsg = login.PromptInfo.CustomMessage;
                    return response;
                }
                CodePrivilege[] privileges = null;
                if (request.Grants != null && request.Grants.Count > 0)
                {
                    privileges = new CodePrivilege[request.Grants.Count];
                    for (int i = 0; i < request.Grants.Count; i++)
                    {
                        privileges[i] = new CodePrivilege { Id = request.Grants[i].Id, Type = request.Grants[i].Type };
                    }
                }
                GrantTokenPrivilegeProvider grant = new GrantTokenPrivilegeProvider(app.Appid, login.User.UserId, request.Scopes, request.Client.DeviceId);
                if (!grant.Grant(request.GrantAll, privileges))
                {
                    response.RetCode = "0500";
                    response.RetMsg = "授权失败，请重试";
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new GrantResponseDTO.Types.Result
                {
                    Code = grant.Auth_Code
                };
                return response;
            });
        }
        public override Task<GrantResponseDTO> GrantByToken(GrantByTokenRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                GrantResponseDTO response = new GrantResponseDTO();
                UserToken token = UserToken.FromCipherToken(request.Token);
                if (token == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的token";
                    return response;
                }
                OAuthApp app = OAuthAppCache.Get(request.Appid);
                if (app == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的应用id";
                    return response;
                }
                if (app.Id != token.AppId)
                {
                    response.RetCode = "0403";
                    response.RetMsg = "无效的token";
                    return response;
                }
                CodePrivilege[] privileges = null;
                if (request.Grants != null && request.Grants.Count > 0)
                {
                    privileges = new CodePrivilege[request.Grants.Count];
                    for (int i = 0; i < request.Grants.Count; i++)
                    {
                        privileges[i] = new CodePrivilege { Id = request.Grants[i].Id, Type = request.Grants[i].Type };
                    }
                }
                GrantTokenPrivilegeProvider grant = new GrantTokenPrivilegeProvider(app.Appid, token.UserId, request.Scopes, request.Client.DeviceId);
                if (!grant.Grant(request.GrantAll, privileges))
                {
                    response.RetCode = "0500";
                    response.RetMsg = "授权失败，请重试";
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new GrantResponseDTO.Types.Result
                {
                    Code = grant.Auth_Code
                };
                return response;
            });
        }
    }
}
