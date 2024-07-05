/***
Create Date:2013/01/10
Description:辅助参数排序
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_order_param]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_order_param]
GO
CREATE PROCEDURE pr_order_param
    (
      @Param_ID INT ,
      @Order_Tpye INT--0:上移；1:下移
    )
AS 
    DECLARE @Current_Order INT ,
        @Param_Type INT ,
        @Temp_Order INT
    BEGIN
        SELECT  @Current_Order = PARAM_ORDER ,
                @Param_Type = PARAM_TYPE
        FROM    dbo.TB_PARAM_PARAM
        WHERE   PARAM_ID = @Param_ID
        IF @Order_Tpye = 0 
            BEGIN
                SELECT  @Temp_Order = MIN(PARAM_ORDER)
                FROM    dbo.TB_PARAM_PARAM
                WHERE   PARAM_TYPE = @Param_Type
                IF @Current_Order = @Temp_Order 
                    BEGIN
                        RETURN
                    END
                ELSE 
                    BEGIN
                        UPDATE  dbo.TB_PARAM_PARAM
                        SET     PARAM_ORDER = @Current_Order
                        WHERE   PARAM_TYPE = @Param_Type
                                AND PARAM_ORDER = @Current_Order - 1
                        UPDATE  dbo.TB_PARAM_PARAM
                        SET     PARAM_ORDER = @Current_Order - 1
                        WHERE   PARAM_ID = @Param_ID
                    END
                SET @Temp_Order = 0
            END
        ELSE 
            IF @Order_Tpye = 1 
                BEGIN
                    SELECT  @Temp_Order = MAX(PARAM_ORDER)
                    FROM    dbo.TB_PARAM_PARAM
                    WHERE   PARAM_TYPE = @Param_Type
                    IF @Current_Order = @Temp_Order 
                        BEGIN
                            RETURN
                        END
                    ELSE 
                        BEGIN
                            UPDATE  dbo.TB_PARAM_PARAM
                            SET     PARAM_ORDER = @Current_Order
                            WHERE   PARAM_TYPE = @Param_Type
                                    AND PARAM_ORDER = @Current_Order + 1
                        
                            UPDATE  dbo.TB_PARAM_PARAM
                            SET     PARAM_ORDER = @Current_Order + 1
                            WHERE   PARAM_ID = @Param_ID
                        END
                END
    END
GO