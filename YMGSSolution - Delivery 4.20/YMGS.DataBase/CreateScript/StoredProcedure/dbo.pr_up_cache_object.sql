IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_cache_object]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_cache_object]
GO
CREATE PROCEDURE [dbo].[pr_up_cache_object]
    (
      @Cache_Type_Id INT 
    )
AS 
    BEGIN
        UPDATE  dbo.TB_CACHE_OBJECT
        SET     CHANGE_TIME = getdate()
        WHERE   CACHE_TYPE_ID = @Cache_Type_Id 
    END
GO