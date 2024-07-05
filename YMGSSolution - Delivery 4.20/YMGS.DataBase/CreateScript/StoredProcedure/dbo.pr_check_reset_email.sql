IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_check_reset_email]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_check_reset_email]
    GO
CREATE PROCEDURE pr_check_reset_email
    (
      @User_Id INT ,
      @Email_address NVARCHAR(100)
    )
AS 
    BEGIN
        DECLARE @Temp_Count INT
        SELECT  @Temp_Count = COUNT(1)
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   EMAIL_ADDRESS = @Email_address
                AND USER_ID <> @User_Id
        SELECT  @Temp_Count 
        --PRINT CONVERT(NVARCHAR(10),@Temp_Count)
    END
GO