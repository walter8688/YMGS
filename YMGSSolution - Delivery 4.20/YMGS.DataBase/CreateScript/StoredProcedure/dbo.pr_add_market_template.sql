IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_market_template]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_market_template]
GO
CREATE PROCEDURE [dbo].[pr_add_market_template]
    (
      @Market_Tmp_Name NVARCHAR(50) ,
	  @Market_Tmp_Name_En NVARCHAR(100),
      @Bet_Type_Id int,
      @Market_Tmp_Type int,
      @Home_Score int,
      @Away_Score int,
      @Goals decimal(18,1),
      @ScoreA decimal(18,1),
      @ScoreB decimal(18,1),
      @Create_User int,
      @Last_Update_User int
    )
AS 
BEGIN
	if(@Market_Tmp_Name is null)
	begin
		RAISERROR ('模板中文名称不能为空' , 16, 1) WITH NOWAIT
		return		
	end

	if(@Market_Tmp_Name_En is null)
	begin
		RAISERROR ('模板英文名称不能为空' , 16, 1) WITH NOWAIT
		return
	end

	if(len(@Market_Tmp_Name)>50)
	begin
		RAISERROR ('模板中文名称不长度不能超过50' , 16, 1) WITH NOWAIT
		return		
	end

	if(len(@Market_Tmp_Name_En)>100)
	begin
		RAISERROR ('模板英文名称不长度不能超过50' , 16, 1) WITH NOWAIT
		return		
	end
	
	if(exists(select * from tb_market_template
		 where market_tmp_name=@Market_Tmp_Name and bet_type_id=@Bet_Type_Id
		 and MARKET_TMP_TYPE=@Market_Tmp_Type))
	begin
		RAISERROR('相同名称的模板已经存在!',16,1) with nowait
		return
	end
	
	if(exists(select * from tb_market_template
		 where market_tmp_name_en=@Market_Tmp_Name_En and bet_type_id=@Bet_Type_Id
		 and MARKET_TMP_TYPE=@Market_Tmp_Type))
	begin
		RAISERROR('相同名称的模板已经存在!',16,1) with nowait
		return
	end

	INSERT INTO dbo.TB_MARKET_TEMPLATE(MARKET_TMP_NAME,MARKET_TMP_NAME_EN,BET_TYPE_ID,MARKET_TMP_TYPE,HOMESCORE
				,AWAYSCORE,GOALS,SCOREA,SCOREB,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
	VALUES(@Market_Tmp_Name,@Market_Tmp_Name_En,@Bet_Type_Id,@Market_Tmp_Type,@Home_Score
			,@Away_Score,@Goals,@ScoreA,@ScoreB,@Create_User,getdate(),@Last_Update_User,getdate())
	SELECT SCOPE_IDENTITY()
END
GO