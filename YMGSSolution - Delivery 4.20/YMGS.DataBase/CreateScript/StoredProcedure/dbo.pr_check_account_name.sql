/***
Create Date:2013/02/07
Description:判断用户名是否存在
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_check_account_name]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_check_account_name]
    GO
CREATE PROCEDURE pr_check_account_name
    (
      @Account_Name NVARCHAR(20)
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   LOGIN_NAME = @Account_Name
    END
GO