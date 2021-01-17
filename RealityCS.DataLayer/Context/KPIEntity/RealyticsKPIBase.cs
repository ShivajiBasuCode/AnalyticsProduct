using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity
{
    public abstract class RealyticsKPIBase:RealitycsBase
    {
        public int FK_LegalEntityId { get; set; }
        
    }
}
