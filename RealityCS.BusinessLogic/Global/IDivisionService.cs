using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public interface IDivisionService
    {
        public Task<int> AddDivision(ManageAddDivisionDTO payload);
        public Task<List<ManageDivisionDTO>> Divisions();
    }
}
