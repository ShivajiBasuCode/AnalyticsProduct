using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationDomain
    {
        [Description("Configuration")]
        Configuration=1,

        [Description("Predictive")]
        Predictive = 2,

        [Description("Prescriptive")]
        Prescriptive = 3,

        [Description("Visualisation")]
        Visualisation = 4,
    }

    public class EnumeratioDomainTable : RealitycsEnumTable<EnumerationDomain>
    {
        public EnumeratioDomainTable(EnumerationDomain enumClass) : base(enumClass)
        {

        }
        public EnumeratioDomainTable() : base()
        {

        }
    }
}
