IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_market]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_market]
    GO
CREATE PROCEDURE pr_get_market ( @Match_Id INT )
AS 
    BEGIN
        SELECT  MAX(TB_MARKET_TEMPLATE.MARKET_TMP_NAME) MARKET_TMP_NAME ,
                TB_MATCH_MARKET.MARKET_TMP_ID ,
                MAX(TB_MATCH_MARKET.MARKET_STATUS) MARKET_STATUS,
                MAX(TB_MARKET_TEMPLATE.BET_TYPE_ID) BET_TYPE_ID,
				MAX(TB_MARKET_TEMPLATE.MARKET_TMP_TYPE) MARKET_TMP_TYPE
        FROM    TB_MATCH_MARKET
                INNER JOIN TB_MARKET_TEMPLATE ON TB_MATCH_MARKET.MARKET_TMP_ID = TB_MARKET_TEMPLATE.MARKET_TMP_ID
        WHERE   MATCH_ID = @Match_Id AND BET_TYPE_ID > 1
        GROUP BY TB_MATCH_MARKET.MARKET_TMP_ID
        ORDER BY MARKET_TMP_ID
    END
GO