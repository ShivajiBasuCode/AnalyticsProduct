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
    public class DepartmentService:IDepartmentService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<MasterDepartment> DepartmentRepository;
        /// <summary>
        /// CTor of Department service
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="workContext"></param>
        /// <param name="DepartmentRepository"></param>
        public DepartmentService(
            IMapper mapper,
            IWorkContext workContext,
            IGenericRepository<MasterDepartment> DepartmentRepository
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.DepartmentRepository = DepartmentRepository;
        }
        /// <summary>
        /// Business logic to add a new department
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Department identifier
        /// </returns>
        public async Task<int> AddDepartment(ManageAddDepartmentDTO payload)
        {
            var NewDepartment = new MasterDepartment()
            {
                Name = payload.Name,
                Description = payload.Description
            };

            await DepartmentRepository.InsertAsync(NewDepartment, true);

            return NewDepartment.PK_Id;
        }

        public async Task<List<ManageDepartmentDTO>> Departments()
        {
            var departments = (from departmentInDB in DepartmentRepository.Table
                               select new ManageDepartmentDTO() 
                               {
                                   Id = departmentInDB.PK_Id,
                                   Name = departmentInDB.Name,
                                   Description = departmentInDB.Description
                               }).ToList();

            return departments;
        }
    }
}
