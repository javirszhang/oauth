using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class TokenRefreshRequestDTO
    {
        public string Appid { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
