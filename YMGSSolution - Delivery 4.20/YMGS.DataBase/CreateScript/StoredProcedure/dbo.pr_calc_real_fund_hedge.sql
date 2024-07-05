IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_real_fund_hedge]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_real_fund_hedge]
GO
CREATE PROCEDURE [dbo].[pr_calc_real_fund_hedge]
    (
		@Match_Id int,
		@Market_Id int,
		@Match_Type int,
		@Trade_User int,
		@Trade_Flag int,--1 back 2 lay
		@Exchange_Back_Lay_Id int,
		@CurUser int
    )
AS
BEGIN
	--第一步：定义中间表
	--保存计算对冲中间数据
	DECLARE @hedge_process TABLE
	(
		TRADE_FLAG int ,	
        EXCHANGE_DEAL_ID int ,	
        FIRST_TEAM_WIN_FUNDS decimal(18,2),
        LAST_TEAM_WIN_FUNDS decimal(18,2),
        HALF_FUNDS decimal(18,2)
	)
	
	--用来保存已经撮合成交的投注和受注记录
	declare @exchange_rec table
	(
		exchange_deal_id int,
		exchange_back_id int,
		exchange_lay_id int,
		back_user int,
		lay_user int,
		deal_amount decimal(18,2),
		status int,
		odds decimal(18,2),
		deal_time datetime,		
		market_flag int,
		bet_type_id int,
		market_tmp_type int,
		trade_flag int -- 1 投注 2 受注 3自己投受注成功		
	)
	
	declare @tmp_commission_rate decimal(18,2)
	set @tmp_commission_rate= 0.05
	
	--第二步:按照下单顺序依次找出所有的已被撮合的投注记录和受注记录
	--包括足球和娱乐类		
	if(@Match_Type=1)
	begin	
		insert into @exchange_rec	
		select A.exchange_deal_id,
			A1.exchange_back_id,
			A2.exchange_lay_id,
			A1.trade_user back_user,
			A2.trade_user lay_user,
			A.deal_amount,
			A.status,
			A.odds,
			A.deal_time,
			B.market_flag,
			C.bet_type_id,
			C.market_tmp_type,
			1 trade_flag
		from tb_exchange_deal A
		left join tb_exchange_back A1 on A1.exchange_back_id=A.exchange_back_id
		left join tb_exchange_lay A2 on A2.exchange_lay_id=A.exchange_lay_id		
		left join tb_match_market B on A.market_id=B.Market_id
		left join tb_market_template C on B.market_tmp_id=C.market_tmp_id
		where A.match_id=@Match_Id
		and A.market_id=@Market_Id
		and A.match_type=@Match_Type
		and (A1.trade_user=@Trade_User or A2.trade_user=@Trade_User)
	end
	if(@Match_Type=2)
	begin
		insert into @exchange_rec	
		select A.exchange_deal_id,
			A1.exchange_back_id,
			A2.exchange_lay_id,
			A1.trade_user back_user,
			A2.trade_user lay_user,
			A.deal_amount,
			A.status,
			A.odds,
			A.deal_time,
			null market_flag,
			null bet_type_id,
			null market_tmp_type,
			1 trade_flag
		from tb_exchange_deal A
		left join tb_exchange_back A1 on A1.exchange_back_id=A.exchange_back_id
		left join tb_exchange_lay A2 on A2.exchange_lay_id=A.exchange_lay_id		
		where A.match_id=@Match_Id
		and A.market_id=@Market_Id
		and A.match_type=@Match_Type
		and (A1.trade_user=@Trade_User or A2.trade_user=@Trade_User)
	end	
	--更新投受注标志
	update @exchange_rec set trade_flag=1 where back_user=@Trade_User and lay_user<>@Trade_User
	update @exchange_rec set trade_flag=2 where back_user<>@Trade_User and lay_user=@Trade_User
	update @exchange_rec set trade_flag=3 where back_user=@Trade_User and lay_user=@Trade_User		

	
	--第三步：如果当前已撮合部分只有一笔投注数据或受注数据
	--则不需要继续计算，说明没有必要对冲。
	declare @tempCount int
	select @tempCount = count(*) from @exchange_rec where trade_flag in(1,2)
	--select @tempCount
	if(@tempCount<=1 and not exists(select * from @exchange_rec where trade_flag=3))
		return

			
	--第四步:计算已撮合部分对冲数据
	--定义临时变量
	declare @tmp_exchange_deal_id int
	declare @tmp_exchange_back_id int
	declare @tmp_exchange_lay_id int
	declare @tmp_back_user int
	declare @tmp_lay_user int
	declare @tmp_deal_amount decimal(18,2)
	declare @tmp_status int
	declare @tmp_odds decimal(18,2)
	declare @tmp_market_flag int
	declare @tmp_bet_type_id int
	declare @tmp_market_tmp_type int
	declare @tmp_trade_flag int -- 1 投注 2 受注 3自己投受
	
	declare exchange_cursor cursor FORWARD_ONLY for	
	select exchange_deal_id,
		exchange_back_id,
		exchange_lay_id,
		back_user,
		lay_user,
		deal_amount,
		status,
		odds,
		market_flag,
		bet_type_id,
		market_tmp_type,
		trade_flag
	from @exchange_rec
	order by deal_time asc	
	open exchange_cursor
	fetch exchange_cursor into @tmp_exchange_deal_id,@tmp_exchange_back_id,@tmp_exchange_lay_id,
		@tmp_back_user,@tmp_lay_user,@tmp_deal_amount,@tmp_status,@tmp_odds,@tmp_market_flag,
		@tmp_bet_type_id,@tmp_market_tmp_type,@tmp_trade_flag
	while @@fetch_status=0
	begin
		declare @tmp_first_win_funds decimal(18,2)
		declare @tmp_last_win_funds decimal(18,2)
		declare @tmp_half_funds decimal(18,2)
		--调用计算输赢的函数计算假设的输或者赢的金额
		select @tmp_first_win_funds=first_team_win_funds,
			@tmp_last_win_funds = last_team_win_funds,
			@tmp_half_funds=half_funds
			from dbo.udf_calc_realtime_football_result(@Match_Id,@Market_Id,
						@Match_Type,@tmp_trade_flag,@tmp_deal_amount,
						@tmp_odds,@tmp_bet_type_id,@tmp_market_tmp_type,@tmp_market_flag)
		insert into @hedge_process(trade_flag,exchange_deal_id,FIRST_TEAM_WIN_FUNDS,last_team_win_funds,half_funds)
		values(@tmp_trade_flag,@tmp_exchange_deal_id,isnull(@tmp_first_win_funds,0),isnull(@tmp_last_win_funds,0),isnull(@tmp_half_funds,0))

		fetch exchange_cursor into @tmp_exchange_deal_id,@tmp_exchange_back_id,@tmp_exchange_lay_id,
			@tmp_back_user,@tmp_lay_user,@tmp_deal_amount,@tmp_status,@tmp_odds,@tmp_market_flag,
			@tmp_bet_type_id,@tmp_market_tmp_type,@tmp_trade_flag
	end
	deallocate exchange_cursor	
	
	 		
	--第五步: 计算本次对冲金额，佣金按照最大佣金率5%来计算。
	declare @tmp_sum_first_funds decimal(18,2)
	declare @tmp_sum_last_funds decimal(18,2)
	declare @tmp_sum_half_funds decimal(18,2)
	declare @tmp_hedge_funds decimal(18,2)
	select @tmp_sum_first_funds=isnull(sum(first_team_win_funds),0),
		@tmp_sum_last_funds=isnull(sum(last_team_win_funds),0),
		@tmp_sum_half_funds = isnull(sum(half_funds),0)
		from @hedge_process		

	if(@tmp_sum_first_funds<=@tmp_sum_last_funds)
		set @tmp_hedge_funds = @tmp_sum_first_funds
	else
		set @tmp_hedge_funds=@tmp_sum_last_funds

	--如果是体育比赛,并且是让球盘时需要考虑平局,半赢等情况
	--判断规则：如果@tmp_hedge_funds大于零，说明处理了半赢等情况
	if(@Match_Type=1 and @tmp_sum_half_funds>0)
	begin
		if(@tmp_sum_half_funds<@tmp_hedge_funds)
			set @tmp_hedge_funds = @tmp_sum_half_funds		
	end
	
	--扣除佣金
	if(@tmp_hedge_funds>0)
	begin
		set @tmp_hedge_funds = (1-@tmp_commission_rate)*@tmp_hedge_funds
	end			
	
		
	--再减去已经对冲的金额
	select @tmp_hedge_funds=@tmp_hedge_funds-isnull(sum(hedge_amounts),0)
	from tb_match_hedge_fund
	where match_id=@Match_Id 
	and market_id=@Market_Id
	and trade_user=@Trade_User
	and match_type=@Match_Type
	and [status]=0
	if(@tmp_hedge_funds<=0)
		return
		
		
	
	--第六步:对冲金额进入用户用户账号资金、对冲资金表中。
	declare @tmp_match_name nvarchar(max)
	declare @tmp_market_name nvarchar(max)
	declare @tmp_name nvarchar(max)
	
	if(@Match_Type=1)
	begin
		select @tmp_match_name=isnull(B.match_name,''),
			@tmp_market_name=isnull(A.market_name,'')
		from tb_match_market A
		left join tb_match B on A.match_id=B.match_id
		where A.match_id=@Match_Id and A.market_id=@Market_Id
	end
	
	if(@Match_Type=2)
	begin
		select @tmp_match_name=isnull(B.champ_event_name,''),
			@tmp_market_name=isnull(C.champ_event_member_name,'')			
		from tb_champ_market A
		left join tb_champ_event B on A.champ_event_id=B.champ_event_id
		left join tb_champ_event_member C on A.champ_member_id=C.champ_event_member_id
		where A.champ_event_id=@Match_Id and A.champ_market_id=@Market_Id		
	end

	set @tmp_name = isnull(@tmp_market_name,'') + '[' + isnull(@tmp_match_name,'') + ']'
						+ '对冲释放资金'
	
	declare @tmp_user_fund_id int
	select @tmp_user_fund_id = user_fund_id from tb_user_fund where user_id=@Trade_User
	if(@tmp_user_fund_id is null)
		return
	--记录对冲记录		
	insert into tb_match_hedge_fund(match_id,market_id,trade_user,
	match_type,trade_flag,exchange_back_lay_id,hedge_amounts,operate_user,operate_time,status)
	values(@Match_Id,@Market_Id,@Trade_User,@Match_Type,@Trade_Flag,@Exchange_Back_Lay_Id,
	@tmp_hedge_funds,@Trade_User,getdate(),0)	
	
	--把对冲记录计入资金历史账号表中
	INSERT INTO TB_FUND_HISTORY(USER_FUND_ID,TRADE_TYPE,TRADE_DESC,TRADE_SERIAL_NO,TRADE_FUND,TRADE_DATE)
	VALUES(@tmp_user_fund_id,10,@tmp_name,@Exchange_Back_Lay_Id,@tmp_hedge_funds ,getdate());

	--更新资金表
	update tb_user_fund set cur_fund=cur_fund+@tmp_hedge_funds where user_fund_id=@tmp_user_fund_id
	
END
GO