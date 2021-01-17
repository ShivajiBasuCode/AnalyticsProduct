using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.GraphicalEntity
{
    public interface IGraphicalEntityConfigurationService
    {
        public Task<int> AddDashboardTemplate(ManageAddGraphicalTemplateDTO payload);
        public Task<bool> UpdateDashboardTemplate(ManageGraphicalTemplateDTO payload);
        public Task<bool> DeleteDashboardTemplate(ManageDeleteGraphicalTemplateDTO payload);
        public Task<List<ManageGraphicalTemplateDTO>> DashboardTemplates();
        public Task<int> AddDashboard(ManageAddGraphicalDashboardDTO payload);
        public Task<bool> UpdateDashboard(ManageGraphicalDashboardDTO payload);
        public Task<bool> DeleteDashboard(ManageDeleteGraphicalDashboardDTO payload);
        public Task<List<ManageGraphicalDashboardDTO>> Dashboards();
        public Task<int> AddCard(ManageAddGraphicalCardDTO payload);
        public Task<bool> AddCards(List<ManageAddGraphicalCardDTO> payload);
        public Task<bool> UpdateCard(ManageGraphicalCardDTO payload);
        public Task<bool> UpdateCards(List<ManageGraphicalCardDTO> payload);
        public Task<bool> DeleteCard(ManageDeleteGraphicalCardDTO payload);
        public Task<List<ManageGraphicalCardDTO>> Cards();
        public Task<List<ManageGraphicalCardDTO>> Cards(ManageSelectGraphicalDashboardDTO payload);
        public Task<int> RegisterCardDataAttribute(ManageAddGraphicalCardAttributeIdDTO payload);
        public Task<bool> UnRegisterCardDataAttribute(ManageGraphicalCardAttributeIdDTO payload);
    }
}
