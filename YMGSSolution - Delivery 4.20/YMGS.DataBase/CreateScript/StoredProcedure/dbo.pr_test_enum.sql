if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_test_enum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_test_enum]
GO

CREATE PROCEDURE dbo.pr_test_enum
AS
BEGIN
	select * from YMGS_TEST
END

GO

-- 测试表结束