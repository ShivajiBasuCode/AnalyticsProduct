using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient
{
    public interface IRealitycsClientMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
