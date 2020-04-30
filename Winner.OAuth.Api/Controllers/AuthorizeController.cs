using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Api.Models;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Token;

namespace Winner.OAuth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : OAuthControllerBase
    {
        /// <summary>
        /// 使用账号获取授权码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost] // api/authorize
        public ResponseResult<GrantResponseDTO> GrantByAccount(GrantByAccountRequestDTO data)
        {
            OAuthApp app = OAuthAppCache.Get(data.Appid);
            List<Scope> scope = ScopeCache.Get(data.Scopes.Split(','));
            if (app == null)
            {
                return Fail<GrantResponseDTO>("无效的应用id", "0400");
            }
            string ip = Request.Headers["X-FORWARD-IP"];
            LoginProvider login = new LoginProvider(data.Account, data.Password, data.Scopes, LoginType.LOGIN_BY_PASSWORD);
            if (!login.Login(data.Client.Type, data.Client.System, data.Client.DeviceId, ip, data.Client.SessionId, data.Client.Version, app.Id))
            {
                return Fail<GrantResponseDTO>(login.PromptInfo.CustomMessage, "0500");
            }
            CodePrivilege[] privileges = null;
            if (data.Privileges != null && data.Privileges.Count > 0)
            {
                privileges = new CodePrivilege[data.Privileges.Count];
                for (int i = 0; i < data.Privileges.Count; i++)
                {
                    privileges[i] = new CodePrivilege { Id = data.Privileges[i].Id, Type = data.Privileges[i].Type };
                }
            }
            GrantTokenPrivilegeProvider grant = new GrantTokenPrivilegeProvider(app.Appid, login.User.UserId, data.Scopes, data.Client.DeviceId);
            if (!grant.Grant(data.GrantAll, privileges))
            {
                return Fail<GrantResponseDTO>("授权失败，请重试", "0500");
            }
            var response = new GrantResponseDTO
            {
                Code = grant.Auth_Code
            };
            return Success(response);
        }
        /// <summary>
        /// 客户端使用已有token获取新授权码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("client")] // api/authorize/client
        public ResponseResult<GrantResponseDTO> GrantByToken(GrantByTokenRequestDTO data)
        {
            GrantResponseDTO response = new GrantResponseDTO();
            UserToken token = UserToken.FromCipherToken(data.Token);
            if (token == null)
            {
                return Fail<GrantResponseDTO>("无效的token", "0400");
            }
            OAuthApp app = OAuthAppCache.Get(data.Appid);
            if (app == null)
            {
                return Fail<GrantResponseDTO>("无效的应用id", "0400");
            }
            if (app.Id != token.AppId)
            {
                return Fail<GrantResponseDTO>("无效的token", "0500");
            }
            CodePrivilege[] privileges = null;
            if (data.Privileges != null && data.Privileges.Count > 0)
            {
                privileges = new CodePrivilege[data.Privileges.Count];
                for (int i = 0; i < data.Privileges.Count; i++)
                {
                    privileges[i] = new CodePrivilege { Id = data.Privileges[i].Id, Type = data.Privileges[i].Type };
                }
            }
            GrantTokenPrivilegeProvider grant = new GrantTokenPrivilegeProvider(app.Appid, token.UserId, data.Scopes, data.Client.DeviceId);
            if (!grant.Grant(data.GrantAll, privileges))
            {
                return Fail<GrantResponseDTO>("授权失败，请重试");
            }
            response.Code = grant.Auth_Code;
            return Success(response);
        }
    }
}
