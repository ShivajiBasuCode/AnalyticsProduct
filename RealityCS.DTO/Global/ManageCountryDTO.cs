using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageCountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public int PhoneCode { get; set; }
        public string Continent { get; set; }
        public string ContinentCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
    }
}
