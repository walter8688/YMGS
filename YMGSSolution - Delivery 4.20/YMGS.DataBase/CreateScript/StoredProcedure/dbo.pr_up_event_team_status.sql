/***
Create Date:2013/01/23
Description:更新赛事成员状态
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_event_team_status]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_event_team_status]
GO
CREATE PROCEDURE [dbo].[pr_up_event_team_status]
    (
      @Event_Team_ID INT ,
      @Event_Team_Status INT
    )
AS 
    BEGIN
        UPDATE  dbo.TB_EVENT_TEAM
        SET     [STATUS] = @Event_Team_Status
        WHERE   EVENT_TEAM_ID = @Event_Team_ID
    END
GO