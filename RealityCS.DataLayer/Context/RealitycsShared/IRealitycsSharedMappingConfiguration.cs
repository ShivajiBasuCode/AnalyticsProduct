using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared
{
    public interface IRealitycsSharedMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
