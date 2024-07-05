IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_user_commission_rate]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_user_commission_rate]
END
GO

CREATE FUNCTION dbo.udf_get_user_commission_rate
(
	@Buyer_Id int,
	@Buyer_Cur_Integral int
)
RETURNS decimal(18,4)
AS
BEGIN
	DECLARE @dblTemp decimal(18,4)
	SELECT @dblTemp=BROKERAGE_RATE FROM TB_BROKERAGE_INTEGRAL_MAP
			WHERE @Buyer_Cur_Integral BETWEEN MIN_INTEGRAL AND MAX_INTEGRAL
	SET @dblTemp = ISNULL(@dblTemp,0)
	return @dblTemp
END
GO