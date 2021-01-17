using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.BusinessLogic.Customer
{
   public interface IFillComboLogic
    {
        /// <summary>
        /// Method use to fill dropdown
        /// </summary>
        /// <returns></returns>
        List<dynamic> DropDown();
        /// <summary>
        /// Fill search parameters dynamically for a table.
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        List<dynamic> SearchParameters(string Table);
    }
}
