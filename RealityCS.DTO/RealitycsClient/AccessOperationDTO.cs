using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class AccessOperationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string DomainName { get; set; }
        public string EntityGroupName { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public bool Assigned { get; set; }
    }
}
