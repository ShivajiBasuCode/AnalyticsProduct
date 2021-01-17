using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Parser
{
   public class DTO_RAR_ITEM_SBC
    {
        public string ORG_CD { get; set; }
        public int ITEM_SBC_ID { get; set; }
        public string SBC_CD { get; set; }
        public string SBC_NAME { get; set; }
        public string SBC_BYR_NAME { get; set; }
        public string SBC_MRCHNDSR_NAME { get; set; }
        public int? ITEM_CLASS_ID { get; set; }
        public string CLASS_CD { get; set; }
        public string CLASS_NAME { get; set; }
        public DateTime? EFF_FROM_DT { get; set; }
        public DateTime? EFF_TO_DT { get; set; }

    }
}
