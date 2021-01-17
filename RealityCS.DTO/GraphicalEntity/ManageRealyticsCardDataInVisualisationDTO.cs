using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManagerRealitycsCardDataFieldInVisualisationDTO
    {
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
    }
    public class ManageRealitycsCardDataInVisualisationDTO
    {
        public List<ManagerRealitycsCardDataFieldInVisualisationDTO> RawRecord { get; set; }
    }
    public class ManageRealyticsCardDataInVisualisationDTO
    {
        public int DashboardId { get; set; }
        public int CardId { get; set; }
        public EnumerationSupportedGraphType GarphType { get; set; }
        public List<ManageRealitycsCardDataInVisualisationDTO> CardRawData { get; set; }
        public ManageRealitycsFormattedCardDataInVisualisationDTO CardFormattedData { get; set; }
    }
}
