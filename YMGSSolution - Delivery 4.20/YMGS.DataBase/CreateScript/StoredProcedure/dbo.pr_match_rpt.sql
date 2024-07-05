IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_match_rpt]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_match_rpt]
go
CREATE PROCEDURE [dbo].[pr_match_rpt]
    (
      @match_type INT ,
      @MATCH_ID INT ,
      @MARKET_ID INT
    )
AS 
    BEGIN
        SELECT  ISNULL(back.ODDS, lay.ODDS) ODDS ,
                back.sumMATCH_AMOUNTS backAmount ,
                lay.sumMATCH_AMOUNTS layAmount ,
                deal.sumMATCH_AMOUNTS dealAmount
        FROM    ( SELECT    ODDS ,
                            SUM(DEAL_AMOUNT) sumMATCH_AMOUNTS
                  FROM      TB_EXCHANGE_DEAL
                  WHERE     match_type = @match_type
                            AND match_id = @MATCH_ID
                            AND MARKET_ID = @MARKET_ID
                  GROUP BY  ODDS
                ) deal
                FULL OUTER JOIN ( SELECT    ODDS ,
                                            SUM(MATCH_AMOUNTS) sumMATCH_AMOUNTS
                                  FROM      TB_EXCHANGE_BACK
                                  WHERE     match_type = @match_type
                                            AND match_id = @MATCH_ID
                                            AND MARKET_ID = @MARKET_ID
                                            AND ( STATUS < 6
                                                  OR MATCH_AMOUNTS >0
                                                )
                                  GROUP BY  ODDS
                                ) back ON deal.ODDS = back.ODDS
                FULL OUTER JOIN ( SELECT    ODDS ,
                                            SUM(MATCH_AMOUNTS) sumMATCH_AMOUNTS
                                  FROM      TB_EXCHANGE_LAY
                                  WHERE     match_type = @match_type
                                            AND match_id = @MATCH_ID
                                            AND MARKET_ID = @MARKET_ID
                                            AND ( STATUS < 6
                                                  OR MATCH_AMOUNTS >0
                                                )
                                  GROUP BY  ODDS
                                ) lay ON deal.ODDS = lay.ODDS
        WHERE   ISNULL(lay.sumMATCH_AMOUNTS, 0) != 0
                OR ISNULL(back.sumMATCH_AMOUNTS, 0) != 0
                OR ISNULL(deal.sumMATCH_AMOUNTS, 0) != 0
        ORDER BY ODDS 
    END
GO