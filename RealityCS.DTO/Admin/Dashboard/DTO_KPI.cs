using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.Admin
{
    public class DTO_KPI
    {
        public int KPI_ID { get; set; } = 0;

        [MaxLength(50)]
        [StringLength(50)]
        //[Required(ErrorMessage = "KPI CD is required")]
        [Display(Name = "KPI CD")]
        public string KPI_CD { get; set; } = string.Empty;
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "KPI CHILD CD")]
        public string KPI_CHILD_CD { get; set; }
        [Display(Name = "KPI LEVEL")]
        public short? KPI_LEVEL { get; set; }
        [Display(Name = "KPI TYPE ID")]
        public byte? KPI_TYPE_ID { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "KPI NAME is required")]
        [Display(Name = "KPI NAME")]
        public string KPI_NAME { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        //[Required(ErrorMessage = "OPERATION CD is required")]
        [Display(Name = "OPERATION CD")]
        public string OPERATION_CD { get; set; } = string.Empty;
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "ORG CD")]
        public string ORG_CD { get; set; }
        [Display(Name = "BSNS UNIT ID")]
        public int? BSNS_UNIT_ID { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "KPI SP NAME")]
        public string KPI_SP_NAME { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "KPI DISPLAY TEXT")]
        public string KPI_DISPLAY_TEXT { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "VERTICAL LABEL")]
        public string VERTICAL_LABEL { get; set; }
        [MaxLength(1)]
        [StringLength(1)]
        //[Required(ErrorMessage = "ISACTIVE is required")]
        [Display(Name = "ISACTIVE")]
        public string ISACTIVE { get; set; }
        [Display(Name = "FK Kpi Industry ID")]
        public int? FK_KpiIndustryID { get; set; }
        [Display(Name = "SQL Query")]
        public string SQLQuery { get; set; }

       

    }
    public class KpiTree
    {
        public string id { get; set; }        
        public string text { get; set; }
        public string icon { get; set; }
        public NodeState state { get; set; }
        //public NodeChildren[] children { get; set; }
        public string parent { get; set; }
        public dynamic li_attr { get; set; }
        public dynamic a_attr { get; set; }
        public dynamic data { get; set; }

    }
    public class NodeState
    {
        public Boolean opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }

    //public class NodeChildren
    //{
    //    public string text { get; set; }
    //    public string icon { get; set; }
    //    public NodeState state { get; set; }
    //}

}
