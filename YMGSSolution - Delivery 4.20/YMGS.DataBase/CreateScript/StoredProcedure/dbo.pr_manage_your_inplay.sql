iF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_manage_your_inplay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_manage_your_inplay]
GO
CREATE PROCEDURE [dbo].[pr_manage_your_inplay]
(
	@User_Id INT,
	@Match_Id INT,
	@Faved INT
)
AS 
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.TB_YOUR_INPLAY WHERE [USER_ID] = @User_Id AND MATCH_ID = @Match_Id)
	BEGIN
		INSERT INTO dbo.TB_YOUR_INPLAY ( [USER_ID], MATCH_ID, IS_FAV ) VALUES  ( @User_Id,@Match_Id,@Faved)
	END
	ELSE
	BEGIN
		UPDATE dbo.TB_YOUR_INPLAY SET IS_FAV = @Faved WHERE [USER_ID] = @User_Id AND MATCH_ID = @Match_Id
	END
	--update cache
	exec pr_up_cache_object 13
END
GO