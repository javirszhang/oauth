using System;
using System.Collections.Generic;
using System.Text;
using Winner.OAuth.Entities;
using Winner.OAuth.Entities.IdentityVerification;
using Winner.User.Interface;

namespace Winner.OAuth.Facade.IdentityVerifications
{
    public class SmsValidCodeVerification : IIdentityVerification
    {
        private string _mobileno;
        private string _validCode;
        private PasswordType _codeType;
        public SmsValidCodeVerification(string mobileno, string validCode, PasswordType codeType)
        {
            this._mobileno = mobileno;
            this._validCode = validCode;
            this._codeType = codeType;
        }
        public string ErrorMessage
        {
            get; private set;
        }

        public bool Verify()
        {
            SmsValidateProvider provider = new SmsValidateProvider(_mobileno, (SmsValidateType)_codeType);
            if (!provider.ValidateCode(_validCode))
            {
                this.ErrorMessage = provider.PromptInfo.CustomMessage;
                return false;
            }
            return true;
        }
    }
}
