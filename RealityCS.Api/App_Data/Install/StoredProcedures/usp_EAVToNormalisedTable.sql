
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
--Execute Query Example:
--USE [Realitycs_piyush]
--GO

--DECLARE	@return_value int

--EXEC	@return_value = [client].[usp_EAVToNormalisedTable]
--		@LegalEntityId = 1,
--		@OperationId = N'BC79AF11-FD3E-40C7-ADFD-81168D81CC74',
--		@DatabaseName = NULL

--SELECT	'Return Value' = @return_value

--GO


--
IF OBJECT_ID('client.usp_EAVToNormalisedTable','P') IS NOT NULL
   DROP PROCEDURE client.usp_EAVToNormalisedTable
GO

--EXEC usp_AllUsers 

CREATE  PROCEDURE [client].[usp_EAVToNormalisedTable]
(
    /* Optional Filters for Dynamic Search*/
    @LegalEntityId INT,
	/*Comma Seperated*/
    @OperationId NVARCHAR(MAX),
    /*� Pagination Parameters */
    @DatabaseName NVARCHAR(MAX)
	--@EntityId INT
)
AS
BEGIN

    BEGIN TRY
        /*�Declaring Local Variables corresponding to parameters for modification */
       
		 DECLARE @DataSourceName AS NVARCHAR(MAX),@AttributeEntityId AS INT,@DataSourceEntityId AS INT,@ParentId INT
		 ,@cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX)

		 --Get Any Operation Parent Id
		 SELECT @ParentId=ParentId from client.[data.RealitycsAttribute] where OperationId=(SELECT TOP 1 value from string_split(@OperationId,','))

		 -- Get Data Source Name
		 SELECT @DataSourceName=V.Value,@DataSourceEntityId=A.FK_EntityId from client.[data.RealitycsAttribute] AS A
		 INNER JOIN client.[data.RealitycsAttributeTextValue] AS V ON A.PK_Id=V.FK_AttributeId
		 where A.PK_Id=@ParentId

		DECLARE @OperationValues NVARCHAR(200)

		IF CURSOR_STATUS('global','cursor_operation')>=-1
		BEGIN
			DEALLOCATE cursor_operation
		END

		DECLARE cursor_operation CURSOR
		FOR SELECT value FROM string_split(@OperationId,',')
		
		OPEN cursor_operation;

		FETCH NEXT FROM cursor_operation INTO @OperationValues;

		WHILE @@FETCH_STATUS = 0
		BEGIN
		select @cols = STUFF((SELECT ',' +  QUOTENAME(AttributeName) 
								from client.[data.RealitycsAttribute] 
								where OperationId = @OperationValues AND FK_EntityId != @DataSourceEntityId
								group by AttributeName
							FOR XML PATH(''), TYPE
						).value('.', 'NVARCHAR(MAX)') 
						,1,1,'')
		set @query = N'SELECT * from 
				 (
      
					SELECT A.AttributeName AS ColumnName,T.Value AS Value,T.RowNum FROM client.[data.RealitycsAttribute] AS A
					INNER JOIN client.[data.RealitycsAttributeTextValue] AS T ON A.PK_Id=T.FK_AttributeId
					Where A.OperationId = ''@OperationValues'' AND A.FK_EntityId != @DataSourceEntityId
					GROUP BY T.RowNum,A.AttributeName,T.Value
				) x
				pivot 
				(
					max(Value)
					for ColumnName in (' + @cols + N') 
				) p 
				ORDER BY RowNum';
		SET @query= REPLACE(@query,'@OperationValues',@OperationValues);
		SET @query= REPLACE(@query,'@DataSourceEntityId',@DataSourceEntityId);
		exec sp_executesql @query;
		--Create and Insert
		DECLARE @TableName NVARCHAR(MAX);
		DECLARE @colsData NVARCHAR(MAX);
		DECLARE @tableQuery NVARCHAR(MAX);
		--ATA20182018_SALES_
		SET @TableName= REPLACE(@OperationValues, '-', '');

		select @colsData = STUFF((SELECT ',' +  QUOTENAME(D.AttributeName) +'  '+F.DataType
                    from client.[data.RealitycsAttribute] AS D
					INNER JOIN client.[data.RealitycsAttributeMetaData] AS F on D.PK_Id=FK_AttributeId 
					where D.OperationId = @OperationValues AND D.FK_EntityId != @DataSourceEntityId
                    group by D.AttributeName,F.DataType
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')


		--Create table 
		set @tableQuery=N'DROP TABLE IF EXISTS client.'+@TableName+' CREATE TABLE client.'+ @TableName +'([RowNum] INT,'+ @colsData+' ) ';

		exec sp_executesql @tableQuery;

		--Insert EAV TO Table 
		DECLARE @Insert NVARCHAR(MAX);
		SET @Insert='Insert into client.'+@TableName+' '+@query;

		--print @Insert;
		exec sp_executesql @Insert;

		DECLARE @ViewQuery NVARCHAR(MAX);
		Declare @DropQuery NVARCHAR(MAX);
		SET @DropQuery='DROP VIEW IF EXISTS client.View'+@TableName;
		exec sp_executesql @DropQuery;
		set @ViewQuery=' CREATE VIEW client.View'+@TableName+' AS
				SELECT *
				FROM client.'+@TableName;
		exec sp_executesql @ViewQuery;

		--Create View SELECT * FROM TABLENAME //All Data in current Database View Name
		



		-- Insert Into Operation
		INSERT INTO client.[data.RealitycsOperationDetail]([CreatedBy]
           ,[CreatedDate]
           ,[OperationId]
           ,[TableName]
           ,[ViewName])
		   VALUES (1,GETUTCDATE(),@OperationValues,@TableName,'View'+@TableName);
		

		FETCH NEXT FROM cursor_operation INTO @OperationValues;
		END;
		CLOSE cursor_operation;

		DEALLOCATE cursor_operation;

		
		--if sum

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
			 1,
			 GETUTCDATE(),
			 1,
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