using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api
{
    public interface IIdentityManager
    {
        Task<string> GenarateToken(string username, string password);        
    }
}
