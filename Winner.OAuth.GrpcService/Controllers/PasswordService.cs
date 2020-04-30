using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Entities.IdentityVerification;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.IdentityVerifications;
using Winner.User.Interface;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class PasswordService : Password.PasswordBase
    {
        public override Task<PasswordResetResponseDTO> Reset(PasswordResetRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                PasswordResetResponseDTO response = new PasswordResetResponseDTO();
                var fac = UserModuleFactory.GetUserModuleInstance();
                if (fac == null)
                {
                    response.RetCode = "0500";
                    response.RetMsg = "系统错误";
                    return response;
                }
                string newPwd = request.NewPwd;
                string validateCode = request.ValidateCode;
                if (request.EncodeType.ToLower() == "base64")
                {
                    request.NewPwd = xUtils.Base64ToBase58(request.NewPwd);
                }
                if (!xUtils.RsaDecrypt(request.NewPwd, out newPwd))
                {
                    response.RetCode = "0400";
                    response.RetMsg = "新密码解密失败";
                    return response;
                }
                if (request.ValidateType == PasswordResetRequestDTO.Types.IdentityValidateType.OldPasswordValidation)
                {
                    if (request.EncodeType.ToLower() == "base64")
                    {
                        request.ValidateCode = xUtils.Base64ToBase58(request.ValidateCode);
                    }
                    if (!xUtils.RsaDecrypt(request.ValidateCode, out validateCode))
                    {
                        //return new ApiResult { retCode = "0400", retMsg = "旧密码解密失败" };
                        response.RetCode = "0400";
                        response.RetMsg = "旧密码解密失败";
                        return response;
                    }
                }
                IUser user = fac.GetUserByCode(request.UserCode);
                if (user == null)
                {
                    response.RetMsg = "用户账户[{request.UserCode}]未注册";
                    response.RetCode = "0400";
                    return response;
                }
                PasswordType passwordType = (PasswordType)request.PwdType;
                var validateType = (IdentityValidateType)request.ValidateType;
                IIdentityVerification verification = IdentityVerificationFactory.GetVerification(validateType, user, passwordType, validateCode);
                if (verification == null)
                {
                    response.RetCode = "0400";
                    response.RetMsg = "指定的身份验证方式不正确";
                    return response;
                }

                IPasswordManager pwdmgt = fac.GetPasswordManager();
                PasswordManagerArgs arg = new PasswordManagerArgs
                {
                    AlterSource = xUtils.GetClientSource(request.ClientSource),
                    NewPassword = newPwd,
                    PwdManager = pwdmgt,
                    PwdType = passwordType,
                    Remarks = string.Format("通过{0}修改", validateType.ToString()),
                    UserId = user.UserId,
                    Use_Place = request.ClientSystem,
                    Verification = verification
                };
                string datasource = string.Concat(xUtils.GetClientSource(request.ClientSource), "-", validateType, "-重置密码");
                UserPasswordManager manager = new UserPasswordManager(arg);
                if (!manager.Alter(datasource, datasource))
                {
                    response.RetMsg = manager.PromptInfo.CustomMessage;
                    response.RetCode = "0500";
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                return response;
            });
        }
    }
}
