IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_sechalfstart_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_sechalfstart_match]
GO
CREATE PROCEDURE [dbo].[pr_sechalfstart_match]
    (
      @Match_Id INT ,
      @Last_Update_User INT 
    )
AS 
    BEGIN
        DECLARE @temp_status INT
        DECLARE @temp_additional_status INT
        DECLARE @temp_match_start_date DATETIME
        SELECT  @temp_status = [STATUS] ,
                @temp_additional_status = ADDITIONALSTATUS ,
                @temp_match_start_date = STARTDATE
        FROM    TB_MATCH
        WHERE   MATCH_ID = @Match_Id
	
	
        IF ( @temp_additional_status = 1 AND @temp_status = 3 ) 
            BEGIN
                UPDATE  dbo.TB_MATCH
                SET     [STATUS] = 7 ,
                        LAST_UPDATE_USER = @Last_Update_User ,
                        LAST_UPDATE_TIME = GETDATE(),
                        Sec_Half_Start_Time = GETDATE()
                WHERE   MATCH_ID = @Match_Id

		--更新缓存
                EXEC pr_up_cache_object 3
            END
        ELSE 
            BEGIN
                RAISERROR ('当前状态不能开始下半场比赛' , 16, 1) WITH NOWAIT
                RETURN
            END
    END
GO