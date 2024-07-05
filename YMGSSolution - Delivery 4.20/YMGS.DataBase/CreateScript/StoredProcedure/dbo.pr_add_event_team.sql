/***
Create Date:2013/01/21
Description:新增赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_event_team]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_event_team]
GO
CREATE PROCEDURE pr_add_event_team
    (
      @Event_Item_ID INT ,
      @Event_Team_Name NVARCHAR(50) ,
      @Event_Team_Name_EN NVARCHAR(50) ,
      @Event_Team_Type1 INT ,
      @Event_Team_Type2 INT ,
      @Status INT ,
      @Create_User INT ,
      @Last_Update_User INT,
      @Param_Zone_Id INT
    )
AS 
    BEGIN
        DECLARE @count INT
        SELECT  @count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM
        WHERE   EVENT_ITEM_ID = @Event_Item_ID
                AND ( EVENT_TEAM_NAME = @Event_Team_Name
                      OR EVENT_TEAM_NAME_EN = @Event_Team_Name_EN
                    )
        IF @count = 0 
            BEGIN
                INSERT  INTO dbo.TB_EVENT_TEAM
                        ( EVENT_ITEM_ID ,
                          EVENT_TEAM_NAME ,EVENT_TEAM_NAME_EN ,EVENT_TEAM_TYPE1 ,EVENT_TEAM_TYPE2 ,
                          STATUS ,PARAM_ZONE_ID,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME
		            )
                VALUES  ( @Event_Item_ID , 
                          @Event_Team_Name , @Event_Team_Name_EN ,@Event_Team_Type1 , @Event_Team_Type2 , 
                          @Status , @Param_Zone_Id,@Create_User , GETDATE() , @Last_Update_User , GETDATE()  )
            END
        ELSE 
            BEGIN
                RAISERROR('相同属性的参赛成员已经存在，不能重复!',16,1) WITH NOWAIT
            END
    END
GO