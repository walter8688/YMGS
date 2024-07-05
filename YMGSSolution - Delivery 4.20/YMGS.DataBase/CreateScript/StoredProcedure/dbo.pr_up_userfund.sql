/***
Create Date:2013/01/28
Description:更新用户资金账户信息
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_userfund]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_userfund]
GO
CREATE PROCEDURE [dbo].[pr_up_userfund]
    (
      @User_Fund_ID INT ,
      @Bank_Name NVARCHAR(40) ,
      @Open_Bank_Name NVARCHAR(50) ,
      @Card_No NVARCHAR(30) ,
      @Account_Holder NVARCHAR(50) ,
      @Current_Fund DECIMAL ,
      @Freezed_Fund DECIMAL ,
      @Current_Intergral INT ,
      @Status INT 
    )
AS 
    BEGIN
        UPDATE  dbo.TB_USER_FUND
        SET     BANK_NAME = @Bank_Name ,
                OPEN_BANK_NAME = @Open_Bank_Name ,
                CARD_NO = @Card_No ,
                ACCOUNT_HOLDER = @Account_Holder ,
                CUR_FUND = @Current_Fund ,
                FREEZED_FUND = @Freezed_Fund ,
                CUR_INTEGRAL = @Current_Intergral ,
                [STATUS] = @Status ,
                LAST_UPDATE_TIME = GETDATE()
        WHERE   USER_FUND_ID = @User_Fund_ID
    END
GO