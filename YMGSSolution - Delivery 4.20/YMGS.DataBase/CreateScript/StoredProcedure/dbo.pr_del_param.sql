/***
Create Date:2013/01/10
Description:ɾ����������
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_param]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_del_param
GO
CREATE PROCEDURE [dbo].pr_del_param ( @Param_ID INT )
AS 
    DECLARE @Current_Order INT ,
        @Param_Type INT
    BEGIN
        DECLARE @Count INT
		--���˲����Ƿ�����
		--1.������Ա
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM
        WHERE   ( EVENT_TEAM_TYPE1 = @Param_ID
                  OR EVENT_TEAM_TYPE2 = @Param_ID
                )
        IF ( @Count > 0 ) 
            BEGIN
                RAISERROR('�����������У�����ɾ��!',16,1) WITH NOWAIT
                RETURN
            END
		
    
		--��ȡ��ǰ������Param_Order&Param_Type
        SELECT  @Current_Order = PARAM_ORDER ,
                @Param_Type = PARAM_TYPE
        FROM    dbo.TB_PARAM_PARAM
        WHERE   PARAM_ID = @Param_ID
        --ɾ������
        DELETE  FROM dbo.TB_PARAM_PARAM
        WHERE   PARAM_ID = @Param_ID 
        --������Ӧ�������͵�Param_Order
        UPDATE  dbo.TB_PARAM_PARAM
        SET     PARAM_ORDER = PARAM_ORDER - 1
        WHERE   PARAM_TYPE = @Param_Type
                AND PARAM_ORDER > @Current_Order
    END
GO