IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_match_market_for_betting]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_match_market_for_betting]
GO
CREATE PROCEDURE [dbo].[pr_get_match_market_for_betting]
AS 
BEGIN
	SELECT	A.MATCH_ID,
			A.MATCH_NAME,
			A.MATCH_NAME_EN,
			E.EVENTTYPE_NAME,
			E.EVENTTYPE_NAME_EN,
			D.EVENTITEM_NAME,
			D.EVENTITEM_NAME_EN,
			C.EVENTZONE_NAME,
			C.EVENTZONE_NAME_EN,
			A.EVENT_ID,
			B.EVENT_NAME,		
			B.EVENT_NAME_EN,
			A.MATCH_DESC,
			A.EVENT_HOME_TEAM_ID,
			A.EVENT_HOME_GUEST_ID,
			A.STARTDATE,
			A.ENDDATE,
			A.AUTO_FREEZE_DATE,
			A.HOME_FIR_HALF_SCORE,
			A.GUEST_FIR_HALF_SCORE,
			A.HOME_SEC_HALF_SCORE,
			A.GUEST_SEC_HALF_SCORE,
			A.HOME_OVERTIME_SCORE,
			A.GUEST_OVERTIME_SCORE,
			A.HOME_POINT_SCORE,
			A.GUEST_POINT_SCORE,
			A.HOME_FULL_SCORE,
			A.GUEST_FULL_SCORE,
			A.[STATUS],
			A.ADDITIONALSTATUS,
			A.RECOMMENDMATCH,
			A.CREATE_USER,
			A.CREATE_TIME,
			A.LAST_UPDATE_USER,
			A.LAST_UPDATE_TIME,
			T1.EVENT_TEAM_NAME EVENT_HOME_TEAM_NAME,
			T1.EVENT_TEAM_NAME_EN EVENT_HOME_TEAM_NAME_EN,
			T2.EVENT_TEAM_NAME EVENT_GUEST_TEAM_NAME,
			T2.EVENT_TEAM_NAME_EN EVENT_GUEST_TEAM_NAME_EN,
			A.IS_ZOUDI,
			A.First_Half_End_Time,
			A.Sec_Half_Start_Time,
			A.HandicapHalfDefault,
			A.HandicapFullDefault
	FROM dbo.TB_MATCH A
	LEFT JOIN TB_EVENT B ON A.EVENT_ID=B.EVENT_ID
	LEFT JOIN TB_EVENT_ZONE C ON B.EVENTZONE_ID=C.EVENTZONE_ID
	LEFT JOIN TB_EVENT_ITEM D ON C.EVENTITEM_ID=D.EVENTITEM_ID
	LEFT JOIN TB_EVENT_TYPE E ON D.EVENTTYPE_ID=E.EVENTTYPE_ID
	LEFT JOIN TB_EVENT_TEAM T1 ON A.EVENT_HOME_TEAM_ID=T1.EVENT_TEAM_ID
	LEFT JOIN TB_EVENT_TEAM T2 ON A.EVENT_HOME_GUEST_ID=T2.EVENT_TEAM_ID	
	WHERE A.STATUS IN(1,2,3,7) AND ADDITIONALSTATUS IN(1,3)
	ORDER BY A.MATCH_NAME

	SELECT A.MARKET_ID,
		A.MARKET_NAME,
		A.MARKET_NAME_EN,
		A.MATCH_ID,
		A.MARKET_TMP_ID,
		A.MARKET_FLAG,
		A.SCOREA,
		A.SCOREB,
		C.MARKET_TMP_NAME,
		C.MARKET_TMP_NAME_EN,
		C.MARKET_TMP_TYPE,				
		C.BET_TYPE_ID,
		D.BET_TYPE_NAME,
		D.BET_TYPE_NAME_EN,
		D.BET_BEFORE_GAME,
		D.BET_GAMING,
		B.MATCH_NAME,
		B.MATCH_NAME_EN,
		B.EVENT_ID,
		B.STATUS,
		B.ADDITIONALSTATUS		
	FROM TB_MATCH_MARKET A
	INNER JOIN TB_MATCH B ON A.MATCH_ID=B.MATCH_ID	
	LEFT JOIN TB_MARKET_TEMPLATE C ON A.MARKET_TMP_ID=C.MARKET_TMP_ID
	LEFT JOIN TB_BET_TYPE D ON C.BET_TYPE_ID=D.BET_TYPE_ID
	WHERE A.MARKET_ID IN(select market_id from dbo.udf_get_canuse_market_for_betting())
	ORDER BY A.MATCH_ID,C.BET_TYPE_ID
END
GO