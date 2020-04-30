using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    public class ThirdpartyBindingRequestDTO
    {
        public string Appid { get; internal set; }
        public string AuthCode { get; set; }
    }
}
