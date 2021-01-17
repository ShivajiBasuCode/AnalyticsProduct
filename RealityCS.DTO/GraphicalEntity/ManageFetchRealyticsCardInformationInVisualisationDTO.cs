using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageFetchRealyticsMultipleCardInformationInVisualisationDTO
    {
        public List<ManageFetchRealyticsCardInformationInVisualisationDTO> Cards { get; set; }
    }
    public class ManageFetchRealyticsCardInformationInVisualisationDTO: ManageFetchAllRealyticsCardInformationForDashboardInVisualisationDTO
    {
        public int CardId { get; set; }
        public int RecordType { get; set; }
    }
}
