IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_user_withdraw]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_user_withdraw]
    GO
CREATE PROCEDURE pr_add_user_withdraw
    (
      @User_Id INT ,
      @Trans_Id NVARCHAR(15) ,
      @WD_Status INT ,
      @WD_Amount DECIMAL(18, 2) ,
      @Remark NVARCHAR(100)
    )
AS 
    BEGIN
        DECLARE @Cur_User_Fund DECIMAL(18, 2) ,
            @WD_Bank_Name NVARCHAR(40) ,
            @WD_Card_No NVARCHAR(30) ,
            @WD_Account_Holder NVARCHAR(40) ,
            @User_Fund_Id INT 
            
        SELECT  @Cur_User_Fund = fund.CUR_FUND ,
                @WD_Bank_Name = fund.BANK_NAME ,
                @WD_Card_No = fund.CARD_NO ,
                @WD_Account_Holder = ACCOUNT_HOLDER ,
                @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND fund
                INNER JOIN dbo.TB_SYSTEM_ACCOUNT users ON fund.USER_ID = users.USER_ID
        WHERE   users.ACCOUNT_STATUS = 1
                AND fund.STATUS = 0
                AND fund.USER_ID = @User_Id
		
        IF @WD_Amount > @Cur_User_Fund 
            BEGIN
                RAISERROR('当前提现资金不足!',16,1) WITH NOWAIT
                RETURN
            END
		
		--1.新增提现记录
        INSERT  INTO dbo.TB_USER_WITHDRAW
                ( USER_ID ,
                  TRANS_ID ,
                  WD_STATUS ,
                  WD_DATE ,
                  WD_AMOUNT ,
                  WD_BANK_NAME ,
                  WD_CARD_NO ,
                  WD_ACCOUNT_HOLDER ,
                  REMARK
                )
        VALUES  ( @User_Id , -- USER_ID - int
                  @Trans_Id , -- TRANS_ID - nvarchar(15)
                  @WD_Status , -- WD_STATUS - int
                  GETDATE() ,
                  @WD_Amount , -- WD_AMOUNT - decimal
                  @WD_Bank_Name , -- WD_BANK_NAME - nvarchar(40)
                  @WD_Card_No , -- WD_CARD_NO - nvarchar(30)
                  @WD_Account_Holder , -- WD_ACCOUNT_HOLDER - nvarchar(40)
                  @Remark  -- REMARK - nvarchar(100)
                )
         --2.冻结提现金额
        UPDATE  dbo.TB_USER_FUND
        SET     CUR_FUND = CUR_FUND - @WD_Amount ,
                FREEZED_FUND = FREEZED_FUND + @WD_Amount
        WHERE   USER_ID = @User_Id
        
        --3.历史记录
        SELECT  @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @User_ID
        INSERT  INTO dbo.TB_FUND_HISTORY
                ( USER_FUND_ID ,
                  TRADE_TYPE ,
                  TRADE_DESC ,
                  TRADE_SERIAL_NO ,
                  TRADE_FUND ,
                  TRADE_DATE
                )
        VALUES  ( @User_Fund_Id , -- USER_FUND_ID - int
                  1 , -- TRADE_TYPE - int
                  N'用户提现申请，金额冻结' , -- TRADE_DESC - nvarchar(100)
                  @User_Id , -- TRADE_SERIAL_NO - int
                  -@WD_Amount , -- TRADE_FUND - decimal
                  GETDATE()  -- TRADE_DATE - datetime
                )
    END
GO