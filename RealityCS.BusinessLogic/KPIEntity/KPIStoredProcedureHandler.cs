using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.BaseContext;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* Generic KPI stored procedure template 
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Shivaji Basu>
-- Create date: <Create Date, 7th Dec 2020,>
-- Description:	<Description, StoredProcedure of a KPI element,>
-- =============================================
IF OBJECT_ID('kpi.usp_GetRuntimeData_ForKPI_MyKPI','P') IS NOT NULL
   DROP PROCEDURE [kpi].[usp_GetRuntimeData_ForKPI_MyKPI]
GO
CREATE PROCEDURE [kpi].[usp_GetRuntimeData_ForKPI_MyKPI]
(
	@From nvarchar(MAX) = N'Default',
	@To nvarchar(MAX),
	@FilerCondition nvarchar(MAX) = null,
	@CardParams nvarchar(MAX) = N'*'
)
AS
BEGIN
BEGIN TRY
	SET NOCOUNT ON;

	DECLARE @qry as nvarchar(MAX);
	IF @From <> N'Default'
	BEGIN
		SET @qry = '
		DECLARE @To as nvarchar(MAX)
		DECLARE @From as nvarchar(MAX)
		SET @From = N'''+@From+'''
		SET @To = N'''+@To+'''
		;WITH CTE_RESULTS AS (
		SELECT ds#1.DepotName, CAST(ds#1.TradePrice as DECIMAL(10,2)) + CAST(ds#1.ProductVAT as DECIMAL(10,2)) As TradePrice_ProductVAT_Total, ds#2.TradePrice, ds#2.DiscountAmount, ds#1.ProductVAT, (CAST(ds#2.DiscountAmount as DECIMAL(10,2)) * CAST(ds#2.Quantity as DECIMAL(10,2))) As DiscountAmount_Quantity_Multiply, (CAST(ds#1.DiscountAmount as DECIMAL(10,2)) / CAST(ds#1.Quantity as DECIMAL(10,2))) As DiscountAmount_Quantity_Divide, ds#1.CreatedDate FROM  [client].[ViewAABC79434C294293904EBF23E698A554] as ds#1 INNER JOIN [client].[ViewADFE352540F64993B7BEF18EDC667F0C] as ds#2 ON ds#1.MemoNo = ds#2.MemoNo
		WHERE CAST(ds#1.CreatedDate as datetime2) >= CAST(@From as datetime2)
		AND CAST(ds#1.CreatedDate AS datetime2) <= CAST(@To AS datetime2)
		)
		SELECT '+ @CardParams+' FROM CTE_RESULTS;';
		exec sp_executesql @qry;
	END
	ELSE
	BEGIN
		SET @qry = '
		DECLARE @To as nvarchar(MAX)
		SET @To = N'''+@To+'''
		;WITH CTE_RESULTS AS (
		SELECT ds#1.DepotName, CAST(ds#1.TradePrice as DECIMAL(10,2)) + CAST(ds#1.ProductVAT as DECIMAL(10,2)) As TradePrice_ProductVAT_Total, ds#2.TradePrice, ds#2.DiscountAmount, ds#1.ProductVAT, (CAST(ds#2.DiscountAmount as DECIMAL(10,2)) * CAST(ds#2.Quantity as DECIMAL(10,2))) As DiscountAmount_Quantity_Multiply, (CAST(ds#1.DiscountAmount as DECIMAL(10,2)) / CAST(ds#1.Quantity as DECIMAL(10,2))) As DiscountAmount_Quantity_Divide, ds#1.CreatedDate FROM  [client].[ViewAABC79434C294293904EBF23E698A554] as ds#1 INNER JOIN [client].[ViewADFE352540F64993B7BEF18EDC667F0C] as ds#2 ON ds#1.MemoNo = ds#2.MemoNo
		WHERE CAST(ds#1.CreatedDate AS datetime2) <= CAST(@To as datetime2)
		)
		SELECT '+ @CardParams+' FROM CTE_RESULTS;';
		exec sp_executesql @qry;
	END
END TRY
BEGIN CATCH
INSERT INTO global.RealitycsDBError
([CreatedBy]
,[CreatedDate]
,[ModifiedBy]
,[ModifiedDate]
,[UserName]
,[ErrorNumber]
,[ErrorState]
,[ErrorSeverity]
,[ErrorLine]
,[ErrorProcedure]
,[ErrorMessage]
,[ErrorDateTime])
VALUES
(
 0,
 GETUTCDATE(),
 0,
 GETUTCDATE(),
 SUSER_SNAME(),
 ERROR_NUMBER(),
 ERROR_STATE(),
 ERROR_SEVERITY(),
 ERROR_LINE(),
 ERROR_PROCEDURE(),
 ERROR_MESSAGE(),
 GETUTCDATE()
);
END CATCH
END
*/

namespace RealityCS.BusinessLogic.KPIEntity
{
    /// <summary>
    /// Helper to generate a KPI stored procedure
    /// </summary>
    public class KPIStoredProcedureHandler : IKPIStoredProcedureHandler, IKPIStoredProcedureExecutionerInVisualisation
    {
        private readonly IMapper _mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<Tbl_Data_RealitycsOperationDetail> RealitycsDataImportOperationDetailsRepository;
        private readonly IGenericRepository<Tbl_Data_RealitycsAttribute> RealitycsCustomerDataAttributeRepository;
        private readonly IGenericRepository<RealyticsKPI> RealyticsKPIRepository;
        private readonly RealitycsKPIContext RealitycsKPIContext;
        private readonly IRealitycsDataProvider dataProvider;
        private readonly IGenericRepository<RealitycsKPIStoredProcedureAliasNameProvider> RealitycsKPIStoredProcedureAliasNameProviderRepository;
        private readonly IGenericRepository<RealitycsKPIStoredProcedureAliasNameProvider> KPIStoredProcedureAliasNameProviderRepository;
        private IDictionary<int, string> DataSourceToAliasNamesMapper = new Dictionary<int, string>();

        public KPIStoredProcedureHandler(
            IMapper mapper,
            IWorkContext workContext,
            IGenericRepository<Tbl_Data_RealitycsOperationDetail> RealitycsDataImportOperationDetailsRepository,
            IGenericRepository<Tbl_Data_RealitycsAttribute> RealitycsCustomerDataAttributeRepository,
            IGenericRepository<RealyticsKPI> RealyticsKPIRepository,
            IGenericRepository<RealitycsKPIStoredProcedureAliasNameProvider> RealitycsKPIStoredProcedureAliasNameProviderRepository,
            IGenericRepository<RealitycsKPIStoredProcedureAliasNameProvider> KPIStoredProcedureAliasNameProviderRepository,
            RealitycsKPIContext RealitycsKPIContext,
            IRealitycsDataProvider dataProvider
            )
        {
            this._mapper = mapper;
            this.workContext = workContext;
            this.RealitycsDataImportOperationDetailsRepository = RealitycsDataImportOperationDetailsRepository;
            this.RealitycsCustomerDataAttributeRepository = RealitycsCustomerDataAttributeRepository;
            this.RealyticsKPIRepository = RealyticsKPIRepository;
            this.RealitycsKPIStoredProcedureAliasNameProviderRepository = RealitycsKPIStoredProcedureAliasNameProviderRepository;
            this.RealitycsKPIContext = RealitycsKPIContext;
            this.KPIStoredProcedureAliasNameProviderRepository = KPIStoredProcedureAliasNameProviderRepository;
            this.dataProvider = dataProvider;
        }
        /// <summary>
        /// Generate the corrospoding KPI's stored procedure body as on when required
        /// </summary>
        /// <returns>
        /// KPI stored procedure 
        /// </returns>
        public async Task<bool> CreateStoredProcedureForKPI(RealyticsKPI KPI)
        {
            await PrepareAliasNamesFromDataSourceNamesAsync(KPI.JoiningRelationship, "ds");
            string StoreProcedureName = string.Format("[usp_GetRuntimeData_ForKPI_{0}]", KPI.Name);
            string StoredProcedureContent = string.Empty;
            StoredProcedureContent += "SET ANSI_NULLS OFF " + Environment.NewLine;
            StoredProcedureContent += "GO " + Environment.NewLine;
            StoredProcedureContent += "SET QUOTED_IDENTIFIER ON " + Environment.NewLine;
            StoredProcedureContent += "GO " + Environment.NewLine;
            StoredProcedureContent += string.Format("IF OBJECT_ID('kpi.{0}','P') IS NOT NULL{1}", StoreProcedureName, Environment.NewLine);
            StoredProcedureContent += string.Format("DROP PROCEDURE IF EXISTS [kpi].{0}{1}", StoreProcedureName, Environment.NewLine);
            StoredProcedureContent += "GO " + Environment.NewLine;
            StoredProcedureContent += string.Format("CREATE PROCEDURE [kpi].{0}{1}", StoreProcedureName, Environment.NewLine);
            StoredProcedureContent += "(" + Environment.NewLine;
            StoredProcedureContent += "\t@From nvarchar(MAX) = N'Default'," + Environment.NewLine;
            StoredProcedureContent += "\t@To nvarchar(MAX)," + Environment.NewLine;
            StoredProcedureContent += "\t@FilerCondition nvarchar(MAX) = null," + Environment.NewLine;
            StoredProcedureContent += "\t@CardParams nvarchar(MAX) = N'*'" + Environment.NewLine;
            StoredProcedureContent += ")" + Environment.NewLine;
            StoredProcedureContent += "AS" + Environment.NewLine;
            StoredProcedureContent += "BEGIN" + Environment.NewLine;
            StoredProcedureContent += "BEGIN TRY" + Environment.NewLine;
            StoredProcedureContent += "	SET NOCOUNT ON;" + Environment.NewLine + Environment.NewLine;
            StoredProcedureContent += await GenerateStoreProcedureDataQueryFromDatasourceElementsAsync(KPI);
            StoredProcedureContent += "END TRY" + Environment.NewLine;
            StoredProcedureContent += "BEGIN CATCH" + Environment.NewLine;
            StoredProcedureContent += "INSERT INTO global.RealitycsDBError" + Environment.NewLine;
            StoredProcedureContent += "([CreatedBy]" + Environment.NewLine;
            StoredProcedureContent += ",[CreatedDate]" + Environment.NewLine;
            StoredProcedureContent += ",[ModifiedBy]" + Environment.NewLine;
            StoredProcedureContent += ",[ModifiedDate]" + Environment.NewLine;
            StoredProcedureContent += ",[UserName]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorNumber]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorState]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorSeverity]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorLine]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorProcedure]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorMessage]" + Environment.NewLine;
            StoredProcedureContent += ",[ErrorDateTime])" + Environment.NewLine;
            StoredProcedureContent += "VALUES" + Environment.NewLine;
            StoredProcedureContent += "(" + Environment.NewLine;
            StoredProcedureContent += " 0," + Environment.NewLine;
            StoredProcedureContent += " GETUTCDATE()," + Environment.NewLine;
            StoredProcedureContent += " 0," + Environment.NewLine;
            StoredProcedureContent += " GETUTCDATE()," + Environment.NewLine;
            StoredProcedureContent += " SUSER_SNAME()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_NUMBER()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_STATE()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_SEVERITY()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_LINE()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_PROCEDURE()," + Environment.NewLine;
            StoredProcedureContent += " ERROR_MESSAGE()," + Environment.NewLine;
            StoredProcedureContent += " GETUTCDATE()" + Environment.NewLine;
            StoredProcedureContent += ");" + Environment.NewLine;
            StoredProcedureContent += "END CATCH" + Environment.NewLine;
            StoredProcedureContent += "END" + Environment.NewLine;
            StoredProcedureContent += "GO" + Environment.NewLine;
            try
            {
                RealitycsDbContextExtensions.ExecuteSqlScript(RealitycsKPIContext, StoredProcedureContent);
            }catch(Exception e )
            {
                await CleanupRecordInStoredProcedureAliasNameProviderTable(KPI.PK_Id);
                throw e;
            }
            return true;
        }
        /// <summary>
        /// Generate SP main data query from data source elements
        /// </summary>
        /// <returns>
        /// SQL string for data retrieval query of the stored procedure
        /// </returns>
        private async Task<string> GenerateStoreProcedureDataQueryFromDatasourceElementsAsync(RealyticsKPI KPI)
        {
            string StoreProcedureDataQuery = string.Empty;
            if (KPI.DataElements.Count <= 0)
            {
                await this.CleanupRecordInStoredProcedureAliasNameProviderTable(KPI.PK_Id);
                throw new ArgumentNullException("KPI Data Source Elements Not Set");
            }
/*
            string TableViewName = GetTableViewName(KPI.CustomerDataElementIdentifier);

            if (string.IsNullOrEmpty(TableViewName))
            {
                throw new ArgumentNullException("View Name against the attribute Id "
                    + String.Format("[%d]", KPI.CustomerDataElementIdentifier)
                    + " Not Found for KPI :"
                    + KPI.Name
                    + "!!");
            }*/
            string ReferenceFieldForTimeStampFilterAttributeWithAlias = await GetReferenceFieldNameUsedForTimeStampFilter(KPI.DataElements);
            string QueryFromDataElements = await GenerateQueryFromDataElementsAsync(KPI.DataElements);
            string QueryFromRelationship = await GenerateRelationshipQuery(KPI.JoiningRelationship);

            StoreProcedureDataQuery += "\tDECLARE @qry as nvarchar(MAX);" + Environment.NewLine;
            StoreProcedureDataQuery += "\tIF @From <> N'Default'" + Environment.NewLine;
            StoreProcedureDataQuery += "\tBEGIN" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSET @qry = '" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tDECLARE @To as nvarchar(MAX)" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tDECLARE @From as nvarchar(MAX)" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSET @From = N'''+@From+'''" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSET @To = N'''+@To+'''" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\t;WITH CTE_RESULTS AS (" + Environment.NewLine; 
            StoreProcedureDataQuery += string.Format("\t\tSELECT {0}", QueryFromDataElements);
            //StoreProcedureDataQuery += GenerateQueryFromDataElementsAsync(KPI.DataElements);
            StoreProcedureDataQuery += string.Format(" FROM {0}", QueryFromRelationship);//GenerateRelationshipQuery(KPI.JoiningRelationship);
            StoreProcedureDataQuery += string.Format("\t\tWHERE CAST({0} as datetime2) >= CAST(@From as datetime2){1}", ReferenceFieldForTimeStampFilterAttributeWithAlias, Environment.NewLine);
            StoreProcedureDataQuery += string.Format("\t\tAND CAST({0} AS datetime2) <= CAST(@To AS datetime2){1}", ReferenceFieldForTimeStampFilterAttributeWithAlias, Environment.NewLine);
//          StoreProcedureDataQuery += string.Format(" FROM [client].[{0}] as {1}{2}", TableViewName, aliasName, Environment.NewLine);
//          StoreProcedureDataQuery += string.Format("\t\tWHERE CAST({0}.{1} as datetime2) >= CAST(@From as datetime2){2}", aliasName, ReferenceFieldForTimeStampFilterAttribute, Environment.NewLine);
//          StoreProcedureDataQuery += string.Format("\t\tAND CAST({0}.{1} AS datetime2) <= CAST(@To AS datetime2){2}", aliasName, ReferenceFieldForTimeStampFilterAttribute, Environment.NewLine);
            StoreProcedureDataQuery += "\t\t)" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSELECT '+ @CardParams+' FROM CTE_RESULTS;';" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\texec sp_executesql @qry;" + Environment.NewLine;
            StoreProcedureDataQuery += "\tEND" + Environment.NewLine;
            StoreProcedureDataQuery += "\tELSE" + Environment.NewLine;
            StoreProcedureDataQuery += "\tBEGIN" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSET @qry = '" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tDECLARE @To as nvarchar(MAX)" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSET @To = N'''+@To+'''" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\t;WITH CTE_RESULTS AS (" + Environment.NewLine;
            StoreProcedureDataQuery += string.Format("\t\tSELECT {0}", QueryFromDataElements);
            //StoreProcedureDataQuery += GenerateQueryFromDataElementsAsync(KPI.DataElements);
            StoreProcedureDataQuery += string.Format(" FROM {0}", QueryFromRelationship);//GenerateRelationshipQuery(KPI.JoiningRelationship);
            StoreProcedureDataQuery += string.Format("\t\tWHERE CAST({0} AS datetime2) <= CAST(@To as datetime2){1}", ReferenceFieldForTimeStampFilterAttributeWithAlias, Environment.NewLine);
//          StoreProcedureDataQuery += string.Format(" FROM [client].[{0}] as {1}{2}", TableViewName, aliasName, Environment.NewLine);
//          StoreProcedureDataQuery += string.Format("\t\tWHERE CAST({0}.{1} AS datetime2) <= CAST(@To as datetime2){2}", aliasName, ReferenceFieldForTimeStampFilterAttributeWithAlias, Environment.NewLine);
            StoreProcedureDataQuery += "\t\t)" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\tSELECT '+ @CardParams+' FROM CTE_RESULTS;';" + Environment.NewLine;
            StoreProcedureDataQuery += "\t\texec sp_executesql @qry;" + Environment.NewLine;
            StoreProcedureDataQuery += "\tEND" + Environment.NewLine;
            return StoreProcedureDataQuery;
        }
        /// <summary>
        /// Prepare the data source names to alias names mapping that will be used in the KPI stored procedure query
        /// </summary>
        /// <param name="JoiningRelationships"></param>
        /// <param name="aliasNamePrefix"></param>
        /// <returns></returns>
        private async Task<bool> PrepareAliasNamesFromDataSourceNamesAsync(ICollection<RealitycsKPIJoiningRelation> JoiningRelationships, string aliasNamePrefix)
        {
            int dsCounter = 0;
            bool AliasNamesSet = false;
            string AliasName = string.Empty;
            string DataSourceName = string.Empty;

            foreach (var JoiningRelationship in JoiningRelationships)
            {
                
                if (!DataSourceToAliasNamesMapper.ContainsKey(JoiningRelationship.JoiningCustomerDataElementIdentifier))
                {
                    AliasName = string.Format("{0}#{1}", aliasNamePrefix, ++dsCounter);
                    DataSourceToAliasNamesMapper.Add(JoiningRelationship.JoiningCustomerDataElementIdentifier, AliasName);
                    AliasNamesSet = true;

                    DataSourceName = this.GetTableViewName(JoiningRelationship.JoiningCustomerDataElementIdentifier);

                    await PushDataToStoredProcedureAliasNameProviderTable(JoiningRelationship.FK_KpiId,
                        JoiningRelationship.JoiningCustomerDataElementIdentifier,
                        AliasName,
                        DataSourceName);
                }
                if (JoiningRelationship.JoiningRelationship != EnumerationJoinTypes.NoJoin)
                {
                    
                    if (!DataSourceToAliasNamesMapper.ContainsKey(JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation))
                    {
                        AliasName = string.Format("{0}#{1}", aliasNamePrefix, ++dsCounter);
                        DataSourceToAliasNamesMapper.Add(JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation, AliasName);
                        AliasNamesSet = true;

                        DataSourceName = this.GetTableViewName(JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation);

                        await PushDataToStoredProcedureAliasNameProviderTable(JoiningRelationship.FK_KpiId,
                            JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation,
                            AliasName,
                            DataSourceName);
                    }
                }
            }
            return AliasNamesSet;
        }
        private async Task<bool> PushDataToStoredProcedureAliasNameProviderTable(int KpiId, int CustomerDataElementIdentifier, string AliasNameAssigned, string DataSourceName)
        {
            RealitycsKPIStoredProcedureAliasNameProvider record = new RealitycsKPIStoredProcedureAliasNameProvider();
            record.FK_KpiId = KpiId;
            record.FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId;
            record.CustomerDataElementIdentifier = CustomerDataElementIdentifier;
            record.AliasNameAssigned = AliasNameAssigned;
            record.DataSourceName = DataSourceName;
            record.CreatedBy = workContext.CurrentCustomer.FK_EmployeeId;
            record.CreatedDate = DateTime.UtcNow;

            await RealitycsKPIStoredProcedureAliasNameProviderRepository.InsertAsync(record, true);

            return true;
        }
        private async Task<bool> CleanupRecordInStoredProcedureAliasNameProviderTable(int KpiId)
        {
            var record = await RealitycsKPIStoredProcedureAliasNameProviderRepository.FindAsync(x => x.FK_KpiId == KpiId && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);
            
            if (record == null)
                return false;

            await RealitycsKPIStoredProcedureAliasNameProviderRepository.DeleteAsync(record, true);
            return true;
        }
        /// <summary>
        /// Business logic to check if any join condition exisits
        /// </summary>
        /// <param name="JoiningRelationships"></param>
        /// <returns></returns>
        private bool IsRelationshipExists(ICollection<RealitycsKPIJoiningRelation> JoiningRelationships)
        {
            bool relationshipExist = false;
            foreach(var JoiningRelationship in JoiningRelationships)
            {
                if (JoiningRelationship.JoiningRelationship != EnumerationJoinTypes.NoJoin)
                {
                    relationshipExist = true;
                    break;
                }
            }
            return relationshipExist;
        }
        /// <summary>
        /// Generate query for all joining relationship in the KPI storedprocedures
        /// </summary>
        /// <param name="JoiningRelationships"></param>
        /// <param name="aliasNamePrefix"></param>
        /// <returns></returns>
        private async Task<string> GenerateRelationshipQuery(ICollection<RealitycsKPIJoiningRelation> JoiningRelationships)
        {
            string SqlQuery = string.Empty;
            string AliasName = string.Empty;
            string DataSourceName = string.Empty;
            string AliasNameInRelation = string.Empty;
            string DataSourceNameInRelation = string.Empty;

            foreach (var JoiningRelationship in JoiningRelationships)
            {
                if (JoiningRelationship.JoiningRelationship == EnumerationJoinTypes.NoJoin)
                {
                    AliasName = DataSourceToAliasNamesMapper[JoiningRelationship.JoiningCustomerDataElementIdentifier];
                    DataSourceName = this.GetTableViewName(JoiningRelationship.JoiningCustomerDataElementIdentifier);
                    SqlQuery += string.Format(" [client].[{0}] as {1} ", DataSourceName, AliasName);
                    DataSourceToAliasNamesMapper.Add(JoiningRelationship.JoiningCustomerDataElementIdentifier, AliasName);
                }
                else
                {
                    DataSourceName = this.GetTableViewName(JoiningRelationship.JoiningCustomerDataElementIdentifier);
                    AliasName = DataSourceToAliasNamesMapper[JoiningRelationship.JoiningCustomerDataElementIdentifier];
                    DataSourceNameInRelation = this.GetTableViewName(JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation);
                    AliasNameInRelation = DataSourceToAliasNamesMapper[JoiningRelationship.JoiningCustomerDataElementIdentifierInRelation];

                    if (!SqlQuery.Contains(DataSourceName)
                        && !SqlQuery.Contains(DataSourceNameInRelation))
                    {
                        SqlQuery += string.Format(" [client].[{0}] as {1} {2} [client].[{3}] as {4} ON {5}.{6} = {7}.{8}",
                            DataSourceName,
                            AliasName,
                            GenerateJoiningRelationshipQuery(JoiningRelationship.JoiningRelationship),
                            DataSourceNameInRelation,
                            AliasNameInRelation,
                            AliasName,
                            JoiningRelationship.JoiningAttribute,
                            AliasNameInRelation,
                            JoiningRelationship.JoiningAttributeInRelation)
                            + Environment.NewLine;
                    } 
                    else if (SqlQuery.Contains(DataSourceName)
                        && !SqlQuery.Contains(DataSourceNameInRelation))
                    {
                        SqlQuery += string.Format(" {1} [client].[{2}] as {3} ON {4}.{5} = {6}.{7}",
                            GenerateJoiningRelationshipQuery(JoiningRelationship.JoiningRelationship),
                            DataSourceNameInRelation,
                            AliasNameInRelation,
                            AliasName,
                            JoiningRelationship.JoiningAttribute,
                            AliasNameInRelation,
                            JoiningRelationship.JoiningAttributeInRelation)
                            + Environment.NewLine;
                    }
                    else if (!SqlQuery.Contains(DataSourceName)
                        && SqlQuery.Contains(DataSourceNameInRelation))
                    {
                        SqlQuery += string.Format(" {1} [client].[{2}] as {3} ON {4}.{5}={6}.{7}",
                            GenerateJoiningRelationshipQuery(JoiningRelationship.JoiningRelationship),
                            DataSourceName,
                            AliasName,
                            DataSourceName,
                            JoiningRelationship.JoiningAttribute,
                            AliasNameInRelation,
                            JoiningRelationship.JoiningAttributeInRelation)
                            + Environment.NewLine;
                    }
                }
            }
            return SqlQuery;
        }
        /// <summary>
        /// Returns query for all SQL Join conditions on the basis of KPI joining types
        /// </summary>
        /// <param name="JoinType"></param>
        /// <returns></returns>
        private string GenerateJoiningRelationshipQuery(EnumerationJoinTypes JoinType)
        {
            string SqlQuery = string.Empty;

            if (JoinType == EnumerationJoinTypes.InnerJoin)
            {
                SqlQuery = "INNER JOIN";
            }
            else if (JoinType == EnumerationJoinTypes.LeftOuterJoin)
            {
                SqlQuery = "LEFT OUTER JOIN";
            }
            else if (JoinType == EnumerationJoinTypes.RightOuterJoin)
            {
                SqlQuery = "RIGHT OUTER JOIN";
            }
            else if (JoinType == EnumerationJoinTypes.FullOuterJoin)
            {
                SqlQuery = "FULL OUTER JOIN";
            }
            else if (JoinType == EnumerationJoinTypes.CrossJoin)
            {
                SqlQuery = "CROSS JOIN";
            }
            return SqlQuery;
        }
        /// <summary>
        /// SQL for creating a temp table based on the data elements defination in KPI
        /// </summary>
        /// <param name="DataElements"></param>
        /// <returns></returns>
        private string GenerateTMPTableQueryFromDataElements(ICollection<RealyticsKPIDataElement> DataElements)
        {
            string TMPTableQuery = string.Empty;
            TMPTableQuery += "\t\tDECLARE @TMP TABLE(";
            foreach (RealyticsKPIDataElement dataElement in DataElements)
            {
                if (dataElement.FormulaToBeApplied == EnumerationKPIFormulas.Summation)
                    TMPTableQuery += string.Format("{0}_{1}_Total nvarchar(MAX), ", dataElement.CustomerDataAttributeOne, dataElement.CustomerDataAttributeTwo);
                else if (dataElement.FormulaToBeApplied == EnumerationKPIFormulas.Differentiation)
                    TMPTableQuery += string.Format("{0}_{1}_Difference nvarchar(MAX), ", dataElement.CustomerDataAttributeOne, dataElement.CustomerDataAttributeTwo);
                else if (dataElement.FormulaToBeApplied == EnumerationKPIFormulas.Multiplication)
                    TMPTableQuery += string.Format("{0}_{1}_Multiply nvarchar(MAX), ", dataElement.CustomerDataAttributeOne, dataElement.CustomerDataAttributeTwo);
                else if (dataElement.FormulaToBeApplied == EnumerationKPIFormulas.Division)
                    TMPTableQuery += string.Format("{0}_{1}_Divide nvarchar(MAX), ", dataElement.CustomerDataAttributeOne, dataElement.CustomerDataAttributeTwo);
                else
                    TMPTableQuery += string.Format("{0} nvarchar(MAX), ", dataElement.CustomerDataAttributeOne);
            }
            //Remove last ", "
            try
            {
                TMPTableQuery = TMPTableQuery.Remove(TMPTableQuery.Length - 2);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
            TMPTableQuery += ");" + Environment.NewLine; ;
            return TMPTableQuery;
        }
        /// <summary>
        /// Getter for date timestamp field name as [alias name].[timestamp field name] in KPI
        /// </summary>
        /// <param name="DataElements"></param>
        /// <returns></returns>
        private async Task<string> GetReferenceFieldNameUsedForTimeStampFilter(ICollection<RealyticsKPIDataElement> DataElements, bool IncludeAlias = true)
        {
            var DataElementWithTimestampFilter = DataElements.FirstOrDefault(d => d.UsedForTimeStampFilter == true);
            
            if (DataElementWithTimestampFilter == null) throw new InvalidProgramException("Fatal Error; No DateTime Field Found in KPI Data Elements!!!");

            if (!IncludeAlias)
            {
                return DataElementWithTimestampFilter.CustomerDataAttributeOne;
            }

            string AliasName = await GetAliasNameAsync(DataElementWithTimestampFilter.FK_KpiId, DataElementWithTimestampFilter.CustomerDataElementIdentifierOne);

            //since for timesanp filter field no formula will be applied and attribute two will not be considered
            return string.Format("{0}.{1}",
                AliasName, 
                DataElementWithTimestampFilter.CustomerDataAttributeOne);
        }
        /// <summary>
        /// Getter for date timestamp field object in KPI
        /// </summary>
        /// <param name="DataElements"></param>
        /// <returns></returns>
        private RealyticsKPIDataElement GetReferenceFieldElementUsedForTimeStampFilter(ICollection<RealyticsKPIDataElement> DataElements)
        {
            var DataElementWithTimestampFilter = DataElements.FirstOrDefault(d => d.UsedForTimeStampFilter == true);
            if (DataElementWithTimestampFilter == null) throw new InvalidProgramException("Fatal Error; No DateTime Field Found in KPI Data Elements!!!");
            return DataElementWithTimestampFilter;
        }
        /// <summary>
        /// Generate query for all KPI data elements with formula
        /// </summary>
        /// <param name="DataElements"></param>
        /// <returns></returns>
        private async Task<string> GenerateQueryFromDataElementsAsync(ICollection<RealyticsKPIDataElement> DataElements)
        {
            string aliasNameOne = string.Empty;
            string aliasNameTwo = string.Empty;
            String StoreProcedureDataQuery = string.Empty;
            int KpiId = 0;
            foreach (RealyticsKPIDataElement dataElement in DataElements)
            {
                aliasNameOne = this.DataSourceToAliasNamesMapper[dataElement.CustomerDataElementIdentifierOne];
                if (KpiId == 0)
                    KpiId = dataElement.FK_KpiId;

                if (dataElement.FormulaToBeApplied == EnumerationKPIFormulas.None)
                {
                    StoreProcedureDataQuery += string.Format("{0}.{1}, ", aliasNameOne, GetQuery(dataElement));
                }
                else
                {
                    if (dataElement.UsedForTimeStampFilter) throw new ArgumentException();

                    switch (dataElement.FormulaToBeApplied)
                    {
                        case EnumerationKPIFormulas.Count:
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Count(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement)));
                            break;
                        case EnumerationKPIFormulas.Average:
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Average(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement)));
                            break;
                        case EnumerationKPIFormulas.Cumulative:
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Cumulative(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement)));
                            break;
                        case EnumerationKPIFormulas.Differentiation:
                            aliasNameTwo = this.DataSourceToAliasNamesMapper[dataElement.CustomerDataElementIdentifierTwo];
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Differentiation(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement), aliasNameTwo, GetQuery(dataElement, 1)));
                            break;
                        case EnumerationKPIFormulas.Percentage:
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Percentage(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement)));
                            break;
                        case EnumerationKPIFormulas.Summation:
                            aliasNameTwo = this.DataSourceToAliasNamesMapper[dataElement.CustomerDataElementIdentifierTwo];
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Summation(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement), aliasNameTwo, GetQuery(dataElement, 1)));
                            break;
                        case EnumerationKPIFormulas.Multiplication:
                            aliasNameTwo = this.DataSourceToAliasNamesMapper[dataElement.CustomerDataElementIdentifierTwo];
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Multiplication(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement), aliasNameTwo, GetQuery(dataElement, 1)));
                            break;
                        case EnumerationKPIFormulas.Division:
                            aliasNameTwo = this.DataSourceToAliasNamesMapper[dataElement.CustomerDataElementIdentifierTwo];
                            StoreProcedureDataQuery += string.Format("{0}, ", RealyticsKPIFormula.Division(dataElement.FormulaToBeApplied, aliasNameOne, GetQuery(dataElement), aliasNameTwo, GetQuery(dataElement, 1)));
                            break;
                    }
                }
            }
            //Remove last ", "
            try
            {
                StoreProcedureDataQuery = StoreProcedureDataQuery.Remove(StoreProcedureDataQuery.Length - 2);
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (KpiId != 0 )
                    await this.CleanupRecordInStoredProcedureAliasNameProviderTable(KpiId);
                throw e;
            }
            return StoreProcedureDataQuery;
        }
        /// <summary>
        /// Get the data source name (table view name) from import operations; based on customer data element identifier
        /// </summary>
        /// <param name="CustomerDataElementIdentifier"></param>
        /// <returns></returns>
        private string GetTableViewName(int CustomerDataElementIdentifier)
        {
            var OperationDetail = (from AttributeInDB in RealitycsCustomerDataAttributeRepository.Table
                                   join OperationDetailInDB in RealitycsDataImportOperationDetailsRepository.Table
                                   on AttributeInDB.OperationId equals OperationDetailInDB.OperationId
                                   where AttributeInDB.PK_Id == CustomerDataElementIdentifier
                                   select new
                                   {
                                       ViewName = OperationDetailInDB.ViewName
                                   }).FirstOrDefault();

            if (OperationDetail == null)
                throw new InvalidOperationException(string.Format("Inside KPIStoredProcedureHandler.GetTableViewName; Operration details not found against the Customer Data Identifier {0}", CustomerDataElementIdentifier));

            return OperationDetail.ViewName;
        }

        /// <summary>
        /// Return the Data source elements in [domain].[table].[column/Field] for KPI DataElements
        /// </summary>
        /// <param 
        /// name="ElementIndex">
        /// can take value 0-1; anything out of range will throw ArgumentOutOfRangeException
        /// </param>
        /// <returns>
        /// return the data source element to construct a SQL query
        /// </returns>
        private string GetQuery(RealyticsKPIDataElement dataElement, int ElementIndex = 0)
        {
            if (ElementIndex > 1 || ElementIndex < 0) throw new ArgumentOutOfRangeException("KPI DataElement Index Has To Be Either Zero or One!!");
            string query = string.Empty;
            try
            {
                if (ElementIndex == 0)
                {
                    if (dataElement.CustomerDataAttributeOne.Length != 0)
                        query += String.Format("{0}", dataElement.CustomerDataAttributeOne.Trim());
                }
                else if (ElementIndex == 1)
                {

                    if (dataElement.CustomerDataAttributeTwo.Length != 0)
                        query += String.Format("{0}", dataElement.CustomerDataAttributeTwo.Trim());
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            return query;
        }
        /// <summary>
        /// Return the Data source elements as attribute name for KPI Drilldown DataElements
        /// </summary>
        /// <param 
        /// name="drilldownDataElement">
        /// </param>
        /// <returns>
        /// return the data source element to construct a SQL query
        /// </returns>
        /// 
        public string GetQuery(RealyticsKPIDrilldownElement drilldownDataElement)
        {
            string query = string.Empty;

            if (drilldownDataElement.CustomerDataAttribute.Length != 0)
                query += String.Format("{0}", drilldownDataElement.CustomerDataAttribute.Trim());

            return query;
        }
        /*
                     try
                    {
                        //prepare input parameters
                        var pLegalEntityId = dataProvider.GetInt32Parameter("LegalEntityId", workContext.CurrentCustomer.FK_LegalEntityId);
                        var pSearch = dataProvider.GetStringParameter("Search", null);
                        var pPageIndex = dataProvider.GetInt32Parameter("PageIndex", 1);
                        var pPageSize = dataProvider.GetInt32Parameter("pageSize", 100);
                        var pSortColumn = dataProvider.GetStringParameter("SortColumn", null);
                        var pSortOrder = dataProvider.GetStringParameter("SortOrder", null);

                        //invoke stored procedure
                        var spResult = clientContext.QueryFromSql<ViewUserList>("client.usp_AllUsers",
                                pLegalEntityId, pSearch, pPageIndex, pPageSize, pSortColumn, pSortOrder).ToList();

                        var result = _mapper.Map<List<ClientUserDTO>>(spResult);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kpiOldName"></param>
        /// <param name="kpiNewName"></param>
        /// <returns></returns>
        public async Task<bool> RenameStoredProcedureOfKPI(string kpiOldName, string kpiNewName)
        {
            try
            {
                //prepare parameters (input/output) 
                var objName = dataProvider.GetStringParameter("objname", string.Format("[kpi].[usp_GetRuntimeData_ForKPI_{0}]", kpiOldName));
                var newName = dataProvider.GetStringParameter("newname", string.Format("usp_GetRuntimeData_ForKPI_{0}", kpiNewName));
                var objectType = dataProvider.GetStringParameter("objtype", null);

                RealitycsKPIContext.ExecuteNonQuery("sys.sp_rename", objName, newName, objectType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        private async Task<string> GetAliasNameAsync(int KpiId, int CustomerDataSourceIdentifier)
        {
            if (DataSourceToAliasNamesMapper.ContainsKey(CustomerDataSourceIdentifier))
            {
                return DataSourceToAliasNamesMapper[CustomerDataSourceIdentifier]; ;
            }
            string DataSourceName = this.GetTableViewName(CustomerDataSourceIdentifier);

            var record = await this.KPIStoredProcedureAliasNameProviderRepository.FindAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                && x.FK_KpiId == KpiId
                && x.CustomerDataElementIdentifier == CustomerDataSourceIdentifier
                && x.DataSourceName == DataSourceName);

            if (record == null) 
                throw new ArgumentException(
                    string.Format("Inside GetAliasName; No Records found in RealitycsKPIStoredProcedureAliasNameProvider with KpiId {0}, Legal Entity Id {1}, CustomerDataIdentifier {2} and Data source Name {3}",
                    KpiId,
                    workContext.CurrentCustomer.FK_LegalEntityId,
                    CustomerDataSourceIdentifier,
                    DataSourceName));

            DataSourceToAliasNamesMapper.Add(CustomerDataSourceIdentifier, record.AliasNameAssigned);
            return record.AliasNameAssigned;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private async Task<string> GetCardParametersAsync(RealyticsGraphicalCard card)
        {
            string Parameters = string.Format("{0}, ", card.ReferenceAxisAttribute);

            if (card.DataPlotAxisDataAttributes.Count <= 0)
            {
                //Remove last ", "
                try
                {
                    Parameters = Parameters.Remove(Parameters.Length - 2);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw e;
                }
                return Parameters;
            }

            foreach (var CardAttribute in card.DataPlotAxisDataAttributes)
            {
                Parameters += string.Format("{0}, ", CardAttribute.DataPlotterAxisAttribute);
            }

            //Remove last ", "
            try
            {
                Parameters = Parameters.Remove(Parameters.Length - 2);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }

            return Parameters;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IDictionary<string, object> GetCardGraphDTO(EnumerationSupportedGraphType type, IDictionary<string, CardCummulativeData> parameters)
        {
            IDictionary<string, object> dynamicDTO = new Dictionary<string, object>();
            if (type == EnumerationSupportedGraphType.PieChart
                || type == EnumerationSupportedGraphType.DonutChart
                || type == EnumerationSupportedGraphType.ColumnChart)
            {
                foreach (var param in parameters)
                {
                    dynamicDTO.Add(new KeyValuePair<string, object>(param.Value.SectionName, param.Value.SectionValue));
                    dynamicDTO.Add(new KeyValuePair<string, object>(param.Value.ValueName, param.Value.Value));
                }
            }
            return dynamicDTO;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IDictionary<string, object> GetCardGraphDTO(EnumerationSupportedGraphType type, KeyValuePair<string, KeyValuePair<Type, object>>[] parameters)
        {
            IDictionary<string, object> dynamicDTO = new Dictionary<string, object>();
            if (type == EnumerationSupportedGraphType.LineChart)
            {
                dynamicDTO.Add(new KeyValuePair<string, object>(parameters[0].Key, parameters[0].Value.Value.ToString().Trim()));
                dynamicDTO.Add(new KeyValuePair<string, object>(parameters[1].Key, parameters[1].Value.Value));
            } 
            else if (type == EnumerationSupportedGraphType.BarChart)
            {
                for (int p = 0; p < parameters.Length; p++)
                {
                    dynamicDTO.Add(new KeyValuePair<string, object>(parameters[p].Key, parameters[p].Value.Value));
                }
            }
            return dynamicDTO;
        }
        private object ConvertToNewTypeAndAdd(object currentValue, object objValueToAdd, Type newType)
        {
            if (newType == typeof(int))
            {
                return (Convert.ToInt32(currentValue) + (int)objValueToAdd);
            }
            else if (newType == typeof(double))
            {
                return Convert.ToDouble(currentValue) + ((double)objValueToAdd);
            }
            else if (newType == typeof(long))
            {
                return Convert.ToInt64(currentValue) + (long)objValueToAdd;
            }
            else if (newType == typeof(float))
            {
                return Convert.ToSingle(currentValue) + (float)objValueToAdd;
            }
            else if (newType == typeof(uint))
            {
                return Convert.ToUInt32(currentValue) + (uint)objValueToAdd;
            }
            else if (newType == typeof(ulong))
            {
                return Convert.ToUInt64(currentValue) + (ulong)objValueToAdd;
            }
            else if (newType == typeof(short))
            {
                return Convert.ToUInt16(currentValue) + (short)objValueToAdd;
            }
            throw new InvalidOperationException(string.Format("Inside ConvertToNewType; Unsuported type {0}", newType));
        }
        private object AddToNumber(object currentValue, object objValueToAdd, Type type)
        {
            if (type == typeof(int))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (int)currentValue + (int)valueToAdd;
            }
            else if (type == typeof(double))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (double)currentValue + (double)valueToAdd;
            }
            else if (type == typeof(long))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (long)currentValue + (long)valueToAdd;
            }
            else if (type == typeof(float))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (float)currentValue + (float)valueToAdd;
            }
            else if (type == typeof(uint))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (uint)currentValue + (uint)valueToAdd;
            }
            else if (type == typeof(ulong))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (ulong)currentValue + (ulong)valueToAdd;
            }
            else if (type == typeof(short))
            {
                Type typeToAdd;
                object valueToAdd = ConvertToNumber(objValueToAdd, out typeToAdd);
                if (type != typeToAdd)
                    return ConvertToNewTypeAndAdd(currentValue, valueToAdd, typeToAdd);
                return (short)currentValue + (short)valueToAdd;
            }
            throw new InvalidOperationException(string.Format("Inside AddToNumber; Unsuported type {0}", type));
        }
        private object ConvertToNumber(object value, out Type type)
        {
            string trimmedValue = value.ToString().Trim();
            object number;

            //try to parse first. what is most likely
            try
            {
                number = int.Parse(trimmedValue);
                type = typeof(int);
            }
            catch (FormatException ei )
            {
                try
                {
                    number = double.Parse(trimmedValue);
                    type = typeof(double);
                }
                catch (FormatException ed)
                {
                    try
                    {
                        number = float.Parse(trimmedValue);
                        type = typeof(float);
                    }
                    catch(FormatException ef)
                    {
                        try
                        {
                            number = long.Parse(trimmedValue);
                            type = typeof(long);
                        }
                        catch(FormatException el)
                        {
                            try
                            {
                                number = short.Parse(trimmedValue);
                                type = typeof(short);
                            }
                            catch(FormatException es)
                            {
                                try
                                {
                                    number = ulong.Parse(trimmedValue);
                                    type = typeof(ulong);
                                }
                                catch(FormatException eu)
                                {
                                    try
                                    {
                                        number = uint.Parse(trimmedValue);
                                        type = typeof(uint);
                                    }catch(FormatException e)
                                    {
                                        throw e;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return number;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNumericType(Object value)
        {
            string trimmedValue = value.ToString().Trim();

            //try to parse first. what is most likely
            bool success = int.TryParse(trimmedValue, out _);
            if (success == false)
            {
                success = double.TryParse(trimmedValue, out _);

                if (success == false)
                {
                    success = float.TryParse(trimmedValue, out _);

                    if (success == false)
                    {
                        success = long.TryParse(trimmedValue, out _);
                        if (success == false)
                        {
                            success = short.TryParse(trimmedValue, out _);

                            if (success == false)
                            {
                                success = uint.TryParse(trimmedValue, out _);

                                if (success == false)
                                {
                                    success = ulong.TryParse(trimmedValue, out _);
                                }
                            }
                        }
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsStringType(Object value)
        {
            string trimmedValue = value.ToString().Trim();
            if (IsNumericType(trimmedValue))
                return false;
            return (trimmedValue.GetType() == typeof(string));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public async Task<ManageRealitycsFormattedCardDataInVisualisationDTO> FetchCardFormattedData(RealyticsGraphicalCard card)
        {
            RealyticsKPI KPI = await RealyticsKPIRepository.FindAsync(k => k.PK_Id == card.KpiId);
            string TimeStampField = await this.GetReferenceFieldNameUsedForTimeStampFilter(KPI.DataElements, false);
            string[] parameterNames = new string[] { "From", "To", "FilterCondition", "CardParams" };

            string query = string.Format("[kpi].[usp_GetRuntimeData_ForKPI_{0}] @{1}, @{2}, @{3}, @{4}",
                KPI.Name,
                parameterNames[0],
                parameterNames[1],
                parameterNames[2],
                parameterNames[3]);

            var paramFrom = dataProvider.GetStringParameter(parameterNames[0], "Default");
            var paramTo = dataProvider.GetStringParameter(parameterNames[1], System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"));
            var paramFilterCondition = dataProvider.GetStringParameter(parameterNames[2], null);
            var paramCardParams = dataProvider.GetStringParameter(parameterNames[3], await GetCardParametersAsync(card));

            try
            {
                List<RealitycsKPIStoredProcedureResponse> resultSet = RealitycsKPIContext.ExecuteReader(query,
                    paramFrom,
                    paramTo,
                    paramFilterCondition,
                    paramCardParams);


                ManageRealitycsFormattedCardDataInVisualisationDTO ResultSetDTO = new ManageRealitycsFormattedCardDataInVisualisationDTO();
                ResultSetDTO.FormattedRecord = new List<IDictionary<string, object>>();

                if (card.SelectedGraphType == EnumerationSupportedGraphType.LineChart)
                {
                    //For timeline graphs each record will form a data point in the graph
                    foreach (RealitycsKPIStoredProcedureResponse record in resultSet)
                    {
                        var fieldTimeStamp = record.Fields.FirstOrDefault(x => x.Key.Equals(TimeStampField));

                        foreach (var field in record.Fields)
                        {
                            if (field.Key.Equals(TimeStampField))
                                continue;

                            //To Validate if it is a numberic field; otherwise throw exception
                            if (IsNumericType(field.Value.Value)) continue;//throw new InvalidOperationException("Inside FetchCardFormattedData; Object Type Not Numeric!!");

                            KeyValuePair<string, KeyValuePair<Type, object>>[] parameters = new KeyValuePair<string, KeyValuePair<Type, object>>[2];
                            parameters[0] = fieldTimeStamp;
                            parameters[1] = field;
                            ResultSetDTO.FormattedRecord.Add(GetCardGraphDTO(card.SelectedGraphType, parameters));
                        }
                    }
                } 
                else if (card.SelectedGraphType == EnumerationSupportedGraphType.BarChart)
                {
                    foreach (RealitycsKPIStoredProcedureResponse record in resultSet)
                    {
                        KeyValuePair<string, KeyValuePair<Type, object>>[] parameters = new KeyValuePair<string, KeyValuePair<Type, object>>[(record.Fields.Count - 1)];
                        int FieldIndex = 0;

                        foreach (var field in record.Fields)
                        {
                            if (field.Key.Equals(TimeStampField))
                                continue;

                            //To Validate if it is a numberic field; otherwise throw exception
                            if (IsNumericType(field.Value.Value)) continue; // throw new InvalidOperationException("Inside FetchCardFormattedData; Object Type Not Numeric!!");

                            parameters[FieldIndex++] = field;
                        }
                        ResultSetDTO.FormattedRecord.Add(GetCardGraphDTO(card.SelectedGraphType, parameters));
                    }

                }
                else if (card.SelectedGraphType == EnumerationSupportedGraphType.PieChart
                || card.SelectedGraphType == EnumerationSupportedGraphType.DonutChart
                || card.SelectedGraphType == EnumerationSupportedGraphType.ColumnChart)
                {
                    //For donut/pie/column chart the records will have to make cummulative based on field name
                    IDictionary<string, CardCummulativeData> cardCummulativeDataDictionary = new Dictionary<string, CardCummulativeData>();
                    CardCummulativeData cardCummulativeData = null;

                    ManageRealitycsFormattedCardDataInVisualisationDTO returnRecord = new ManageRealitycsFormattedCardDataInVisualisationDTO();

                    foreach (RealitycsKPIStoredProcedureResponse record in resultSet)
                    {
                        //Only one Timestamp field in the current record
                        var fieldTimeStamp = record.Fields.FirstOrDefault(x => x.Key.Equals(TimeStampField));
                        //Only one string type field in the current record
                        var SelectionGrouping = record.Fields.FirstOrDefault(x => !IsNumericType(x.Value.Value)
                                                    && x.Key != fieldTimeStamp.Key);

                        //The data should support at least one selection grouping field
                        if (SelectionGrouping.Key == string.Empty)
                        {
                            break; //unable to plot
                            //throw new InvalidOperationException(string.Format("Inside FetchCardFormattedData; for {0} graph type, no Section Field of string type Found!!", card.SelectedGraphType.ToString()));
                        }
                        //string currentRecordSectionValue = string.Empty;

                        foreach (var field in record.Fields)
                        {
                            if (field.Key.Equals(TimeStampField))//skip timestamp field for data collection
                                continue;

                            bool bNumericField = IsNumericType(field.Value.Value);
                            bool bStringField = IsStringType(field.Value.Value);
                            //To Validate if it is a numberic field; otherwise throw exception
                            if (!bNumericField && !bStringField) continue;//throw new InvalidOperationException("Inside FetchCardFormattedData; Object Type Not Numeric and Not String type!!");

                            //Section Value Field
                            if (bStringField) 
                            {
                                if (field.Key != SelectionGrouping.Key) throw new InvalidOperationException(string.Format("Inside FetchCardFormattedData; more than one string type field exists {0}, the expected only field is {1}", field.Key, SelectionGrouping.Key)); 

                                if (!cardCummulativeDataDictionary.ContainsKey((string)field.Value.Value))//In case section field is NOT encountered (first record) 
                                {
                                    cardCummulativeData = new CardCummulativeData();
                                    cardCummulativeData.SectionName = field.Key;
                                    cardCummulativeData.SectionValue = (string)field.Value.Value;
                                    cardCummulativeDataDictionary.Add((string)field.Value.Value, cardCummulativeData);
                                }
                                else //Section Field is already encountered (from the second record)
                                {
                                    cardCummulativeData = cardCummulativeDataDictionary[(string)field.Value.Value];
                                    ValidateCardCummulativeDataObject(cardCummulativeData, field);
                                }
                            } 
                            else if (bNumericField)//value field
                            {
                                if (!cardCummulativeDataDictionary.ContainsKey((string)SelectionGrouping.Value.Value))
                                {
                                    cardCummulativeData = new CardCummulativeData();
                                    cardCummulativeData.SectionName = SelectionGrouping.Key;
                                    cardCummulativeData.SectionValue = (string)SelectionGrouping.Value.Value;
                                    cardCummulativeData.ValueName = field.Key;
                                    Type type;
                                    cardCummulativeData.Value = ConvertToNumber(field.Value.Value, out type);
                                    cardCummulativeData.ValueType = type;
                                    cardCummulativeDataDictionary.Add((string)SelectionGrouping.Value.Value, cardCummulativeData);
                                }
                                else
                                {
                                    cardCummulativeData = cardCummulativeDataDictionary[(string)SelectionGrouping.Value.Value];
                                    ValidateCardCummulativeDataObject(cardCummulativeData, SelectionGrouping, field);
                                    cardCummulativeData.Value = AddToNumber(cardCummulativeData.Value, field.Value.Value, cardCummulativeData.ValueType);
                                }
                            }
                        }
                    }
                    ResultSetDTO.FormattedRecord.Add(GetCardGraphDTO(card.SelectedGraphType, cardCummulativeDataDictionary));
                }
                return ResultSetDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="groupField"></param>
        /// <returns></returns>
        private bool ValidateCardCummulativeDataObject(CardCummulativeData Data, KeyValuePair<string, KeyValuePair<Type, object>> groupField)
        {
            if (Data.SectionName != groupField.Key) throw new InvalidOperationException(string.Format("Field name in data [{0}]; do not match with group field [{1}]", 
                Data.SectionName,
                groupField.Key));

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="groupField"></param>
        /// <param name="valueField"></param>
        /// <returns></returns>
        private bool ValidateCardCummulativeDataObject(CardCummulativeData Data, KeyValuePair<string, KeyValuePair<Type, object>> groupField, KeyValuePair<string, KeyValuePair<Type, object>> valueField)
        {
            bool ReturnValue = ValidateCardCummulativeDataObject(Data, groupField);
            
            if (Data.ValueName != valueField.Key) throw new InvalidOperationException(string.Format("Field name in data [{0}]; do not match with value field [{1}]",
                Data.ValueName,
                valueField.Key));

            return ReturnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public async Task<List<ManageRealitycsCardDataInVisualisationDTO>> FetchCardRawData(RealyticsGraphicalCard card)
        {
            RealyticsKPI KPI = await RealyticsKPIRepository.FindAsync(k => k.PK_Id == card.KpiId);
            string[] parameterNames = new string[]{ "From", "To", "FilterCondition", "CardParams" };

            string query = string.Format("[kpi].[usp_GetRuntimeData_ForKPI_{0}] @{1}, @{2}, @{3}, @{4}", 
                KPI.Name,
                parameterNames[0],
                parameterNames[1],
                parameterNames[2],
                parameterNames[3]);

            var paramFrom = dataProvider.GetStringParameter(parameterNames[0], "Default");
            var paramTo = dataProvider.GetStringParameter(parameterNames[1], System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"));
            var paramFilterCondition = dataProvider.GetStringParameter(parameterNames[2], null);
            var paramCardParams = dataProvider.GetStringParameter(parameterNames[3], await GetCardParametersAsync(card));

            try
            {
                List<RealitycsKPIStoredProcedureResponse> resultSet = RealitycsKPIContext.ExecuteReader(query, 
                    paramFrom, 
                    paramTo, 
                    paramFilterCondition, 
                    paramCardParams);

                List<ManageRealitycsCardDataInVisualisationDTO> ResultSetDTO = new List<ManageRealitycsCardDataInVisualisationDTO>();
                foreach(RealitycsKPIStoredProcedureResponse record in resultSet)
                {
                    ManageRealitycsCardDataInVisualisationDTO recordDTO = new ManageRealitycsCardDataInVisualisationDTO();
                    recordDTO.RawRecord = new List<ManagerRealitycsCardDataFieldInVisualisationDTO>();
                    ResultSetDTO.Add(recordDTO);
                    foreach(var field in record.Fields)
                    {
                        recordDTO.RawRecord.Add(new ManagerRealitycsCardDataFieldInVisualisationDTO()
                        {
                            FieldName = field.Key,
                            FieldValue = field.Value.Value
                        });
                    }
                }
                return ResultSetDTO;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kpiName"></param>
        /// <returns></returns>
        public async Task<bool> IsStoreProcedureAlreadyExistsForKPI(string kpiName)
        {
            bool Result = false;
            try
            {
                //prepare parameters (input/output) 

                var paramSPName = dataProvider.GetStringParameter("sp_name", string.Format("usp_GetRuntimeData_ForKPI_{0}", kpiName));
                var paramReturn = dataProvider.GetOutputInt32Parameter("return");
                
                RealitycsKPIContext.ExecuteNonQuery("kpi.usp_IsStoredProcedureExists", paramSPName, paramReturn);

                Result = ((int)paramReturn.Value > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kpiName"></param>
        /// <returns></returns>
        public async Task<bool> DropStoredProcedureOfKPI(string kpiName)
        {
            /*
             IF OBJECT_ID('graphical.usp_VisulisationNavigationDashboards','P') IS NOT NULL
               DROP PROCEDURE [graphical].[usp_VisulisationNavigationDashboards]
            GO
             */
            string sql = string.Empty;
            sql = string.Format("IF OBJECT_ID('graphical.{0}','P') IS NOT NULL{1}", kpiName, Environment.NewLine);
            sql += string.Format("  DROP PROCEDURE [graphical].[{0}]{1}", kpiName, Environment.NewLine);
            sql += "GO" + Environment.NewLine;
            RealitycsDbContextExtensions.ExecuteSqlScript(RealitycsKPIContext, sql);
            return true;
        }
    }
}
