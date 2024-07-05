iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_currency]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_currency]
GO
CREATE PROCEDURE [dbo].[pr_get_currency]
AS 
BEGIN
	SELECT * from dbo.TB_CURRENCY
END
GO