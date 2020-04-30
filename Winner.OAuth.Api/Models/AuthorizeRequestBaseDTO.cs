using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 请求参数基类
    /// </summary>
    public abstract class AuthorizeRequestBaseDTO
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 防跨站随机串
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 授权作用域
        /// </summary>
        public string Scopes { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// 授权所有权限
        /// </summary>
        public bool GrantAll { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public ClientInfo Client { get; set; }
        /// <summary>
        /// 授权权限列表
        /// </summary>
        public List<AuthorizationCodeRight> Privileges { get; set; }
    }
}
