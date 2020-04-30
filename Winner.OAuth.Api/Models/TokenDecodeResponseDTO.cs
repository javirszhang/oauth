using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class TokenDecodeResponseDTO
    {
        public int Appid { get; internal set; }
        public DateTime ExpireTime { get; internal set; }
        public string Usercode { get; internal set; }
        public int Userid { get; internal set; }
    }
}
