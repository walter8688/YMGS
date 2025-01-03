IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_abort_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_abort_event]
GO
CREATE PROCEDURE [dbo].[pr_abort_event]
    (
      @EventID INT ,
      @LastUpdateUser INT
    )
AS 
    BEGIN
        DECLARE @Status INT
        SELECT  @Status = [STATUS]
        FROM    dbo.TB_EVENT
        WHERE   EVENT_ID = @EventID
        IF @Status = 2 
            BEGIN
                RAISERROR('当前状态已经是终止状态!',16,1) WITH NOWAIT
                RETURN
            END
        UPDATE  dbo.TB_EVENT
        SET     [STATUS] = 2 ,
                LAST_UPDATE_USER = @LastUpdateUser
        WHERE   EVENT_ID = @EventID 

		--更新缓存对象表
		exec pr_up_cache_object 2
    END
GO