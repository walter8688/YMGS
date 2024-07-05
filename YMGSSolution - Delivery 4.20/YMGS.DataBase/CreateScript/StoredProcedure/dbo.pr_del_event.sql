/***
Create Date:2013/01/21
Description:删除赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_event]
go
CREATE PROCEDURE [dbo].[pr_del_event] ( @EVENT_ID INT )
AS 
    DECLARE @Count INT ,
        @Error_Msg NVARCHAR(200)
    BEGIN
		--判断赛事是否和比赛相关联
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_MATCH
        WHERE   EVENT_ID = @Event_ID
        IF @Count > 0 
            BEGIN
                SET @Error_Msg = '赛事已经和比赛相关联，不能删除!'
                RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                RETURN
            END
    
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_CHAMP_EVENT
        WHERE   Event_ID = @EVENT_ID
        IF @Count > 0 
            BEGIN
                SET @Error_Msg = '赛事已经和比赛相关联，不能删除!'
                RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                RETURN
            END
    
        DELETE  FROM dbo.TB_EVENT_TEAM_MAP
        WHERE   EVENT_ID = @EVENT_ID
        DELETE  FROM dbo.TB_EVENT
        WHERE   EVENT_ID = @EVENT_ID

		--更新缓存对象表
        EXEC pr_up_cache_object 2
    END
 GO