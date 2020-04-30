using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int Expires { get; set; }
        public string Openid { get; set; }
        public int RefreshExpires { get; set; }
        public string RefreshToken { get; set; }
    }
}
