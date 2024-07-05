IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_apply_agent]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_apply_agent]
GO
CREATE PROCEDURE pr_get_apply_agent
    (
      @Role_Id INT ,
      @Apply_Start_Date DATETIME ,
      @Apply_End_Date DATETIME ,
      @User_Name NVARCHAR(100) ,
      @Apply_Status INT
    )
AS 
    BEGIN
        SELECT  A.* ,
                B.ROLE_NAME ,
                C.LOGIN_NAME
        FROM    tb_apply_proxy A
                INNER JOIN dbo.TB_SYSTEM_ROLE B ON A.Role_ID = b.ROLE_ID
                INNER JOIN dbo.TB_SYSTEM_ACCOUNT C ON A.USER_ID = C.USER_ID
        WHERE   ( A.ROLE_Id = @Role_Id
                  OR @Role_Id IS NULL
                  OR @Role_Id = -1
                )
                AND ( A.Apply_Date >= @Apply_Start_Date
                      OR @Apply_Start_Date IS NULL
                    )
                AND ( A.Apply_Date <= @Apply_End_Date
                      OR @Apply_End_Date IS NULL
                    )
                AND ( C.LOGIN_NAME LIKE '%' + @User_Name + '%'
                      OR @User_Name IS NULL
                      OR @User_Name = ''
                    )
                AND ( A.Apply_Status = @Apply_Status
                      OR @Apply_Status IS NULL
                      OR @Apply_Status = -1
                    )
        ORDER BY A.Apply_Date DESC
    END
GO