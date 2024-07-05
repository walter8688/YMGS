/***
Create Date:2013/02/05
Description:获取佣金积分对应数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_brokerage_integral_map]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_brokerage_integral_map]
GO
CREATE PROCEDURE pr_get_brokerage_integral_map
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_BROKERAGE_INTEGRAL_MAP
        WHERE   [Status] = 1
        ORDER BY Min_Integral 
    END
    
GO