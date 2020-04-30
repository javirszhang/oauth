using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Entities;
using Winner.OAuth.Api.Models;
using Winner.Framework.Utils;
using Winner.OAuth.DataAccess.Interfaces;
using System.Text;

namespace Winner.OAuth.Api.Controllers
{
    /// <summary>
    /// token控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : OAuthControllerBase
    {
        /// <summary>
        /// token解码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("decode")]
        public ResponseResult<TokenDecodeResponseDTO> Decode(TokenDecodeRequestDTO request)
        {
            TokenDecodeResponseDTO response = new TokenDecodeResponseDTO();
            var app = OAuthAppCache.Get(request.Appid);
            if (app == null)
            {
                return Fail<TokenDecodeResponseDTO>("无效的应用id", "0400");
            }
            var ut = UserTokenProvider.DecryptAccessToken(request.Token);
            if (ut == null || !ut.Success)
            {
                return Fail<TokenDecodeResponseDTO>("无效的token", "0400");
            }
            if (ut.Content.AppId != app.Id)
            {
                return Fail<TokenDecodeResponseDTO>("操作不允许", "0402");
            }
            response = new TokenDecodeResponseDTO
            {
                Appid = ut.Content.AppId,
                ExpireTime = ut.Content.Expire_Time,
                Usercode = ut.Content.UserCode,
                Userid = ut.Content.UserId
            };
            return Success(response);
        }
        /// <summary>
        /// 通过授权码获取token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public ResponseResult<TokenResponseDTO> GetByCode(CodeExchangeTokenRequestDTO request)
        {
            OpenOAuthProvider provider = new OpenOAuthProvider(request.Appid, request.Secret, request.Code, request.GrantType);
            if (!provider.OAuthAccess())
            {
                return Fail<TokenResponseDTO>(provider.PromptInfo.CustomMessage);
            }
            TokenResponseDTO response = new TokenResponseDTO
            {
                Expires = provider.OAuthUser.Expire_In,
                Openid = provider.OAuthUser.Open_Id,
                RefreshExpires = provider.OAuthUser.Refresh_Expire_In,
                RefreshToken = provider.OAuthUser.Refresh_Token,
                Token = provider.OAuthUser.Token
            };
            return Success(response);
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public ResponseResult<TokenResponseDTO> Refresh(TokenRefreshRequestDTO request)
        {
            RefreshTokenProvider refresh = new RefreshTokenProvider(request.Appid, request.RefreshToken);
            if (!refresh.Refresh())
            {
                return Fail<TokenResponseDTO>(refresh.PromptInfo.CustomMessage);
            }

            TokenResponseDTO response = new TokenResponseDTO
            {
                RefreshToken = refresh.OAuthUser.Refresh_Token,
                Expires = refresh.OAuthUser.Expire_In,
                Openid = refresh.OAuthUser.Open_Id,
                RefreshExpires = refresh.OAuthUser.Refresh_Expire_In,
                Token = refresh.OAuthUser.Token
            };
            return Success(response);
        }
        /// <summary>
        /// 验证token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("validate")]
        public ResponseResult Validate(string token)
        {
            StringBuilder logtext = new StringBuilder();
            logtext.AppendLine($"验证token={token}");
            if (string.IsNullOrEmpty(token))
            {
                logtext.AppendLine("token为空");
                Log.Info(logtext.ToString());
                return Fail("无效的token", "0401");
            }

            var ut = OAuth.Token.UserToken.FromCipherToken(token);
            if (ut.Expire_Time < DateTime.Now)
            {
                logtext.AppendLine("Token已过期");
                Log.Info(logtext.ToString());
                return Fail("token已过期", "0401");

            }
            var daTokenCollection = DaoFactory.Tauth_TokenCollection();
            if (!daTokenCollection.ListByUserId_AppId(ut.UserId, ut.AppId))
            {
                logtext.AppendLine("数据库未找到该Token，无效的Token[db fail]");
                Log.Info(logtext.ToString());
                return Fail("无效的token", "0401");
            }
            ITauth_Token daToken = null;
            foreach (ITauth_Token item in daTokenCollection)
            {
                if (item.Token_Code.Equals(token))
                {
                    daToken = item;
                }
            }
            if (daToken == null)
            {
                logtext.AppendLine("数据库未找到该Token，无效的Token[not found]");
                Log.Info(logtext.ToString());
                return Fail("无效的token", "0401");
            }
            Log.Info("Token有效");
            Log.Info(logtext.ToString());
            return Success();
        }
    }
}
