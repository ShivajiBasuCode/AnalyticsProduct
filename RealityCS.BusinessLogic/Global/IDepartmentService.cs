using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public interface IDepartmentService
    {
        public Task<int> AddDepartment(ManageAddDepartmentDTO payload);
        public Task<List<ManageDepartmentDTO>> Departments();
    }
}
