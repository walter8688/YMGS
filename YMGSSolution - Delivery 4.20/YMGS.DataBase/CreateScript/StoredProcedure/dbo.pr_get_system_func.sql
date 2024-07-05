if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_system_func]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_system_func]
GO

CREATE PROCEDURE [dbo].[pr_get_system_func]
(
@PARENT_FUNC_ID int,
@FUNC_ID INT,
@FUNC_NAME nvarchar(50)
)
AS
BEGIN
	SELECT * FROM dbo.TB_System_Func 
	WHERE (FUNC_ID=@FUNC_ID or @FUNC_ID=0) and ( @PARENT_FUNC_ID=0 or PARENT_FUNC_ID=@PARENT_FUNC_ID) and FUNC_NAME like '%'+@FUNC_NAME+'%';
END

GO