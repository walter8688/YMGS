/***
Create Date:2013/01/21
Description:更新赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_event_team]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_event_team]
GO
CREATE PROCEDURE pr_up_event_team
    (
      @Event_Team_ID INT ,
      @Event_Item_ID INT ,
      @Event_Team_Name NVARCHAR(50) ,
      @Event_Team_Name_EN NVARCHAR(50) ,
      @Event_Team_Type1 INT ,
      @Event_Team_Type2 INT ,
      @Status INT ,
      @Last_Update_User INT,
      @Param_Zone_Id INT
    )
AS 
    BEGIN
        DECLARE @Count INT
        SELECT  @count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM
        WHERE   EVENT_ITEM_ID = @Event_Item_ID
                AND ( EVENT_TEAM_NAME = @Event_Team_Name
                      OR EVENT_TEAM_NAME_EN = @Event_Team_Name_EN
                    )
                AND EVENT_TEAM_ID <> @Event_Team_ID
        IF @count > 0 
            BEGIN
                RAISERROR('相同属性的参数成员已经存在，不能重复!',16,1) WITH NOWAIT
            END
        ELSE 
            BEGIN
                UPDATE  dbo.TB_EVENT_TEAM
                SET     EVENT_ITEM_ID = @Event_Item_ID ,
                        EVENT_TEAM_NAME = @Event_Team_Name ,
                        EVENT_TEAM_NAME_EN = @Event_Team_Name_EN ,
                        EVENT_TEAM_TYPE1 = @Event_Team_Type1 ,
                        EVENT_TEAM_TYPE2 = @Event_Team_Type2 ,
                        [STATUS] = @Status ,
                        PARAM_ZONE_ID = @Param_Zone_Id,
                        LAST_UPDATE_USER = @Last_Update_User ,
                        LAST_UPDATE_TIME = GETDATE()
                WHERE   EVENT_TEAM_ID = @Event_Team_ID
            END
    END
GO