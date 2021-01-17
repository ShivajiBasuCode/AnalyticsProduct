using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Models
{
    public class JobDivaExcelData
    {
        public int PKJobID { get; set; }
        public DateTime? DateofIssue { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobDivaRefNo { get; set; }
        public string OptionalRefNo { get; set; }
        public string Priority { get; set; }
        public string JobStatus { get; set; }
        public string PositionType { get; set; }
        public string Openings { get; set; }
        public string Fills { get; set; }
        public string MaxSubmittals { get; set; }
        public string Manager { get; set; }
        public decimal? MinBillRate { get; set; }
        public decimal? MaxBillRate { get; set; }
        public string BillRatePer { get; set; }
        public string Division { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public string Skills { get; set; }
        public string PrimarySales { get; set; }
        public string PrimaryRecruiter { get; set; }
        public string Users { get; set; }
        public string Candidate { get; set; }
        public string Email { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? RejectionDate { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedByEmailID { get; set; }
        public string Comments { get; set; }

    }
}
