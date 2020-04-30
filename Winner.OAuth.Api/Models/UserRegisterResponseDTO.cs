using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class UserRegisterResponseDTO
    {
        public int Expires { get; internal set; }
        public string Openid { get; internal set; }
        public int RefreshExpires { get; internal set; }
        public string RefreshToken { get; internal set; }
        public string Token { get; internal set; }
        public string UserCode { get; internal set; }
        public bool RegisterResult { get; internal set; }
    }
}
