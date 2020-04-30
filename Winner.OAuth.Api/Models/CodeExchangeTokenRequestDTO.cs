using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 授权码交换token参数
    /// </summary>
    public class CodeExchangeTokenRequestDTO
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; internal set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; internal set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string Code { get; internal set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public string GrantType { get; internal set; }
    }
}
