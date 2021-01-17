using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public class Tbl_Data_RealitycsAttributeDateTimeValue : RealitycsClientBase
    {
        public int FK_EntityId { get; set; }
        public DateTime Value { get; set; }
        public int FK_AttributeId { get; set; }
        public virtual Tbl_Data_RealitycsEntity FK_Entity { get; set; }
        public virtual Tbl_Data_RealitycsAttribute FK_Attribute { get; set; }

    }
}
