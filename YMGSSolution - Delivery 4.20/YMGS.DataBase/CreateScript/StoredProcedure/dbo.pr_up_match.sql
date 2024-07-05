IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_match]
GO
CREATE PROCEDURE [dbo].[pr_up_match]
    (
	  @Match_Id int,
      @Match_Name NVARCHAR(100) ,
	  @Match_Name_En NVARCHAR(100) ,
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
      @Last_Update_User INT,
      @Is_ZouDi BIT,
      @HandicapHalfDefault NVARCHAR(50),
	  @HandicapFullDefault NVARCHAR(50)
    )
AS 
BEGIN
	if(@Match_Name is null)
	begin
		RAISERROR ('�����������Ʋ���Ϊ��!' , 16, 1) WITH NOWAIT
		return		
	END
	
	if(@Match_Name_En is null)
	begin
		RAISERROR('����Ӣ�����Ʋ���Ϊ��!',16,1) WITH NOWAIT
		return
	end

	/*IF(Exists(SELECT * FROM dbo.TB_MATCH WHERE MATCH_NAME=@Match_Name AND MATCH_ID<>@Match_Id))
	begin
		RAISERROR ('��ͬ���ı������Ƶı����Ѿ�����!' , 16, 1) WITH NOWAIT
		return
	end

	IF(Exists(SELECT * FROM dbo.TB_MATCH WHERE MATCH_NAME_EN=@Match_Name_En AND MATCH_ID<>@Match_Id))
	begin
		RAISERROR ('��ͬӢ�ı������Ƶı����Ѿ�����!' , 16, 1) WITH NOWAIT
		return
	END*/
	
	DECLARE @Event_Start_Date DATETIME
	DECLARE @Event_End_Date DATETIME
	
	SELECT @Event_Start_Date = [START_DATE],@Event_End_Date = END_DATE 
	FROM dbo.TB_EVENT WHERE EVENT_ID = @Event_Id
	
	IF @StartDate < @Event_Start_Date
	BEGIN
		RAISERROR ('������ʼʱ�䲻���������¿�ʼʱ��' , 16, 1) WITH NOWAIT
		return
	END
	
	IF @EndDate > @Event_End_Date
	BEGIN
		RAISERROR ('��������ʱ�䲻���������½���ʱ��' , 16, 1) WITH NOWAIT
		return
	END

	declare @temp_status int
	declare @temp_additional_status int
	select @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS 
	FROM TB_MATCH WHERE MATCH_ID=@Match_Id
	if(@temp_status<>0)
	begin
		RAISERROR ('��ǰ״̬�²��ܸ��±���!' , 16, 1) WITH NOWAIT	
		return	
	end

	UPDATE dbo.TB_MATCH
	SET MATCH_NAME=@Match_Name,
		MATCH_NAME_EN=@Match_Name_En,
	    MATCH_DESC=@Match_Desc,
	    EVENT_ID=@Event_Id,
	    EVENT_HOME_TEAM_ID=@Event_Home_Team_Id,
	    EVENT_HOME_GUEST_ID=@Event_Home_Guest_Id,				
		STARTDATE=@StartDate,
		ENDDATE=@EndDate,
		AUTO_FREEZE_DATE=@Auto_Freeze_Date,
		RECOMMENDMATCH=@Recommend_Match,
		IS_ZOUDI = @Is_ZouDi,
		[STATUS]=@Status,
		ADDITIONALSTATUS=@Additional_Status,
		LAST_UPDATE_USER=@Last_Update_User,
		LAST_UPDATE_TIME=getdate(),
		HandicapHalfDefault = @HandicapHalfDefault,
	    HandicapFullDefault = @HandicapFullDefault
	WHERE MATCH_ID=@Match_Id

	--���±�������
	exec pr_up_cache_object 3
END
GO