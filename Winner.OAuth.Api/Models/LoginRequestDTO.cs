using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 登录请求参数
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 登录作用域
        /// </summary>
        public string Scopes { get; set; }
        /// <summary>
        /// 登录类型（1 - 密码登录，2 - 短信验证码登录）
        /// </summary>
        public int LoginType { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public ClientInfo Client { get; set; }
    }
}
