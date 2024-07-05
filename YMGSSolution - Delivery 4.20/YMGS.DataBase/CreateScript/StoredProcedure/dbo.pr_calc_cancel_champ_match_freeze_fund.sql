IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_cancel_champ_match_freeze_fund]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_cancel_champ_match_freeze_fund]
GO
CREATE PROCEDURE [dbo].[pr_calc_cancel_champ_match_freeze_fund]
    (
		@Champ_Event_Id int,
		@Cur_User_Id int,
		@Log_Id int
    )
AS
--declare @Champ_Event_Id int
--declare @Cur_User_Id int

BEGIN

SET XACT_ABORT ON
BEGIN TRANSACTION
	
	DECLARE @Trade_User INT	--交易用户ID
	DECLARE @Match_Amounts DECIMAL(18,2)--撮合剩余金额
	DECLARE @User_Fund_Id INT --用户资金账号ID
	DECLARE @Exchange_Back_OR_Lay_Id INT
	DECLARE @Status INT
	DECLARE @Match_Name nvarchar(100)--比赛名称
	DECLARE @Market_Name nvarchar(100)--市场名称
	DECLARE @Odds decimal(18,2)--赔率
	declare @Bet_Amounts decimal(18,2)--投受注金额
	declare @Tmp_String nvarchar(max)
	declare @Match_Status int	

	--查询当前可以进行结算的所有投注记录
	DECLARE ExchangeBackCursor CURSOR LOCAL FORWARD_ONLY FOR	
	SELECT A.TRADE_USER,A.MATCH_AMOUNTS,E.USER_FUND_ID,A.EXCHANGE_BACK_ID,A.STATUS,B.CHAMP_EVENT_NAME,D.CHAMP_EVENT_MEMBER_NAME,A.BET_AMOUNTS
	FROM TB_EXCHANGE_BACK A INNER JOIN TB_CHAMP_EVENT B ON A.MATCH_ID=B.CHAMP_EVENT_ID
	LEFT JOIN TB_CHAMP_MARKET C ON A.MARKET_ID=C.CHAMP_MARKET_ID
	LEFT JOIN TB_CHAMP_EVENT_MEMBER D ON C.CHAMP_MEMBER_ID=D.CHAMP_EVENT_MEMBER_ID
	LEFT JOIN TB_USER_FUND E ON E.USER_ID=A.TRADE_USER
	WHERE A.MATCH_ID=@Champ_Event_Id
	AND A.STATUS IN(1,3) --当前可撮合、已结算的投注记录需要取消冻结资金
	AND B.CHAMP_EVENT_STATUS=5
	AND A.MATCH_AMOUNTS>0	
	OPEN ExchangeBackCursor
	FETCH NEXT FROM ExchangeBackCursor INTO @Trade_User,@Match_Amounts,@User_Fund_Id,@Exchange_Back_OR_Lay_Id,@Status,@Match_Name,@Market_Name,@Bet_Amounts
	WHILE(@@FETCH_STATUS=0)
	BEGIN
		--把解冻的资金写入历史记录
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']，结算时未成交的投注解冻的资金'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@User_Fund_Id,4,@Tmp_String,@Exchange_Back_OR_Lay_Id,@Match_Amounts,@Trade_User
		
		UPDATE TB_USER_FUND SET CUR_FUND=CUR_FUND+@Match_Amounts,FREEZED_FUND=FREEZED_FUND-@Match_Amounts
		WHERE USER_FUND_ID=@User_Fund_Id
		
		UPDATE TB_EXCHANGE_BACK SET STATUS=4 WHERE EXCHANGE_BACK_ID=@Exchange_Back_OR_Lay_Id

		--对于没有被撮合的投受注记录，记录到结算日志中，已经撮合的应该已经记录过了
		if(@Bet_Amounts = @Match_Amounts)
		begin
			--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
			--投注记录日志的Log_type_Id=1
			exec pr_calc_log @Log_Id,1,@Trade_User,@Exchange_Back_OR_Lay_Id,null,null,null,null,null,null
		end 
		
		FETCH NEXT FROM ExchangeBackCursor INTO @Trade_User,@Match_Amounts,@User_Fund_Id,@Exchange_Back_OR_Lay_Id,@Status,@Match_Name,@Market_Name,@Bet_Amounts
	END
	DEALLOCATE ExchangeBackCursor
	
	
	
	--查询当前可以进行结算的所有受注记录
	DECLARE ExchangeLayCursor CURSOR LOCAL FORWARD_ONLY FOR	
	SELECT A.TRADE_USER,A.MATCH_AMOUNTS,E.USER_FUND_ID,A.EXCHANGE_LAY_ID,A.STATUS,
		B.CHAMP_EVENT_NAME,D.CHAMP_EVENT_MEMBER_NAME,A.ODDS,A.BET_AMOUNTS
	FROM TB_EXCHANGE_LAY A INNER JOIN TB_CHAMP_EVENT B ON A.MATCH_ID=B.CHAMP_EVENT_ID
	LEFT JOIN TB_CHAMP_MARKET C ON A.MARKET_ID=C.CHAMP_MARKET_ID
	LEFT JOIN TB_CHAMP_EVENT_MEMBER D ON C.CHAMP_MEMBER_ID=D.CHAMP_EVENT_MEMBER_ID
	LEFT JOIN TB_USER_FUND E ON E.USER_ID=A.TRADE_USER
	WHERE A.MATCH_ID=@Champ_Event_Id
	AND A.STATUS IN(1,3) --当前可撮合、已结算撮合记录的投注记录需要取消冻结资金
	AND B.CHAMP_EVENT_STATUS=5
	AND A.MATCH_AMOUNTS>0
	OPEN ExchangeLayCursor
	FETCH NEXT FROM ExchangeLayCursor INTO @Trade_User,@Match_Amounts,@User_Fund_Id,@Exchange_Back_OR_Lay_Id,@Status,@Match_Name,@Market_Name,@Odds,@Bet_Amounts
	WHILE(@@FETCH_STATUS=0)
	BEGIN	
		declare @dblTemp decimal(18,2)
		SET @dblTemp = @Match_Amounts * (@Odds-1);
		--把解冻的资金写入历史记录
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		set @Tmp_String = @Match_Name + '[' + @Market_Name + ']，结算时未成交的受注解冻的资金'
		exec pr_calc_add_fundhistory_withlog @Log_Id,@User_Fund_Id,4,@Tmp_String,@Exchange_Back_OR_Lay_Id,@dblTemp,@Trade_User
		
		
		UPDATE TB_USER_FUND SET CUR_FUND=CUR_FUND+@dblTemp,FREEZED_FUND=FREEZED_FUND-@dblTemp
		WHERE USER_FUND_ID=@User_Fund_Id
		
		UPDATE TB_EXCHANGE_LAY SET STATUS=4 WHERE EXCHANGE_LAY_ID=@Exchange_Back_OR_Lay_Id
		
		--对于没有被撮合的投受注记录，记录到结算日志中，已经撮合的应该已经记录过了
		if(@Bet_Amounts = @Match_Amounts)
		begin
			--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
			--受记录日志的Log_type_Id=2
			exec pr_calc_log @Log_Id,2,@Trade_User,@Exchange_Back_OR_Lay_Id,null,null,null,null,null,null	
		end 
		
		FETCH NEXT FROM ExchangeLayCursor INTO @Trade_User,@Match_Amounts,@User_Fund_Id,@Exchange_Back_OR_Lay_Id,@Status,@Match_Name,@Market_Name,@Odds,@Bet_Amounts
	END
	DEALLOCATE ExchangeLayCursor
	
	--扣除已释放的对冲资金
	--比赛ID,0半场结算 1 全场结算,1 体育比赛 2 冠军赛事,用户ID,计算日志ID
	exec pr_calc_real_hedge_rollback @Champ_Event_Id,1,1,@Cur_User_Id,@Log_Id
	
	select @Match_Status=CHAMP_EVENT_STATUS from TB_CHAMP_EVENT where CHAMP_EVENT_ID=@Champ_Event_Id
	
	--更新比赛状态为已结算
	UPDATE TB_CHAMP_EVENT SET CHAMP_EVENT_STATUS=4 WHERE CHAMP_EVENT_ID=@Champ_Event_Id
	
	--更新结算日志表中比赛状态等信息
	update tb_settlement_log set original_status=@Match_Status,[status]=1,
	endtime=getdate()
	where log_id=@Log_Id	
	
COMMIT TRANSACTION
END
GO