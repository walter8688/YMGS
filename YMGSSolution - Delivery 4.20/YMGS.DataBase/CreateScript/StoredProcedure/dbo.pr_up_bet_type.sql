IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_bet_type]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_bet_type]
GO
CREATE PROCEDURE [dbo].[pr_up_bet_type]
    (
      @Bet_Type_Id INT ,
      @Bet_Type_Name NVARCHAR(40) ,
      @Bet_Before_Game BIT ,
	  @Bet_Gaming BIT,
	  @Last_Update_User INT
    )
AS 
    BEGIN
        UPDATE  dbo.TB_BET_TYPE
        SET     BET_TYPE_NAME = @Bet_Type_Name ,
                BET_BEFORE_GAME = @Bet_Before_Game ,
                BET_GAMING = @Bet_Gaming,
                LAST_UPDATE_USER = @Last_Update_User,
                LAST_UPDATE_TIME = GETDATE()
        WHERE   BET_TYPE_ID = @Bet_Type_Id 
    END
GO