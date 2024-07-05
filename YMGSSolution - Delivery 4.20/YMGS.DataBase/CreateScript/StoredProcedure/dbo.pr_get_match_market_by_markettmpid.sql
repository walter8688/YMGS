IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_match_market_by_markettmpid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_match_market_by_markettmpid]
GO
CREATE PROCEDURE [dbo].[pr_get_match_market_by_markettmpid] ( @marketTmpId INT )
AS 
    BEGIN
        SELECT  mt.BET_TYPE_ID ,
                mt.Market_Tmp_Type ,
                mm.MARKET_ID ,
                mm.MARKET_NAME ,
                ISNULL(mm.MARKET_NAME_EN, mm.MARKET_NAME) MARKET_NAME_EN ,
                mm.MATCH_ID ,
                mm.MARKET_TMP_ID ,
                mm.MARKET_FLAG ,
                mm.SCOREA ,
                mm.SCOREB ,
                CASE WHEN mt.Market_Tmp_Type = 0
                          AND mt.BET_TYPE_ID = 2 THEN 'Half Time Score'
                     WHEN mt.Market_Tmp_Type = 1
                          AND mt.BET_TYPE_ID = 2 THEN 'Correct Score'
                     WHEN mt.Market_Tmp_Type = 0
                          AND mt.BET_TYPE_ID = 4 THEN 'Half Asian Handicap'
                     WHEN mt.Market_Tmp_Type = 1
                          AND mt.BET_TYPE_ID = 4 THEN 'Asian Handicap'
                     ELSE mt.MARKET_TMP_NAME_EN
                END AS MARKET_TMP_NAME_EN ,
                CASE WHEN mt.Market_Tmp_Type = 0
                          AND mt.BET_TYPE_ID = 2 THEN '半场波胆'
                     WHEN mt.Market_Tmp_Type = 1
                          AND mt.BET_TYPE_ID = 2 THEN '全场波胆'
                     WHEN mt.Market_Tmp_Type = 0
                          AND mt.BET_TYPE_ID = 4 THEN '半场让分盘'
                     WHEN mt.Market_Tmp_Type = 1
                          AND mt.BET_TYPE_ID = 4 THEN '全场让分盘'
                     ELSE mt.MARKET_TMP_NAME
                END AS MARKET_TMP_NAME ,
                mat.MATCH_NAME ,
                mat.MATCH_NAME_EN ,
                mat.STARTDATE STARTDATE ,
                ISNULL(back.MATCH_AMOUNTS, 0) backMATCH_AMOUNTS ,
                ISNULL(back.ODDS, 0) backodds ,
                ISNULL(lay.MATCH_AMOUNTS, 0) layMATCH_AMOUNTS ,
                ISNULL(lay.ODDS, 0) layodds
        FROM    TB_Match_Market mm
                LEFT JOIN TB_Market_Template mt ON mt.MARKET_TMP_ID = mm.MARKET_TMP_ID
                LEFT JOIN TB_MATCH mat ON mat.MATCH_ID = mm.MATCH_ID
                LEFT JOIN ( SELECT  back.MARKET_ID ,
                                    back.ODDS ,
                                    SUM(back.MATCH_AMOUNTS) MATCH_AMOUNTS
                            FROM    TB_EXCHANGE_BACK back
                            WHERE   EXISTS ( SELECT MARKET_ID
                                             FROM   ( SELECT  MARKET_ID ,
                                                              MIN(ODDS) ODDS
                                                      FROM    TB_EXCHANGE_BACK
                                                      WHERE   STATUS = 1
                                                              AND MATCH_TYPE = 1
                                                      GROUP BY MARKET_ID
                                                    ) eb
                                             WHERE  eb.MARKET_ID = back.MARKET_ID
                                                    AND eb.ODDS = back.ODDS )
                                    AND MATCH_TYPE = 1
                            GROUP BY back.MARKET_ID ,
                                    back.ODDS
                          ) back ON back.MARKET_ID = mm.MARKET_ID
                LEFT JOIN ( SELECT  lay.MARKET_ID ,
                                    lay.ODDS ,
                                    SUM(lay.MATCH_AMOUNTS) MATCH_AMOUNTS
                            FROM    TB_EXCHANGE_LAY lay
                            WHERE   EXISTS ( SELECT MARKET_ID
                                             FROM   ( SELECT  MARKET_ID ,
                                                              MAX(ODDS) ODDS
                                                      FROM    TB_EXCHANGE_LAY
                                                      WHERE   STATUS = 1
                                                              AND MATCH_TYPE = 1
                                                      GROUP BY MARKET_ID
                                                    ) la
                                             WHERE  la.MARKET_ID = lay.MARKET_ID
                                                    AND la.ODDS = lay.ODDS )
                                    AND MATCH_TYPE = 1
                            GROUP BY lay.MARKET_ID ,
                                    lay.ODDS
                          ) lay ON lay.MARKET_ID = mm.MARKET_ID
        WHERE   mm.MARKET_TMP_ID = @marketTmpId
                AND mm.MARKET_ID IN (
                SELECT  market_id
                FROM    dbo.udf_get_canuse_market_for_betting() )
    END
GO