using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    public class RegisterProvider : FacadeBase
    {
        private string _userCode, _password, _smsCode, _refereeCode;
        public RegisterProvider(string userCode, string password, string smsCode, string refereeCode)
        {
            this._userCode = userCode;
            this._password = password;
            this._smsCode = smsCode;
            this._refereeCode = refereeCode;
        }
        public bool Register(params Action<IUser>[] listeners)
        {
            if (!this._userCode.IsMobileNo())
            {
                Alert((ResultType)409, "请输入正确的手机号码");
                return false;
            }
            if (!AppConfig.DisableMobileVerification)
            {
                if (string.IsNullOrEmpty(_smsCode))
                {
                    Alert((ResultType)409, "短信验证码不能为空");
                    return false;
                }
                SmsValidateProvider validate = new SmsValidateProvider(this._userCode, Entities.SmsValidateType.注册);
                if (!validate.ValidateCode(this._smsCode))
                {
                    Alert(validate.PromptInfo);
                    return false;
                }
            }
            UserCreationProvider userCreation = new UserCreationProvider(this._userCode, this._password, this._refereeCode);
            if (listeners != null || listeners.Length > 0)
            {
                foreach (Action<IUser> lst in listeners)
                {
                    userCreation.Success += lst;
                }
            }
            if (!userCreation.AddUser())
            {
                Alert(userCreation.PromptInfo);
                return false;
            }
            return true;
        }
    }
}
