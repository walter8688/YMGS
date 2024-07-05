IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_brokerage_report]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_brokerage_report]
GO
CREATE PROCEDURE [dbo].[pr_get_brokerage_report]
    (
      @Start_Date DATETIME ,
      @End_Date DATETIME 
    )
AS 
    BEGIN
        SELECT  *
        FROM    ( SELECT    N'Ó¶½ð' AS reportType ,
                            settle.SETTLE_TIME ,
                            TB_EVENT.EVENT_NAME ,
                            dbo.TB_MATCH.MATCH_NAME ,
                            TB_BET_TYPE.BET_TYPE_NAME ,
                            dbo.TB_MATCH_MARKET.MARKET_NAME ,
                            deal.ODDS ,
                            deal.DEAL_AMOUNT ,
                            deal.DEAL_TIME ,
                            winuser.LOGIN_NAME winuser ,
                            loseuser.LOGIN_NAME loseuser ,
                            ISNULL(settle.BROKERAGE_RATE, 0) BROKERAGE_RATE ,
                            ISNULL(settle.BROKERAGE, 0) BROKERAGE
                  FROM      dbo.TB_EXCHANGE_SETTLE settle
                            INNER JOIN dbo.TB_EXCHANGE_DEAL deal ON settle.EXCHANGE_DEAL_ID = deal.EXCHANGE_DEAL_ID
                            INNER JOIN dbo.TB_EXCHANGE_LAY ON deal.EXCHANGE_LAY_ID = dbo.TB_EXCHANGE_LAY.EXCHANGE_LAY_ID
                            INNER JOIN dbo.TB_EXCHANGE_BACK ON deal.EXCHANGE_BACK_ID = dbo.TB_EXCHANGE_BACK.EXCHANGE_BACK_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT winuser ON winuser.USER_ID = settle.WIN_USER_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT loseuser ON loseuser.USER_ID = settle.LOSE_USER_ID
                            INNER JOIN dbo.TB_MATCH_MARKET ON deal.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
                            INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
                            INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
                            INNER JOIN dbo.TB_MATCH ON deal.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                            INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
                  WHERE     deal.MATCH_TYPE = 1
                            AND ( settle.SETTLE_TIME >= @Start_Date
                                  OR @Start_Date IS NULL
                                )
                            AND ( settle.SETTLE_TIME <= @End_Date
                                  OR @End_Date IS NULL
                                )
                  UNION
                  SELECT    N'Ó¶½ð' AS reportType ,
                            settle.SETTLE_TIME ,
                            TB_EVENT.EVENT_NAME ,
                            champ.Champ_Event_Name MATCH_NAME ,
                            '' BET_TYPE_NAME ,
                            '' MARKET_NAME ,
                            deal.ODDS ,
                            deal.DEAL_AMOUNT ,
                            deal.DEAL_TIME ,
                            winuser.LOGIN_NAME winuser ,
                            loseuser.LOGIN_NAME loseuser ,
                            ISNULL(settle.BROKERAGE_RATE, 0) BROKERAGE_RATE ,
                            ISNULL(settle.BROKERAGE, 0) BROKERAGE
                  FROM      dbo.TB_EXCHANGE_SETTLE settle
                            INNER JOIN dbo.TB_EXCHANGE_DEAL deal ON settle.EXCHANGE_DEAL_ID = deal.EXCHANGE_DEAL_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT winuser ON winuser.USER_ID = settle.WIN_USER_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT loseuser ON loseuser.USER_ID = settle.LOSE_USER_ID
                            INNER JOIN dbo.TB_CHAMP_EVENT champ ON deal.MATCH_ID = champ.Champ_Event_ID
                            LEFT JOIN dbo.TB_EVENT ON champ.Event_ID = dbo.TB_EVENT.EVENT_ID
                  WHERE     deal.MATCH_TYPE = 2
                            AND ( settle.SETTLE_TIME >= @Start_Date
                                  OR @Start_Date IS NULL
                                )
                            AND ( settle.SETTLE_TIME <= @End_Date
                                  OR @End_Date IS NULL
                                )
                ) TEMP
        ORDER BY TEMP.SETTLE_TIME DESC
    END
GO