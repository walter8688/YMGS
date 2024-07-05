IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_match]
GO
CREATE PROCEDURE [dbo].[pr_del_match]
    (
	  @Match_Id int
    )
AS 
BEGIN
	DECLARE @temp_status int
	DECLARE @temp_additional_status int
	SELECT @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS
	FROM TB_MATCH
	WHERE MATCH_ID=@Match_Id
	
	IF(@temp_status<>0)
	BEGIN
		RAISERROR ('只有未激活状态的比赛才能被删除，当前比赛不能被删除!' , 16, 1) WITH NOWAIT
		RETURN;
	END
	ELSE
	BEGIN
		DELETE FROM dbo.TB_MATCH WHERE MATCH_ID=@Match_Id
		DELETE FROM dbo.TB_MATCH_MARKET WHERE MATCH_ID=@Match_Id
		--更新缓存对象表
		exec pr_up_cache_object 3
	END
END
GO