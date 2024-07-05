if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_current_datetime]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_current_datetime]
GO

CREATE PROCEDURE [dbo].[pr_get_current_datetime]
AS
BEGIN
	select GETDATE()
END

GO