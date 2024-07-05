/***
Create Date:2013/01/21
Description:更新赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_event]
    GO
    
CREATE PROCEDURE [dbo].[pr_up_event]
    (
      @Event_ID INT ,
      @Event_ZoneID INT ,
      @Event_Name NVARCHAR(100) ,
      @Event_Name_EN NVARCHAR(100) ,
      @Event_Desc NVARCHAR(100) ,
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      --@Status INT ,
      @Last_Update_User INT ,
      @Event_Team_IDS NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @Count INT ,
            @SplitStr NVARCHAR(2) ,
            @Error_Msg NVARCHAR(200)
            --判断赛事是否和比赛相关联
        --SELECT  @Count = COUNT(1)
        --FROM    dbo.TB_MATCH
        --WHERE   EVENT_ID = @Event_ID
        --IF @Count > 0 
        --    BEGIN
        --        SET @Error_Msg = '赛事已经和比赛相关联，不能修改!'
        --        RAISERROR(@Error_Msg,16,1) WITH NOWAIT
        --        RETURN
        --    END
            --判断相同赛事名称是否存在
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT
        WHERE   EVENTZONE_ID = @Event_ZoneID
                AND (EVENT_NAME = @Event_Name OR EVENT_NAME_EN = @Event_Name_EN)
                AND EVENT_ID <> @Event_ID
        IF @Count > 0 
            BEGIN
                SET @Error_Msg = '相同属性的赛事已经存在，修改失败!'
                RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                RETURN
            END
        --更新赛事信息
        UPDATE  dbo.TB_EVENT
        SET     EVENTZONE_ID = @Event_ZoneID ,
                EVENT_NAME = @Event_Name ,
                EVENT_NAME_EN = @Event_Name_EN,
                EVENT_DESC = @Event_Desc ,
                [START_DATE] = @Start_Date ,
                END_DATE = @End_Date ,
                LAST_UPDATE_USER = @Last_Update_User ,
                LAST_UPDATE_TIME = GETDATE()
        WHERE   EVENT_ID = @Event_ID
        
		--更新缓存对象表
		exec pr_up_cache_object 2

        --更新赛事成员信息
        DELETE  FROM dbo.TB_EVENT_TEAM_MAP
        WHERE   EVENT_ID = @Event_ID
        IF ( @Event_Team_IDS IS NOT NULL
             AND @Event_Team_IDS <> ''
           ) 
            BEGIN
                SET @SplitStr = ','
                DECLARE EventTeam_Cursor CURSOR
                FOR
                    SELECT  [Value]
                    FROM    [dbo].[SplitString](@Event_Team_IDS, @SplitStr, 1) 
                OPEN EventTeam_Cursor
                DECLARE @Event_Team_ID INT
                FETCH NEXT FROM EventTeam_Cursor INTO @Event_Team_ID
                WHILE @@FETCH_STATUS = 0 
                    BEGIN
                        INSERT  INTO dbo.TB_EVENT_TEAM_MAP
                                ( TEAM_ID, EVENT_ID )
                        VALUES  ( @Event_Team_ID, -- TEAM_ID - int
                                  @Event_ID  -- EVENT_ID - int
                                  )
                        FETCH NEXT FROM EventTeam_Cursor INTO @Event_Team_ID
                    END
                CLOSE  EventTeam_Cursor
                DEALLOCATE EventTeam_Cursor
            END
        
    END
GO