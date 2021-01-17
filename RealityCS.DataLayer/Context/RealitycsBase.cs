using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context
{
    public class RealitycsBase 
    {
        public int PK_Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
