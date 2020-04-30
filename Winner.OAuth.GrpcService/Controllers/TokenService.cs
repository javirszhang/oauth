using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.DataAccess.Interfaces;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class TokenService : Token.TokenBase
    {
        public override Task<TokenDecodeResponseDTO> Decode(TokenDecodeRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                TokenDecodeResponseDTO response = new TokenDecodeResponseDTO();
                var app = OAuthAppCache.Get(request.Appid);
                if (app == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的应用id";
                    return response;
                }
                var ut = UserTokenProvider.DecryptAccessToken(request.Token);
                if (ut == null || !ut.Success)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "无效的token";
                    return response;
                }
                if (ut.Content.AppId != app.Id)
                {
                    response.RetCode = "0402";
                    response.RetMsg = "操作不允许";
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new TokenDecodeResponseDTO.Types.Result
                {
                    Appid = ut.Content.AppId,
                    ExpireTime = ut.Content.Expire_Time.ToString(),
                    Usercode = ut.Content.UserCode,
                    Userid = ut.Content.UserId
                };
                return response;
            });
        }
        public override Task<TokenResponseDTO> GetByCode(CodeExchangeTokenRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                TokenResponseDTO response = new TokenResponseDTO();
                OpenOAuthProvider provider = new OpenOAuthProvider(request.Appid, request.Secret, request.Code, request.GrantType);
                if (!provider.OAuthAccess())
                {
                    response.RetCode = "0500";
                    response.RetMsg = provider.PromptInfo.CustomMessage;
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new TokenResponseDTO.Types.Result
                {
                    Expires = provider.OAuthUser.Expire_In,
                    Openid = provider.OAuthUser.Open_Id,
                    RefreshExpires = provider.OAuthUser.Refresh_Expire_In,
                    RefreshToken = provider.OAuthUser.Refresh_Token,
                    Token = provider.OAuthUser.Token
                };
                return response;
            });
        }
        public override Task<TokenResponseDTO> Refresh(TokenRefreshRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                TokenResponseDTO response = new TokenResponseDTO();
                RefreshTokenProvider refresh = new RefreshTokenProvider(request.Appid, request.RefreshToken);
                if (!refresh.Refresh())
                {
                    response.RetMsg = refresh.PromptInfo.CustomMessage;
                    response.RetCode = refresh.PromptInfo.ResultType <= 0 ? "0500" : ((int)refresh.PromptInfo.ResultType).ToString().PadLeft(4, '0');
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                response.Data = new TokenResponseDTO.Types.Result
                {
                    RefreshToken = refresh.OAuthUser.Refresh_Token,
                    Expires = refresh.OAuthUser.Expire_In,
                    Openid = refresh.OAuthUser.Open_Id,
                    RefreshExpires = refresh.OAuthUser.Refresh_Expire_In,
                    Token = refresh.OAuthUser.Token
                };
                return response;
            });
        }        
        public override Task<TokenValidateResponseDTO> Validate(TokenValidateRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                TokenValidateResponseDTO response = new TokenValidateResponseDTO();
                StringBuilder logtext = new StringBuilder();
                logtext.AppendLine($"验证token={request.Token}");
                if (string.IsNullOrEmpty(request.Token))
                {
                    logtext.AppendLine("token为空");
                    Log.Info(logtext.ToString());
                    response.RetCode = "0401";
                    response.RetMsg = "无效的token";
                    return response;
                }

                var ut = OAuth.Token.UserToken.FromCipherToken(request.Token);
                if (ut.Expire_Time < DateTime.Now)
                {
                    logtext.AppendLine("Token已过期");
                    Log.Info(logtext.ToString());
                    response.RetCode = "0401";
                    response.RetMsg = "token已过期";
                    return response;
                }
                //Tauth_Token daToken = new Tauth_Token();
                var daTokenCollection = DaoFactory.Tauth_TokenCollection();
                if (!daTokenCollection.ListByUserId_AppId(ut.UserId, ut.AppId))
                {
                    logtext.AppendLine("数据库未找到该Token，无效的Token[db fail]");
                    Log.Info(logtext.ToString());
                    response.RetCode = "0401";
                    response.RetMsg = "无效的token";
                    return response;
                }
                ITauth_Token daToken = null;
                foreach (ITauth_Token item in daTokenCollection)
                {
                    if (item.Token_Code.Equals(request.Token))
                    {
                        daToken = item;
                    }
                }
                if (daToken == null)
                {
                    logtext.AppendLine("数据库未找到该Token，无效的Token[not found]");
                    Log.Info(logtext.ToString());
                    response.RetCode = "0401";
                    response.RetMsg = "无效的token";
                    return response;
                }
                Log.Info("Token有效");
                Log.Info(logtext.ToString());
                response.RetCode = "0000";
                response.RetMsg = "ok";
                return response;
            });
        }
    }
}
