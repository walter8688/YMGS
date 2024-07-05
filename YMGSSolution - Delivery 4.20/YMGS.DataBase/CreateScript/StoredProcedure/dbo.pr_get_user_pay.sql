IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_user_pay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_user_pay]
    GO
CREATE PROCEDURE pr_get_user_pay
    (
      @Start_Date DATETIME ,
      @End_Date DATETIME ,
      @User_ID INT
    )
AS 
    BEGIN
        SELECT  *
        FROM    TB_USER_PAY
        WHERE   ( TRAN_DATE >= @Start_Date
                  OR @Start_Date IS NULL
                )
                AND ( TRAN_DATE <= @End_Date
                      OR @End_Date IS NULL
                    )
                AND USER_ID = @User_ID
        ORDER BY TRAN_DATE DESC
    END
GO