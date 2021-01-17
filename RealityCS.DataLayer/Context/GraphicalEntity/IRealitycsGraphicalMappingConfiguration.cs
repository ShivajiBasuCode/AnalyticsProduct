using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity
{
    public interface IRealitycsGraphicalMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
