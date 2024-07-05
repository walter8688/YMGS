/***
Create Date:2013/01/17
Description:更新赛事区域
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_event_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_event_zone]
GO
CREATE PROCEDURE [dbo].[pr_up_event_zone]
    (
      @Event_Zone_ID INT ,
      @EventItem_ID INT ,
      @EventZone_Name NVARCHAR(40) ,
      @EventZone_Name_En NVARCHAR(40) ,
      @EventZone_Desc NVARCHAR(100) ,
      @Last_Update_User INT,
      @Param_Zone_Id INT
    )
AS 
    DECLARE @Count INT
    BEGIN
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_EVENT_ZONE
        WHERE   EVENTITEM_ID = @EventItem_ID
                AND ( EVENTZONE_NAME = @EventZone_Name
                      OR EVENTZONE_NAME_EN = @EventZone_Name_En
                    )
                AND EVENTZONE_ID <> @Event_Zone_ID
        IF @Count = 0 
            BEGIN
                UPDATE  dbo.TB_EVENT_ZONE
                SET     EVENTITEM_ID = @EventItem_ID ,
                        EVENTZONE_NAME = @EventZone_Name ,
                        EVENTZONE_NAME_EN = @EventZone_Name_En ,
                        EVENTZONE_DESC = @EventZone_Desc ,
                        LAST_UPDATE_USER = @Last_Update_User ,
                        LAST_UPDATE_TIME = GETDATE(),
                        PARAM_ZONE_ID = @Param_Zone_Id
                WHERE   EVENTZONE_ID = @Event_Zone_ID

				--更新缓存对象表
                EXEC pr_up_cache_object 1
            END
        ELSE 
            BEGIN
                RAISERROR('相同区域名的区域已经存在!',16,1) WITH NOWAIT
            END
    END
GO