using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Token
    {
    }
    public partial interface ITauth_TokenCollection
    {
        bool ListByUserId(int userid);
        bool ListByUserId_AppId(int userid, int appid);
    }
}
