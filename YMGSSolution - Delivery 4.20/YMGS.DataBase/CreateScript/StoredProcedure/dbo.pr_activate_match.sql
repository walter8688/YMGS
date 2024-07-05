IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_activate_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_activate_match]
GO
CREATE PROCEDURE [dbo].[pr_activate_match]
    (
      @Match_Id INT ,
      @Last_Update_User INT 
    )
AS 
    BEGIN
        DECLARE @temp_status INT
        DECLARE @temp_additional_status INT
        DECLARE @temp_match_start_date DATETIME
        SELECT  @temp_status = [STATUS] ,
                @temp_additional_status = ADDITIONALSTATUS ,
                @temp_match_start_date = STARTDATE
        FROM    TB_MATCH
        WHERE   MATCH_ID = @Match_Id
	
        IF GETDATE() > @temp_match_start_date AND @temp_status = 0
            BEGIN
                RAISERROR ('����ʱ���Ѿ�����������ʼʱ��!' , 16, 1) WITH NOWAIT
                RETURN
            END
	
        IF ( @temp_status = 0 ) 
            BEGIN
                UPDATE  dbo.TB_MATCH
                SET     [STATUS] = 1 ,
                        LAST_UPDATE_USER = @Last_Update_User ,
                        LAST_UPDATE_TIME = GETDATE()
                WHERE   MATCH_ID = @Match_Id

		--���»���
                EXEC pr_up_cache_object 3
            END
        ELSE 
            IF ( ( @temp_status = 1
                   OR @temp_status = 2
                   OR @temp_status = 3
                   OR @temp_status = 7
                 )
                 AND ( @temp_additional_status = 2
                       OR @temp_additional_status = 3
                     )
               ) 
                BEGIN
                    UPDATE  dbo.TB_MATCH
                    SET     ADDITIONALSTATUS = 1 ,
                            LAST_UPDATE_USER = @Last_Update_User ,
                            LAST_UPDATE_TIME = GETDATE()
                    WHERE   MATCH_ID = @Match_Id

		--���»���
                    EXEC pr_up_cache_object 3
                END
            ELSE 
                BEGIN
                    RAISERROR ('��ǰ״̬���ܼ������' , 16, 1) WITH NOWAIT
                    RETURN
                END
    END
GO