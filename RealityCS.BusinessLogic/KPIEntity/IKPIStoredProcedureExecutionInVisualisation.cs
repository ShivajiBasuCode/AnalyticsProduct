using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.KPIEntity
{
    public interface IKPIStoredProcedureExecutionerInVisualisation
    {
        Task<List<ManageRealitycsCardDataInVisualisationDTO>> FetchCardRawData(RealyticsGraphicalCard card);
        Task<ManageRealitycsFormattedCardDataInVisualisationDTO> FetchCardFormattedData(RealyticsGraphicalCard card);
    }
}
