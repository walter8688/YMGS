if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_bet_type]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_bet_type]
GO

CREATE PROCEDURE [dbo].[pr_get_bet_type]
AS
BEGIN
	SELECT BET_TYPE_ID,BET_TYPE_NAME,BET_BEFORE_GAME,BET_GAMING,
    CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME
    FROM TB_BET_TYPE
END

GO
