IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_half_end_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_half_end_match]
GO
CREATE PROCEDURE pr_half_end_match
    (
      @Match_Id INT ,
      @Last_Update_User_Id INT
    )
AS 
    BEGIN
        DECLARE @Cur_Status INT ,
            @Cur_Addtional_status INT
        SELECT  @Cur_Status = [STATUS] ,
                @Cur_Addtional_status = ADDITIONALSTATUS
        FROM    dbo.TB_MATCH
        WHERE   MATCH_ID = @Match_Id
        
        IF ( @Cur_Addtional_status = 1
             AND @Cur_Status = 2
           ) 
            BEGIN
                UPDATE  dbo.TB_MATCH
                SET     [STATUS] = 3 ,
                        LAST_UPDATE_USER = @Last_Update_User_Id ,
                        LAST_UPDATE_TIME = GETDATE(),
                        First_Half_End_Time = GETDATE()
                WHERE   MATCH_ID = @Match_Id
        
        --���»�������
                EXEC pr_up_cache_object 3
            END
        ELSE 
            BEGIN
                RAISERROR('��ǰ״̬�²��ܸ��ı���״̬Ϊ�볡�ѽ���',16,1) WITH NOWAIT
            END
    END
GO