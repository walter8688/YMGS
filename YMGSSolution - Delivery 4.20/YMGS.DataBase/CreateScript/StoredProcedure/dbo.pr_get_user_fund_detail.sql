IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_user_fund_detail]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_user_fund_detail]
GO
CREATE PROCEDURE [dbo].[pr_get_user_fund_detail]
    (
      @User_Id INT ,
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @Trade_Type INT
    )
AS 
    BEGIN
        SELECT  TB_FUND_HISTORY.*
        FROM    dbo.TB_FUND_HISTORY
                INNER JOIN dbo.TB_USER_FUND ON dbo.TB_FUND_HISTORY.USER_FUND_ID = dbo.TB_USER_FUND.USER_FUND_ID
        WHERE   ( TB_USER_FUND.USER_ID = @User_Id
                  OR @User_Id = -1
                  OR @User_Id IS NULL
                )
                AND ( dbo.TB_FUND_HISTORY.TRADE_DATE >= @Start_Date
                      OR @Start_Date IS NULL
                    )
                AND ( dbo.TB_FUND_HISTORY.TRADE_DATE <= @End_Date
                      OR @End_Date IS NULL
                    )
                AND ( dbo.TB_FUND_HISTORY.TRADE_TYPE = @Trade_Type
                      OR @Trade_Type = -1
                      OR @Trade_Type IS NULL
                    )
                AND ( TRADE_TYPE IN ( 0, 1, 5, 6, 7, 8, 9,10,11 ) )
        ORDER BY TRADE_DATE DESC
    END
GO