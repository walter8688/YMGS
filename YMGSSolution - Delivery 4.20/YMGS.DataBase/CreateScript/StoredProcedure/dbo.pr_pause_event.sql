IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_pause_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_pause_event]
GO
CREATE PROCEDURE [dbo].[pr_pause_event]
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
        IF @Status = 0 
            BEGIN
                UPDATE  dbo.TB_EVENT
                SET     [STATUS] = 1 ,
                        LAST_UPDATE_USER = @LastUpdateUser
                WHERE   EVENT_ID = @EventID 

				--���»�������
				exec pr_up_cache_object 2                
            END
        ELSE 
            BEGIN
                RAISERROR('��ǰ״̬,������ͣ!',16,1) WITH NOWAIT
                RETURN
            END
    END
GO