IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_set_user_fund]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_set_user_fund]
GO
CREATE PROCEDURE [dbo].[pr_set_user_fund]
    (
      @UserID INT ,
      @ModifiedUser INT ,
      @CurFund DECIMAL(20, 4)
    )
AS 
    BEGIN
        DECLARE @Temp_Fund DECIMAL(20, 4)
        DECLARE @Temp_User_Fund_Id INT
		
        SELECT  @Temp_Fund = CUR_FUND ,@Temp_User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @UserID
    
        UPDATE  dbo.TB_USER_FUND
        SET     CUR_FUND = @CurFund ,LAST_UPDATE_TIME = GETDATE()
        WHERE   USER_ID = @UserID
        
        INSERT  INTO dbo.TB_FUND_HISTORY
                ( USER_FUND_ID ,TRADE_TYPE ,TRADE_DESC ,TRADE_SERIAL_NO ,TRADE_FUND ,TRADE_DATE)
        VALUES  ( @Temp_User_Fund_Id ,9 ,N'管理员' + CONVERT(NVARCHAR(100),@ModifiedUser) + '线下转账' ,NULL ,@CurFund - @Temp_Fund ,GETDATE())
    END
GO