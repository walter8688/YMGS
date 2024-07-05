IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_user_pay_successed]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_user_pay_successed]
    GO
CREATE PROCEDURE pr_user_pay_successed
    (
      @Order_Id NVARCHAR(16) ,
      @Vcard_Id INT
    )
AS 
    BEGIN
        DECLARE @Cur_Status INT
		
        SELECT  @Cur_Status = TRAN_STATUS
        FROM    dbo.TB_USER_PAY
        WHERE   ORDER_ID = @Order_Id
        IF @Cur_Status <> 0 
            BEGIN
                RAISERROR('Error',16,1) WITH NOWAIT
            END
		--���µ��Ѹ���״̬
        UPDATE  dbo.TB_USER_PAY
        SET     TRAN_STATUS = 1 ,
                VCARD_ID = @Vcard_Id
        WHERE   ORDER_ID = @Order_Id
        
        DECLARE @Amt DECIMAL(18, 2) 
        DECLARE @User_Id INT
        DECLARE @User_Pay_Id INT
            
        SELECT  @Amt = TRAN_AMOUNT ,
                @User_Id = [USER_ID] ,
                @User_Pay_Id = USER_PAY_ID
        FROM    dbo.TB_USER_PAY
        WHERE   ORDER_ID = @Order_Id
                AND TRAN_STATUS = 1
        
        --����V���������û�
        UPDATE  dbo.TB_VCARD_DETAIL
        SET     VCARD_STATUS = 1 ,
                ACTIVATE_USER_ID = @User_Id ,
                ACTIVATE_DATE = GETDATE()
        WHERE   VCARD_ID = @Vcard_Id
                
        --�����û��ʽ��˻�        
        UPDATE  dbo.TB_USER_FUND
        SET     CUR_FUND = CUR_FUND + @Amt
        WHERE   [USER_ID] = @User_Id
        
        --���µ��ѳ�ֵ״̬
        UPDATE  dbo.TB_USER_PAY
        SET     TRAN_STATUS = 2 ,
                TRAN_DATE = GETDATE()
        WHERE   ORDER_ID = @Order_Id
        
        --��ʷ��¼
        DECLARE @User_Fund_Id INT
        
        SELECT  @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @User_Id
        SELECT  *
        FROM    dbo.TB_USER_PAY
        
        INSERT  INTO dbo.TB_FUND_HISTORY
                ( USER_FUND_ID ,TRADE_TYPE ,TRADE_DESC ,TRADE_SERIAL_NO ,TRADE_FUND ,TRADE_DATE)
        VALUES  ( @User_Fund_Id , 1 , N'�û��������߳�ֵ' , @User_Pay_Id , @Amt , GETDATE()  )
    END
GO

