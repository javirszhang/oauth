using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class UserRegisterRequestDTO
    {
        public int Appid { get; internal set; }
        public string UserCode { get; internal set; }
        public string Password { get; internal set; }
        public string SmsCode { get; internal set; }
        public string RefereeCode { get; internal set; }
        public ClientInfo Client { get; set; }
    }
}
