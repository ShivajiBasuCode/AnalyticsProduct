using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.GraphicalEntity
{
    public interface IGraphicalEntityVisualisationService
    {
        public Task<ManageRealyticsCardDataInVisualisationDTO> GetCard(ManageFetchRealyticsCardInformationInVisualisationDTO payload);
        public Task<List<ManageRealyticsCardDataInVisualisationDTO>> GetCards(List<ManageFetchRealyticsCardInformationInVisualisationDTO> payload);
        public Task<List<ManageRealyticsCardDataInVisualisationDTO>> GetCards(ManageFetchAllRealyticsCardInformationForDashboardInVisualisationDTO payload);
        public Task<List<ManageDashboardNavigationInVisualisationDTO>> NavigationDashboards();
    }
}
