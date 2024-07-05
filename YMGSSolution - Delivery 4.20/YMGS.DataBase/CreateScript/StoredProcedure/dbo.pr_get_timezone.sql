iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_timezone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_timezone]
GO
CREATE PROCEDURE [dbo].[pr_get_timezone]
AS 
BEGIN
	SELECT * from dbo.TB_TIMEZONE
END
GO