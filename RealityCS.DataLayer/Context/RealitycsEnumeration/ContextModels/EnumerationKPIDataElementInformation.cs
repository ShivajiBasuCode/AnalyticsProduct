using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationKPIDataElementInformation
    { 
        [Description("None")]
        None = 0,

        [Description("HasDrilldownData")]
        HasDrilldownData = 1,

        [Description("IncludedInStoredprocedure")]
        IncludedInStoredprocedure = 2,
    }

    public class EnumerationKPIDataElementInformationTable : RealitycsEnumTable<EnumerationKPIDataElementInformation>
    {
        public EnumerationKPIDataElementInformationTable(EnumerationKPIDataElementInformation enumClass) : base(enumClass)
        {

        }
        public EnumerationKPIDataElementInformationTable() : base()
        {

        }
    }
}
