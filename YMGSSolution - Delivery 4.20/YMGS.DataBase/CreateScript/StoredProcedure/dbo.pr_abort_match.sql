IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_abort_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_abort_match]
GO
CREATE PROCEDURE [dbo].[pr_abort_match]
    (
	  @Match_Id int,
	  @Last_Update_User int
    )
AS 
BEGIN
	DECLARE @temp_status int
	DECLARE @temp_additional_status int
	SELECT @temp_status=[STATUS],@temp_additional_status=ADDITIONALSTATUS
	FROM TB_MATCH
	WHERE MATCH_ID=@Match_Id
	
	IF(@temp_status=0 OR @temp_status = 3 OR @temp_status=4 or @temp_status=5 or @temp_status=6)
	BEGIN
		RAISERROR ('��ǰ״̬������ֹ����!' , 16, 1) WITH NOWAIT
		RETURN
	END
	ELSE
	BEGIN
		SET XACT_ABORT ON
        BEGIN TRANSACTION
        --��ȡ������ص�Ͷע/��ע/�������
        DECLARE @TempUserId INT
        DECLARE @TempFund DECIMAL(20,4)
        DECLARE @CurFreezedFund DECIMAL(20,4)
        DECLARE @TempUserFundId INT
        DECLARE @ExchangeId INT
        DECLARE AbortMatchCur CURSOR FORWARD_ONLY
        FOR
        SELECT TRADE_USER,MATCH_AMOUNTS Fund,EXCHANGE_BACK_ID ExchangeId FROM dbo.TB_EXCHANGE_BACK WHERE MATCH_ID = @Match_Id AND MATCH_AMOUNTS > 0 AND MATCH_TYPE = 1 AND STATUS <> 6
        UNION ALL
        SELECT TRADE_USER,(ODDS-1)*MATCH_AMOUNTS Fund,EXCHANGE_LAY_ID ExchangeId FROM dbo.TB_EXCHANGE_LAY WHERE MATCH_ID = @Match_Id AND MATCH_AMOUNTS > 0 AND MATCH_TYPE = 1 AND STATUS <> 6
        UNION ALL
        SELECT dbo.TB_EXCHANGE_BACK.TRADE_USER,DEAL_AMOUNT Fund,EXCHANGE_DEAL_ID ExchangeId FROM dbo.TB_EXCHANGE_BACK INNER JOIN dbo.TB_EXCHANGE_DEAL
        ON dbo.TB_EXCHANGE_BACK.EXCHANGE_BACK_ID = dbo.TB_EXCHANGE_DEAL.EXCHANGE_BACK_ID 
        WHERE dbo.TB_EXCHANGE_DEAL.MATCH_ID = @Match_Id AND dbo.TB_EXCHANGE_DEAL.MATCH_TYPE = 1 AND TB_EXCHANGE_DEAL.STATUS <> 3
        UNION ALL
        SELECT dbo.TB_EXCHANGE_LAY.TRADE_USER,(dbo.TB_EXCHANGE_DEAL.ODDS-1)*DEAL_AMOUNT Fund,EXCHANGE_DEAL_ID ExchangeId FROM dbo.TB_EXCHANGE_LAY INNER JOIN dbo.TB_EXCHANGE_DEAL
        ON dbo.TB_EXCHANGE_LAY.EXCHANGE_LAY_ID = dbo.TB_EXCHANGE_DEAL.EXCHANGE_LAY_ID
        WHERE dbo.TB_EXCHANGE_DEAL.MATCH_ID = @Match_Id AND dbo.TB_EXCHANGE_DEAL.MATCH_TYPE = 1 AND TB_EXCHANGE_DEAL.STATUS <> 3
        OPEN AbortMatchCur
        FETCH NEXT FROM AbortMatchCur INTO @TempUserId,@TempFund,@ExchangeId
        WHILE @@FETCH_STATUS = 0
        BEGIN
			SELECT @TempUserFundId = USER_FUND_ID,@CurFreezedFund = FREEZED_FUND FROM dbo.TB_USER_FUND WHERE USER_ID = @TempUserId
			IF @TempFund > @CurFreezedFund
			BEGIN
				RAISERROR('�ɽⶳ�ʽ���',16,1)
				RETURN
				ROLLBACK TRANSACTION
			END
			--�����ʽ��˻�
			UPDATE dbo.TB_USER_FUND SET CUR_FUND = CUR_FUND + @TempFund,FREEZED_FUND = FREEZED_FUND - @TempFund ,
			LAST_UPDATE_TIME = GETDATE() WHERE USER_ID = @TempUserId
			--��¼��ʷ����
			INSERT INTO dbo.TB_FUND_HISTORY( USER_FUND_ID ,TRADE_TYPE ,TRADE_DESC ,
			          TRADE_SERIAL_NO ,TRADE_FUND ,TRADE_DATE)
			VALUES  ( @TempUserFundId , 4 , N'��ֹ�г�ʱ�ⶳ���ʽ�' , 
			          @ExchangeId , @TempFund , GETDATE() )
        	FETCH NEXT FROM AbortMatchCur INTO @TempUserId,@TempFund,@ExchangeId
        END
        CLOSE AbortMatchCur
        DEALLOCATE AbortMatchCur
        
        --����״̬
        UPDATE dbo.TB_EXCHANGE_BACK SET STATUS = 6,MATCH_AMOUNTS = 0 WHERE MATCH_ID = @Match_Id AND MATCH_TYPE = 1
        UPDATE dbo.TB_EXCHANGE_LAY SET STATUS = 6,MATCH_AMOUNTS = 0 WHERE MATCH_ID = @Match_Id AND MATCH_TYPE = 1
        UPDATE dbo.TB_EXCHANGE_DEAL SET STATUS = 3 WHERE MATCH_ID = @Match_Id AND MATCH_TYPE = 1
        
		UPDATE dbo.TB_MATCH
		SET [STATUS]=6,
			LAST_UPDATE_USER=@Last_Update_User,
			LAST_UPDATE_TIME=getdate()
		WHERE MATCH_ID=@Match_Id

		--�۳����ͷŵĶԳ��ʽ�
		--����ID,0�볡���� 1 ȫ������,1 �������� 2 �ھ�����,�û�ID,������־ID
		exec pr_calc_real_hedge_rollback @Match_Id,1,1,@Last_Update_User,null

		--���»���
		exec pr_up_cache_object 3
		COMMIT TRANSACTION
	END
END
GO