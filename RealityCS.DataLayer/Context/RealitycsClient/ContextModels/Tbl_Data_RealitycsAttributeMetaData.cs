using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class Tbl_Data_RealitycsAttributeMetaData : RealitycsClientBase
    {
        public int FK_AttributeId { get; set; }
        public string DataType { get; set; }
        public virtual Tbl_Data_RealitycsAttribute Attribute { get; set; }
        //public virtual MasterEntityGroup FK_EntityGroup { get; set; }
    }
}

