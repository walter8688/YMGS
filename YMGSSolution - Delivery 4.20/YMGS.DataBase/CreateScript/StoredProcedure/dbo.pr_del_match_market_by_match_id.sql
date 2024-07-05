IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_match_market_by_match_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_match_market_by_match_id]
GO
CREATE PROCEDURE [dbo].[pr_del_match_market_by_match_id]
    (
	  @Match_Id int
    )
AS 
BEGIN
	--如果市场被引用则不能被删除，暂时未实现
	DELETE FROM dbo.TB_MATCH_MARKET WHERE MATCH_ID=@Match_Id
END
GO