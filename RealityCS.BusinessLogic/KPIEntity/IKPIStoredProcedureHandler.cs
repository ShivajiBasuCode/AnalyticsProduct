using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.KPIEntity
{
    public interface IKPIStoredProcedureHandler
    {
        Task<bool> CreateStoredProcedureForKPI(RealyticsKPI KPI);

        Task<bool> RenameStoredProcedureOfKPI(string kpiOldName, string kpiNewName);

        Task<bool> DropStoredProcedureOfKPI(string kpiName);

        Task<bool> IsStoreProcedureAlreadyExistsForKPI(string kpiName);
    }
}
