using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class TokenDecodeRequestDTO
    {
        public int Appid { get; internal set; }
        public string Token { get; internal set; }
    }
}
