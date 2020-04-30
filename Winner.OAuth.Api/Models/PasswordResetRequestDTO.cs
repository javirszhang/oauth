using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Entities.IdentityVerification;
using Winner.User.Interface;

namespace Winner.OAuth.Api.Models
{
    public class PasswordResetRequestDTO
    {
        public int PwdType { get; internal set; }
        public int ValidateType { get; internal set; }
        public string UserCode { get; internal set; }
        public string ValidateCode { get; internal set; }
        public string EncodeType { get; internal set; }
        public string NewPwd { get; internal set; }
        public ClientInfo Client { get; set; }
    }
}
