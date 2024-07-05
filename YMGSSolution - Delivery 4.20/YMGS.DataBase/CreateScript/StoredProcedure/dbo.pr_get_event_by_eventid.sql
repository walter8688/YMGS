/***
Create Date:2013/01/31
Description:根据赛事ID获取赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_event_by_eventid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_event_by_eventid]
    GO
CREATE PROCEDURE [pr_get_event_by_eventid] ( @Event_Id INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_EVENT
        WHERE   EVENT_ID = @Event_Id
    END
GO