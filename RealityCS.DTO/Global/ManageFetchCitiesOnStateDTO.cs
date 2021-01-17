using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageFetchCitiesOnCountryAndStateDTO : ManageFetchCitiesOnStateDTO
    {
        public int CountryID { get; set; }
    }
    public class ManageFetchCitiesOnStateDTO
    {
        public int StateId { get; set; }
    }
}
