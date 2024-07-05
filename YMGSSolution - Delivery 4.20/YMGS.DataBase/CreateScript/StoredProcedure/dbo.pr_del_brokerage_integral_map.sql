/***
Create Date:2013/02/05
Description:删除佣金积分对应数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_brokerage_integral_map]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_brokerage_integral_map]
GO
CREATE PROCEDURE pr_del_brokerage_integral_map
    (
      @Brokerage_Rage_Id INT 
    )
AS 
    BEGIN
        UPDATE  dbo.TB_BROKERAGE_INTEGRAL_MAP
        SET     [Status] = 0
        WHERE   Brokerage_Rate_ID = @Brokerage_Rage_Id
    END
    
GO