using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.Entities.IdentityVerification;

namespace Winner.OAuth.Facade
{
    public class UserPasswordManager : FacadeBase
    {
        private PasswordManagerArgs _arg;
        public UserPasswordManager(PasswordManagerArgs arg)
        {
            this._arg = arg;
        }

        public bool Alter(string dataSource, string remarks = null)
        {
            if (this._arg == null)
            {
                Alert("系统错误，修改密码失败");
                return false;
            }
            if (this._arg.PwdManager == null)
            {
                Alert("系统错误，修改密码失败");
                return false;
            }
            if (this._arg.Verification == null)
            {
                Alert("无法验证用户");
                return false;
            }
            if (!this._arg.Verification.Verify())
            {
                Alert(this._arg.Verification.ErrorMessage);
                return false;
            }
            if (!UpdatePassword(dataSource, remarks))
            {
                Alert("修改密码失败");
                return false;
            }
            return true;
        }
        private bool UpdatePassword(string dataSource, string remarks = null)
        {
            try
            {
                return this._arg.PwdManager.UpdatePassword(this._arg.UserId, this._arg.PwdType, this._arg.NewPassword, dataSource, remarks);
            }
            catch (Exception ex)
            {
                Log.Error("修改密码失败", ex);
                return false;
            }
        }
    }
}
