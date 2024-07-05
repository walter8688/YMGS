iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_suspend_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_suspend_match]
GO
CREATE PROCEDURE [dbo].[pr_suspend_match]
    (
	  @Match_Id int,
	  @Last_Update_User int
    )
AS 
BEGIN
	--只有处于已激活，比赛开始， 半场已结束，并且辅助状态为正常的才能暂停

	DECLARE @temp_status int
	DECLARE @temp_additional_status int
	SELECT @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS
	FROM TB_MATCH
	WHERE MATCH_ID=@Match_Id
	
	IF((@temp_status=1 OR @temp_status=2 OR @temp_status=3) AND @temp_additional_status=1)
	BEGIN
		UPDATE dbo.TB_MATCH
		SET ADDITIONALSTATUS=2,
			LAST_UPDATE_USER=@Last_Update_User,
			LAST_UPDATE_TIME=getdate()
		WHERE MATCH_ID=@Match_Id

		--更新缓存对象表
		exec pr_up_cache_object 3
	END
	ELSE
	BEGIN
		RAISERROR ('当前比赛不能暂停!' , 16, 1) WITH NOWAIT
		RETURN
	END
END
GO