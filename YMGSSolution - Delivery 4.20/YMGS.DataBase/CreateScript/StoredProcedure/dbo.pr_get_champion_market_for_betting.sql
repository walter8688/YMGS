IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champion_market_for_betting]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champion_market_for_betting]
GO
CREATE PROCEDURE [dbo].[pr_get_champion_market_for_betting]
AS 
BEGIN
	SELECT A.CHAMP_EVENT_ID,
		A.CHAMP_EVENT_TYPE,
		A.EVENT_ID,
		A.CHAMP_EVENT_NAME,
		A.CHAMP_EVENT_NAME_EN,
		A.CHAMP_EVENT_DESC,
		A.CHAMP_EVENT_STARTDATE,
		A.CHAMP_EVENT_ENDDATE,
		A.CHAMP_EVENT_STATUS,
		A.CREATE_USER,
		A.CREATE_TIME,
		A.LAST_UPDATE_USER,
		A.LAST_UPDATE_TIME,
		B.EVENT_NAME,
		B.EVENT_NAME_EN,
		B.EVENTZONE_ID		
	FROM TB_CHAMP_EVENT A
	LEFT JOIN TB_EVENT B ON A.EVENT_ID=B.EVENT_ID
	WHERE A.CHAMP_EVENT_STATUS=1
	ORDER BY CHAMP_EVENT_STARTDATE
	
	SELECT A.CHAMP_MARKET_ID,
		A.CHAMP_EVENT_ID,
		A.CHAMP_MEMBER_ID,
		C.CHAMP_EVENT_MEMBER_NAME,
		C.CHAMP_EVENT_MEMBER_NAME_EN
	FROM TB_CHAMP_MARKET A
	INNER JOIN TB_CHAMP_EVENT B ON A.CHAMP_EVENT_ID=B.CHAMP_EVENT_ID
	LEFT JOIN TB_CHAMP_EVENT_MEMBER C ON C.CHAMP_EVENT_MEMBER_ID=A.CHAMP_MEMBER_ID
	WHERE B.CHAMP_EVENT_STATUS=1
	ORDER BY CHAMP_MEMBER_ID
		
END
GO