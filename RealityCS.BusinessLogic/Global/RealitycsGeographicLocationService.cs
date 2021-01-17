using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsShared;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public class RealitycsGeographicLocationService : IRealitycsGeographicLocationService
    {
        private readonly RealitycsSharedContext realitycsSharedContext;
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<MasterCountry> CountryRepository;
        private readonly IGenericRepository<MasterCity> CityRepository;
        private readonly IGenericRepository<MasterState> StateRepository;

        public RealitycsGeographicLocationService(
                IMapper mapper,
                IWorkContext workContext,
                RealitycsSharedContext realitycsSharedContext,
                IGenericRepository<MasterCountry> CountryRepository,
                IGenericRepository<MasterCity> CityRepository,
                IGenericRepository<MasterState> StateRepository
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.realitycsSharedContext = realitycsSharedContext;
            this.CountryRepository = CountryRepository;
            this.CityRepository = CityRepository;
            this.StateRepository = StateRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ManageCityDTO>> Cities()
        {
            var cities = (from cityInDB in CityRepository.Table
                             select new ManageCityDTO()
                             {
                                 Id = cityInDB.PK_Id,
                                 Name = cityInDB.Name,
                                 City_ascii = cityInDB.City_ascii,
                                 Latitude = cityInDB.Latitude,
                                 Longitude = cityInDB.Longitude
                             }).ToList();

            return cities;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<List<ManageCityDTO>> Cities(ManageFetchCitiesOnCountryAndStateDTO payload)
        {
            var cities = (from cityInDB in CityRepository.Table
                          join stateInDB in StateRepository.Table on cityInDB.StateId equals stateInDB.PK_Id
                          where stateInDB.CountryId == payload.CountryID && stateInDB.PK_Id == payload.StateId
                          select new ManageCityDTO()
                          {
                              Id = cityInDB.PK_Id,
                              Name = cityInDB.Name,
                              City_ascii = cityInDB.City_ascii,
                              Latitude = cityInDB.Latitude,
                              Longitude = cityInDB.Longitude
                          }).ToList();

            return cities;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<List<ManageCityDTO>> Cities(ManageFetchCitiesOnStateDTO payload)
        {
            var cities = (from cityInDB in CityRepository.Table
                          where cityInDB.StateId == payload.StateId
                          select new ManageCityDTO()
                          {
                              Id = cityInDB.PK_Id,
                              Name = cityInDB.Name,
                              City_ascii = cityInDB.City_ascii,
                              Latitude = cityInDB.Latitude,
                              Longitude = cityInDB.Longitude
                          }).ToList();

            return cities;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ManageCountryDTO>> Countries()
        {
            var countries = (from countryInDB in CountryRepository.Table
                               select new ManageCountryDTO()
                               {
                                   Id = countryInDB.PK_Id,
                                   Name = countryInDB.Name,
                                   TwoLetterIsoCode = countryInDB.TwoLetterIsoCode,
                                   PhoneCode = countryInDB.PhoneCode,
                                   Continent = countryInDB.Continent,
                                   ContinentCode = countryInDB.ContinentCode
                               }).ToList();

            return countries;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<List<ManageSateDTO>> States(ManageFetchStatesOnCountryDTO payload)
        {
            var states = (from statesInDB in StateRepository.Table
                          where statesInDB.CountryId == payload.CountryID
                          select new ManageSateDTO()
                          {
                              Id = statesInDB.PK_Id,
                              Name = statesInDB.Name,
                          }).ToList();

            return states;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ManageSateDTO>> States()
        {
            var states = (from statesInDB in StateRepository.Table
                             select new ManageSateDTO()
                             {
                                 Id = statesInDB.PK_Id,
                                 Name = statesInDB.Name,
                             }).ToList();

            return states;
        }
    }
}
