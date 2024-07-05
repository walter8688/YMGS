/***
Create Date:2013/01/31
Description:���¹ھ�����
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_champ_event]
    GO
CREATE PROCEDURE pr_del_champ_event ( @Champ_Event_Id INT )
AS 
    BEGIN
        DECLARE @Current_Status INT
        SELECT  @Current_Status = Champ_Event_Status
        FROM    dbo.TB_Champ_Event
        WHERE   Champ_Event_ID = @Champ_Event_Id
        IF @Current_Status <> 0 
            BEGIN
                RAISERROR('��ǰ״̬����ɾ��',16,0) WITH NOWAIT
                RETURN
            END
        DELETE  FROM dbo.TB_Champ_Event
        WHERE   Champ_Event_ID = @Champ_Event_Id
        DELETE  FROM dbo.TB_Champ_Event_Member
        WHERE   Champ_Event_ID = @Champ_Event_Id
        DELETE  FROM dbo.TB_Champ_Market
        WHERE   Champ_Event_ID = @Champ_Event_Id
        
        --���»�������
		exec pr_up_cache_object 4
    END
GO