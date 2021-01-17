using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DTO.RealitycsClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
    public interface IRealitycsUserRegistrationService
    {
        Task<CustomerRegistrationResult> RegisterLegalEntityUser(CustomerRegistrationRequest request);
        Task<UserLoginResults> ValidateLegalEntityUser(string usernameOrEmail, string password);
    }
}
