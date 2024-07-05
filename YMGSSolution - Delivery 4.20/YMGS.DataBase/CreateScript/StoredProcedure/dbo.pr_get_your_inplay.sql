iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_your_inplay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_your_inplay]
GO
CREATE PROCEDURE [dbo].[pr_get_your_inplay]
(
	@User_Id INT
)
AS 
BEGIN
	SELECT * FROM dbo.TB_YOUR_INPLAY WHERE [USER_ID] = @User_Id AND IS_FAV = 1 ORDER BY MATCH_ID
END
GO