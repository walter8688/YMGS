/***
Create Date:2013/01/31
Description:��ͣ�ھ�����
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_pause_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_pause_champ_event]
    GO
CREATE PROCEDURE pr_pause_champ_event ( @Champ_Event_Id INT )
AS 
    BEGIN
        DECLARE @Current_Status INT
        SELECT  @Current_Status = Champ_Event_Status
        FROM    dbo.TB_Champ_Event
        WHERE   Champ_Event_ID = @Champ_Event_Id
        --δ����&��ͣ״̬���Լ���
        IF @Current_Status = 0
            OR @Current_Status = 2
            OR @Current_Status = 3 
            BEGIN
                RAISERROR('��ǰ״̬������ͣ',16,0) WITH NOWAIT
                RETURN
            END
        UPDATE  dbo.TB_Champ_Event
        SET     Champ_Event_Status = 2
        WHERE   Champ_Event_ID = @Champ_Event_Id
        
        --���»�������
		exec pr_up_cache_object 4
    END
GO