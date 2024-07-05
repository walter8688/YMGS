IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_bet_report]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_bet_report]
GO
CREATE PROCEDURE [dbo].[pr_get_bet_report]
    (
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @Exchange_Type INT ,
      @Bet_Type NVARCHAR(10) ,
      @Event_Item_Name NVARCHAR(100) ,
      @Event_Zone_Name NVARCHAR(100) ,
      @Event_Name NVARCHAR(100)
    )
AS 
    BEGIN
	 --投注&&受注
		IF @Exchange_Type = -1
			BEGIN
				 SELECT  *
					FROM    ( SELECT    '受注' ExchangeType ,
										dbo.TB_EVENT.EVENT_NAME EventName ,
										dbo.TB_MATCH.MATCH_NAME MatchName ,
										dbo.TB_BET_TYPE.BET_TYPE_NAME BET_NAME ,
										dbo.TB_MATCH_MARKET.MARKET_NAME ,
										lay.ODDS ,
										lay.BET_AMOUNTS ,
										lay.MATCH_AMOUNTS ,
										lay.TRADE_TIME ,
										users.LOGIN_NAME ,
										CASE lay.STATUS
										  WHEN 1 THEN N'可撮合'
										  WHEN 2 THEN N'撮合完'
										  WHEN 3 THEN N'已结算'--已结算撮合成交记录
										  WHEN 4 THEN N'已结算'
										  WHEN 5 THEN N'已封盘'
										  WHEN 6 THEN N'已取消'
										END AS Exchange_Status ,
										CAST(ISNULL(lay.HOME_TEAM_SCORE, 0) AS VARCHAR)
										+ ':'
										+ CAST(ISNULL(lay.GUEST_TEAM_SCORE, 0) AS VARCHAR) MatchScore ,
										CASE TB_MARKET_TEMPLATE.Market_Tmp_Type
										  WHEN 0 THEN '半场'
										  WHEN 1 THEN '全场'
										  WHEN 2 THEN '半全场'
										END AS Market_Tmp_Type
							  FROM      dbo.TB_EXCHANGE_LAY lay
										INNER JOIN dbo.TB_MATCH ON lay.MATCH_ID = dbo.TB_MATCH.MATCH_ID
										INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
										INNER JOIN dbo.TB_EVENT_ZONE ON dbo.TB_EVENT.EVENTZONE_ID = dbo.TB_EVENT_ZONE.EVENTZONE_ID
										INNER JOIN dbo.TB_EVENT_ITEM ON dbo.TB_EVENT_ZONE.EVENTITEM_ID = dbo.TB_EVENT_ITEM.EventItem_ID
										INNER JOIN dbo.TB_MATCH_MARKET ON lay.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
										INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
										INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
										INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON lay.TRADE_USER = users.USER_ID
							  WHERE     lay.MATCH_TYPE = 1
										AND ( lay.TRADE_TIME >= @Start_Date
											  OR @Start_Date IS NULL
											)
										AND ( lay.TRADE_TIME <= @End_Date
											  OR @End_Date IS NULL
											)
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
							  SELECT    '受注' ExchangeType ,
										champ.Champ_Event_Name EventName ,
										champ.Champ_Event_Name MatchName ,
										CASE champ.Champ_Event_Type
										  WHEN 1 THEN '冠军'
										  WHEN 2 THEN '娱乐'
										END AS BET_NAME ,
										'' MARKET_NAME ,
										lay.ODDS ,
										lay.BET_AMOUNTS ,
										lay.MATCH_AMOUNTS ,
										lay.TRADE_TIME ,
										users.LOGIN_NAME ,
										CASE lay.STATUS
										  WHEN 1 THEN N'可撮合'
										  WHEN 2 THEN N'撮合完'
										  WHEN 3 THEN N'已结算'--已结算撮合成交记录
										  WHEN 4 THEN N'已结算'
										  WHEN 5 THEN N'已封盘'
										  WHEN 6 THEN N'已取消'
										END AS Exchange_Status ,
										'' MatchScore ,
										'' Market_Tmp_Type
							  FROM      dbo.TB_EXCHANGE_LAY lay
										INNER JOIN dbo.TB_Champ_Market champMarket ON lay.MARKET_ID = champMarket.Champ_Market_ID
										INNER JOIN dbo.TB_CHAMP_EVENT champ ON champMarket.Champ_Event_ID = champ.Champ_Event_ID
										INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON lay.TRADE_USER = users.USER_ID
							  WHERE     lay.MATCH_TYPE = 2
										AND ( lay.TRADE_TIME >= @Start_Date
											  OR @Start_Date IS NULL
											)
										AND ( lay.TRADE_TIME <= @End_Date
											  OR @End_Date IS NULL
											)
							) TEMP
					WHERE   ( TEMP.BET_NAME = @Bet_Type
							  OR @Bet_Type = ''
							  OR @Bet_Type IS NULL
							)
					UNION
					SELECT  *
                    FROM    ( SELECT    '投注' ExchangeType ,
                                        dbo.TB_EVENT.EVENT_NAME EventName ,
                                        dbo.TB_MATCH.MATCH_NAME MatchName ,
                                        dbo.TB_BET_TYPE.BET_TYPE_NAME BET_NAME ,
                                        dbo.TB_MATCH_MARKET.MARKET_NAME ,
                                        back.ODDS ,
                                        back.BET_AMOUNTS ,
                                        back.MATCH_AMOUNTS ,
                                        back.TRADE_TIME ,
                                        users.LOGIN_NAME ,
                                        CASE back.STATUS
                                          WHEN 1 THEN N'可撮合'
                                          WHEN 2 THEN N'撮合完'
                                          WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                          WHEN 4 THEN N'已结算'
                                          WHEN 5 THEN N'已封盘'
                                          WHEN 6 THEN N'已取消'
                                        END AS Exchange_Status ,
                                        CAST(ISNULL(back.HOME_TEAM_SCORE, 0) AS VARCHAR)
                                        + ':'
                                        + CAST(ISNULL(back.GUEST_TEAM_SCORE, 0) AS VARCHAR) MatchScore ,
                                        CASE TB_MARKET_TEMPLATE.Market_Tmp_Type
                                          WHEN 0 THEN '半场'
                                          WHEN 1 THEN '全场'
                                          WHEN 2 THEN '半全场'
                                        END AS Market_Tmp_Type
                              FROM      dbo.TB_EXCHANGE_BACK back
                                        INNER JOIN dbo.TB_MATCH ON back.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                                        INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
                                        INNER JOIN dbo.TB_EVENT_ZONE ON dbo.TB_EVENT.EVENTZONE_ID = dbo.TB_EVENT_ZONE.EVENTZONE_ID
                                        INNER JOIN dbo.TB_EVENT_ITEM ON dbo.TB_EVENT_ZONE.EVENTITEM_ID = dbo.TB_EVENT_ITEM.EventItem_ID
                                        INNER JOIN dbo.TB_MATCH_MARKET ON back.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
                                        INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
                                        INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
                                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON back.TRADE_USER = users.USER_ID
                              WHERE     back.MATCH_TYPE = 1
                                        AND ( back.TRADE_TIME >= @Start_Date
                                              OR @Start_Date IS NULL
                                            )
                                        AND ( back.TRADE_TIME <= @End_Date
                                              OR @End_Date IS NULL
                                            )
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
                              SELECT    '投注' ExchangeType ,
                                        champ.Champ_Event_Name EventName ,
                                        champ.Champ_Event_Name MatchName ,
                                        CASE champ.Champ_Event_Type
                                          WHEN 1 THEN '冠军'
                                          WHEN 2 THEN '娱乐'
                                        END AS BET_NAME ,
                                        '' MARKET_NAME ,
                                        back.ODDS ,
                                        back.BET_AMOUNTS ,
                                        back.MATCH_AMOUNTS ,
                                        back.TRADE_TIME ,
                                        users.LOGIN_NAME ,
                                        CASE back.STATUS
                                          WHEN 1 THEN N'可撮合'
                                          WHEN 2 THEN N'撮合完'
                                          WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                          WHEN 4 THEN N'已结算'
                                          WHEN 5 THEN N'已封盘'
                                          WHEN 6 THEN N'已取消'
                                        END AS Exchange_Status ,
                                        '' MatchScore ,
                                        '' Market_Tmp_Type
                              FROM      dbo.TB_EXCHANGE_BACK back
                                        INNER JOIN dbo.TB_Champ_Market champMarket ON back.MARKET_ID = champMarket.Champ_Market_ID
                                        INNER JOIN dbo.TB_CHAMP_EVENT champ ON champMarket.Champ_Event_ID = champ.Champ_Event_ID
                                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON back.TRADE_USER = users.USER_ID
                              WHERE     back.MATCH_TYPE = 2
                                        AND ( back.TRADE_TIME >= @Start_Date
                                              OR @Start_Date IS NULL
                                            )
                                        AND ( back.TRADE_TIME <= @End_Date
                                              OR @End_Date IS NULL
                                            )
                            ) TEMP
                    WHERE   ( TEMP.BET_NAME = @Bet_Type
                              OR @Bet_Type = ''
                              OR @Bet_Type IS NULL
                            )
                    ORDER BY TEMP.TRADE_TIME DESC
			END
	    --受注
        IF @Exchange_Type = 1 
            BEGIN
                SELECT  *
                FROM    ( SELECT    '受注' ExchangeType ,
                                    dbo.TB_EVENT.EVENT_NAME EventName ,
                                    dbo.TB_MATCH.MATCH_NAME MatchName ,
                                    dbo.TB_BET_TYPE.BET_TYPE_NAME BET_NAME ,
                                    dbo.TB_MATCH_MARKET.MARKET_NAME ,
                                    lay.ODDS ,
                                    lay.BET_AMOUNTS ,
                                    lay.MATCH_AMOUNTS ,
                                    lay.TRADE_TIME ,
                                    users.LOGIN_NAME ,
                                    CASE lay.STATUS
                                      WHEN 1 THEN N'可撮合'
                                      WHEN 2 THEN N'撮合完'
                                      WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                      WHEN 4 THEN N'已结算'
                                      WHEN 5 THEN N'已封盘'
                                      WHEN 6 THEN N'已取消'
                                    END AS Exchange_Status ,
                                    CAST(ISNULL(lay.HOME_TEAM_SCORE, 0) AS VARCHAR)
                                    + ':'
                                    + CAST(ISNULL(lay.GUEST_TEAM_SCORE, 0) AS VARCHAR) MatchScore ,
                                    CASE TB_MARKET_TEMPLATE.Market_Tmp_Type
                                      WHEN 0 THEN '半场'
                                      WHEN 1 THEN '全场'
                                      WHEN 2 THEN '半全场'
                                    END AS Market_Tmp_Type
                          FROM      dbo.TB_EXCHANGE_LAY lay
                                    INNER JOIN dbo.TB_MATCH ON lay.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                                    INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
                                    INNER JOIN dbo.TB_EVENT_ZONE ON dbo.TB_EVENT.EVENTZONE_ID = dbo.TB_EVENT_ZONE.EVENTZONE_ID
                                    INNER JOIN dbo.TB_EVENT_ITEM ON dbo.TB_EVENT_ZONE.EVENTITEM_ID = dbo.TB_EVENT_ITEM.EventItem_ID
                                    INNER JOIN dbo.TB_MATCH_MARKET ON lay.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
                                    INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
                                    INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
                                    INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON lay.TRADE_USER = users.USER_ID
                          WHERE     lay.MATCH_TYPE = 1
                                    AND ( lay.TRADE_TIME >= @Start_Date
                                          OR @Start_Date IS NULL
                                        )
                                    AND ( lay.TRADE_TIME <= @End_Date
                                          OR @End_Date IS NULL
                                        )
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
                          SELECT    '受注' ExchangeType ,
                                    champ.Champ_Event_Name EventName ,
                                    champ.Champ_Event_Name MatchName ,
                                    CASE champ.Champ_Event_Type
                                      WHEN 1 THEN '冠军'
                                      WHEN 2 THEN '娱乐'
                                    END AS BET_NAME ,
                                    '' MARKET_NAME ,
                                    lay.ODDS ,
                                    lay.BET_AMOUNTS ,
                                    lay.MATCH_AMOUNTS ,
                                    lay.TRADE_TIME ,
                                    users.LOGIN_NAME ,
                                    CASE lay.STATUS
                                      WHEN 1 THEN N'可撮合'
                                      WHEN 2 THEN N'撮合完'
                                      WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                      WHEN 4 THEN N'已结算'
                                      WHEN 5 THEN N'已封盘'
                                      WHEN 6 THEN N'已取消'
                                    END AS Exchange_Status ,
                                    '' MatchScore ,
                                    '' Market_Tmp_Type
                          FROM      dbo.TB_EXCHANGE_LAY lay
                                    INNER JOIN dbo.TB_Champ_Market champMarket ON lay.MARKET_ID = champMarket.Champ_Market_ID
                                    INNER JOIN dbo.TB_CHAMP_EVENT champ ON champMarket.Champ_Event_ID = champ.Champ_Event_ID
                                    INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON lay.TRADE_USER = users.USER_ID
                          WHERE     lay.MATCH_TYPE = 2
                                    AND ( lay.TRADE_TIME >= @Start_Date
                                          OR @Start_Date IS NULL
                                        )
                                    AND ( lay.TRADE_TIME <= @End_Date
                                          OR @End_Date IS NULL
                                        )
                        ) TEMP
                WHERE   ( TEMP.BET_NAME = @Bet_Type
                          OR @Bet_Type = ''
                          OR @Bet_Type IS NULL
                        )
                ORDER BY TEMP.TRADE_TIME DESC
            END
        --投注
        ELSE 
            IF @Exchange_Type = 0 
                BEGIN
                    SELECT  *
                    FROM    ( SELECT    '投注' ExchangeType ,
                                        dbo.TB_EVENT.EVENT_NAME EventName ,
                                        dbo.TB_MATCH.MATCH_NAME MatchName ,
                                        dbo.TB_BET_TYPE.BET_TYPE_NAME BET_NAME ,
                                        dbo.TB_MATCH_MARKET.MARKET_NAME ,
                                        back.ODDS ,
                                        back.BET_AMOUNTS ,
                                        back.MATCH_AMOUNTS ,
                                        back.TRADE_TIME ,
                                        users.LOGIN_NAME ,
                                        CASE back.STATUS
                                          WHEN 1 THEN N'可撮合'
                                          WHEN 2 THEN N'撮合完'
                                          WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                          WHEN 4 THEN N'已结算'
                                          WHEN 5 THEN N'已封盘'
                                          WHEN 6 THEN N'已取消'
                                        END AS Exchange_Status ,
                                        CAST(ISNULL(back.HOME_TEAM_SCORE, 0) AS VARCHAR)
                                        + ':'
                                        + CAST(ISNULL(back.GUEST_TEAM_SCORE, 0) AS VARCHAR) MatchScore ,
                                        CASE TB_MARKET_TEMPLATE.Market_Tmp_Type
                                          WHEN 0 THEN '半场'
                                          WHEN 1 THEN '全场'
                                          WHEN 2 THEN '半全场'
                                        END AS Market_Tmp_Type
                              FROM      dbo.TB_EXCHANGE_BACK back
                                        INNER JOIN dbo.TB_MATCH ON back.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                                        INNER JOIN dbo.TB_EVENT ON dbo.TB_MATCH.EVENT_ID = dbo.TB_EVENT.EVENT_ID
                                        INNER JOIN dbo.TB_EVENT_ZONE ON dbo.TB_EVENT.EVENTZONE_ID = dbo.TB_EVENT_ZONE.EVENTZONE_ID
                                        INNER JOIN dbo.TB_EVENT_ITEM ON dbo.TB_EVENT_ZONE.EVENTITEM_ID = dbo.TB_EVENT_ITEM.EventItem_ID
                                        INNER JOIN dbo.TB_MATCH_MARKET ON back.MARKET_ID = dbo.TB_MATCH_MARKET.MARKET_ID
                                        INNER JOIN dbo.TB_MARKET_TEMPLATE ON dbo.TB_MATCH_MARKET.MARKET_TMP_ID = dbo.TB_MARKET_TEMPLATE.MARKET_TMP_ID
                                        INNER JOIN dbo.TB_BET_TYPE ON dbo.TB_MARKET_TEMPLATE.BET_TYPE_ID = dbo.TB_BET_TYPE.BET_TYPE_ID
                                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON back.TRADE_USER = users.USER_ID
                              WHERE     back.MATCH_TYPE = 1
                                        AND ( back.TRADE_TIME >= @Start_Date
                                              OR @Start_Date IS NULL
                                            )
                                        AND ( back.TRADE_TIME <= @End_Date
                                              OR @End_Date IS NULL
                                            )
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
                              SELECT    '投注' ExchangeType ,
                                        champ.Champ_Event_Name EventName ,
                                        champ.Champ_Event_Name MatchName ,
                                        CASE champ.Champ_Event_Type
                                          WHEN 1 THEN '冠军'
                                          WHEN 2 THEN '娱乐'
                                        END AS BET_NAME ,
                                        '' MARKET_NAME ,
                                        back.ODDS ,
                                        back.BET_AMOUNTS ,
                                        back.MATCH_AMOUNTS ,
                                        back.TRADE_TIME ,
                                        users.LOGIN_NAME ,
                                        CASE back.STATUS
                                          WHEN 1 THEN N'可撮合'
                                          WHEN 2 THEN N'撮合完'
                                          WHEN 3 THEN N'已结算'--已结算撮合成交记录
                                          WHEN 4 THEN N'已结算'
                                          WHEN 5 THEN N'已封盘'
                                          WHEN 6 THEN N'已取消'
                                        END AS Exchange_Status ,
                                        '' MatchScore ,
                                        '' Market_Tmp_Type
                              FROM      dbo.TB_EXCHANGE_BACK back
                                        INNER JOIN dbo.TB_Champ_Market champMarket ON back.MARKET_ID = champMarket.Champ_Market_ID
                                        INNER JOIN dbo.TB_CHAMP_EVENT champ ON champMarket.Champ_Event_ID = champ.Champ_Event_ID
                                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON back.TRADE_USER = users.USER_ID
                              WHERE     back.MATCH_TYPE = 2
                                        AND ( back.TRADE_TIME >= @Start_Date
                                              OR @Start_Date IS NULL
                                            )
                                        AND ( back.TRADE_TIME <= @End_Date
                                              OR @End_Date IS NULL
                                            )
                            ) TEMP
                    WHERE   ( TEMP.BET_NAME = @Bet_Type
                              OR @Bet_Type = ''
                              OR @Bet_Type IS NULL
                            )
                    ORDER BY TEMP.TRADE_TIME DESC
                END
    END
GO