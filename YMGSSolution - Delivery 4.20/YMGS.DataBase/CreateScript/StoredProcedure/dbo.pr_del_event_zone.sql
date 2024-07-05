/***
Create Date:2013/01/17
Description:删除赛事区域
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_event_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_event_zone]
GO
CREATE PROCEDURE [dbo].[pr_del_event_zone] ( @Event_Zone_ID INT )
AS 
    BEGIN
		--检查赛事下是否有相关比赛
        DECLARE @Temp_Count INT
		
        SELECT  @Temp_Count = COUNT(1)
        FROM    dbo.TB_EVENT
        WHERE   EVENTZONE_ID = @Event_Zone_ID
		
        IF ( @Temp_Count <> 0 ) 
            BEGIN
                RAISERROR('赛事区域已于赛事关联,不能删除!',16,1) WITH NOWAIT
                RETURN
            END
		
        DELETE  FROM dbo.TB_EVENT_ZONE
        WHERE   EVENTZONE_ID = @Event_Zone_ID
		--更新缓存对象表
        EXEC pr_up_cache_object 1
    END
GO