IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_deal_report]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_deal_report]
GO
CREATE PROCEDURE [dbo].[pr_get_deal_report]
    (
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @Bet_Type NVARCHAR(10) ,
      @Event_Item_Name NVARCHAR(100) ,
      @Event_Zone_Name NVARCHAR(100) ,
      @Event_Name NVARCHAR(100)
    )
AS 
    BEGIN
        SELECT  *
        FROM    ( SELECT    dbo.TB_EVENT.EVENT_NAME EventName ,
                            dbo.TB_MATCH.MATCH_NAME MatchName ,
                            dbo.TB_BET_TYPE.BET_TYPE_NAME BET_NAME ,
                            dbo.TB_MATCH_MARKET.MARKET_NAME ,
                            deal.ODDS ,
                            deal.DEAL_AMOUNT ,
                            deal.DEAL_TIME ,
                            BackUser.LOGIN_NAME BackUser ,
                            LayUser.LOGIN_NAME LayUser ,
                            CASE TB_MARKET_TEMPLATE.Market_Tmp_Type
                              WHEN 0 THEN '半场'
                              WHEN 1 THEN '全场'
                              WHEN 2 THEN '半全场'
                            END AS Market_Tmp_Type
                  FROM      TB_EXCHANGE_DEAL deal
                            INNER JOIN dbo.TB_MATCH ON deal.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                            INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
                            INNER JOIN dbo.TB_EVENT_ZONE ON dbo.TB_EVENT.EVENTZONE_ID = dbo.TB_EVENT_ZONE.EVENTZONE_ID
                            INNER JOIN dbo.TB_EVENT_ITEM ON dbo.TB_EVENT_ZONE.EVENTITEM_ID = dbo.TB_EVENT_ITEM.EventItem_ID
                            INNER JOIN dbo.TB_MATCH_MARKET ON deal.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
                            INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
                            INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
                            INNER JOIN dbo.TB_EXCHANGE_LAY lay ON lay.EXCHANGE_LAY_ID = deal.EXCHANGE_LAY_ID
                            INNER JOIN dbo.TB_EXCHANGE_BACK back ON back.EXCHANGE_BACK_ID = deal.EXCHANGE_BACK_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT LayUser ON lay.TRADE_USER = LayUser.USER_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT BackUser ON back.TRADE_USER = BackUser.USER_ID
                  WHERE     deal.MATCH_TYPE = 1
                            AND ( dbo.TB_EVENT_ITEM.EventItem_Name = @Event_Item_Name
                                  OR @Event_Item_Name IS NULL
                                  OR @Event_Item_Name = ''
                                )
                            AND ( dbo.TB_EVENT_ZONE.EVENTZONE_NAME = @Event_Zone_Name
                                  OR @Event_Zone_Name IS NULL
                                  OR @Event_Zone_Name = ''
                                )
                            AND ( dbo.TB_EVENT.EVENT_NAME = @Event_Name
                                  OR @Event_Name IS NULL
                                  OR @Event_Name = ''
                                )
                  UNION
                  SELECT    champ.Champ_Event_Name EventName ,
                            champ.Champ_Event_Name MatchName ,
                            CASE champ.Champ_Event_Type
                              WHEN 1 THEN '冠军'
                              WHEN 2 THEN '娱乐'
                            END AS BET_NAME ,
                            '' MARKET_NAME ,
                            deal.ODDS ,
                            deal.DEAL_AMOUNT ,
                            deal.DEAL_TIME ,
                            BackUser.LOGIN_NAME BackUser ,
                            LayUser.LOGIN_NAME LayUser ,
                            '' Market_Tmp_Type
                  FROM      dbo.TB_EXCHANGE_DEAL deal
                            INNER JOIN dbo.TB_Champ_Market champMarket ON deal.MARKET_ID = champMarket.Champ_Market_ID
                            INNER JOIN dbo.TB_CHAMP_EVENT champ ON champMarket.Champ_Event_ID = champ.Champ_Event_ID
                            INNER JOIN dbo.TB_EXCHANGE_LAY lay ON lay.EXCHANGE_LAY_ID = deal.EXCHANGE_LAY_ID
                            INNER JOIN dbo.TB_EXCHANGE_BACK back ON back.EXCHANGE_BACK_ID = deal.EXCHANGE_BACK_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT LayUser ON lay.TRADE_USER = LayUser.USER_ID
                            INNER JOIN dbo.TB_SYSTEM_ACCOUNT BackUser ON back.TRADE_USER = BackUser.USER_ID
                  WHERE     deal.MATCH_TYPE = 2
                ) TEMP
        WHERE   ( TEMP.BET_NAME = @Bet_Type
                  OR @Bet_Type = ''
                  OR @Bet_Type IS NULL
                )
                AND ( TEMP.DEAL_TIME >= @Start_Date
                      OR @Start_Date IS NULL
                    )
                AND ( TEMP.DEAL_TIME <= @End_Date
                      OR @End_Date IS NULL
                    )
        ORDER BY TEMP.DEAL_TIME DESC
    END
GO