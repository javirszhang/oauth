using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.OAuth.Entities.IdentityVerification
{
    public interface IIdentityVerification
    {
        bool Verify();
        string ErrorMessage { get; }
    }
}
