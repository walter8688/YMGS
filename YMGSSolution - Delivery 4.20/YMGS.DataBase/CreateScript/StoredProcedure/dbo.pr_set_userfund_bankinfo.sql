/***
Create Date:2013/02/17
Description:设置资金账户银行信息
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_set_userfund_bankinfo]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_set_userfund_bankinfo]
    GO
CREATE PROCEDURE pr_set_userfund_bankinfo
    (
      @User_Id INT ,
      @Bank_Name NVARCHAR(40) ,
      @Open_Bank_Name NVARCHAR(50) ,
      @Card_No NVARCHAR(30) ,
      @Account_Holder NVARCHAR(40)
    )
AS 
    BEGIN
        UPDATE  dbo.TB_USER_FUND
        SET     BANK_NAME = @Bank_Name ,
                OPEN_BANK_NAME = @Open_Bank_Name ,
                CARD_NO = @Card_No ,
                ACCOUNT_HOLDER = @Account_Holder
        WHERE   [USER_ID] = @User_Id
    END
    
GO