IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_freezing_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_freezing_match]
GO
CREATE PROCEDURE pr_freezing_match
    (
      @Match_Id INT ,
      @Last_Update_User_Id INT
    )
AS 
    BEGIN
        DECLARE @Cur_Status INT ,
            @Cur_Addtional_status INT
        SELECT  @Cur_Status = [STATUS] ,
                @Cur_Addtional_status = ADDITIONALSTATUS
        FROM    dbo.TB_MATCH
        WHERE   MATCH_ID = @Match_Id
        
        IF ( @Cur_Addtional_status = 1
             AND (@Cur_Status = 1 OR @Cur_Status = 2 OR @Cur_Status = 3 or @Cur_Status = 7)
           ) 
            BEGIN
                UPDATE  dbo.TB_MATCH
                SET     ADDITIONALSTATUS = 3 ,
                        LAST_UPDATE_USER = @Last_Update_User_Id ,
                        LAST_UPDATE_TIME = GETDATE()
                WHERE   MATCH_ID = @Match_Id
        
        --更新缓存对象表
                EXEC pr_up_cache_object 3
            END
        ELSE 
            BEGIN
                RAISERROR('当前状态下不能封盘比赛',16,1) WITH NOWAIT
            END
    END
GO