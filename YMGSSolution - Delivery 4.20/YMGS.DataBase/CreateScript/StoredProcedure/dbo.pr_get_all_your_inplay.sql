iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_all_your_inplay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_all_your_inplay]
GO
CREATE PROCEDURE [dbo].[pr_get_all_your_inplay]
AS 
BEGIN
	SELECT * FROM dbo.TB_YOUR_INPLAY ORDER BY MATCH_ID,IS_FAV
END
GO