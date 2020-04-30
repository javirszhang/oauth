using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// 客户端版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 客户端系统
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// 客户端ip
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 客户端类型（1. 安卓, 2. 苹果, 3. 服务端, 4. 前端）
        /// </summary>
        public int Type { get; set; }
    }
}
