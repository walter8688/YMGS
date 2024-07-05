IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_atuo_freeze_start_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_atuo_freeze_start_match]
    GO
CREATE PROCEDURE [dbo].[pr_atuo_freeze_start_match] ( @Match_Id INT )
AS 
    BEGIN
        DECLARE @Start_Date DATETIME
        DECLARE @AUTO_FREEZE_DATE DATETIME
        DECLARE @Is_Auto_Freezed BIT
    
        SELECT  @Start_Date = STARTDATE ,
                @AUTO_FREEZE_DATE = AUTO_FREEZE_DATE,
                @Is_Auto_Freezed = IS_AUTO_FREEZED
        FROM    dbo.TB_MATCH
        WHERE   [STATUS] = 1
                AND ADDITIONALSTATUS <> 2
                AND MATCH_ID = @Match_Id
                
        IF GETDATE() > @AUTO_FREEZE_DATE AND @Is_Auto_Freezed = 0
            BEGIN
                --非走地比赛，关闭其市场
                DECLARE @Temp_Count INT
                SELECT  @Temp_Count = COUNT(1) FROM dbo.TB_MATCH WHERE MATCH_ID = @Match_Id AND IS_ZOUDI = 0
                IF @Temp_Count = 1 
                    BEGIN
                        UPDATE dbo.TB_MATCH_MARKET SET MARKET_STATUS = 0 WHERE MATCH_ID = @Match_Id
                        EXEC pr_up_cache_object 3
                    END
                --清理市场
                EXEC pr_clear_market @Match_Id, 1
                UPDATE dbo.TB_MATCH SET IS_AUTO_FREEZED = 1,LAST_UPDATE_TIME=GETDATE() WHERE MATCH_ID = @Match_Id
            END
            
        IF GETDATE() >= @Start_Date AND @Is_Auto_Freezed = 1
        BEGIN
            UPDATE  dbo.TB_MATCH
            SET     STATUS = 2 ,ADDITIONALSTATUS = 1, LAST_UPDATE_TIME = GETDATE()
            WHERE   MATCH_ID = @Match_Id
                    
            EXEC pr_up_cache_object 3
        END
    END
GO