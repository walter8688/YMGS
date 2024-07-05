if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_system_account]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_system_account]
GO

CREATE PROCEDURE [dbo].[pr_system_account]
(
@USERNAME NVARCHAR(40),
@Start_Date DATETIME,
@End_Date DATETIME
)
AS
BEGIN
SELECT A.LOGIN_NAME,A.USER_ID,A.USER_NAME,A.ROLE_ID,B.ROLE_NAME,A.ACCOUNT_STATUS,F.CUR_FUND,A.CREATE_DATE FROM dbo.TB_SYSTEM_ACCOUNT A
LEFT JOIN TB_SYSTEM_ROLE B ON B.ROLE_ID=A.ROLE_ID
LEFT JOIN TB_USER_FUND F ON A.USER_ID = F.USER_ID
WHERE A.USER_NAME LIKE '%'+@USERNAME+'%'
AND (A.CREATE_DATE >= @Start_Date OR @Start_Date IS NULL)
AND (A.CREATE_DATE <= @End_Date OR @End_Date IS NULL)

END

GO