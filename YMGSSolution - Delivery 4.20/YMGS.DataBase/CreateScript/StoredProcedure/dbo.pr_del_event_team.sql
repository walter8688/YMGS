/***
Create Date:2013/01/21
Description:删除赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_event_team]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_event_team]
GO
CREATE PROCEDURE pr_del_event_team ( @Event_Team_ID INT )
AS 
    BEGIN
        DECLARE @Count INT ,
            @RaisErrorCode NVARCHAR(50)
	--判断参赛成员是否被赛事所引用
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM_MAP
        WHERE   TEAM_ID = @Event_Team_ID
	--被引用不能被删除	
        IF @Count > 0 
            BEGIN
                SET @RaisErrorCode = '参赛成员已被引用，不能被删除!'
                RAISERROR (@RaisErrorCode,16,1) WITH NOWAIT
                RETURN 
            END
        ELSE--未被引用可以删除
            BEGIN
                DELETE  FROM dbo.TB_EVENT_TEAM
                WHERE   EVENT_TEAM_ID = @Event_Team_ID
            END
    END
GO