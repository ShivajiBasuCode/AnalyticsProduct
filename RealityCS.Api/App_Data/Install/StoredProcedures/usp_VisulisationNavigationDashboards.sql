GO
/****** Object:  StoredProcedure [dbo].[usp_VisulisationNavigationDashboards]    Script Date: 12/7/2020 11:20:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Shivaji Basu
-- Created date:  07th Nov 2020
-- Description:   Visualization Navigation Toolbar content
-- Updated date:  07th Nov 2020
-- =============================================
IF OBJECT_ID('graphical.usp_VisulisationNavigationDashboards','P') IS NOT NULL
   DROP PROCEDURE [graphical].[usp_VisulisationNavigationDashboards]
GO
CREATE PROCEDURE [graphical].[usp_VisulisationNavigationDashboards]
(
	    @LegalEntityId INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
BEGIN TRY
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT vs.PK_Id as ValuestreamId, vs.Name as ValuestreamName, vs.Description as ValuestreamDescription,
	db.PK_Id as DashboardId, db.Name as DashboardName, db.Description as DashboardDescription
	FROM graphical.[RealyticsGraphicalDashboard] as db INNER JOIN kpi.[RealyticsKPIValueStream] as vs
	ON db.ValueStreamId = vs.PK_Id
	where db.FK_LegalEntityId = @LegalEntityId AND vs.FK_LegalEntityId = @LegalEntityId
	Order by ValuestreamId
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
