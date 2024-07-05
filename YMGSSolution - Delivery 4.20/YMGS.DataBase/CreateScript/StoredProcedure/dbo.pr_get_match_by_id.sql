IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_match_by_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_match_by_id]
GO
CREATE PROCEDURE [dbo].[pr_get_match_by_id]
    (
      @Match_Id int
    )
AS 
BEGIN
	SELECT	MATCH_ID,
			MATCH_NAME,
			MATCH_NAME_EN,
			MATCH_DESC,
			EVENT_ID,
			EVENT_HOME_TEAM_ID,
			EVENT_HOME_GUEST_ID,
			STARTDATE,ENDDATE,
			AUTO_FREEZE_DATE,
			HOME_FIR_HALF_SCORE,
			GUEST_FIR_HALF_SCORE,
			HOME_SEC_HALF_SCORE,
			GUEST_SEC_HALF_SCORE,
			HOME_OVERTIME_SCORE,
			GUEST_OVERTIME_SCORE,
			HOME_POINT_SCORE,
			GUEST_POINT_SCORE,
			HOME_FULL_SCORE,
			GUEST_FULL_SCORE,
			RECOMMENDMATCH,
			IS_ZOUDI,
			[STATUS],
			ADDITIONALSTATUS,
			CREATE_USER,
			CREATE_TIME,
			LAST_UPDATE_USER,
			LAST_UPDATE_TIME,
			HandicapHalfDefault,
			HandicapFullDefault,
			ISNULL(SETTLE_STATUS,0) SETTLE_STATUS
	FROM dbo.TB_MATCH
	WHERE MATCH_ID=@Match_Id

	SELECT MARKET_ID,MARKET_NAME,MATCH_ID,MARKET_TMP_ID,MARKET_FLAG,SCOREA,SCOREB,MARKET_STATUS
	FROM dbo.TB_MATCH_MARKET
	WHERE MATCH_ID=@Match_Id
END
GO