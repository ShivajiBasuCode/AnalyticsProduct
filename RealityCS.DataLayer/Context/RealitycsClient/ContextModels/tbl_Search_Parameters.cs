using System;
using System.Collections.Generic;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class tbl_Search_Parameters
    {
        public int PK_SearchParameterID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public TimeSpan? CreatedTime { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public TimeSpan? ModifiedTime { get; set; }
        public string TableName { get; set; }
        public string CustomTableName { get; set; }
        public string ColumnName { get; set; }
        public string CustomColumnName { get; set; }
        public string Datatype { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
