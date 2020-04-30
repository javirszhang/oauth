using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Scope_Right
    {
    }
    public partial interface ITauth_Scope_RightCollection
    {
        /// <summary>
        /// 查询作用域的权限列表
        /// </summary>
        /// <param name="scopeArray"></param>
        /// <returns></returns>
        bool ListFKJoin(params int[] scopeArray);
    }
}
