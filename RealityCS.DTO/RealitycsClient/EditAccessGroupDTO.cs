using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class EditAccessGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public bool IsActive { get; set; }
        public List<AccessOperationDTO> operations { get; set; }


    }


}
