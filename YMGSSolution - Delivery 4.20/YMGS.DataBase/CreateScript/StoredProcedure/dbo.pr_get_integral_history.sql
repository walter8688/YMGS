IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_integral_history]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_integral_history]
    GO
CREATE PROCEDURE pr_get_integral_history
    (
      @User_Id INT ,
      @Start_Date DATETIME ,
      @End_Date DATETIME
    )
AS 
    BEGIN
        SELECT  TB_INTEGRAL_HISTORY.*
        FROM    dbo.TB_INTEGRAL_HISTORY
                INNER JOIN dbo.TB_USER_FUND ON dbo.TB_INTEGRAL_HISTORY.USER_FUND_ID = dbo.TB_USER_FUND.USER_FUND_ID
        WHERE   ( dbo.TB_USER_FUND.USER_ID = @User_Id
                  OR @User_Id = -1
                  OR @User_Id IS NULL
                )
                AND ( dbo.TB_INTEGRAL_HISTORY.TRADE_DATE >= @Start_Date
                      OR @Start_Date IS NULL
                    )
                AND ( dbo.TB_INTEGRAL_HISTORY.TRADE_DATE <= @End_Date
                      OR @End_Date IS NULL
                    )
        ORDER BY dbo.TB_INTEGRAL_HISTORY.TRADE_DATE DESC             
    END
GO