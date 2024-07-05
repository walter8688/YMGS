IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_settlement_rollback]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_settlement_rollback]
GO
CREATE PROCEDURE [dbo].[pr_calc_settlement_rollback]
    (
		@Match_Id int,--比赛ID
		@Calc_Flag int, --0半场结算 1 全场结算
		@Match_Type int, --1 体育比赛 2 冠军赛事
		@User_Id int, --当前结算用户ID
		@Log_Id int -- 结算日志ID
    )
AS


BEGIN
SET XACT_ABORT ON
BEGIN TRANSACTION TradeTrans
	
	--定义变量
	declare @User_Fund_Id int --用户资金账号ID
	declare @Tmp_Value decimal(18,2)
	
	--定义结算日志缓存表
	declare @Settlement_Log table
	(
		log_type_id int,
		log_object int,
		log_int_data1 int,
		log_int_data2 int,
		log_dbl_data1 decimal(18,2),
		log_dbl_data2 decimal(18,2)		
	)
	
	--定义代理和总代理佣金缓存表
	declare @Agent_Funds table
	(
		user_fund_id int,
		funds decimal(18,2)
	)

	--获得用户的资金
	select @User_Fund_Id=user_fund_Id  from tb_user_fund where user_id=@User_Id
	
	--缓存用户的相关结算日志
	insert into @Settlement_Log
	select log_type_id,
			log_object,
			log_int_data1,
			log_int_data2,
			log_dbl_data1,
			log_dbl_data2
	from tb_settlement_log_detail
	where log_id=@Log_Id and trade_user=@User_Id
	
	--开始依据结算日志回退相关记录
	
	--Step1 更新用户的投注记录的状态为结算前状态
	update tb_exchange_back
	set status=case tb_exchange_back.match_amounts
				when 0 then 2
				else 1
				end
	from @Settlement_Log A
	where tb_exchange_back.exchange_back_id=A.log_object
	and A.log_type_id=1
	
	--Step2 更新用户的受注记录的状态为结算前状态
	update tb_exchange_lay
	set status=case tb_exchange_lay.match_amounts
				when 0 then 2
				else 1
				end
	from @Settlement_Log A
	where tb_exchange_lay.exchange_lay_id=A.log_object
	and A.log_type_id=2
	
	--Step3 更新撮合记录状态未未结算
	update tb_exchange_deal
	set status=1
	where match_id=@Match_Id
	and match_type=@Match_Type
	and exchange_deal_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=3
	)

	--Step4 删除该用户撮合记录对应的结算记录。
	delete from tb_exchange_settle
	where exchange_settle_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=4
	)

	--Step5 删除该用户撮合记录对应的结算记录。
	update tb_match_hedge_fund
	set status=0
	where match_id=@Match_Id 
	and match_type=@Match_Type
	and hedge_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=7
	)
	
	--Step6 获得该用户的资金历史记录，撤销该资金历史记录对资金账号的影响。
	--log_dbl_data1: 4 解冻资金 5  结算 赢得利润和所输的本金 (直接给cur_fund变化)
	--6 所扣除的佣金 (直接给cur_fund变化)	11 对冲记录 (直接给cur_fund变化)				

	--Step6.1 扣除被解冻资金		
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=5 and A.log_int_data1=4
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_fund=cur_fund-@Tmp_Value,
	freezed_fund=freezed_fund+@Tmp_Value
	where user_fund_id=@User_Fund_Id

	--Step6.2 回滚(所赢得的利润、所输的本金、所扣的佣金、以及对冲记录
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=5 and A.log_int_data1<>4
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_fund=cur_fund-@Tmp_Value
	where user_fund_id=@User_Fund_Id
	
	--Step7 删除该用户的资金历史记录。
	delete from tb_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=5
	)
	
	
	--Step8 回滚该用户的积分历史记录
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=8
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_integral=cur_integral-@Tmp_Value
	where user_fund_id=@User_Fund_Id

	--Step9 删除该用户的积分历史记录。
	delete from tb_integral_history
	where integral_history_id in(
	select log_object from @Settlement_Log where log_type_id=8
	)

	--Step10 遍历用户所缴纳给代理和总代理的佣金记录，撤销该佣金记录对代理和总代理的影响。
	insert into @Agent_Funds
	select A.log_int_data2,isnull(sum(A.log_dbl_data1),0)
	from @Settlement_Log A
	where A.log_type_id=9
	group by A.log_int_data2
	
	update tb_user_fund
	set cur_fund=cur_fund-B.funds
	from tb_user_fund A
	inner join  @Agent_Funds B ON A.user_fund_id=B.user_fund_id

	--Step11 删除代理和总代理的佣金对应的资金历史记录
	delete from tb_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=9
	)
	
	--Step12 回滚系统资资金
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=6
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_system_main_fund
	set total_money=total_money-@Tmp_Value
	where main_func_id=1	
	
	--Step13 删除系统资金历史记录
	delete from tb_main_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=6
	)

COMMIT TRANSACTION TradeTrans
END
GO