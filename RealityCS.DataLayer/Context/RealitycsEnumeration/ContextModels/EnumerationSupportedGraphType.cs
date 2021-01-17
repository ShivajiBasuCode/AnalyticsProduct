using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationSupportedGraphType
    {
        [Description("PieChart")] //am4charts.PieChart
        PieChart = 1,

        [Description("DonutChart")]//am4charts.PieChart
        DonutChart = 2,

        [Description("LineChart")]//am4charts.XYChart
        LineChart = 3,

        [Description("ColumnChart")]//am4charts.XYChart
        ColumnChart = 4,

        [Description("BarChart")]//am4charts.XYChart3D
        BarChart = 5,

        [Description("MapChart")]//am4maps.MapChart
        MapChart = 6,

        [Description("TimelineChart")]
        TimelineChart = 7,

        [Description("BubbleChart")]
        BubbleChart = 8,

        [Description("GaugesChart")]
        GaugesChart = 9,
    }

    public class EnumerationSupportedGraphTypesTable : RealitycsEnumTable<EnumerationSupportedGraphType>
    {
        public EnumerationSupportedGraphTypesTable(EnumerationSupportedGraphType enumClass) : base(enumClass)
        {

        }
        public EnumerationSupportedGraphTypesTable() : base()
        {

        }
    }
}
