using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Oracle
{
    public partial class Tauth_Session
    {
    }

    public partial class Tauth_SessionCollection
    {
        public bool ListBySessionId(string sessionId)
        {
            string condition = "SESSION_ID=:SESSION_ID";
            AddParameter("SESSION_ID", sessionId);
            return ListByCondition(condition);
        }
    }
}
