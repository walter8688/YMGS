IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_brokerage_by_integral]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_brokerage_by_integral]
    GO
CREATE PROCEDURE pr_get_brokerage_by_integral ( @Integral INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_BROKERAGE_INTEGRAL_MAP
        WHERE   Max_Integral >= @Integral
                AND Min_Integral <= @Integral           
    END
GO