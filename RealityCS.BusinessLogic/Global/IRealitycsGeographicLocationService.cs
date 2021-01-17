using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RealityCS.DataLayer.Context.RealitycsShared
{
    public interface IRealitycsGeographicLocationService
    {
        public Task<List<ManageCountryDTO>> Countries();
        public Task<List<ManageCityDTO>> Cities();
        public Task<List<ManageCityDTO>> Cities(ManageFetchCitiesOnCountryAndStateDTO payload);
        public Task<List<ManageCityDTO>> Cities(ManageFetchCitiesOnStateDTO payload);
        public Task<List<ManageSateDTO>> States(ManageFetchStatesOnCountryDTO payload);
        public Task<List<ManageSateDTO>> States();
    }
}
