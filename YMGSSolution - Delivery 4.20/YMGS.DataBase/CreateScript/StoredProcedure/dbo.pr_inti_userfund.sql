/***
Create Date:2013/01/28
Description:初始化用户资金账户信息
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_inti_userfund]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_inti_userfund]
GO
CREATE PROCEDURE [dbo].[pr_inti_userfund] ( @UserID INT )
AS 
    BEGIN
        DECLARE @Count INT
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_USER_FUND
        WHERE   [USER_ID] = @UserID
        IF @Count = 0 
            BEGIN
                INSERT  INTO dbo.TB_USER_FUND
                        ( USER_ID ,
                          BANK_NAME ,
                          OPEN_BANK_NAME ,
                          CARD_NO ,
                          ACCOUNT_HOLDER ,
                          CUR_FUND ,
                          FREEZED_FUND ,
                          CUR_INTEGRAL ,
                          [STATUS] ,
                          LAST_UPDATE_TIME
	                  )
                VALUES  ( @UserID , -- USER_ID - int
                          N'' , -- BANK_NAME - nvarchar(40)
                          N'' , -- OPEN_BANK_NAME - nvarchar(50)
                          N'' , -- CARD_NO - nvarchar(30)
                          N'' , -- ACCOUNT_HOLDER - nvarchar(40)
                          0 , -- CUR_FUND - decimal
                          0 , -- FREEZED_FUND - decimal
                          0 , -- CUR_INTEGRAL - int
                          0 , -- STATUS - int
                          GETDATE()  -- LAST_UPDATE_TIME - datetime
	                  )
            END
    END
GO