iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_modify_match_datetime]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_modify_match_datetime]
GO
CREATE PROCEDURE [dbo].[pr_modify_match_datetime]
    (
	  @Match_Id int,
      @StartDate datetime,
      @EndDate datetime,
      @Auto_Freeze_Date datetime,
      @Last_Update_User int
    )
AS 
BEGIN
	declare @temp_status int
	declare @temp_additional_status int
	select @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS 
	FROM TB_MATCH WHERE MATCH_ID=@Match_Id
	if(@temp_status<>0 AND @temp_status<>1)
	begin
		RAISERROR ('只有未激活和已激活的比赛才能修改比赛时间!' , 16, 1) WITH NOWAIT		
		RETURN;
	end

	UPDATE dbo.TB_MATCH
	SET STARTDATE=@StartDate,
		ENDDATE=@EndDate,
		AUTO_FREEZE_DATE=@Auto_Freeze_Date,
		LAST_UPDATE_USER=@Last_Update_User,
		LAST_UPDATE_TIME=getdate()
	WHERE MATCH_ID=@Match_Id

	--更新缓存对象表
	exec pr_up_cache_object 3
END
GO