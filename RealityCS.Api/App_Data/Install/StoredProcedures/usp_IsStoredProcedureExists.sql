SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Shivaji Basu
-- Created date:  28th Oct 2020
-- Description:   Check if a stored procedure exists
-- Updated date:  28th Oct 2020
-- =============================================

/****** Object:  StoredProcedure [dbo].[usp_IsStoredProcedureExists]    Script Date: 10/24/2020 1:20:39 PM ******/
DROP PROCEDURE IF EXISTS [kpi].[usp_IsStoredProcedureExists]
GO

CREATE procedure [kpi].[usp_IsStoredProcedureExists]
@sp_name varchar(300),
@Return int output
as
BEGIN
BEGIN TRY
if exists (select * from sys.objects  where type = 'p'  AND name = @sp_name)
	set @Return = 1
else
	set @Return = 0
return @Return
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