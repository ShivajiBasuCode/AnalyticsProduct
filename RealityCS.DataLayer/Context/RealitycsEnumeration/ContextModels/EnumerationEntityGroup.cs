using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
 
    public enum EnumerationEntityGroup
    {
        [Description("Customer Data")]
        CustomerData = 1,

        [Description("Kpi Elements")]
        KpiElements = 2,

        [Description("Graphical Elements")]
        GraphicalElements = 3,

        //[Description("Realytics Global Operations")]
        //RealyticsGlobalOperations = 4,

        //[Description("Realytics Global Operations")]
        //RealyticsLegalEnitityOperations = 5,
    }


    public class EnumerationEnumerationEntityGroupTable : RealitycsEnumTable<EnumerationEntityGroup>
    {
        public EnumerationEnumerationEntityGroupTable(EnumerationEntityGroup enumClass) : base(enumClass)
        {

        }
        public EnumerationEnumerationEntityGroupTable() : base()
        {

        }
    }
}
