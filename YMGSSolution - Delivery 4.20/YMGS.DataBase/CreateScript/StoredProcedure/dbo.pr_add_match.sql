IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_match]
GO
CREATE PROCEDURE [dbo].[pr_add_match]
    (
      @Match_Name NVARCHAR(100) ,
	  @Match_Name_En NVARCHAR(100),
      @Match_Desc NVARCHAR(100),
      @Event_Id int,      
      @Event_Home_Team_Id int,
      @Event_Home_Guest_Id int,      
      @StartDate datetime,
      @EndDate datetime,
      @Auto_Freeze_Date datetime,
      @Recommend_Match bit,
      @Status int,
      @Additional_Status int,
      @Create_User int,
      @Last_Update_User INT,
      @Is_ZouDi BIT,
      @HandicapHalfDefault NVARCHAR(50),
	  @HandicapFullDefault NVARCHAR(50)
    )
AS 
BEGIN
	if(@Match_Name is null)
	begin
		RAISERROR ('比赛中文名称不能为空!' , 16, 1) WITH NOWAIT
		return		
	end

	if(@Match_Name_En is null)
	begin
		RAISERROR('比赛英文名称不能为空!',16,1) WITH NOWAIT
		return
	end

	/*IF(Exists(SELECT * FROM dbo.TB_MATCH WHERE MATCH_NAME=@Match_Name))
	begin
		RAISERROR ('相同中文比赛名称的比赛已经存在!' , 16, 1) WITH NOWAIT
		return
	end

	IF(Exists(SELECT * FROM dbo.TB_MATCH WHERE MATCH_NAME_EN=@Match_Name_En))
	begin
		RAISERROR ('相同英文比赛名称的比赛已经存在!' , 16, 1) WITH NOWAIT
		return
	end*/
	
	DECLARE @Event_Start_Date DATETIME
	DECLARE @Event_End_Date DATETIME
	
	SELECT @Event_Start_Date = [START_DATE],@Event_End_Date = END_DATE 
	FROM dbo.TB_EVENT WHERE EVENT_ID = @Event_Id
	
	IF @StartDate < @Event_Start_Date
	BEGIN
		RAISERROR ('比赛开始时间不能早于赛事开始时间' , 16, 1) WITH NOWAIT
		return
	END
	
	IF @EndDate > @Event_End_Date
	BEGIN
		RAISERROR ('比赛结束时间不能晚于赛事结束时间' , 16, 1) WITH NOWAIT
		return
	END
	
	DECLARE @Event_Status INT
	
	SELECT @Event_Status = STATUS FROM dbo.TB_EVENT WHERE EVENT_ID = @Event_Id
	IF @Event_Status = 2
	BEGIN
		RAISERROR ('相关赛事已经结束,比赛不能新增' , 16, 1) WITH NOWAIT
		return
	END
	

	INSERT INTO dbo.TB_MATCH(MATCH_NAME,MATCH_NAME_EN,MATCH_DESC,EVENT_ID,EVENT_HOME_TEAM_ID,EVENT_HOME_GUEST_ID,				
				STARTDATE,ENDDATE,AUTO_FREEZE_DATE,
				RECOMMENDMATCH,IS_ZOUDI,STATUS,ADDITIONALSTATUS,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,
				HandicapHalfDefault,HandicapFullDefault)
	VALUES(@Match_Name,@Match_Name_En,@Match_Desc,@Event_Id,@Event_Home_Team_Id,@Event_Home_Guest_Id,
			@StartDate,@EndDate,@Auto_Freeze_Date,
			@Recommend_Match,@Is_ZouDi,@Status,@Additional_Status,@Create_User,getdate(),@Last_Update_User,getdate(),
			@HandicapHalfDefault,@HandicapFullDefault)
	SELECT SCOPE_IDENTITY()

	--更新缓存
	exec pr_up_cache_object 3
END
GO