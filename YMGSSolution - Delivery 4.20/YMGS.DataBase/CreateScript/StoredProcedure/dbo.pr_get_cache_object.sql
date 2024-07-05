iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_cache_object]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_cache_object]
GO
CREATE PROCEDURE [dbo].[pr_get_cache_object]
AS 
BEGIN
	SELECT * from TB_CACHE_OBJECT
END
GO