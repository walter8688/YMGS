IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_market_template]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_market_template]
GO
CREATE PROCEDURE [dbo].[pr_up_market_template]
    (
      @Market_Tmp_Id INT ,
      @Market_Tmp_Name NVARCHAR(50) ,
	  @Market_Tmp_Name_En NVARCHAR(50) ,
      @Bet_Type_Id int,
      @Market_Tmp_Type int,
      @Home_Score int,
      @Away_Score int,
      @Goals decimal(18,1),
      @ScoreA decimal(18,1),
      @ScoreB decimal(18,1),
      @Last_Update_User int
    )
AS 
BEGIN
	if(@Market_Tmp_Name is null)
	begin
		RAISERROR ('市场模板名称不能为空!' , 16, 1) WITH NOWAIT
		return		
	end

	if(len(@Market_Tmp_Name)>50)
	begin
		RAISERROR ('模板中文名称长度不能超过50' , 16, 1) WITH NOWAIT
		return		
	end

	if(len(@Market_Tmp_Name_En)>100)
	begin
		RAISERROR ('模板英文名称不能超过100' , 16, 1) WITH NOWAIT
		return		
	end

	if(exists(select * from tb_market_template
		 where market_tmp_name=@Market_Tmp_Name and bet_type_id=@Bet_Type_Id
		 and MARKET_TMP_TYPE=@Market_Tmp_Type and MARKET_TMP_ID<>@Market_Tmp_Id))
	begin
		RAISERROR('当前模板已经存在!',16,1) with nowait
		return
	end
	
	if(exists(select * from tb_market_template
		 where market_tmp_name_en=@Market_Tmp_Name_En and bet_type_id=@Bet_Type_Id
		 and MARKET_TMP_TYPE=@Market_Tmp_Type and MARKET_TMP_ID<>@Market_Tmp_Id))
	begin
		RAISERROR('当前模板已经存在!',16,1) with nowait
		return
	end

    UPDATE  dbo.TB_MARKET_TEMPLATE
    SET     MARKET_TMP_NAME = @Market_Tmp_Name ,
			MARKET_TMP_NAME_EN = @Market_Tmp_Name_En,
            BET_TYPE_ID =@Bet_Type_Id,
            MARKET_TMP_TYPE=@Market_Tmp_Type,
            HOMESCORE=@Home_Score,
            AWAYSCORE=@Away_Score,
            GOALS=@Goals,
            SCOREA=@ScoreA,
            SCOREB=@ScoreB,
            LAST_UPDATE_USER=@Last_Update_User,
            LAST_UPDATE_TIME=GETDATE()
    WHERE   MARKET_TMP_ID = @Market_Tmp_Id 
END
GO