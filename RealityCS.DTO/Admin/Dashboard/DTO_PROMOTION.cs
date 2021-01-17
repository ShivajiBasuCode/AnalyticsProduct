using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin
{
   public class DTO_PROMOTION
    {
        public int PRMTN_ID { get; set; }
        public string PRMTN_CD { get; set; }
        public string PRMTN_NAME { get; set; }
        public string PRMTN_DESC { get; set; }
        public string THEME { get; set; }
        public string ORG_CD { get; set; }
        public int? BSNS_UNIT_ID { get; set; }
        public string PRMTN_PRPS { get; set; }
        public DateTime? STRT_DT { get; set; }
        public DateTime? END_DT { get; set; }
        public string PERSON_RESPBL { get; set; }
        public int? CMPGN_ID { get; set; }
        public string STATUS_CD { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }

    }
}
