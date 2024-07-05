/***
Create Date:2013/01/31
Description:新增冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_champ_event]
    GO
CREATE PROCEDURE pr_add_champ_event
    (
      @Champ_Event_Type INT ,
      @Event_Id INT ,
      @Champ_Event_Name NVARCHAR(100) ,
      @Champ_Event_Name_EN NVARCHAR(100) ,
      @Champ_Event_Desc NVARCHAR(100) ,
      @Champ_Event_StartDate DATETIME ,
      @Champ_Event_EndDate DATETIME ,
      @Champ_Event_Status INT ,
      @Create_User INT ,
      @Last_Update_User INT ,
      @Champ_Event_Members NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @Count INT ,
            @Champ_Event_Id INT ,
            @SplitStr NVARCHAR(2) ,
            @Champ_Event_Member_Name NVARCHAR(100) ,
            @Champ_Event_Member_Name_En NVARCHAR(100)
        BEGIN
            IF @Champ_Event_Type = 1 
                BEGIN
                    DECLARE @Event_Status INT
                    DECLARE @Event_Start_Date DATETIME
                    DECLARE @Event_End_Date DATETIME
				
                    SELECT  @Event_Status = STATUS
                    FROM    dbo.TB_EVENT
                    WHERE   EVENT_ID = @Event_Id
                    IF @Event_Status = 2 
                        BEGIN
                            RAISERROR ('相关赛事已经结束,冠军赛事不能新增' , 16, 1) WITH NOWAIT
                            RETURN
                        END
				
                    SELECT  @Event_Start_Date = [START_DATE] ,
                            @Event_End_Date = END_DATE
                    FROM    dbo.TB_EVENT
                    WHERE   EVENT_ID = @Event_Id
				
                    IF @Champ_Event_StartDate < @Event_Start_Date 
                        BEGIN
                            RAISERROR ('冠军赛事开始时间不能早于赛事开始时间' , 16, 1) WITH NOWAIT
                            RETURN
                        END
				
                    IF @Champ_Event_EndDate > @Event_End_Date 
                        BEGIN
                            RAISERROR ('冠军赛事结束时间不能晚于赛事结束时间' , 16, 1) WITH NOWAIT
                            RETURN
                        END
                END
        
            IF @Champ_Event_Type = 2 
                BEGIN
                    SET @Event_Id = -1
                END
            SELECT  @Count = COUNT(*)
            FROM    dbo.TB_Champ_Event
            WHERE   ( Champ_Event_Name = @Champ_Event_Name
                      OR Champ_Event_Name_En = @Champ_Event_Name_EN
                    )
                    AND Champ_Event_Type = @Champ_Event_Type
            IF @Count > 0 
                BEGIN
                    RAISERROR('此赛事已经存在!',16,1) WITH NOWAIT
                    RETURN
                END
            ELSE 
                BEGIN
                    INSERT  INTO dbo.TB_Champ_Event
                            ( Champ_Event_Type ,
                              Event_ID ,
                              Champ_Event_Name ,
                              Champ_Event_Name_En ,
                              Champ_Event_Desc ,
                              Champ_Event_StartDate ,
                              Champ_Event_EndDate ,
                              Champ_Event_Status ,
                              Create_User ,
                              Create_Time ,
                              Last_Update_User ,
                              Last_Update_Time    
                            )
                    VALUES  ( @Champ_Event_Type , -- Champ_Event_Type - int
                              @Event_Id , -- Event_ID - int
                              @Champ_Event_Name , -- Champ_Event_Name - nvarchar(100)
                              @Champ_Event_Name_EN ,
                              @Champ_Event_Desc , -- Champ_Event_Desc - nvarchar(100)
                              @Champ_Event_StartDate , -- Champ_Event_StartDate - datetime
                              @Champ_Event_EndDate , -- Champ_Event_EndDate - datetime
                              @Champ_Event_Status , -- Champ_Event_Status - int
                              @Create_User , -- Create_User - int
                              GETDATE() , -- Create_Time - datetime
                              @Last_Update_User , -- Last_Update_User - int
                              GETDATE()  -- Last_Update_Time - datetime
                            )
					--更新缓存对象表
                    EXEC pr_up_cache_object 4
					
                    SET @Champ_Event_Id = SCOPE_IDENTITY()
			                  --新增冠军赛事成员
                    IF LEN(@Champ_Event_Members) > 0
                        AND @Champ_Event_Members IS NOT NULL 
                        BEGIN                            
                            SET @SplitStr = ','
                            DECLARE cur_champ_member CURSOR
                            FOR
                                SELECT  [Value]
                                FROM    [dbo].[SplitString](@Champ_Event_Members,
                                                            @SplitStr, 1) 
                            OPEN cur_champ_member
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
                                              @Champ_Event_Member_Name_En , -- Champ_Event_Member_Name_En - nvarchar(100)
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
	 
    END
GO