/***
Create Date:2013/01/21
Description:ɾ�����³�Ա
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_event_team]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_event_team]
GO
CREATE PROCEDURE pr_del_event_team ( @Event_Team_ID INT )
AS 
    BEGIN
        DECLARE @Count INT ,
            @RaisErrorCode NVARCHAR(50)
	--�жϲ�����Ա�Ƿ�����������
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM_MAP
        WHERE   TEAM_ID = @Event_Team_ID
	--�����ò��ܱ�ɾ��	
        IF @Count > 0 
            BEGIN
                SET @RaisErrorCode = '������Ա�ѱ����ã����ܱ�ɾ��!'
                RAISERROR (@RaisErrorCode,16,1) WITH NOWAIT
                RETURN 
            END
        ELSE--δ�����ÿ���ɾ��
            BEGIN
                DELETE  FROM dbo.TB_EVENT_TEAM
                WHERE   EVENT_TEAM_ID = @Event_Team_ID
            END
    END
GO