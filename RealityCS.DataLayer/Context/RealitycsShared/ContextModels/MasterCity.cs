using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public class MasterCity : RealitycsSharedBase
    {

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the city_ascii
        /// </summary>
        public string City_ascii { get; set; }

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the population
        /// </summary>
        public string Population { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual MasterState States { get; set; }
    }
}
