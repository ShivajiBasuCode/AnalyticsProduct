using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity
{
    public interface IRealitycsKPIMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
