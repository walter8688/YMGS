IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_market_template_by_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_market_template_by_id]
GO
CREATE PROCEDURE [dbo].[pr_del_market_template_by_id]
(
	@Market_Tmp_Id int    	
)
AS 
BEGIN
	--执行删除操作
	DELETE FROM TB_MARKET_TEMPLATE WHERE MARKET_TMP_ID=@Market_Tmp_Id
END
GO