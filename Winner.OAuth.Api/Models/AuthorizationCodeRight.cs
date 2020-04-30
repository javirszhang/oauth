using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 授权权限
    /// </summary>
    public class AuthorizationCodeRight
    {
        /// <summary>
        /// 权限id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public int Type { get; set; }
    }
}
