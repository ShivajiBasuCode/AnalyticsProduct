using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationKPIFormulas
    {
        [Description("None")]
        None = 0,

        [Description("Count")]
        Count = 1,

        [Description("Average")]
        Average = 2,

        [Description("Percentage")]
        Percentage = 3,

        [Description("Cumulative")]
        Cumulative = 4,

        [Description("Summation")]
        Summation = 5,

        [Description("Differentiation")]
        Differentiation = 6,

        [Description("Multiplication")]
        Multiplication = 7,

        [Description("Division")]
        Division = 8,
    }

    public class EnumerationKPIFormulasTable : RealitycsEnumTable<EnumerationKPIFormulas>
    {
        public EnumerationKPIFormulasTable(EnumerationKPIFormulas enumClass) : base(enumClass)
        {

        }
        public EnumerationKPIFormulasTable() : base()
        {

        }
    }
}
