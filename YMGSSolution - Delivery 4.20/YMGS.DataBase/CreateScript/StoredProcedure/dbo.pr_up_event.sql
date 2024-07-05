/***
Create Date:2013/01/21
Description:��������
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
            --�ж������Ƿ�ͱ��������
        --SELECT  @Count = COUNT(1)
        --FROM    dbo.TB_MATCH
        --WHERE   EVENT_ID = @Event_ID
        --IF @Count > 0 
        --    BEGIN
        --        SET @Error_Msg = '�����Ѿ��ͱ���������������޸�!'
        --        RAISERROR(@Error_Msg,16,1) WITH NOWAIT
        --        RETURN
        --    END
            --�ж���ͬ���������Ƿ����
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT
        WHERE   EVENTZONE_ID = @Event_ZoneID
                AND (EVENT_NAME = @Event_Name OR EVENT_NAME_EN = @Event_Name_EN)
                AND EVENT_ID <> @Event_ID
        IF @Count > 0 
            BEGIN
                SET @Error_Msg = '��ͬ���Ե������Ѿ����ڣ��޸�ʧ��!'
                RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                RETURN
            END
        --����������Ϣ
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
        
		--���»�������
		exec pr_up_cache_object 2

        --�������³�Ա��Ϣ
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