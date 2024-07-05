if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_role_func_map]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_role_func_map]
GO

CREATE PROCEDURE [dbo].[pr_get_role_func_map]
(
@ROLE_ID int
)
AS
BEGIN
	SELECT * FROM dbo.TB_ROLE_FUNC_MAP WHERE ROLE_ID=@ROLE_ID;
END

GO