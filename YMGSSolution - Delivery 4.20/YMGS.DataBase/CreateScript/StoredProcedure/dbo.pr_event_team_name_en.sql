IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_event_team_name_en]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_event_team_name_en]
go
CREATE PROCEDURE [dbo].[pr_event_team_name_en]
    (
      @HomeTeamId INT ,
      @GuestTeamId INT
    )
AS 
    BEGIN
        SELECT  EVENT_TEAM_NAME_EN
        FROM    ( SELECT    EVENT_TEAM_NAME_EN ,
                            1 ordNo
                  FROM      dbo.TB_EVENT_TEAM
                  WHERE     EVENT_TEAM_ID = @HomeTeamId
                  UNION
                  SELECT    EVENT_TEAM_NAME_EN ,
                            2 ordNu
                  FROM      dbo.TB_EVENT_TEAM
                  WHERE     EVENT_TEAM_ID = @GuestTeamId
                ) temp
        ORDER BY ordNo
    END
GO