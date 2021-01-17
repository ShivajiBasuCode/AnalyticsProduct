using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Parser
{
   public class DTO_RAR_SKU_ITEM
    {
        public string ORG_CD { get; set; }
        public int SKU_ITEM_ID { get; set; }
        public string SKU_ITEM_NBR { get; set; }
        public string GLBL_ITEM_ID { get; set; }
        public string GLBL_ITEM_ID_TYP { get; set; }
        public string SKU_ITEM_NAME { get; set; }
        public string SKU_ITEM_DESC { get; set; }
        public string SKU_ITEM_LONG_DESC { get; set; }
        public string SKU_ITEM_TYP_CD { get; set; }
        public string UOM_CD { get; set; }
        public string SKU_ITEM_INDSTRY_IDNT_CD { get; set; }
        public int? SKU_SLNG_PRC_ID { get; set; }
        public string SKU_ITEM_SLNG_PRICE_NBR { get; set; }
        public double? UNIT_PRICE_FCTR { get; set; }
        public string ALRT_TRX_IND { get; set; }
        public string RECALL_IND { get; set; }
        public string ATTR_1 { get; set; }
        public string ATTR_2 { get; set; }
        public string ATTR_3 { get; set; }
        public string ATTR_4 { get; set; }
        public DateTime? AVLBL_FOR_SL_DT { get; set; }
        public DateTime? SU_LAST_RCVD_COST_ESTBL_DT { get; set; }
        public double? SU_LAST_RCVD_NET_COST_AMT { get; set; }
        public double? SU_LAST_RCVD_NET_COST_AMT_LCL { get; set; }
        public double? SU_LAST_RCVD_NET_COST_AMT_RPT { get; set; }
        public int? ITEM_ID { get; set; }
        public string ITEM_NBR { get; set; }
        public string STCK_ITEM_TYP_CD { get; set; }
        public string SZ_TYP_CD { get; set; }
        public string STCK_ITEM_STYLE_CD { get; set; }
        public string STCK_ITEM_DYE_CD { get; set; }
        public double? SKU_MRP { get; set; }
        public string PROD_REF_CODE { get; set; }
        public string IS_DRIVER { get; set; }
        public string STCK_ITEM_COLOR { get; set; }
        public string STCK_ITEM_SIZE { get; set; }
        public string STCK_ITEM_COATING_CD { get; set; }
        public string STCK_ITEM_WEAVE_CD { get; set; }
        public string STCK_ITEM_FABRIC_CD { get; set; }
        public string STCK_ITEM_FIBER_CD { get; set; }
        public string PCK_SELLBL_CD { get; set; }
        public string PCK_SELLBL_DESC { get; set; }
        public string PCK_SMPL_CD { get; set; }
        public string PCK_SMPL_DESC { get; set; }
        public string PCK_ORDRBL_CD { get; set; }
        public string PCK_ORDRBL_DESC { get; set; }
        public string PCK_IND { get; set; }
        public string PKG_SZ { get; set; }
        public DateTime? EFF_FROM_DT { get; set; }
        public DateTime? EFF_TO_DT { get; set; }

    }
}
