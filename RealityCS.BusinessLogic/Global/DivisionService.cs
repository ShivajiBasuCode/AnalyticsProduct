using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DTO.Global;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public class DivisionService:IDivisionService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<MasterDivision> DivisionRepository;
        public DivisionService(
            IMapper mapper,
            IWorkContext workContext,
            IGenericRepository<MasterDivision> DivisionRepository
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.DivisionRepository = DivisionRepository;

        }
        /// <summary>
        /// Business logic to add a new division
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Division identifier
        /// </returns>
        public async Task<int> AddDivision(ManageAddDivisionDTO payload)
        {
            var NewDivision = new MasterDivision()
            {
                Name = payload.Name,
                Description = payload.Description
            };

            await DivisionRepository.InsertAsync(NewDivision, true);

            return NewDivision.PK_Id;
        }

        public async Task<List<ManageDivisionDTO>> Divisions()
        {
            var divisions = (from divisionInDB in DivisionRepository.Table
                               select new ManageDivisionDTO()
                               {
                                   Id = divisionInDB.PK_Id,
                                   Name = divisionInDB.Name,
                                   Description = divisionInDB.Description
                               }).ToList();

            return divisions;
        }
    }
}
