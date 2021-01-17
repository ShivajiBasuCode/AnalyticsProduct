using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationJoinTypes
    {
        [Description("No Join")]
        NoJoin = 1,

        [Description("Inner Join")]
        InnerJoin = 2,

        [Description("Left Outer Join")]
        LeftOuterJoin = 3,

        [Description("Right Outer Join")]
        RightOuterJoin = 4,

        [Description("Full Outer Join")]
        FullOuterJoin = 5,

        [Description("Cross Join")]
        CrossJoin = 6,
    }
}
