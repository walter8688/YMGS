/***
Create Date:2013/01/17
Description:获取赛事区域
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_event_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_event_zone]
go
CREATE PROCEDURE [dbo].[pr_get_event_zone]
    (
      @Event_Item_ID INT ,
      @Event_Zone_Name NVARCHAR(40) ,
      @Event_Zone_Name_En NVARCHAR(40) ,
      @Event_Zone_Desc NVARCHAR(100) ,
      @Param_Zone_Id INT
    )
AS 
    BEGIN
        SELECT  TB_EVENT_ZONE.EVENTZONE_ID ,
                TB_EVENT_ZONE.EVENTITEM_ID ,
                RTRIM(LTRIM(TB_EVENT_ZONE.EVENTZONE_NAME)) EVENTZONE_NAME ,
                RTRIM(LTRIM(TB_EVENT_ZONE.EVENTZONE_NAME_EN)) EVENTZONE_NAME_EN ,
                TB_EVENT_ZONE.EVENTZONE_DESC ,
                TB_EVENT_ZONE.CREATE_USER ,
                TB_EVENT_ZONE.CREATE_TIME ,
                TB_EVENT_ZONE.LAST_UPDATE_USER ,
                TB_EVENT_ZONE.LAST_UPDATE_TIME ,
                TB_EVENT_ITEM.EventItem_Name ,
                ISNULL(TB_EVENT_ZONE.PARAM_ZONE_ID,-1) PARAM_ZONE_ID ,
                ISNULL(TB_PARAM_ZONE.ZONE_NAME,'') ZONE_NAME
        FROM    TB_EVENT_ZONE
                INNER JOIN TB_EVENT_ITEM ON TB_EVENT_ZONE.EventItem_ID = TB_EVENT_ITEM.EVENTITEM_ID
                LEFT JOIN TB_PARAM_ZONE ON TB_EVENT_ZONE.PARAM_ZONE_ID = TB_PARAM_ZONE.ZONE_ID
        WHERE   ( TB_EVENT_ZONE.EVENTITEM_ID = @Event_Item_ID
                  OR @Event_Item_ID = -1
                )
                AND ( TB_EVENT_ZONE.EVENTZONE_NAME LIKE '%'
                      + @Event_Zone_Name + '%'
                      OR @Event_Zone_Name = ''
                    )
                AND ( TB_EVENT_ZONE.EVENTZONE_DESC LIKE '%'
                      + @Event_Zone_Desc + '%'
                      OR @Event_Zone_Desc = ''
                    )
                AND ( TB_EVENT_ZONE.EVENTZONE_NAME_EN LIKE '%'
                      + @Event_Zone_Name_En + '%'
                      OR @Event_Zone_Name_En = ''
                    )
                AND ( @Param_Zone_Id = -1
                      OR TB_EVENT_ZONE.PARAM_ZONE_ID = @Param_Zone_Id
                      OR TB_EVENT_ZONE.PARAM_ZONE_ID IS NULL
                    )
        ORDER BY TB_PARAM_ZONE.ZONE_NAME
    END
GO