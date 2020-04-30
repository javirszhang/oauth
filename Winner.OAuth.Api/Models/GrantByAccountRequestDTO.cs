using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 使用账号密码获取授权码参数
    /// </summary>
    public class GrantByAccountRequestDTO : AuthorizeRequestBaseDTO
    {
        /// <summary>
        /// 账号（手机号/账号/邮箱）
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 登陆密码（RSA加密）
        /// </summary>
        public string Password { get; set; }
    }
}
