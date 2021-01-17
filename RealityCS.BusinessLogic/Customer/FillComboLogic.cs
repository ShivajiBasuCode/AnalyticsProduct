using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RealityCS.BusinessLogic.Customer
{
    public class FillComboLogic : IFillComboLogic
    {

        private readonly RealitycsClientContext realitycsClientContext;

        public FillComboLogic(RealitycsClientContext realitycsClientContext)
        {
            this.realitycsClientContext = realitycsClientContext;
        }
       
        public List<dynamic> DropDown()
        {
            dynamic[] d =
             {
                new  { id=1,text="Item 1"},
                new  { id=2,text="Item 2"},
                new  { id=3,text="Item 3"},
                new  { id=4,text="Item 4"},
                new  { id=5,text="Item 5"},
            };

            return d.ToList();
        }
        
        public List<dynamic> SearchParameters(string Table)
        {
            //var result = realitycsClientContext.tbl_Search_Parameters.Where(x => x.IsDeleted == false && x.IsActive == true && x.CustomTableName == Table)
            //    .Select(x => new DTO_FillDropdown
            //    {
            //        id = x.ColumnName,
            //        text = x.CustomColumnName,
            //        DisplayOrder = (int)x.DisplayOrder
            //    }).OrderBy(x => x.DisplayOrder).ToList<dynamic>();


            return null;


        }
    }
}
