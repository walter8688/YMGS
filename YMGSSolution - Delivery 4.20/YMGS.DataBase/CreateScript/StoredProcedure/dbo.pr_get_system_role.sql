if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_system_role]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_system_role]
GO

CREATE PROCEDURE [dbo].[pr_get_system_role]
(
@ROLE_ID int,
@ROLE_NAME nvarchar(40)
)
AS
BEGIN
	SELECT sa.user_name createrName,sac.USER_NAME laster,sr.* FROM dbo.TB_SYSTEM_ROLE sr 
left join dbo.TB_SYSTEM_ACCOUNT sa on sa.USER_ID=sr.CREATE_USER 
left join dbo.TB_SYSTEM_ACCOUNT sac on sac.USER_ID=sr.LAST_UPDATE_USER
WHERE (sr.ROLE_ID=@ROLE_ID or @ROLE_ID=0)  and sr.ROLE_NAME like '%'+@ROLE_NAME+'%'
	;
END

GO