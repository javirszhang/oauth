using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Interfaces
{
    public partial interface IVauth_Scope_Right
    {
    }
    public partial interface IVauth_Scope_RightCollection
    {
        bool ListByScope(params int[] scopeIds);
    }
}
