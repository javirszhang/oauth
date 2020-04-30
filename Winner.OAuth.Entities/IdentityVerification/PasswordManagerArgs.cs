using System;
using System.Collections.Generic;
using System.Text;
using Winner.User.Interface;

namespace Winner.OAuth.Entities.IdentityVerification
{
    public class PasswordManagerArgs
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public PasswordType PwdType { get; set; }
        public IIdentityVerification Verification { get; set; }
        public IPasswordManager PwdManager { get; set; }

        public string AlterSource { get; set; }
        public string Use_Place { get; set; }
        public string Remarks { get; set; }
    }
}
