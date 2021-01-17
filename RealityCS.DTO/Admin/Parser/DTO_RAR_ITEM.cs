using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Admin.Parser
{
  public  class DTO_RAR_ITEM
    {
        public string ORG_CD { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NBR { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_DESC { get; set; }
        public DateTime? AVLBL_FOR_SL_DT { get; set; }
        public string ITEM_SHORT_DESC { get; set; }
        public string ITEM_SCND_DESC { get; set; }
        public string UOM_CD { get; set; }
        public int? ITEM_SBC_ID { get; set; }
        public string SBC_CD { get; set; }
        public string SBC_NAME { get; set; }
        public string BRND_CD { get; set; }
        public string TAX_EXMPT_CD { get; set; }
        public string CONV_TYP_CD { get; set; }
        public string INV_ACCT_MTHD_CD { get; set; }
        public string KIT_SET_CD { get; set; }
        public string ORDR_COLLCTN_CD { get; set; }
        public string SCRTY_REQD_TYP_CD { get; set; }
        public string CUST_PCKUP_TYP_CD { get; set; }
        public string USG_CD { get; set; }
        public string SL_WT_OR_UNIT_CNT_CD { get; set; }
        public string AUTHRZD_FOR_SL_IND { get; set; }
        public string COMMSN_IND { get; set; }
        public string DISC_IND { get; set; }
        public string FULL_PALLET_ITEM_IND { get; set; }
        public string INV_IND { get; set; }
        public string MRCHNDS_IND { get; set; }
        public string PRICE_AUDIT_IND { get; set; }
        public string PRSHBL_IND { get; set; }
        public string RECIPE_IND { get; set; }
        public string ENV_TYP_CD { get; set; }
        public string SELLBL_IND { get; set; }
        public string HZRDS_MTRL_TYP_CD { get; set; }
        public string SHRNK_IND { get; set; }
        public string SWELL_IND { get; set; }
        public string STOP_SL_IND { get; set; }
        public string SUB_IDNT_IND { get; set; }
        public string STORE_REORDRBL_IND { get; set; }
        public DateTime? EFF_FROM_DT { get; set; }
        public DateTime? EFF_TO_DT { get; set; }

    }
}
