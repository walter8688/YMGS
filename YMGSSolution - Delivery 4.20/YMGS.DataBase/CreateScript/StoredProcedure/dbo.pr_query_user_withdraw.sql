IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_query_user_withdraw]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_query_user_withdraw]
    GO
CREATE PROCEDURE pr_query_user_withdraw
    (
      @WD_SDate DATETIME ,
      @WD_EDate DATETIME ,
      @User_Name NVARCHAR(40) ,
      @WD_Status INT ,
      @WD_From_Amt DECIMAL(18, 2) ,
      @WD_To_Amt DECIMAL(18, 2) ,
      @WD_Bank_Name NVARCHAR(50) ,
      @WD_Card_No NVARCHAR(30) ,
      @WD_ACCOUNT_HOLDER NVARCHAR(40) ,
      @Trans_Id NVARCHAR(15)
    )
AS 
    BEGIN
        SELECT  WD.* ,
                ACCOUNT.LOGIN_NAME
        FROM    dbo.TB_USER_WITHDRAW WD
                INNER JOIN dbo.TB_SYSTEM_ACCOUNT ACCOUNT ON WD.USER_ID = ACCOUNT.USER_ID
                                                            AND ACCOUNT.ACCOUNT_STATUS = 1
        WHERE   ( WD.WD_DATE >= @WD_SDate
                  OR @WD_SDate IS NULL
                )
                AND ( WD.WD_DATE <= @WD_EDate
                      OR @WD_EDate IS NULL
                    )
                AND ( ACCOUNT.LOGIN_NAME LIKE '%' + @User_Name + '%'
                      OR LOGIN_NAME = ''
                      OR LOGIN_NAME IS NULL
                    )
                AND ( wd.WD_STATUS = @WD_Status
                      OR @WD_Status = -1
                      OR @WD_Status IS NULL
                    )
                AND ( WD.WD_AMOUNT >= @WD_From_Amt
                      OR @WD_From_Amt IS NULL
                      OR @WD_From_Amt = -1
                    )
                AND ( WD_AMOUNT <= @WD_To_Amt
                      OR @WD_To_Amt IS NULL
                      OR @WD_To_Amt = -1
                    )
                AND ( wd.WD_BANK_NAME LIKE '%' + @WD_Bank_Name + '%'
                      OR @WD_Bank_Name = ''
                      OR @WD_Bank_Name IS NULL
                    )
                AND ( wd.WD_CARD_NO LIKE '%' + @WD_Card_No + '%'
                      OR @WD_Card_No = ''
                      OR @WD_Card_No IS NULL
                    )
                AND ( WD.WD_ACCOUNT_HOLDER LIKE '%' + @WD_ACCOUNT_HOLDER + '%'
                      OR @WD_ACCOUNT_HOLDER = ''
                      OR @WD_ACCOUNT_HOLDER IS NULL
                    )
                AND ( wd.TRANS_ID LIKE '%' + @Trans_Id + '%'
                      OR @Trans_Id = ''
                      OR @Trans_Id IS NULL
                    )
        ORDER BY WD.WD_DATE ,
                WD_STATUS ,
                ACCOUNT.LOGIN_NAME
    END
GO