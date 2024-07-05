/***
Create Date:2013/01/31
Description:终止冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_abort_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_abort_champ_event]
    GO
CREATE PROCEDURE pr_abort_champ_event ( @Champ_Event_Id INT )
AS 
    BEGIN
		SET XACT_ABORT ON
		BEGIN TRANSACTION
		
        DECLARE @Current_Status INT
        SELECT  @Current_Status = Champ_Event_Status
        FROM    dbo.TB_Champ_Event
        WHERE   Champ_Event_ID = @Champ_Event_Id
        --未激活&暂停状态可以激活
        IF @Current_Status = 0
            OR @Current_Status = 3 
            BEGIN
                RAISERROR('当前状态不能终止',16,0) WITH NOWAIT
                RETURN
            END
        
        DECLARE @TempUserId INT
        DECLARE @TempFund DECIMAL(20,4)
        DECLARE @CurFreezedFund DECIMAL(20,4)
        DECLARE @TempUserFundId INT
        DECLARE @ExchangeId INT
            
        DECLARE AbortMatchCur CURSOR FORWARD_ONLY
        FOR
        SELECT TRADE_USER,MATCH_AMOUNTS Fund,EXCHANGE_BACK_ID ExchangeId FROM dbo.TB_EXCHANGE_BACK WHERE MATCH_ID = @Champ_Event_Id AND MATCH_AMOUNTS > 0 AND MATCH_TYPE = 2 AND STATUS <> 6
        UNION ALL
        SELECT TRADE_USER,(ODDS-1)*MATCH_AMOUNTS Fund,EXCHANGE_LAY_ID ExchangeId FROM dbo.TB_EXCHANGE_LAY WHERE MATCH_ID = @Champ_Event_Id AND MATCH_AMOUNTS > 0 AND MATCH_TYPE = 2 AND STATUS <> 6
        UNION ALL
        SELECT dbo.TB_EXCHANGE_BACK.TRADE_USER,DEAL_AMOUNT Fund,EXCHANGE_DEAL_ID ExchangeId FROM dbo.TB_EXCHANGE_BACK INNER JOIN dbo.TB_EXCHANGE_DEAL
        ON dbo.TB_EXCHANGE_BACK.EXCHANGE_BACK_ID = dbo.TB_EXCHANGE_DEAL.EXCHANGE_BACK_ID 
        WHERE dbo.TB_EXCHANGE_DEAL.MATCH_ID = @Champ_Event_Id AND dbo.TB_EXCHANGE_DEAL.MATCH_TYPE = 2 AND TB_EXCHANGE_DEAL.STATUS <> 3
        UNION ALL
        SELECT dbo.TB_EXCHANGE_LAY.TRADE_USER,(dbo.TB_EXCHANGE_DEAL.ODDS-1)*DEAL_AMOUNT Fund,EXCHANGE_DEAL_ID ExchangeId FROM dbo.TB_EXCHANGE_LAY INNER JOIN dbo.TB_EXCHANGE_DEAL
        ON dbo.TB_EXCHANGE_LAY.EXCHANGE_LAY_ID = dbo.TB_EXCHANGE_DEAL.EXCHANGE_LAY_ID
        WHERE dbo.TB_EXCHANGE_DEAL.MATCH_ID = @Champ_Event_Id AND dbo.TB_EXCHANGE_DEAL.MATCH_TYPE = 2 AND TB_EXCHANGE_DEAL.STATUS <> 3
        
        OPEN AbortMatchCur
        FETCH NEXT FROM AbortMatchCur INTO @TempUserId,@TempFund,@ExchangeId
        WHILE @@FETCH_STATUS = 0
        BEGIN
			SELECT @TempUserFundId = USER_FUND_ID,@CurFreezedFund = FREEZED_FUND FROM dbo.TB_USER_FUND WHERE USER_ID = @TempUserId
			IF @TempFund > @CurFreezedFund
			BEGIN
				RAISERROR('可解冻资金不足',16,1)
				RETURN
				ROLLBACK TRANSACTION
			END
			--更新资金账户
			UPDATE dbo.TB_USER_FUND SET CUR_FUND = CUR_FUND + @TempFund,FREEZED_FUND = FREEZED_FUND - @TempFund ,
			LAST_UPDATE_TIME = GETDATE() WHERE USER_ID = @TempUserId
			--记录历史数据
			INSERT INTO dbo.TB_FUND_HISTORY( USER_FUND_ID ,TRADE_TYPE ,TRADE_DESC ,
			          TRADE_SERIAL_NO ,TRADE_FUND ,TRADE_DATE)
			VALUES  ( @TempUserFundId , 4 , N'终止市场时解冻的资金' , 
			          @ExchangeId , @TempFund , GETDATE() )
        	FETCH NEXT FROM AbortMatchCur INTO @TempUserId,@TempFund,@ExchangeId
        END
        CLOSE AbortMatchCur
        DEALLOCATE AbortMatchCur
        
        --更新状态
        UPDATE dbo.TB_EXCHANGE_BACK SET STATUS = 6,MATCH_AMOUNTS = 0 WHERE MATCH_ID = @Champ_Event_Id AND MATCH_TYPE = 2
        UPDATE dbo.TB_EXCHANGE_LAY SET STATUS = 6,MATCH_AMOUNTS = 0 WHERE MATCH_ID = @Champ_Event_Id AND MATCH_TYPE = 2
        UPDATE dbo.TB_EXCHANGE_DEAL SET STATUS = 3 WHERE MATCH_ID = @Champ_Event_Id AND MATCH_TYPE = 2
        
        UPDATE  dbo.TB_Champ_Event
        SET     Champ_Event_Status = 3
        WHERE   Champ_Event_ID = @Champ_Event_Id

		--扣除已释放的对冲资金
		--比赛ID,0半场结算 1 全场结算,1 体育比赛 2 冠军赛事,用户ID,计算日志ID
		exec pr_calc_real_hedge_rollback @Champ_Event_Id,1,2,null,null

  
        --更新缓存对象表
		exec pr_up_cache_object 4 
		COMMIT TRANSACTION
    END
GO