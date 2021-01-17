using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
    public interface ITokenManager
    {
        Task<string> GenarateToken(string emailId, int legalEntityId);
        User GetUserByToken(string token);
    }
}
