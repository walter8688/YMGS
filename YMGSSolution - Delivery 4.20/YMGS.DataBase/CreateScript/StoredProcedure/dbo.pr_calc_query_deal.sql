IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_query_deal]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_query_deal]
GO
CREATE PROCEDURE [dbo].[pr_calc_query_deal]
    (
	  @Match_Id int,
	  @Calc_Flag int --0半场结算 1 全场结算
    )
AS

BEGIN
	--查询当前可以进行结算的所有撮合交易记录
	SELECT A.EXCHANGE_DEAL_ID,
		A.MATCH_ID,
		A.MARKET_ID,
		A.EXCHANGE_BACK_ID,
		A.EXCHANGE_LAY_ID,
		A.DEAL_AMOUNT,
		A.DEAL_TIME,
		A.STATUS,
		A.MATCH_TYPE,
		A.ODDS,
		C.MARKET_NAME,
		C.MARKET_FLAG,
		C.SCOREA MARKET_SCOREA,
		C.SCOREB MARKET_SCOREB,
		D.MARKET_TMP_TYPE,
		D.MARKET_TMP_NAME,
		D.BET_TYPE_ID
	FROM TB_EXCHANGE_DEAL A INNER JOIN TB_MATCH B ON A.MATCH_ID=B.MATCH_ID
	INNER JOIN TB_MATCH_MARKET C ON A.MARKET_ID=C.MARKET_ID
	LEFT JOIN TB_MARKET_TEMPLATE D ON C.MARKET_TMP_ID=D.MARKET_TMP_ID
	WHERE A.MATCH_ID=@Match_Id
	AND (
		(@Calc_Flag=0 AND B.STATUS in(select * from dbo.udf_get_footbal_halfcalc_status()) AND D.MARKET_TMP_TYPE=0) ---半场的玩法
		OR
		(@Calc_Flag=1 AND B.STATUS=4)----全场结算时结算所有的撮合交易记录
	)
	AND B.ADDITIONALSTATUS=1 --正常比赛中的比赛
	AND A.STATUS=1 --正常的撮合交易记录
	AND A.MATCH_TYPE=1
END
GO