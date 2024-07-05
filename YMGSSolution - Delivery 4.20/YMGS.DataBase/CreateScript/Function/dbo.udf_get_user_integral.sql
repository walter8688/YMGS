IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_user_integral]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_user_integral]
END
GO

CREATE FUNCTION dbo.udf_get_user_integral
(
	@Bet_Moneys decimal(18,2)
)
RETURNS int
AS
BEGIN
	DECLARE @iTemp int
	SET @iTemp = @Bet_Moneys * 1
	return @iTemp
END
GO