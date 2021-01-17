using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationDrilldownOrder
    {
        [Description("FirstOrder")]
        FirstOrder = 1,

        [Description("SecondOrder")]
        SecondOrder = 2,

        [Description("ThirdOrder")]
        ThirdOrder = 3,

        [Description("FourthOrder")]
        FourthOrder = 4,

        [Description("FifthOrder")]
        FifthOrder = 5,

        [Description("SixthOrder")]
        SixthOrder = 6,

        [Description("SeventhOrder")]
        SeventhOrder = 7,

        [Description("EigthOrder")]
        EigthOrder = 8,

        [Description("NinthOrder")]
        NinthOrder = 9,

        [Description("TenthOrder")]
        TenthOrder = 10,
    }

    public class EnumerationDrilldownOrderTable : RealitycsEnumTable<EnumerationDrilldownOrder>
    {
        public EnumerationDrilldownOrderTable(EnumerationDrilldownOrder enumClass) : base(enumClass)
        {

        }
        public EnumerationDrilldownOrderTable() : base()
        {

        }
    }
}
