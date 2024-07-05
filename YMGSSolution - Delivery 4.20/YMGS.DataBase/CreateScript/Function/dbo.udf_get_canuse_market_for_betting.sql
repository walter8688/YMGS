IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_canuse_market_for_betting]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_canuse_market_for_betting]
END
GO

CREATE function [dbo].[udf_get_canuse_market_for_betting]
(
) 
returns @TABLE table 
(
    [Market_Id] int,
    [Match_Id] int
) 
as
begin 

	INSERT INTO @TABLE
	SELECT DISTINCT A.MARKET_ID,B.MATCH_ID
	FROM TB_MATCH_MARKET A
	INNER JOIN TB_MATCH B ON A.MATCH_ID=B.MATCH_ID	
	LEFT JOIN TB_MARKET_TEMPLATE C ON A.MARKET_TMP_ID=C.MARKET_TMP_ID
	WHERE B.STATUS IN(1,2,3,7) AND B.ADDITIONALSTATUS IN(1,3)  AND ISNULL(A.MARKET_STATUS,1)=1
	AND 
	( 
		(
			ISNULL(B.IS_ZOUDI,0)=1 --如果是走地
			AND (
					(C.BET_TYPE_ID=1 AND (
						(MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --半场标准盘只能在赛前和上半场赛中下注
						or
						(MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --全场标准盘只能在赛前和赛中下注
						OR
						(MARKET_TMP_TYPE=2 AND B.STATUS IN(1)) --半场/全场标准盘只能在赛前下注
					))
					OR
					(C.BET_TYPE_ID=2 AND B.STATUS IN(1)) --波胆:只能赛前下注
					OR
					(C.BET_TYPE_ID=3 AND MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --大小球:半场只能在赛前和上半场比赛中下注
					OR
					(C.BET_TYPE_ID=3 AND MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --大小球:全场大小球可以在赛前赛中都可以下注
					OR
					(C.BET_TYPE_ID=4 AND MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --让球盘:赛前赛中都可以下注	
					OR
					(C.BET_TYPE_ID=4 AND MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --让球盘:赛前赛中都可以下注
				)
		)
		
		OR
		
		(
			ISNULL(B.IS_ZOUDI,0)=0 --非走地
			AND B.STATUS=1
		)
	)

    return
end
GO