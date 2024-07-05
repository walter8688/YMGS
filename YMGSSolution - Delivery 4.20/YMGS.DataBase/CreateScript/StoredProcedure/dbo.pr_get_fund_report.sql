IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_fund_report]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_fund_report]
    GO
CREATE PROCEDURE pr_get_fund_report
    (
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @Fund_Type INT
    )
AS 
    BEGIN
        IF ( @Fund_Type = -1 ) 
            BEGIN
                SELECT  *
                FROM    ( SELECT    CASE TB_USER_PAY.TRAN_TYPE
                                      WHEN 0 THEN N'V网卡充值'
                                      WHEN 1 THEN N'在线充值'
                                      ELSE ''
                                    END AS TRAN_TYPE ,
                                    TRAN_DATE ,
                                    TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    CASE TRAN_STATUS
                                      WHEN 0 THEN N'等待付款'
                                      WHEN 1 THEN N'付款成功'
                                      WHEN 2 THEN N'充值成功'
                                      WHEN 3 THEN N'充值失败'
                                    END AS TRAN_STATUS
                          FROM      TB_USER_PAY
                                    INNER JOIN TB_SYSTEM_ACCOUNT ON TB_USER_PAY.USER_ID = TB_SYSTEM_ACCOUNT.USER_ID
                          WHERE     ( TRAN_DATE >= @Start_Date
                                      OR @Start_Date IS NULL
                                    )
                                    AND ( TRAN_DATE <= @End_Date
                                          OR @End_Date IS NULL
                                        )
                          UNION
                          SELECT    N'提现' AS TRAN_TYPE ,
                                    WD_DATE AS TRAN_DATE ,
                                    WD_AMOUNT AS TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    CASE TB_USER_WITHDRAW.WD_STATUS
                                      WHEN 0 THEN N'申请中'
                                      WHEN 1 THEN N'已确认'
                                      WHEN 2 THEN N'已拒绝'
                                      WHEN 3 THEN N'已转账'
                                      WHEN 4 THEN N'已取消'
                                    END AS TRAN_STATUS
                          FROM      TB_USER_WITHDRAW
                                    INNER JOIN TB_SYSTEM_ACCOUNT ON TB_USER_WITHDRAW.USER_ID = TB_SYSTEM_ACCOUNT.USER_ID
                          WHERE     ( WD_DATE >= @Start_Date
                                      OR @Start_Date IS NULL
                                    )
                                    AND ( WD_DATE <= @End_Date
                                          OR @End_Date IS NULL
                                        )
                          UNION
                          SELECT    N'线下转账' AS TRAN_TYPE ,
                                    TRADE_DATE AS TRAN_DATE ,
                                    TRADE_FUND AS TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    '' AS TRAN_STATUS
                          FROM      dbo.TB_FUND_HISTORY
                                    INNER JOIN dbo.TB_USER_FUND ON dbo.TB_FUND_HISTORY.USER_FUND_ID = dbo.TB_USER_FUND.USER_FUND_ID
                                    INNER JOIN dbo.TB_SYSTEM_ACCOUNT ON dbo.TB_USER_FUND.USER_ID = dbo.TB_SYSTEM_ACCOUNT.USER_ID
                          WHERE     TRADE_TYPE = 9
                                    AND ( TRADE_DATE >= @Start_Date
                                          OR @Start_Date IS NULL
                                        )
                                    AND ( TRADE_DATE <= @End_Date
                                          OR @End_Date IS NULL
                                        )
                        ) temp
                ORDER BY temp.TRAN_DATE DESC ,
                        temp.TRAN_TYPE ,
                        temp.TRAN_STATUS

            END
        ELSE 
            IF @Fund_Type = 0
                OR @Fund_Type = 1 
                BEGIN
                    SELECT  CASE TB_USER_PAY.TRAN_TYPE
                              WHEN 0 THEN N'V网卡充值'
                              WHEN 1 THEN N'在线充值'
                            END AS TRAN_TYPE ,
                            TRAN_DATE ,
                            TRAN_AMOUNT ,
                            TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                            CASE TRAN_STATUS
                              WHEN 0 THEN N'等待付款'
                              WHEN 1 THEN N'付款成功'
                              WHEN 2 THEN N'充值成功'
                              WHEN 3 THEN N'充值失败'
                            END AS TRAN_STATUS
                    FROM    TB_USER_PAY
                            INNER JOIN TB_SYSTEM_ACCOUNT ON TB_USER_PAY.USER_ID = TB_SYSTEM_ACCOUNT.USER_ID
                    WHERE   ( TRAN_DATE >= @Start_Date
                              OR @Start_Date IS NULL
                            )
                            AND ( TRAN_DATE <= @End_Date
                                  OR @End_Date IS NULL
                                )
                            AND TB_USER_PAY.TRAN_TYPE = @Fund_Type
                    ORDER BY TRAN_DATE DESC ,
                            TRAN_TYPE ,
                            TRAN_STATUS
                END
            ELSE 
                IF @Fund_Type = 2 
                    BEGIN
                        SELECT  N'提现' AS TRAN_TYPE ,
                                WD_DATE AS TRAN_DATE ,
                                WD_AMOUNT AS TRAN_AMOUNT ,
                                TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                CASE TB_USER_WITHDRAW.WD_STATUS
                                  WHEN 0 THEN N'申请中'
                                  WHEN 1 THEN N'已确认'
                                  WHEN 2 THEN N'已拒绝'
                                  WHEN 3 THEN N'已转账'
                                  WHEN 4 THEN N'已取消'
                                END AS TRAN_STATUS
                        FROM    TB_USER_WITHDRAW
                                INNER JOIN TB_SYSTEM_ACCOUNT ON TB_USER_WITHDRAW.USER_ID = TB_SYSTEM_ACCOUNT.USER_ID
                        WHERE   ( WD_DATE >= @Start_Date
                                  OR @Start_Date IS NULL
                                )
                                AND ( WD_DATE <= @End_Date
                                      OR @End_Date IS NULL
                                    )
                        ORDER BY TRAN_DATE DESC ,
                                TRAN_TYPE ,
                                TRAN_STATUS
                    END
                ELSE 
                    IF @Fund_Type = 3 
                        BEGIN
                            SELECT  N'线下转账' AS TRAN_TYPE ,
                                    TRADE_DATE AS TRAN_DATE ,
                                    TRADE_FUND AS TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    '' AS TRAN_STATUS
                            FROM    dbo.TB_FUND_HISTORY
                                    INNER JOIN dbo.TB_USER_FUND ON dbo.TB_FUND_HISTORY.USER_FUND_ID = dbo.TB_USER_FUND.USER_FUND_ID
                                    INNER JOIN dbo.TB_SYSTEM_ACCOUNT ON dbo.TB_USER_FUND.USER_ID = dbo.TB_SYSTEM_ACCOUNT.USER_ID
                            WHERE   TRADE_TYPE = 9
                                    AND ( TRADE_DATE >= @Start_Date
                                          OR @Start_Date IS NULL
                                        )
                                    AND ( TRADE_DATE <= @End_Date
                                          OR @End_Date IS NULL
                                        )
                            ORDER BY TRAN_DATE DESC ,
                                    TRAN_TYPE ,
                                    TRAN_STATUS
                        END
    END
GO
