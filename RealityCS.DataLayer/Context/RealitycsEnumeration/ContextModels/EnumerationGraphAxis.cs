using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationGraphAxis
    {
        [Description("X - Axis")]
        XAxis=1,

        [Description("Y - Axis")]
        YAxis = 2,
    }

    public class EnumerationGraphAxisTable : RealitycsEnumTable<EnumerationGraphAxis>
    {
        public EnumerationGraphAxisTable(EnumerationGraphAxis enumClass) : base(enumClass)
        {

        }
        public EnumerationGraphAxisTable() : base()
        {

        }
    }
}
