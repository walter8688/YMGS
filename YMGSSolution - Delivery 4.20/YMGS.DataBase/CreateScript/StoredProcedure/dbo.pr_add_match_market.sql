IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_match_market]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_match_market]
GO
CREATE PROCEDURE [dbo].[pr_add_match_market]
    (
      @Market_Name NVARCHAR(200) ,
	  @Market_Name_En NVARCHAR(200),
      @Match_Id int,
      @Market_Tmp_Id int,      
      @Market_Flag int,
      @ScoreA decimal(18,1),
      @ScoreB decimal(18,1)
    )
AS 
BEGIN
	INSERT INTO dbo.TB_MATCH_MARKET(MARKET_NAME,MARKET_NAME_EN,MATCH_ID,MARKET_TMP_ID,MARKET_FLAG,SCOREA,SCOREB)				
	VALUES(@Market_Name,@Market_Name_En,@Match_Id,@Market_Tmp_Id,@Market_Flag,@ScoreA,@ScoreB)
	SELECT SCOPE_IDENTITY()
END
GO