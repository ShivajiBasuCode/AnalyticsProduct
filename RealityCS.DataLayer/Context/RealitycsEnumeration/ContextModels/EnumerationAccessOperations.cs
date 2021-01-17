using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationAccessOperations
    {
        //All Global Operations Starts from 500
        [Description("global_operations_configure_user")]
        global_operations_configure_user = 500,

        [Description("global_operations_configure_legal_entity")]
        global_operations_configure_legal_entity = 501,

        //All Legal Entity Operations Starts from 600
        [Description("legal_entity_operations_configure_user")]
        legal_entity_operations_configure_user = 600,

        //All Enitity Operations Starts from 1
        [Description("kpi_crud_operations")]
        kpi_crud_operations = 1,

        [Description("kpi_activate_threshold")]
        kpi_activate_threshold = 2,

        [Description("kpi_activate_benchmark")]
        kpi_activate_benchmark = 3,

        [Description("kpi_governance")]
        kpi_governance = 4,

        [Description("kpi_drilldown")]
        kpi_drilldown = 5,

        [Description("kpi_assessments")]
        kpi_assessments = 6,

        [Description("kpi_predictive")]
        kpi_predictive = 7,

        [Description("kpi_prescriptive")]
        kpi_prescriptive = 8,

        [Description("kpi_risks")]
        kpi_risks = 9,

        [Description("kpi_erp_link")]
        kpi_erp_link = 10,

        [Description("kpi_threshold_measure_value")]
        kpi_threshold_measure_value = 11,

        [Description("kpi_benchmark_measure_value")]
        kpi_benchmark_measure_value = 12,

        [Description("data_import_data_source")]
        data_import_data_source = 13,

        [Description("data_import_data_source_elements")]
        data_import_data_source_elements = 14,
    }

    public class EnumerationAccessOperationsTable : RealitycsEnumTable<EnumerationAccessOperations>
    {
        public EnumerationAccessOperationsTable(EnumerationAccessOperations enumClass) : base(enumClass)
        {

        }
        public EnumerationAccessOperationsTable() : base()
        {

        }
    }
}
