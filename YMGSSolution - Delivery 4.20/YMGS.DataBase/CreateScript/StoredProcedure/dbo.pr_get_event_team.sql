/***
Create Date:2013/01/@Param_Zone_ID
Description:获取赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_event_team]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_event_team]
go
CREATE PROCEDURE [dbo].[pr_get_event_team]
    (
      @Event_Item_ID INT ,
      @Event_Team_Name NVARCHAR(50) ,
      @Event_Team_Name_EN NVARCHAR(50) ,
      @Event_Team_Type1 INT ,
      @Event_Team_Type2 INT ,
      @Status INT ,
      @Param_Zone_ID INT 
    )
AS 
    BEGIN
        SELECT  TB_EVENT_TEAM.EVENT_TEAM_ID ,
                TB_EVENT_TEAM.EVENT_ITEM_ID ,
                TB_EVENT_TEAM.EVENT_TEAM_NAME ,
                TB_EVENT_TEAM.EVENT_TEAM_NAME_EN ,
                TB_EVENT_TEAM.EVENT_TEAM_TYPE1 ,
                TB_EVENT_TEAM.EVENT_TEAM_TYPE2 ,
                CASE TB_EVENT_TEAM.[STATUS]
                  WHEN 0 THEN '启用'
                  WHEN 1 THEN '禁用'
                END STATUSNAME ,
                TB_EVENT_TEAM.[STATUS] ,
                TB_EVENT_ITEM.EventItem_Name ,
                Param1.PARAM_NAME PARAMNAME1 ,
                Param2.PARAM_NAME PARAMNAME2 ,
                Param1.PARAM_ID PARAMID1 ,
                Param2.PARAM_ID PARAMID2 ,
                dbo.TB_PARAM_ZONE.ZONE_NAME ,
                dbo.TB_EVENT_TEAM.PARAM_ZONE_ID
        FROM    dbo.TB_EVENT_TEAM
                LEFT OUTER JOIN dbo.TB_EVENT_ITEM ON TB_EVENT_TEAM.EVENT_ITEM_ID = TB_EVENT_ITEM.EventItem_ID
                LEFT OUTER JOIN dbo.TB_PARAM_PARAM Param1 ON TB_EVENT_TEAM.EVENT_TEAM_TYPE1 = Param1.PARAM_ID
                LEFT OUTER JOIN dbo.TB_PARAM_PARAM Param2 ON TB_EVENT_TEAM.EVENT_TEAM_TYPE2 = Param2.PARAM_ID
                LEFT OUTER JOIN dbo.TB_PARAM_ZONE ON TB_EVENT_TEAM.PARAM_ZONE_ID = dbo.TB_PARAM_ZONE.ZONE_ID
        WHERE   ( TB_EVENT_TEAM.EVENT_ITEM_ID = @Event_Item_ID
                  OR @Event_Item_ID = -1
                )
                AND ( TB_EVENT_TEAM.EVENT_TEAM_NAME LIKE '%'
                      + @Event_Team_Name + '%'
                      OR @Event_Team_Name = ''
                    )
                AND ( TB_EVENT_TEAM.EVENT_TEAM_TYPE1 = @Event_Team_Type1
                      OR @Event_Team_Type1 = -1
                    )
                AND ( TB_EVENT_TEAM.EVENT_TEAM_TYPE2 = @Event_Team_Type2
                      OR @Event_Team_Type2 = -1
                    )
                AND ( TB_EVENT_TEAM.[STATUS] = @Status
                      OR @Status = -1
                    )
                AND ( dbo.TB_EVENT_TEAM.EVENT_TEAM_NAME_EN LIKE '%'
                      + @Event_Team_Name_EN + '%'
                      OR @Event_Team_Name_EN = ''
                    )
                AND ( dbo.TB_EVENT_TEAM.PARAM_ZONE_ID IN
					  (SELECT temp.ZONE_ID FROM 
					  (SELECT p3.ZONE_ID FROM dbo.TB_PARAM_ZONE p1 INNER JOIN dbo.TB_PARAM_ZONE p2 ON p1.ZONE_ID = p2.PARENT_ZONE_ID
					  INNER JOIN dbo.TB_PARAM_ZONE p3 ON p3.PARENT_ZONE_ID = p2.ZONE_ID WHERE p1.ZONE_ID = @Param_Zone_ID
					  UNION ALL
					  SELECT p2.ZONE_ID FROM dbo.TB_PARAM_ZONE p1 INNER JOIN dbo.TB_PARAM_ZONE p2
					  ON p1.ZONE_ID = p2.PARENT_ZONE_ID WHERE p1.ZONE_ID = @Param_Zone_ID
					  UNION ALL
					  SELECT p1.ZONE_ID FROM TB_PARAM_ZONE p1 WHERE p1.ZONE_ID = @Param_Zone_ID) temp )	
                      OR @Param_Zone_ID IS NULL
                      OR @Param_Zone_ID = -1
                    )
        ORDER BY TB_EVENT_TEAM.EVENT_ITEM_ID ,
                TB_EVENT_TEAM.EVENT_TEAM_NAME
    END
GO