using RealityCS.DTO.KPIEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.KPIEntity
{
    /// <summary>
    /// Interface for KPI service context
    /// </summary>
    public interface IKPIEntityConfigurationService
    {
        /// <summary>
        /// Register a new KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>
        /// KPI identifier
        /// </returns>
        Task<int> AddKPI(ManageAddKpiDTO kpi);

        /// <summary>
        /// Update existing KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        Task<bool> UpdateKPI(ManageKpiDTO kpi);

        /// <summary>
        /// Update existing KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        Task<bool> DeleteKPI(ManageDeleteKpiDTO kpi);

        /// <summary>
        /// List down all the registered KPIs with KPI Informations and KPI Data and drilldown informations
        /// </summary>
        /// <returns>
        /// Collection of KPIs
        /// </returns>
        Task<ICollection<ManageKpiDTO>> KPIs();

        /// <summary>
        /// List down all the registered KPI lite informations (names, description)
        /// </summary>
        /// <returns>
        /// Collection of KPI Lites
        /// </returns>
        Task<ICollection<ManageKpiLiteDTO>> KPIsLite();

        /// <summary>
        /// Get KPI Information(with out data) for a perticular KPI
        /// </summary>
        /// <param name="fetchKpi"></param>       
        /// <returns>
        /// KPI Information(with out data)
        /// </returns>
        Task<ManageKpiDTO> KPIInformation(FetchKpiDTO fetchKpi);

        /// <summary>
        /// List down KPI Data Information(data elements and drilldown information for a perticular KPI
        /// </summary>
        /// <param name="fetchKpi"></param>
        /// <returns>
        /// Collection of data information
        /// </returns>
        Task<ICollection<ManageKpiDataElementDTO>> KPIDataInformation(FetchKpiDTO fetchKpi);

        /// <summary>
        /// Register a new KPI Data Element of an existing KPI
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        Task<bool> AddKPIDataElements(List<ManageAddKpiDataElementDTO> dataElements );

        /// <summary>
        /// Update a KPI Data Element of an existing KPI
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        Task<bool> UpdateKPIDataElement(ManageKpiDataElementDTO dataElement);

        /// <summary>
        /// Delete a KPI Data Element of an existing KPI
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns>
        /// Success or 
        /// </returns>
        Task<bool> DeleteKPIDataElement(ManageDeleteKpiDataElementDTO dataElement);

        /// <summary>
        /// Register a new KPI Drilldown Data Element of an existing KPI Data Element
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns></returns>
        Task<int> AddKPIDrilldownDataElement(ManageAddKpiDataElementDrilldownDTO dataElement);

        /// <summary>
        /// Update a KPI Drilldown Data Element of an existing KPI Data Element
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns></returns>
        Task<bool> UpdateKPIDrilldownDataElement(ManageKpiDataElementDrilldownDTO dataElement);

        /// <summary>
        /// Delete a KPI Drilldown Data Element of an existing KPI Data Element
        /// </summary>
        /// <param name="dataElement"></param>
        /// <returns></returns>
        Task<bool> DeleteCascadeKPIDrilldownDataElement(ManageDeleteCascadeKpiDrilldownDataElementDTO dataElement);

        /// <summary>
        /// Register a new value stream
        /// </summary>
        /// <param name="valueStream"></param>
        /// <returns></returns>
        Task<int> AddValueStream(ManageAddValueStreamDTO valueStream);
        /// <summary>
        /// List down all the registered value streams
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ManageValueStreamDTO>> ValueStreams();
        /// <summary>
        /// Add Risk Operation
        /// </summary>
        /// <param name="risk"></param>
        /// <returns></returns>
        Task<int> AddRisk(ManageAddRiskDTO risk);
        /// <summary>
        /// Update Risk Operation
        /// </summary>
        /// <param name="risk"></param>
        /// <returns></returns>
        Task<bool> UpdateRisk(ManageRiskDTO risk);
        /// <summary>
        /// Delete Risk Operation
        /// </summary>
        /// <param name="risk"></param>
        /// <returns></returns>
        Task<bool> DeleteRisk(ManageDeleteRiskDTO risk);
        /// <summary>
        /// List down all the risks
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ManageRiskDTO>> Risks();
        /// <summary>
        /// Add a data source relation
        /// </summary>
        /// <param name="relation"></param>
        /// <returns>Relation primary id</returns>
        Task<int> AddDatasourceRelation(ManageAddKPIJoiningRelationDTO relation);
        /// <summary>
        /// Update a data source relation
        /// </summary>
        /// <param name="relation"></param>
        /// <returns>true-success / false-failure</returns>
        Task<bool> UpdateDatasourceRelation(ManageKPIJoiningRelationDTO relation);
        /// <summary>
        /// Delete a data source relation
        /// </summary>
        /// <param name="relation"></param>
        /// <returns>true-success / false-failure</returns>
        Task<bool> DeleteDatasourceRelation(ManageDeleteKPIJoiningRelationDTO relation);
        /// <summary>
        /// List down all the data source relations
        /// </summary>
        /// <returns>Collection of Relations</returns>
        Task<ICollection<ManageKPIJoiningRelationDTO>> DatasourceRelations();
    }
}
