using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;

namespace Winner.OAuth.Entities
{
    public class Scope
    {
        [MapMember("SCOPE_ID")]
        public int Id { get; set; }
        [MapMember("SCOPE_CODE")]
        public string Code { get; set; }
        [MapMember("SCOPE_NAME")]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remarks { get; set; }
        /// <summary>
        /// 是否需要显式授权
        /// </summary>
        public bool IsExpllicit { get; set; }
    }
}
