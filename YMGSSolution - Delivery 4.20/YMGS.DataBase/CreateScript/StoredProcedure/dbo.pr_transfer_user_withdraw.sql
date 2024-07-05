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
                RAISERROR('��ǰ״̬����ת��!',16,1) WITH NOWAIT
                RETURN
            END
        --1.�������ּ�¼״̬
        UPDATE  dbo.TB_USER_WITHDRAW
        SET     WD_STATUS = 3 ,
                TRANS_ID = @Trans_ID
        WHERE   USER_WD_ID = @User_WD_Id
        
        --2.�ۼ�������
        UPDATE  dbo.TB_USER_FUND
        SET     FREEZED_FUND = FREEZED_FUND - @Cur_WD_Amt
        WHERE   USER_ID = @User_ID
        
        --3.��ʷ��¼
        SELECT  @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @User_ID
    END
GO