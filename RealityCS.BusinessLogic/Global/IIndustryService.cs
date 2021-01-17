using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Global
{
    public interface IIndustryService
    {
        public Task<int> AddIndustry(ManageAddIndustryDTO payload);
        public Task<List<ManageIndustryDTO>> Industries();
    }
}
