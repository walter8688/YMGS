IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_update_market_status]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_update_market_status]
    GO
CREATE PROCEDURE pr_update_market_status
    (
      @Match_Id INT ,
      @Market_Tmp_Id NVARCHAR(1000) 
    )
AS 
    BEGIN
		UPDATE TB_MATCH_MARKET SET MARKET_STATUS = 1 WHERE MATCH_ID = @Match_Id
		DECLARE @sql NVARCHAR(1100)
		SET @sql = 'UPDATE TB_MATCH_MARKET SET MARKET_STATUS = 0 WHERE MATCH_ID = '+CONVERT(NVARCHAR(100),@Match_Id)+' AND MARKET_TMP_ID IN'+@Market_Tmp_Id
		--PRINT @sql
        EXEC sp_ExecuteSql @sql
        EXEC pr_up_cache_object 3
    END
GO