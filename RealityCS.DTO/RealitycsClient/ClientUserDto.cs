using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class ClientUserDTO
    {
        public int Id { get; set; }
        public int LegalEntityId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public int EmployeeId { get; set; }
        public int AccessGroupId { get; set; }
        public string AccessGroupName { get; set; }
        public bool IsActive { get; set; }

    }
}
