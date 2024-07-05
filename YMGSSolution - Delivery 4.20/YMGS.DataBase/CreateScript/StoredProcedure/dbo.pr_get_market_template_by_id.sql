IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_market_template_by_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_market_template_by_id]
GO
CREATE PROCEDURE [dbo].[pr_get_market_template_by_id]
    (
      @Market_Tmp_id int
    )
AS 
BEGIN
	SELECT MARKET_TMP_ID,MARKET_TMP_NAME,MARKET_TMP_NAME_EN,BET_TYPE_ID,MARKET_TMP_TYPE,HOMESCORE
		 ,AWAYSCORE,GOALS,SCOREA,SCOREB,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME
	FROM dbo.TB_MARKET_TEMPLATE
	WHERE MARKET_TMP_ID=@Market_Tmp_id
END
GO