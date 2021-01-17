using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DTO.RealitycsClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
    public interface ILegalEntityService
    {
        Task<int> AddLegalEntity(ManageAddLegalEntityDTO payload);
        Task<bool> UpdateLegalEntity(ManageLegalEntityDTO payload);
        Task<List<ManageLegalEntityDTO>> LegalEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ImportClientData(ClientDataImportDTO request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="values"></param>
        /// <param name="ElementEntityName"></param>
        /// <param name="ParentId"></param>
        /// <param name="operationId"></param>
        /// <param name="GetParentId"></param>
        /// <returns></returns>
        Task<bool> SaveImportDataFromCSV(List<string> attributes, List<List<string>> values, string ElementEntityName, int ParentId, Guid operationId, Action<int> GetParentId = null);
        /// <summary>
        /// List of data source name 
        /// </summary>
        /// <returns></returns>
        Task<List<ClientDropDownDTO>> GetDataSourceName();
        /// <summary>
        /// List of data source with values
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        Task<List<ClientDropDownDTO>> GetDataSource(int ParentId);
        /// <summary>
        /// list of data source elements 
        /// </summary>
        /// <param name="dataSourceAttributeId"></param>
        /// <returns></returns>
        Task<List<AttributeAliasNameDTO>> GetDataSourceElements(int dataSourceAttributeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task DataSourceElementsAliasName(SaveAttributeAliasNameDTO request);

    }
}
