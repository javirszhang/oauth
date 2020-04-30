using System;
using System.Collections.Generic;
using System.Text;
using Winner.OAuth.Entities.IdentityVerification;
using Winner.User.Interface;

namespace Winner.OAuth.Facade.IdentityVerifications
{
    /// <summary>
    /// 身份验证器工厂
    /// </summary>
    public static class IdentityVerificationFactory
    {
        /// <summary>
        /// 获取身份验证器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="user"></param>
        /// <param name="passwordType"></param>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public static IIdentityVerification GetVerification(IdentityValidateType type, IUser user, PasswordType passwordType, string validateCode)
        {
            IIdentityVerification verification = null;
            switch (type)
            {
                case IdentityValidateType.短信验证:
                    verification = new SmsValidCodeVerification(user.GetUserVoucher(UserVoucherType.手机号), validateCode, passwordType);
                    break;
                case IdentityValidateType.旧密码验证:
                    verification = new PasswordValidVerification(user, validateCode, passwordType);
                    break;
                default:
                    break;
            }
            return verification;
        }
    }
}
