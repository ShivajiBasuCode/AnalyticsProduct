using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Import
{
    public interface IRealitycsImportManagerConfiguration
    {
        Task<bool> ImportDataFromCSV(IFormFile file, int ParentId, Guid operationId);
    }
}
