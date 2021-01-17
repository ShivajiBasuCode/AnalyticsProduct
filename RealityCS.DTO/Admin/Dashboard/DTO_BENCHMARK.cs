using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Dashboard
{
   public class DTO_BENCHMARK
    {
        public int BM_ID { get; set; }
        public string ORG_CD { get; set; }
        public int? BSNS_UNIT_ID { get; set; }
        public int KPI_ID { get; set; }
        public int TLT_ID { get; set; }
        public string FRM_DATE_CD { get; set; }
        public string TO_DATE_CD { get; set; }
        public decimal? UP_THR { get; set; }
        public decimal? MID_THR { get; set; }
        public decimal LOWER_THR { get; set; }
        public bool ISDEFAULT { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }

        /// <summary>
        /// properties mentioned bellow,used to getting data from ajax call
        /// </summary>
        public int BMID { get; set; }
        public int KPIID { get; set; }
        public int? BSNSUID { get; set; }
        public int? FRMDTCD { get; set; }
        public string TODTCD { get; set; }
        public int? TLTID { get; set; }
        public string TimeLines { get; set; }
        public decimal UpperValue { get; set; }
        public decimal MidValue { get; set; }
        public decimal LowerValue { get; set; }
        public bool Default { get; set; }

    }
}
