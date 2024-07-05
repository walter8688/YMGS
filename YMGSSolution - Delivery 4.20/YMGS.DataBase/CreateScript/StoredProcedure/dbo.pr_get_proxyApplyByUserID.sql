IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_proxyApplyByUserID]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_proxyApplyByUserID]
GO

CREATE PROCEDURE [dbo].[pr_get_proxyApplyByUserID]
    (
      @Apply_SDate DATETIME ,
      @Apply_EDate DATETIME ,
      @Apply_Status INT,
      @User_ID INT
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_APPLY_PROXY  WITH(NOLOCK) 
        WHERE   ( Apply_Date >= @Apply_SDate
                  OR @Apply_SDate IS NULL
                )
                AND ( Apply_Date <= @Apply_EDate
                      OR @Apply_EDate IS NULL
                    )
                AND(
                Apply_Status = @Apply_Status
                OR @Apply_Status = -1
                )
                AND ( User_ID = @User_ID
                      OR @User_ID = ''
                    )
        ORDER BY Apply_Date DESC
    END

GO
