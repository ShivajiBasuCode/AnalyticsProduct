using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumeratioOperationLevel
    {
        [Description("GlobalLevel")]
        GlobalLevel = 1,

        [Description("LegalEntityLevel")]
        LegalEntityLevel = 2,

        [Description("EntityLevel")]
        Prescriptive = 3,

    }

    public class EnumeratioOperationLevelnTable : RealitycsEnumTable<EnumeratioOperationLevel>
    {
        public EnumeratioOperationLevelnTable(EnumeratioOperationLevel enumClass) : base(enumClass)
        {

        }
        public EnumeratioOperationLevelnTable() : base()
        {

        }
    }
}
