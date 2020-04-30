using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Token_Right
    {
    }
    public partial interface ITauth_Token_RightCollection
    {
        /// <summary>
        /// 查询指定Token的有效权限
        /// </summary>
        /// <returns></returns>
        bool ListEffectiveByTokenId(int tokenId);

        bool ListAllByTokenId(int tokenId);
    }
}
