iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_recommend_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_recommend_match]
GO
CREATE PROCEDURE [dbo].[pr_recommend_match]
    (
	  @Match_Id int,
	  @Is_Recommend bit,
	  @Last_Update_User int
    )
AS 
BEGIN
	--处于终止、已计算、比赛结束状态的比赛不能推荐或取消推荐
	DECLARE @temp_status int
	DECLARE @temp_additional_status int
	SELECT @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS
	FROM TB_MATCH
	WHERE MATCH_ID=@Match_Id
	
	IF(@temp_status=0 or @temp_status=4 OR @temp_status=5 OR @temp_status=6)
	BEGIN
		RAISERROR ('当前比赛状态，不能进行推荐或取消推荐!' , 16, 1) WITH NOWAIT
		RETURN
	END
	ELSE
	BEGIN
		UPDATE dbo.TB_MATCH
		SET 
			RECOMMENDMATCH=@Is_Recommend,
			LAST_UPDATE_USER=@Last_Update_User,
			LAST_UPDATE_TIME=getdate()
		WHERE MATCH_ID=@Match_Id

		--更新缓存对象表
		exec pr_up_cache_object 3
	END
END
GO
