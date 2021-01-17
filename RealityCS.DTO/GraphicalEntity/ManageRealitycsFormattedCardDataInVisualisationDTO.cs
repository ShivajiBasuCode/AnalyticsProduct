using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class CardCummulativeData
    {
        public string SectionName { get; set; }
        public string SectionValue { get; set; }
        public string ValueName { get; set; }
        public object Value { get; set; }
        public Type ValueType { get; set; }
    }
    public class ManageRealitycsGraphBarChartFormattedDataDTO : ManageRealitycsGraphFormattedDataDTO
    {
        public int PropertyValueOne { get; set; }
        public int PropertyValueTwo { get; set; }
    }
    public class ManageRealitycsGraphPieOrDonutOrColumnChartFormattedDataDTO : ManageRealitycsGraphFormattedDataDTO
    {
        public string PropertyName { get; set; }
        public int PropertyValue { get; set; }
    }

    public class ManageRealitycsGraphLineChartFormattedDataDTO : ManageRealitycsGraphFormattedDataDTO
    {
        public DateTime TimeStamp { get; set; }
        public int PropertyValue { get; set; }
    }

    public abstract class ManageRealitycsGraphFormattedDataDTO
    {

    }
    public class ManageRealitycsFormattedCardDataInVisualisationDTO
    {
        public List<IDictionary<string, object>> FormattedRecord { get; set; }  
    }
}
