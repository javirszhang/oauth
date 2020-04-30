using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Api.Models;
using Winner.OAuth.Entities.IdentityVerification;
using Winner.OAuth.Facade;
using Winner.OAuth.Facade.IdentityVerifications;
using Winner.User.Interface;

namespace Winner.OAuth.Api.Controllers
{
    /// <summary>
    /// 密码管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : OAuthControllerBase
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Reset(PasswordResetRequestDTO request)
        {
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                return Fail("系统错误");
            }
            string newPwd = request.NewPwd;
            string validateCode = request.ValidateCode;
            if (request.EncodeType.ToLower() == "base64")
            {
                request.NewPwd = xUtils.Base64ToBase58(request.NewPwd);
            }
            if (!xUtils.RsaDecrypt(request.NewPwd, out newPwd))
            {
                return Fail("新密码解密失败");
            }
            if (request.ValidateType == (int)IdentityValidateType.旧密码验证)
            {
                if (request.EncodeType.ToLower() == "base64")
                {
                    request.ValidateCode = xUtils.Base64ToBase58(request.ValidateCode);
                }
                if (!xUtils.RsaDecrypt(request.ValidateCode, out validateCode))
                {
                    return Fail("旧密码解密失败");
                }
            }
            IUser user = fac.GetUserByCode(request.UserCode);
            if (user == null)
            {
                return Fail("用户账户[{request.UserCode}]未注册", "0400");
            }
            PasswordType passwordType = (PasswordType)request.PwdType;
            var validateType = (IdentityValidateType)request.ValidateType;
            IIdentityVerification verification = IdentityVerificationFactory.GetVerification(validateType, user, passwordType, validateCode);
            if (verification == null)
            {
                return Fail("指定的身份验证方式不正确", "0400");
            }

            IPasswordManager pwdmgt = fac.GetPasswordManager();
            PasswordManagerArgs arg = new PasswordManagerArgs
            {
                AlterSource = xUtils.GetClientSource(request.Client.Type),
                NewPassword = newPwd,
                PwdManager = pwdmgt,
                PwdType = passwordType,
                Remarks = string.Format("通过{0}修改", validateType.ToString()),
                UserId = user.UserId,
                Use_Place = request.Client.System,
                Verification = verification
            };
            string datasource = string.Concat(xUtils.GetClientSource(request.Client.Type), "-", validateType, "-重置密码");
            UserPasswordManager manager = new UserPasswordManager(arg);
            if (!manager.Alter(datasource, datasource))
            {
                return Fail(manager.PromptInfo.CustomMessage);
            }
            return Success();
        }
    }
}
