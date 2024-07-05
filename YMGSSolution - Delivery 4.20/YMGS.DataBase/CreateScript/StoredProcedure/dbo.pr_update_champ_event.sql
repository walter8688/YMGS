/***
Create Date:2013/01/31
Description:更新冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_update_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_update_champ_event]
    GO
CREATE PROCEDURE pr_update_champ_event
    (
      @Champ_Event_Id INT ,
      @Champ_Event_Name NVARCHAR(100) ,
      @Champ_Event_Name_EN NVARCHAR(100) ,
      @Champ_Event_Desc NVARCHAR(100) ,
      @Champ_Event_StartDate DATETIME ,
      @Champ_Event_EndDate DATETIME ,
      @Last_Update_User INT ,
      @Champ_Event_Members NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @Count INT ,
            @Current_Status INT
        SELECT  @Current_Status = Champ_Event_Status
        FROM    dbo.TB_Champ_Event
        WHERE   Champ_Event_ID = @Champ_Event_Id
        IF @Current_Status <> 0 
            BEGIN
                RAISERROR('当前状态不能编辑',16,0) WITH NOWAIT
                RETURN
            END
        
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_Champ_Event
        WHERE   Champ_Event_Name = @Champ_Event_Name
                AND Champ_Event_ID <> @Champ_Event_Id
    	
        IF @Count = 0 
            BEGIN
                UPDATE  dbo.TB_Champ_Event
                SET     Champ_Event_Name = @Champ_Event_Name ,
                        Champ_Event_Name_En = @Champ_Event_Name_EN ,
                        Champ_Event_Desc = @Champ_Event_Desc ,
                        Champ_Event_StartDate = @Champ_Event_StartDate ,
                        Champ_Event_EndDate = @Champ_Event_EndDate ,
                        Last_Update_User = @Last_Update_User ,
                        Last_Update_Time = GETDATE()
                WHERE   Champ_Event_ID = @Champ_Event_Id
    			--更新缓存对象表
                EXEC pr_up_cache_object 4
			    --更新冠军赛事成员
			    --删除旧数据
                DELETE  FROM dbo.TB_Champ_Event_Member
                WHERE   Champ_Event_ID = @Champ_Event_Id
                DELETE  FROM dbo.TB_Champ_Market
                WHERE   Champ_Event_ID = @Champ_Event_Id
                --新增新数据
                IF ( LEN(@Champ_Event_Members) > 0
                     AND @Champ_Event_Members IS NOT NULL
                   ) 
                    BEGIN
                        DECLARE @SplitStr NVARCHAR(1)
                        SET @SplitStr = ','
                        DECLARE cur_champ_member CURSOR
                        FOR
                            SELECT  [Value]
                            FROM    [dbo].[SplitString](@Champ_Event_Members,
                                                        @SplitStr, 1) 
                        OPEN cur_champ_member
                        DECLARE @Champ_Event_Member_Name NVARCHAR(100) ,
                            @Champ_Event_Member_Name_En NVARCHAR(100)
                        FETCH NEXT FROM cur_champ_member INTO @Champ_Event_Member_Name 
                        WHILE @@FETCH_STATUS = 0 
                            BEGIN
                                IF CHARINDEX('|', @Champ_Event_Member_Name) > 0 
                                    BEGIN
                                        SET @Champ_Event_Member_Name_En = SUBSTRING(@Champ_Event_Member_Name,
                                                              CHARINDEX('|',
                                                              @Champ_Event_Member_Name)
                                                              + 1,
                                                              LEN(@Champ_Event_Member_Name))
                                        SET @Champ_Event_Member_Name = SUBSTRING(@Champ_Event_Member_Name,
                                                              0,
                                                              CHARINDEX('|',
                                                              @Champ_Event_Member_Name))
                                    END
                                INSERT  INTO dbo.TB_Champ_Event_Member
                                        ( Champ_Event_Member_Name ,
                                          Champ_Event_Member_Name_En ,
                                          Champ_Event_ID
                                        )
                                VALUES  ( @Champ_Event_Member_Name , -- Champ_Event_Member_Name - nvarchar(100)
                                          @Champ_Event_Member_Name_En ,
                                          @Champ_Event_Id  -- Champ_Event_ID - int         
                                        )
                                FETCH NEXT FROM cur_champ_member INTO @Champ_Event_Member_Name 
                            END
                        CLOSE cur_champ_member
                        DEALLOCATE cur_champ_member
                                    --添加冠军赛事市场
                        INSERT  INTO dbo.TB_Champ_Market
                                ( Champ_Event_ID ,
                                  Champ_Member_ID    
                                )
                                SELECT  @Champ_Event_Id ,
                                        Champ_Event_Member_ID
                                FROM    dbo.TB_Champ_Event_Member
                                WHERE   Champ_Event_ID = @Champ_Event_Id
                    END
            END
    END
GO