IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_transfer_user_withdraw]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_transfer_user_withdraw]
    GO
CREATE PROCEDURE pr_transfer_user_withdraw
    (
      @User_WD_Id INT ,
      @Trans_ID NVARCHAR(15)
    )
AS 
    BEGIN
        DECLARE @Cur_WD_Status INT ,
            @Cur_WD_Amt DECIMAL(18, 2) ,
            @User_ID INT ,
            @User_Fund_Id INT
        SELECT  @Cur_WD_Status = WD_STATUS ,
                @Cur_WD_Amt = WD_AMOUNT ,
                @User_ID = USER_ID
        FROM    dbo.TB_USER_WITHDRAW
        WHERE   USER_WD_ID = @User_WD_Id
        IF @Cur_WD_Status = 0
            OR @Cur_WD_Status = 2
            OR @Cur_WD_Status = 3
            OR @Cur_WD_Status = 4 
            BEGIN
                RAISERROR('当前状态不能转账!',16,1) WITH NOWAIT
                RETURN
            END
        --1.更新提现记录状态
        UPDATE  dbo.TB_USER_WITHDRAW
        SET     WD_STATUS = 3 ,
                TRANS_ID = @Trans_ID
        WHERE   USER_WD_ID = @User_WD_Id
        
        --2.扣减冻结金额
        UPDATE  dbo.TB_USER_FUND
        SET     FREEZED_FUND = FREEZED_FUND - @Cur_WD_Amt
        WHERE   USER_ID = @User_ID
        
        --3.历史记录
        SELECT  @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @User_ID
    END
GO