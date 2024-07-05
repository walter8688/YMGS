IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_sport_match_settle]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_sport_match_settle]
GO
CREATE PROCEDURE [dbo].[pr_calc_sport_match_settle]
    (
		@Match_Id int,
		@Exchange_Deal_Id int,
		@Exchange_Back_Id int,
		@Exchange_Lay_Id int,
		@Is_Buyer_Win INT,
		@Bet_Calculate_Flag decimal(18,2),
		@Deal_Amount decimal(18,2),
		@Bet_Odds decimal(18,2),
		@Cur_User_Id int,
		@Match_Name nvarchar(100),
		@Market_Tmp_Name nvarchar(100),
		@Market_Name nvarchar(100),
		@Log_Id int
    )
AS
BEGIN

SET XACT_ABORT ON
BEGIN TRANSACTION TradeTrans
	DECLARE @Exchange_Settle_Id INT
	DECLARE @Buyer_Id INT						--买件ID
	DECLARE @Seller_Id INT						--卖家ID
	DECLARE @Buyer_Fund_Id INT					--买家资金账号ID
	DECLARE @Seller_Fund_Id INT					--卖家资金账号ID
	DECLARE @Buyer_Cur_Integral INT				--买家当前积分
	DECLARE @Seller_Cur_Integral INT			--卖家当前积分
	DECLARE @Buyer_Commission decimal(18,2)		--买家佣金
	DECLARE @Seller_Commission decimal(18,2)	--卖家佣金
	DECLARE @Buyer_Trade_Integral decimal(18,2)	--买家当前交易积分
	DECLARE @Seller_Trade_Integral decimal(18,2)--卖家当前交易积分
	DECLARE @Buyer_Benefits decimal(18,2)		--买家盈利
	DECLARE @Seller_Benefits decimal(18,2)		--卖家盈利
	DECLARE @Dbl_Temp decimal(18,2)				--临时变量
	DECLARE @Seller_Point decimal(18,2)			--卖家的本金
	
	DECLARE @Agent_Fund_Id int					--代理资金账号ID
	DECLARE @Agent_Commission decimal(18,2)		--代理返点佣金
	DECLARE @Main_Agent_Fund_Id int					--代理资金账号ID
	DECLARE @Main_Agent_Commission decimal(18,2)		--代理返点佣金
	DECLARE @Agent_Commission_Rate DECIMAL(18,2)  --代理的返点率
	DECLARE	@Main_Agent_Commission_Rate DECIMAL(18,2) --总代理的返点率
	DECLARE @Brokerage_Rate DECIMAL(18,4) --佣金率
	declare @Tmp_String nvarchar(max)
	declare @Tmp_Freezed_fund decimal(18,2) --临时解冻资金变量
	declare @Tmp_Int_Value int --临时整数变量
	declare @Tmp_Dbl_Value decimal(18,2)
	
	--获得买家和卖家的资金账号、积分等基础数据
	SELECT @Buyer_Id=TRADE_USER FROM TB_EXCHANGE_BACK WHERE EXCHANGE_BACK_ID=@Exchange_Back_Id
	SELECT @Seller_Id=TRADE_USER FROM TB_EXCHANGE_LAY WHERE EXCHANGE_LAY_ID=@Exchange_Lay_Id
	SELECT @Buyer_Fund_Id=USER_FUND_ID,@Buyer_Cur_Integral= CUR_INTEGRAL FROM TB_USER_FUND WHERE USER_ID=@Buyer_Id
	SELECT @Seller_Fund_Id=USER_FUND_ID,@Seller_Cur_Integral= CUR_INTEGRAL FROM TB_USER_FUND WHERE USER_ID=@Seller_Id

	IF(@Buyer_Id is null or @Seller_Id is null or @Buyer_Fund_Id is null or @Seller_Fund_Id is null)
	BEGIN
		RAISERROR('用户账号信息不完整!',16,1) WITH NOWAIT
		return
	END
	
	IF(@Log_Id is null or @Log_Id=-1)
	begin
		RAISERROR('计算日志没有启用,请核查!',16,1) WITH NOWAIT
		return
	end

	--如果当前比赛已经被其它用户在计算，则不能开始该次计算。
	declare @Temp_Log_Id int
	select top 1 @Temp_Log_Id = log_id from tb_settlement_log
	where match_id=@Match_Id and match_type=1
	order by begintime desc	
	if(@Temp_Log_Id <> @Log_Id)
	begin
		RAISERROR('当前比赛已经被其它用户在计算,请核查!',16,1) WITH NOWAIT
		return
	end
	
	--如果买家赢	
	IF(@Is_Buyer_Win=1)
	BEGIN
		--结算买家	
		SET @Buyer_Benefits = @Deal_Amount * @Bet_Calculate_Flag * (@Bet_Odds-1)
		SET @Brokerage_Rate = dbo.[udf_get_user_commission_rate](@Cur_User_Id,@Buyer_Cur_Integral)
		SET @Buyer_Commission = @Brokerage_Rate * @Buyer_Benefits
		
		--计算买家的积分
		SET @Buyer_Trade_Integral = @Deal_Amount + @Buyer_Benefits
		
		--把买家的盈利部分更新入资金历史账号表
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所赢得的利润'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,5,@Tmp_String,@Exchange_Deal_Id,@Buyer_Benefits,@Buyer_Id
		
		
		--把买家的佣金更新入资金历史账号表
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所扣除的佣金'
		set @Tmp_Dbl_Value = @Buyer_Commission*-1.0
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,6,@Tmp_String,@Exchange_Deal_Id,@Tmp_Dbl_Value,@Buyer_Id
		

		--把买家的积分更新到积分历史记录表中
		--@Log_Id,@User_Fund_Id,@Exchange_Deal_Id,@Deal_Fund,@Got_Integral,@Trade_Desc,@User_Id
		exec pr_calc_add_integralhistory_withlog @Log_Id,@Buyer_Fund_Id,@Exchange_Deal_Id,@Buyer_Trade_Integral,@Buyer_Trade_Integral,null,@Buyer_Id
		
		
		--解冻买家的资金计入日志
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = '解冻资金'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Deal_Amount,@Buyer_Id
		
		
		--把买家的盈利和佣金更新更新到用户资金账号表,并且解冻撮合交易所冻结的资金
		UPDATE TB_USER_FUND SET FREEZED_FUND=FREEZED_FUND-@Deal_Amount,
								CUR_FUND=CUR_FUND+@Deal_Amount+@Buyer_Benefits -@Buyer_Commission,
								CUR_INTEGRAL=CUR_INTEGRAL+@Buyer_Trade_Integral
		WHERE USER_FUND_ID=@Buyer_Fund_Id
			
		
		--结算卖家
		--计算卖家的积分
		SET @Seller_Trade_Integral =  @Deal_Amount*(@Bet_Odds-1)

		--把卖家的所输的资金更新入资金历史账号表
		--卖家所输掉的钱		
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		SET @Dbl_Temp = (@Deal_Amount * @Bet_Calculate_Flag * (@Bet_Odds-1))		
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所输的本金'
		set @Tmp_Dbl_Value = @Dbl_Temp*-1
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,5,@Tmp_String,@Exchange_Deal_Id,@Tmp_Dbl_Value,@Seller_Id
		
		--解冻卖家冻结资金并记录日志
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = '解冻资金'
		set @Tmp_Freezed_fund = @Deal_Amount * (@Bet_Odds-1)
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Tmp_Freezed_fund,@Seller_Id


		--把卖家的所输资金更新到用户资金账号表,并且解冻撮合交易所冻结的资金
		UPDATE TB_USER_FUND SET FREEZED_FUND=FREEZED_FUND-@Tmp_Freezed_fund,
								CUR_FUND=CUR_FUND+@Tmp_Freezed_fund-@Dbl_Temp,
								CUR_INTEGRAL=CUR_INTEGRAL+@Seller_Trade_Integral
		WHERE USER_FUND_ID=@Seller_Fund_Id				
		
		
		--把卖家的积分更新到积分历史记录表中
		--@Log_Id,@User_Fund_Id,@Exchange_Deal_Id,@Deal_Fund,@Got_Integral,@Trade_Desc,@User_Id
		exec pr_calc_add_integralhistory_withlog @Log_Id,@Seller_Fund_Id,@Exchange_Deal_Id,@Seller_Trade_Integral,@Seller_Trade_Integral,null,@Seller_Id		
		
		--生成交易结算记录
		--@Log_Id,@Exchange_Deal_Id,@Win_User_Id,@Lose_User_Id,@Win_Integral,@Lose_Integral,@Brokerage,@Brokerage_Rate,@Exchange_Win_Flag,@Exchange_Settle_Id output
		exec pr_calc_add_exchangesettle_withlog @Log_Id,@Exchange_Deal_Id,@Buyer_Id,@Seller_Id,@Buyer_Trade_Integral,@Seller_Trade_Integral,@Buyer_Commission,@Brokerage_Rate,0,@Exchange_Settle_Id output
	END
	ELSE IF(@Is_Buyer_Win = 2)--如果卖家赢
	BEGIN
		--结算卖家
		SET @Seller_Benefits = @Deal_Amount * @Bet_Calculate_Flag
		SET @Brokerage_Rate = dbo.udf_get_user_commission_rate(@Cur_User_Id,@Seller_Cur_Integral)
		SET @Seller_Commission = @Brokerage_Rate * @Seller_Benefits
		
		--卖家的本金
		SET @Seller_Point = @Deal_Amount * @Bet_Calculate_Flag*(@Bet_Odds-1)
		--计算卖家的积分
		SET @Seller_Trade_Integral = @Seller_Point + @Seller_Benefits
		
		--把卖家的盈利部分更新入资金历史账号表
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所赢得的利润'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,5,@Tmp_String,@Exchange_Deal_Id,@Seller_Benefits,@Seller_Id
		
		--把卖家的佣金更新如资金历史账号表
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所扣除的佣金'
		set @Tmp_Dbl_Value = @Seller_Commission*-1
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,6,@Tmp_String,@Exchange_Deal_Id,@Tmp_Dbl_Value,@Seller_Id
		
		--把卖家的积分更新到积分历史记录表中
		--@Log_Id,@User_Fund_Id,@Exchange_Deal_Id,@Deal_Fund,@Got_Integral,@Trade_Desc,@User_Id
		exec pr_calc_add_integralhistory_withlog @Log_Id,@Seller_Fund_Id,@Exchange_Deal_Id,@Seller_Trade_Integral,@Seller_Trade_Integral,null,@Seller_Id


		--解冻卖家冻结资金并记录日志
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = '解冻资金'
		set @Tmp_Freezed_fund = @Deal_Amount * (@Bet_Odds-1)
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Tmp_Freezed_fund,@Seller_Id

		
		--把卖家的盈利和佣金更新更新到用户资金账号表,并且解冻撮合交易所冻结的资金
		UPDATE TB_USER_FUND SET FREEZED_FUND=FREEZED_FUND-@Tmp_Freezed_fund,
								CUR_FUND=CUR_FUND+@Tmp_Freezed_fund
											+@Seller_Benefits -@Seller_Commission,
								CUR_INTEGRAL=CUR_INTEGRAL+@Seller_Trade_Integral
		WHERE USER_FUND_ID=@Seller_Fund_Id
		
		
		
		--结算买家
		--计算买家的积分
		SET @Buyer_Trade_Integral = @Deal_Amount
		SET @Dbl_Temp = @Deal_Amount * @Bet_Calculate_Flag

		--把买家的所输的资金更新入资金历史账号表
		--买家所输掉的钱
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '所输的本金'
		set @Tmp_Dbl_Value = @Dbl_Temp*-1
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,5,@Tmp_String,@Exchange_Deal_Id,@Tmp_Dbl_Value,@Buyer_Id

		--解冻买家冻结资金并记录日志
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = '解冻资金'
		set @Tmp_Freezed_fund = @Deal_Amount
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Tmp_Freezed_fund,@Buyer_Id

		
		--买家的所输资金更新到用户资金账号表,并且解冻撮合交易所冻结的资金
		UPDATE TB_USER_FUND SET FREEZED_FUND=FREEZED_FUND-@Tmp_Freezed_fund,
								CUR_FUND=CUR_FUND+@Tmp_Freezed_fund-@Dbl_Temp,
								CUR_INTEGRAL=CUR_INTEGRAL+@Buyer_Trade_Integral
		WHERE USER_FUND_ID=@Buyer_Fund_Id				

		
		--把买家的积分更新到积分历史记录表中
		--@Log_Id,@User_Fund_Id,@Exchange_Deal_Id,@Deal_Fund,@Got_Integral,@Trade_Desc,@User_Id
		exec pr_calc_add_integralhistory_withlog @Log_Id,@Buyer_Fund_Id,@Exchange_Deal_Id,@Buyer_Trade_Integral,@Buyer_Trade_Integral,null,@Buyer_Id
					
		--生成交易结算记录
		--@Log_Id,@Exchange_Deal_Id,@Win_User_Id,@Lose_User_Id,@Win_Integral,@Lose_Integral,@Brokerage,@Brokerage_Rate,@Exchange_Win_Flag,@Exchange_Settle_Id output
		exec pr_calc_add_exchangesettle_withlog @Log_Id,@Exchange_Deal_Id,@Seller_Id,@Buyer_Id,@Seller_Trade_Integral,@Buyer_Trade_Integral,@Seller_Commission,@Brokerage_Rate,1,@Exchange_Settle_Id output
	END
	ELSE IF(@Is_Buyer_Win = 3) --让分盘平局
	BEGIN
		--解冻买家冻结的资金
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '让分盘平局买家解冻的资金'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Buyer_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Deal_Amount,@Buyer_Id

		UPDATE dbo.TB_USER_FUND SET CUR_FUND = CUR_FUND + @Deal_Amount,FREEZED_FUND = FREEZED_FUND - @Deal_Amount
		WHERE USER_ID = @Buyer_Fund_Id
			
			
		--解冻卖家冻结的资金	
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '让分盘平局卖家解冻的资金'
		set @Tmp_Freezed_fund = (@Bet_Odds - 1)*@Deal_Amount
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Seller_Fund_Id,4,@Tmp_String,@Exchange_Deal_Id,@Tmp_Freezed_fund,@Seller_Id

		UPDATE dbo.TB_USER_FUND SET CUR_FUND = CUR_FUND + @Tmp_Freezed_fund,
		FREEZED_FUND = FREEZED_FUND - @Tmp_Freezed_fund
		WHERE USER_ID = @Seller_Fund_Id					
			
			
		--生成交易结算记录
		--@Log_Id,@Exchange_Deal_Id,@Win_User_Id,@Lose_User_Id,@Win_Integral,@Lose_Integral,@Brokerage,@Brokerage_Rate,@Exchange_Win_Flag,@Exchange_Settle_Id output
		exec pr_calc_add_exchangesettle_withlog @Log_Id,@Exchange_Deal_Id,@Buyer_Id,@Seller_Id,0,0,0,0,2,@Exchange_Settle_Id output		
	END
	
	IF(@Is_Buyer_Win <> 3)--让分盘平局不需要计算佣金
	BEGIN
		--计算代理佣金
		declare @Temp_User_Id int
		declare @Temp_Benefits decimal(18,2)
		declare @Temp_Commission decimal(18,2)
		declare @Temp_Fund_id int
		IF(@Is_Buyer_Win=1)
		BEGIN
			set @Temp_User_Id = @Buyer_Id
			set @Temp_Benefits = @Buyer_Benefits	
			set @Temp_Commission = @Buyer_Commission	
			set @Temp_Fund_id = @Buyer_Fund_Id
		END
		ELSE IF(@Is_Buyer_Win = 2)
		BEGIN
			set @Temp_User_Id = @Seller_Id
			set @Temp_Benefits = @Seller_Benefits				
			set @Temp_Commission = @Seller_Commission
			set @Temp_Fund_id = @Seller_Fund_Id
		END
		
		exec dbo.pr_calc_agent_commission @Temp_User_Id,@Temp_Benefits,@Agent_Fund_Id output,
							@Agent_Commission output,@Main_Agent_Fund_Id output,@Main_Agent_Commission OUTPUT,
							@Agent_Commission_Rate OUTPUT,@Main_Agent_Commission_Rate OUTPUT
		if(@Agent_Fund_Id is not null and @Agent_Fund_Id>0 and @Agent_Commission>0)
		begin		
			--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@Come_User_Id
			set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '给代理的返点佣金'
			exec pr_calc_add_agentfundhistory_withlog @Log_Id,@Agent_Fund_Id,7,@Tmp_String,@Exchange_Deal_Id,@Agent_Commission,@Temp_User_Id
			
			UPDATE dbo.TB_EXCHANGE_SETTLE SET AGENT_COMMISSION_RATE = ISNULL(@Agent_Commission_Rate,0)
			WHERE EXCHANGE_SETTLE_ID = @Exchange_Settle_Id	
				
			UPDATE TB_USER_FUND SET CUR_FUND=CUR_FUND+@Agent_Commission
			WHERE USER_FUND_ID=@Agent_Fund_Id
		end
		if(@Main_Agent_Fund_Id is not null and @Main_Agent_Fund_Id>0 and @Main_Agent_Commission>0)
		begin
			--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@Come_User_Id
			set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '给总代理的返点佣金'
			exec pr_calc_add_agentfundhistory_withlog @Log_Id,@Main_Agent_Fund_Id,8,@Tmp_String,@Exchange_Deal_Id,@Main_Agent_Commission,@Temp_User_Id
			
			UPDATE dbo.TB_EXCHANGE_SETTLE SET MAIN_AGENT_COMMISSION_RATE = ISNULL(@Main_Agent_Commission_Rate,0)
			WHERE EXCHANGE_SETTLE_ID = @Exchange_Settle_Id	
				
			UPDATE TB_USER_FUND SET CUR_FUND=CUR_FUND+@Main_Agent_Commission
			WHERE USER_FUND_ID=@Main_Agent_Fund_Id
		end
		
		--给系统账号结算佣金返点
		SET @Main_Agent_Commission=ISNULL(@Main_Agent_Commission,0)
		SET @Agent_Commission = ISNULL(@Agent_Commission,0)
		IF(@Temp_Commission-@Main_Agent_Commission-@Agent_Commission>0)
		BEGIN
			set @Tmp_String = @Match_Name + '[' + @Market_Name + ']' + '给系统账号的返点佣金'
			INSERT INTO TB_MAIN_FUND_HISTORY(COME_USER_FUND_ID,FUND_AMOUNTS,FUND_MEMO,EXCHANGE_SETTLE_ID,TRADE_DATE)
			VALUES(@Temp_Fund_id,@Temp_Commission-@Main_Agent_Commission-@Agent_Commission,
					@Tmp_String,@Exchange_Settle_Id,getdate());	
			select @Tmp_Int_Value = SCOPE_IDENTITY() 			
			
			--增加系统资金历史记录
			--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
			--系统资金历史记录日志的Log_type_Id=6
			set @Tmp_Dbl_Value = @Temp_Commission-@Main_Agent_Commission-@Agent_Commission
			exec pr_calc_log @Log_Id,6,@Temp_User_Id,@Tmp_Int_Value,@Exchange_Settle_Id,null,@Tmp_Dbl_Value,null,null,@Tmp_String						
			
			UPDATE TB_SYSTEM_MAIN_FUND SET TOTAL_MONEY=TOTAL_MONEY+@Temp_Commission-@Main_Agent_Commission-@Agent_Commission
			WHERE MAIN_FUNC_ID=1
		END				
	END

	--更新撮合交易记录状态并记录日志
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--撮合交易记录日志的Log_type_Id=3
	exec pr_calc_log @Log_Id,3,@Buyer_Id,@Exchange_Deal_Id,1,null,null,null,null,null	
	UPDATE TB_EXCHANGE_DEAL SET STATUS=2 WHERE EXCHANGE_DEAL_ID=@Exchange_Deal_Id
	
	--更新投注表状态为已结算并记录日志
	UPDATE TB_EXCHANGE_BACK SET STATUS=3 WHERE EXCHANGE_BACK_ID=@Exchange_Back_Id	
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--投注记录日志的Log_type_Id=1
	exec pr_calc_log @Log_Id,1,@Buyer_Id,@Exchange_Back_Id,null,null,null,null,null,null	
	
	
	--更新受注状态为已结算并记录日志
	UPDATE TB_EXCHANGE_LAY SET STATUS=3 WHERE EXCHANGE_LAY_ID=@Exchange_Lay_Id
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--受注记录日志的Log_type_Id=2
	exec pr_calc_log @Log_Id,2,@Seller_Id,@Exchange_Lay_Id,null,null,null,null,null,null	

COMMIT TRANSACTION TradeTrans
END
GO