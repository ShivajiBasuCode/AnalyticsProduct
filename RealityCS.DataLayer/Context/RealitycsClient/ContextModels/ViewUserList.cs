using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public class ViewUserList
    {
        public int TotalRecords { get; set; }
        public int Id { get; set; }
        public int LegalEntityId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public int EmplooyeId { get; set; }
        public int AccessGroupId { get; set; }
        public string AccessGroupName { get; set; }
        public bool IsActive { get; set; }
    }
}
