/***
Create Date:2013/01/10
Description:删除辅助参数
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
		--检查此参数是否被引用
		--1.参赛成员
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_EVENT_TEAM
        WHERE   ( EVENT_TEAM_TYPE1 = @Param_ID
                  OR EVENT_TEAM_TYPE2 = @Param_ID
                )
        IF ( @Count > 0 ) 
            BEGIN
                RAISERROR('参数被引用中，不能删除!',16,1) WITH NOWAIT
                RETURN
            END
		
    
		--获取当前参数的Param_Order&Param_Type
        SELECT  @Current_Order = PARAM_ORDER ,
                @Param_Type = PARAM_TYPE
        FROM    dbo.TB_PARAM_PARAM
        WHERE   PARAM_ID = @Param_ID
        --删除参数
        DELETE  FROM dbo.TB_PARAM_PARAM
        WHERE   PARAM_ID = @Param_ID 
        --更新相应参数类型的Param_Order
        UPDATE  dbo.TB_PARAM_PARAM
        SET     PARAM_ORDER = PARAM_ORDER - 1
        WHERE   PARAM_TYPE = @Param_Type
                AND PARAM_ORDER > @Current_Order
    END
GO