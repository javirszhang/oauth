using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 授权响应参数
    /// </summary>
    public class GrantResponseDTO
    {
        /// <summary>
        /// 授权码
        /// </summary>
        public string Code { get; set; }
    }
}
