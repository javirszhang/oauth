using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Session
    {
    }
    public partial interface ITauth_SessionCollection
    {
        bool ListBySessionId(string sessionId);
    }
}
