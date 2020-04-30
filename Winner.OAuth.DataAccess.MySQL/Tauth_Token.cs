using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.MySQL
{
    public partial class Tauth_Token
    {
    }
    public partial class Tauth_TokenCollection
    {
        public bool ListByUserId(int userid)
        {
            string condition = "USER_ID=?USER_ID";
            AddParameter("USER_ID", userid);
            return ListByCondition(condition);
        }

        public bool ListByUserId_AppId(int userid, int appid)
        {
            string condition = "USER_ID=?USER_ID AND APP_ID=?APP_ID";
            AddParameter("USER_ID", userid);
            AddParameter("APP_ID", appid);
            return ListByCondition(condition);
        }
    }
}
