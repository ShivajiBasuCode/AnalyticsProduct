using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationLogLevelType
    {
        [Description("Debug")]
        Debug=1,

        [Description("Information")]
        Information = 2,

        [Description("Warning")]
        Warning = 3,

        [Description("Error")]
        Error = 4,

        [Description("Fatal")]
        Fatal = 5
    }

    public class EnumerationLogLevelTypeTable:RealitycsEnumTable<EnumerationLogLevelType>
    {
        public EnumerationLogLevelTypeTable(EnumerationLogLevelType enumClass):base(enumClass)
        {

        }
        public EnumerationLogLevelTypeTable():base()
        {

        }
    }
}
