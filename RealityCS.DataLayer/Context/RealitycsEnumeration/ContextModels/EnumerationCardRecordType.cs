using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationCardRecordType
    {
        [Description("RawRecord")]
        RawRecord = 1,

        [Description("FormattedRecord")]
        FormattedRecord = 2,

        [Description("RawAndFormattedRecord")]
        RawAndFormattedRecord = 3,
    }
}
