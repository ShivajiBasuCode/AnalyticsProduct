using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Parser
{
   public class DTO_RAR_ITEM_DEPT
    {
        public string ORG_CD { get; set; }
        public int ITEM_DEPT_ID { get; set; }
        public string DEPT_CD { get; set; }
        public string DEPT_NAME { get; set; }
        public int? ITEM_GRP_ID { get; set; }
        public string GRP_CD { get; set; }
        public string GRP_NAME { get; set; }
        public string DEPT_BYR_NAME { get; set; }
        public string DEPT_MRCHNDSR_NAME { get; set; }
        public DateTime? EFF_FROM_DT { get; set; }
        public DateTime? EFF_TO_DT { get; set; }

    }
}
