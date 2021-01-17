using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DTO.Global;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public class IndustryService : IIndustryService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<MasterIndustry> IndustryRepository;

        public IndustryService(
            IMapper mapper,
            IWorkContext workContext,
            IGenericRepository<MasterIndustry> IndustryRepository
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.IndustryRepository = IndustryRepository;
        }
        /// <summary>
        /// Business logic to add a new Industry
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Industry identifier
        /// </returns>
        public async Task<int> AddIndustry(ManageAddIndustryDTO payload)
        {
            var NewIndustry = new MasterIndustry()
            {
                Name = payload.Name,
                Description = payload.Description
            };

            await IndustryRepository.InsertAsync(NewIndustry, true);

            return NewIndustry.PK_Id;

        }

        public async Task<List<ManageIndustryDTO>> Industries()
        {
            var industries = (from industryInDB in IndustryRepository.Table
                              select new ManageIndustryDTO()
                              {
                                  Id = industryInDB.PK_Id,
                                  Name = industryInDB.Name,
                                  Description = industryInDB.Description
                              }).ToList();

            return industries;
        }
    }
}
