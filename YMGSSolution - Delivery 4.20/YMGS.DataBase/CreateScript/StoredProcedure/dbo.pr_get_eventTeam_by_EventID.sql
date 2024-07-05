/***
Create Date:2013/01/22
Description:根据赛事获取赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_eventTeam_by_EventID]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_eventTeam_by_EventID]
go
CREATE PROCEDURE [dbo].[pr_get_eventTeam_by_EventID] ( @Event_ID INT )
AS 
    BEGIN
        SELECT  team.EVENT_ITEM_ID ,
                team.EVENT_TEAM_ID ,
                team.EVENT_TEAM_NAME ,
                team.EVENT_TEAM_NAME_EN ,
                ( team.EVENT_TEAM_NAME + '|' + team.EVENT_TEAM_NAME_EN ) EVENT_TEAM_NAME_ALL ,
                team.EVENT_TEAM_TYPE1 ,
                team.EVENT_TEAM_TYPE2 ,
                team.LAST_UPDATE_TIME ,
                team.LAST_UPDATE_USER ,
                team.STATUS ,
                team.CREATE_TIME ,
                team.CREATE_USER
        FROM    dbo.TB_EVENT_TEAM_MAP eventTeam
                INNER JOIN dbo.TB_EVENT_TEAM team ON eventTeam.TEAM_ID = team.EVENT_TEAM_ID
        WHERE   eventTeam.EVENT_ID = @Event_ID
                AND team.[STATUS] = 0
        ORDER BY team.EVENT_TEAM_NAME
    END
GO