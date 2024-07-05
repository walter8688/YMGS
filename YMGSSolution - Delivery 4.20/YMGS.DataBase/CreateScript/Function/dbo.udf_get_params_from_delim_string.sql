IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_params_from_delim_string]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_params_from_delim_string]
END
GO

CREATE FUNCTION dbo.udf_get_params_from_delim_string
(
    @RepParam nvarchar(max), 
    @Delim char(1)= ','
)
RETURNS @Values TABLE ([param] nvarchar(max))
AS
BEGIN
  DECLARE @chrind int
  DECLARE @Piece nvarchar(max)
  SELECT @chrind = 1 
  WHILE @chrind > 0
    BEGIN
      SELECT @chrind = CHARINDEX(@Delim,@RepParam)
      IF @chrind  > 0
        SELECT @Piece = LEFT(@RepParam,@chrind - 1)
      ELSE
        SELECT @Piece = @RepParam
      INSERT  @Values([param]) VALUES( RTRIM(LTRIM(@Piece)))
      SELECT @RepParam = RIGHT(@RepParam,LEN(@RepParam) - @chrind)
      IF LEN(@RepParam) = 0 BREAK
    END
  RETURN
END
GO