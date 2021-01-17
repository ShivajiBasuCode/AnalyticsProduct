using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class Tbl_Data_RealitycsOperationDetail : RealitycsClientBase
    {
        public Guid OperationId { get; set; }
        public string TableName { get; set; }
        public string ViewName { get; set; }
        //public virtual MasterEntityGroup FK_EntityGroup { get; set; }
    }
}

