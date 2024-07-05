IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_my_match]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_my_match]
GO
CREATE PROCEDURE pr_get_my_match ( @Trade_User INT )
AS 
    BEGIN
        SELECT DISTINCT
                matchlist.*
        FROM    ( SELECT    dbo.TB_MATCH.MATCH_NAME ,
                            dbo.TB_MATCH.MATCH_NAME_EN
                  FROM      dbo.TB_EXCHANGE_LAY
                            INNER JOIN dbo.TB_MATCH ON dbo.TB_EXCHANGE_LAY.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                                                       AND dbo.TB_EXCHANGE_LAY.MATCH_TYPE = 1
                                                       AND TRADE_USER = @Trade_User
                  UNION
                  SELECT    dbo.TB_MATCH.MATCH_NAME ,
                            dbo.TB_MATCH.MATCH_NAME_EN
                  FROM      dbo.TB_EXCHANGE_BACK
                            INNER JOIN dbo.TB_MATCH ON dbo.TB_EXCHANGE_BACK.MATCH_ID = dbo.TB_MATCH.MATCH_ID
                                                       AND dbo.TB_EXCHANGE_BACK.MATCH_TYPE = 1
                                                       AND TRADE_USER = @Trade_User
                  UNION
                  SELECT    TB_CHAMP_EVENT.Champ_Event_Name MATCH_NAME ,
                            dbo.TB_CHAMP_EVENT.Champ_Event_Name_En MATCH_NAME_EN
                  FROM      dbo.TB_EXCHANGE_LAY
                            INNER JOIN dbo.TB_CHAMP_EVENT ON dbo.TB_EXCHANGE_LAY.MATCH_ID = dbo.TB_CHAMP_EVENT.Event_ID
                                                             AND dbo.TB_EXCHANGE_LAY.MATCH_TYPE = 2
                                                             AND TRADE_USER = @Trade_User
                  UNION
                  SELECT    TB_CHAMP_EVENT.Champ_Event_Name MATCH_NAME ,
                            dbo.TB_CHAMP_EVENT.Champ_Event_Name_En MATCH_NAME_EN
                  FROM      dbo.TB_EXCHANGE_BACK
                            INNER JOIN dbo.TB_CHAMP_EVENT ON dbo.TB_EXCHANGE_BACK.MATCH_ID = dbo.TB_CHAMP_EVENT.Event_ID
                                                             AND dbo.TB_EXCHANGE_BACK.MATCH_TYPE = 2
                                                             AND TRADE_USER = @Trade_User
                ) matchlist
        ORDER BY matchlist.MATCH_NAME
    END
	GO
	