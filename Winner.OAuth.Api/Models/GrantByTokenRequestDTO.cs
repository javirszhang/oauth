using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 使用token获取授权码参数
    /// </summary>
    public class GrantByTokenRequestDTO : AuthorizeRequestBaseDTO
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
}
