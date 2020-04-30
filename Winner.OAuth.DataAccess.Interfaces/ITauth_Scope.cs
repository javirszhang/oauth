using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface ITauth_Scope
    {
    }
    public partial interface ITauth_ScopeCollection
    {
        bool ListByScopes(params string[] scopes);
    }
}
