
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Piyush Garg
-- Created date:  28th Oct 2020
-- Description:   Get all Users
-- Updated date:  28th Oct 2020
-- =============================================

-- Drop stored procedure if it already exists
IF OBJECT_ID('client.usp_AllUsers','P') IS NOT NULL
   DROP PROCEDURE client.usp_AllUsers
GO

--EXEC usp_AllUsers 

CREATE  PROCEDURE [client].[usp_AllUsers]
(
    /* Optional Filters for Dynamic Search*/
    @LegalEntityId INT,
    @Search NVARCHAR(200) = NULL,
    /*� Pagination Parameters */
    @PageIndex INT = 1,
    @PageSize INT = 10,
    /*� Sorting Parameters */
    @SortColumn NVARCHAR(200) = 'CreatedDate',
    @SortOrder NVARCHAR(4)='ASC'
)
AS
BEGIN

    BEGIN TRY
        /*�Declaring Local Variables corresponding to parameters for modification */
        DECLARE 
        @lSearch NVARCHAR(200),
        @lPageNbr INT,
        @lPageSize INT,
        @lSortCol NVARCHAR(200),
        @lFirstRec INT,
        @lLastRec INT,
        @lTotalRows INT
        /*Setting Local Variables*/
        SET @lSearch = @Search
        SET @lPageNbr = @PageIndex
        SET @lPageSize = @PageSize
        SET @lSortCol = LTRIM(RTRIM(@SortColumn))
        SET @lFirstRec =( @lPageNbr - 1 ) * @lPageSize
        SET @lLastRec = ( @lPageNbr * @lPageSize + 1 )
        SET @lTotalRows = @lLastRec - @lFirstRec  + 1 
        print @lFirstRec
        PRINT @lLastRec
        PRINT @lTotalRows
		
		;WITH CTE_RESULTS AS (
			SELECT 
			ROW_NUMBER() OVER 
               (ORDER BY               
                 U.CreatedDate DESC
              ) AS ROWNUM,
            COUNT(*) OVER () AS TotalRecords,
			U.PK_Id AS Id,
            U.UserName,
            U.EmailId,
            U.FK_LegalEntityId AS LegalEntityId,
            U.FK_EmployeeId AS EmplooyeId,
            U.IsActive,
            AG.PK_Id AS AccessGroupId,
            AG.Name AS AccessGroupName
			FROM client.[User] AS U
			INNER JOIN global.MasterAccessGroup as AG ON U.FK_AccessGroupId=AG.PK_Id
			WHERE U.FK_LegalEntityId=@LegalEntityId
		)

		SELECT * FROM CTE_RESULTS WHERE
		ROWNUM>@lFirstRec AND ROWNUM < @lLastRec

        END TRY
        BEGIN CATCH
            INSERT INTO global.RealitycsDBErrors
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