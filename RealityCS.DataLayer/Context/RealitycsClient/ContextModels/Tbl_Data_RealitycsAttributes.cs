using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class Tbl_Data_RealitycsAttribute : RealitycsClientBase
    {
        public int FK_EntityId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeDescription { get; set; }
        public int FK_EntityGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string AliasName { get; set; }
        public string DataType { get; set; }
        public Guid OperationId { get; set; }
        public int ParentId { get; set; }
        public virtual Tbl_Data_RealitycsEntity Entity { get; set; }

        public virtual List<Tbl_Data_RealitycsAttributeBooleanValue> BooleanValue { get; set; }
        public virtual List<Tbl_Data_RealitycsAttributeTextValue> TextValue { get; set; }
        public virtual List<Tbl_Data_RealitycsAttributeIntValue> IntValue { get; set; }
        public virtual List<Tbl_Data_RealitycsAttributeDateTimeValue> DateTimeValue { get; set; }
        public virtual Tbl_Data_RealitycsAttributeMetaData AttributeMetaData { get; set; }

        //public virtual MasterEntityGroup FK_EntityGroup { get; set; }
    }
}

