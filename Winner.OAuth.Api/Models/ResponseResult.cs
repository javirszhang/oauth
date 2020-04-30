using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.Api.Models
{
    /// <summary>
    /// 响应参数基类
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string RetCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string RetMsg { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseResult<T> : ResponseResult
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
    }
}
