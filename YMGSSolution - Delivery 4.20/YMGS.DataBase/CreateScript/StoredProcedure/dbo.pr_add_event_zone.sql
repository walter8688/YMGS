/***
Create Date:2013/01/17
Description:新增赛事区域
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_event_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_event_zone]
GO
CREATE PROCEDURE [dbo].[pr_add_event_zone]
    (
      @EventItem_ID INT ,
      @EventZone_Name NVARCHAR(40) ,
      @EventZone_Name_En NVARCHAR(40) ,
      @EventZone_Desc NVARCHAR(100) ,
      @Create_User INT ,
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
        IF @Count = 0 
            BEGIN
                INSERT  INTO dbo.TB_EVENT_ZONE
                        ( EVENTITEM_ID ,EVENTZONE_NAME ,EVENTZONE_NAME_EN ,EVENTZONE_DESC ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME,PARAM_ZONE_ID)
                VALUES  ( @EventItem_ID , @EventZone_Name , @EventZone_Name_En ,@EventZone_Desc , @Create_User , GETDATE() , @Last_Update_User , GETDATE() ,@Param_Zone_Id)
				--更新缓存对象表
                EXEC pr_up_cache_object 1
            END
        ELSE 
            BEGIN
                RAISERROR('相同区域名的区域已经存在!',16,1) WITH NOWAIT
            END
    END
GO