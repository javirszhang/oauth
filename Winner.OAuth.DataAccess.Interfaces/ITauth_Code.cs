using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Code
    {
        /// <summary>
        /// 查询有效的授权码
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool SelectByAppid_UserId(int appid, int userId);
    }
    public partial interface ITauth_CodeCollection
    {

    }
}
