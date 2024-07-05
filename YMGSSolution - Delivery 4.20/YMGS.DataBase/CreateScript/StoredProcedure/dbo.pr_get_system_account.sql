if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_system_account]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_system_account]
GO

CREATE PROCEDURE [dbo].[pr_get_system_account]
(
@LOGIN_NAME NVARCHAR(20),
@USER_ID INT,
@EMAIL NVARCHAR(50)
)
AS
BEGIN
	SELECT * FROM dbo.TB_SYSTEM_ACCOUNT WHERE LOGIN_NAME=@LOGIN_NAME OR [USER_ID]=@USER_ID OR (EMAIL_ADDRESS=@EMAIL and EMAIL_ADDRESS!='');
END

GO