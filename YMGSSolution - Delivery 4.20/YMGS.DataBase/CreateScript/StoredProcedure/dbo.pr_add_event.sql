/***
Create Date:2013/01/21
Description:新增赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_event]
    GO
CREATE PROCEDURE [dbo].[pr_add_event]
    (
      @Event_ZoneID INT ,
      @Event_Name NVARCHAR(100) ,
      @Event_Name_EN NVARCHAR(100) ,
      @Event_Desc NVARCHAR(100) ,
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @Status INT ,
      @Create_User INT ,
      @Last_Update_User INT ,
      @Event_Team_IDS NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @Count INT ,
            @SplitStr NVARCHAR(2) ,
            @Event_ID INT
        IF @End_Date < GETDATE()    
        BEGIN
        	RAISERROR ('赛事结束时间能早于当前时间',16,1) WITH NOWAIT
        END
        
    	--判断相同赛事区域下是否存在相同的赛事
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT
        WHERE   EVENTZONE_ID = @Event_ZoneID
                AND ( EVENT_NAME = @Event_Name
                      OR EVENT_NAME_EN = @Event_Name_EN
                    )
        IF @Count > 0 
            BEGIN
                RAISERROR ('相同赛事区域下已经存在相同的赛事',16,1) WITH NOWAIT
            END
        ELSE 
            BEGIN
    		--1.新增赛事
                INSERT  INTO dbo.TB_EVENT
                        ( EVENTZONE_ID ,
                          EVENT_NAME ,
                          EVENT_NAME_EN ,
                          EVENT_DESC ,
                          START_DATE ,
                          END_DATE ,
                          STATUS ,
                          CREATE_USER ,
                          CREATE_TIME ,
                          LAST_UPDATE_USER ,
                          LAST_UPDATE_TIME
    		            )
                VALUES  ( @Event_ZoneID , -- EVENTZONE_ID - int
                          @Event_Name , -- EVENT_NAME - nvarchar(100)
                          @Event_Name_EN ,
                          @Event_Desc , -- EVENT_DESC - nvarchar(100)
                          @Start_Date , -- START_DATE - datetime
                          @End_Date , -- END_DATE - datetime
                          @Status , -- STATUS - int
                          @Create_User , -- CREATE_USER - int
                          GETDATE() , -- CREATE_TIME - datetime
                          @Last_Update_User , -- LAST_UPDATE_USER - int
                          GETDATE()  -- LAST_UPDATE_TIME - datetime
    		            )
                SET @Event_ID = SCOPE_IDENTITY()

				--更新缓存对象表
                EXEC pr_up_cache_object 2

    		 --2.新增赛事相关赛事成员
                IF ( @Event_Team_IDS IS NOT NULL
                     AND @Event_Team_IDS <> ''
                   ) 
                    BEGIN
                        SET @SplitStr = ','
                        DECLARE EventTeam_Cursor CURSOR
                        FOR
                            SELECT  [Value]
                            FROM    [dbo].[SplitString](@Event_Team_IDS,
                                                        @SplitStr, 1) 
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
    END
GO