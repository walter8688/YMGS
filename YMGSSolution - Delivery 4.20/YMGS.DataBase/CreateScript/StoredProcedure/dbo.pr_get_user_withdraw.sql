IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_user_withdraw]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_user_withdraw]
    GO
CREATE PROCEDURE pr_get_user_withdraw
    (
      @WD_SDate DATETIME ,
      @WD_EDate DATETIME ,
      @User_Id INT
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_USER_WITHDRAW
        WHERE   ( WD_DATE >= @WD_SDate
                  OR @WD_SDate IS NULL
                )
                AND ( WD_DATE <= @WD_EDate
                      OR @WD_EDate IS NULL
                    )
                AND ( USER_ID = @User_Id
                      OR @User_Id = ''
                    )
	    ORDER BY WD_DATE
    END
GO