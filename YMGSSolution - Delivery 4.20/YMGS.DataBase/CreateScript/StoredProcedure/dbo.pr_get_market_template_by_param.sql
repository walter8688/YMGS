IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_market_template_by_param]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_market_template_by_param]
GO
CREATE PROCEDURE [dbo].[pr_get_market_template_by_param]
    (
      @Bet_Type_Id int,
      @Market_Tmp_Name nvarchar(50),
      @Market_TMP_Type INT
    )
AS 
BEGIN
	SELECT MARKET_TMP_ID,MARKET_TMP_NAME,MARKET_TMP_NAME_EN,BET_TYPE_ID,MARKET_TMP_TYPE,HOMESCORE
		 ,AWAYSCORE,GOALS,SCOREA,SCOREB,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME
	FROM dbo.TB_MARKET_TEMPLATE
	WHERE (@Bet_Type_Id is null OR @Bet_Type_Id=-1 OR BET_TYPE_ID=@Bet_Type_Id)
		AND (MARKET_TMP_NAME LIKE '%' + @Market_Tmp_Name + '%')
		AND (@Market_TMP_Type IS NULL OR @Market_TMP_Type = -1 OR Market_Tmp_Type = @Market_TMP_Type)
END
GO