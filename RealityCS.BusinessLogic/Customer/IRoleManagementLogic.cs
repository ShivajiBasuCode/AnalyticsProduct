using RealityCS.DTO.RealitycsAuth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
  public  interface IRoleManagementLogic
    {
        List<DTO_Role> List(DTO_RoleSearch roleSearch);

        DTO_Role GetById(Int32 RoleID);

        Task<DTO_Role> Add(DTO_Role role);

        Task<DTO_Role> Update(DTO_Role role);

        Task<bool> ChangeStatus(Int32 RoleID, bool IsActive);

        Task<bool> Delete(Int32 ClientID);
    }
}
