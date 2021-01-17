using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity
{
    public abstract class RealyticsGraphicalBase : RealitycsBase
    {
        public int FK_LegalEntityId { get; set; }
    }
}
