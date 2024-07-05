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
                                      WHEN 0 THEN N'V������ֵ'
                                      WHEN 1 THEN N'���߳�ֵ'
                                      ELSE ''
                                    END AS TRAN_TYPE ,
                                    TRAN_DATE ,
                                    TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    CASE TRAN_STATUS
                                      WHEN 0 THEN N'�ȴ�����'
                                      WHEN 1 THEN N'����ɹ�'
                                      WHEN 2 THEN N'��ֵ�ɹ�'
                                      WHEN 3 THEN N'��ֵʧ��'
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
                          SELECT    N'����' AS TRAN_TYPE ,
                                    WD_DATE AS TRAN_DATE ,
                                    WD_AMOUNT AS TRAN_AMOUNT ,
                                    TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                    CASE TB_USER_WITHDRAW.WD_STATUS
                                      WHEN 0 THEN N'������'
                                      WHEN 1 THEN N'��ȷ��'
                                      WHEN 2 THEN N'�Ѿܾ�'
                                      WHEN 3 THEN N'��ת��'
                                      WHEN 4 THEN N'��ȡ��'
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
                          SELECT    N'����ת��' AS TRAN_TYPE ,
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
                              WHEN 0 THEN N'V������ֵ'
                              WHEN 1 THEN N'���߳�ֵ'
                            END AS TRAN_TYPE ,
                            TRAN_DATE ,
                            TRAN_AMOUNT ,
                            TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                            CASE TRAN_STATUS
                              WHEN 0 THEN N'�ȴ�����'
                              WHEN 1 THEN N'����ɹ�'
                              WHEN 2 THEN N'��ֵ�ɹ�'
                              WHEN 3 THEN N'��ֵʧ��'
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
                        SELECT  N'����' AS TRAN_TYPE ,
                                WD_DATE AS TRAN_DATE ,
                                WD_AMOUNT AS TRAN_AMOUNT ,
                                TB_SYSTEM_ACCOUNT.LOGIN_NAME ,
                                CASE TB_USER_WITHDRAW.WD_STATUS
                                  WHEN 0 THEN N'������'
                                  WHEN 1 THEN N'��ȷ��'
                                  WHEN 2 THEN N'�Ѿܾ�'
                                  WHEN 3 THEN N'��ת��'
                                  WHEN 4 THEN N'��ȡ��'
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
                            SELECT  N'����ת��' AS TRAN_TYPE ,
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
