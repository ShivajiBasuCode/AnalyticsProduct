using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Parser
{
   public class DTO_RAR_ORG_RGN
    {
        public int ORG_RGN_ID { get; set; }
        public byte ORG_ID { get; set; }
        public string ORG_CD { get; set; }
        public string RGN_CD { get; set; }
        public string GLBL_LOC_ID { get; set; }
        public string GLBL_LOC_ID_TYP { get; set; }
        public string RGN_NAME { get; set; }
        public string RGN_MGR_NAME { get; set; }
        public double? ORG_AREA_ID { get; set; }
        public string AREA_CD { get; set; }
        public string AREA_NAME { get; set; }
        public DateTime? EFF_FROM_DT { get; set; }
        public DateTime? EFF_TO_DT { get; set; }

    }
}
