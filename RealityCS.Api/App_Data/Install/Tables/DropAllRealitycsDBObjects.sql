﻿DROP table if exists [client].[data.RealitycsAttributeBooleanValue]
DROP table if exists [client].[data.RealitycsAttributeIntValue]
DROP table if exists [client].[data.RealitycsAttributeTextValue]
DROP table if exists [client].[data.RealitycsAttributeDateTimeValue]
DROP table if exists [client].[data.RealitycsOperationDetail]
DROP table if exists [client].[data.RealitycsAttributeMetaData]
DROP table if exists [client].[data.RealitycsAttribute]
DROP table if exists [client].[data.RealitycsEntity]
DROP table if exists [client].[kpi.entity]
DROP table if exists [client].[RealitycsUserPassword]
DROP table if exists [client].[User]
DROP table if exists [client].[LegalEntity]
DROP table if exists [client].[Tbl_Graphical_RealitycsEntity]
DROP table if exists [client].[tbl_Master_ClientInformation]
DROP table if exists [global].[AccessGroupAndOperationRelation]
DROP table if exists [global].[MasterAccessOperation]
DROP table if exists [global].[MasterAccessGroup]
DROP table if exists [global].[MasterDepartment]
DROP table if exists [global].[MasterDivision]
DROP table if exists [global].[MasterEntity]
DROP table if exists [global].[MasterEntityGroup]
DROP table if exists [global].[MasterDomain]
DROP table if exists [global].[MasterIndustry]
DROP table if exists [global].[RealitycsDBError]
DROP table if exists [global].[RealitycsLog]
DROP table if exists [global].[MasterCity]
DROP table if exists [global].[MasterState]
DROP table if exists [global].[MasterCountry]
DROP table if exists [enum].[LogLevelType]
DROP table if exists [enum].[RealitycsGraphicalAxisType]
DROP table if exists [enum].[RealitycsKPIDataElementFormulaType]
DROP table if exists [enum].[RealitycsKPIDataInformationType]
DROP table if exists [enum].[RealitycsKPIDrilldownLevelsType]
DROP table if exists [enum].[RealitycsSupportedGraphType]
DROP table if exists [kpi].[RealitycsKPIJoiningRelation]
DROP table if exists [kpi].[RealyticsKPIRiskRegister]
DROP table if exists [kpi].[RealyticsKPIDrilldownElement]
DROP table if exists [kpi].[RealyticsKPIDataElement]
DROP table if exists [kpi].[RealyticsKPI]
DROP table if exists [kpi].[RealyticsKPIValueStream]
DROP table if exists [graphical].[RealyticsGraphicalDashboardTemplate]
DROP table if exists [graphical].[RealyticsGraphicalCardDataPlotterAxisAttribute]
DROP table if exists [graphical].[RealyticsGraphicalCard]
DROP table if exists [graphical].[RealyticsGraphicalDashboard]
DROP procedure if exists [client].[usp_AllUsers]
DROP procedure if exists [client].[usp_EAVToNormalisedTable]
DROP procedure if exists [graphical].[usp_VisulisationNavigationDashboards]
DROP procedure if exists [kpi].[usp_IsStoredProcedureExists]
GO