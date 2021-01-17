using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration
{
    public interface IRealitycsEnumerationMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
