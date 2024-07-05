IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_footbal_halfcalc_status]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_footbal_halfcalc_status]
END
GO

CREATE FUNCTION dbo.udf_get_footbal_halfcalc_status
(
)
returns @TABLE table 
(
    [Status] int
) 
as
begin 

	INSERT INTO @TABLE
	select 3
	
	INSERT INTO @TABLE
	select 4

	INSERT INTO @TABLE
	select 7
	
    return
end
GO