IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_real_fund_hedge_main]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_real_fund_hedge_main]
GO
CREATE PROCEDURE [dbo].[pr_calc_real_fund_hedge_main]
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
	--保存此次下单撮合交易的所有用户信息
	declare @users table(
		trade_user int
	)
	
	insert into @users
	select distinct
		case @Trade_Flag
		when 1 then C.trade_user
		when 2 then B.trade_user
		end
	from tb_exchange_deal A
	left join tb_exchange_back B on A.exchange_back_id=B.exchange_back_id
	left join tb_exchange_lay C on A.exchange_lay_id=C.exchange_lay_id
	where A.match_id=@Match_Id
	and A.market_id=@Market_Id
	and A.match_type=@Match_Type
	and (
		(A.exchange_back_id=@Exchange_Back_Lay_Id and @Trade_Flag=1)
		or
		(A.exchange_lay_id=@Exchange_Back_Lay_Id and @Trade_Flag=2)
	)	
	union	
	select top 1 @Trade_User
	from tb_exchange_deal A
	where A.match_id=@Match_Id
	and A.market_id=@Market_Id
	and A.match_type=@Match_Type
	and (A.exchange_back_id=@Exchange_Back_Lay_Id or A.exchange_lay_id=@Exchange_Back_Lay_Id)

	declare @tmp_trade_user int
	declare usercursor cursor FORWARD_ONLY for
	select trade_user from @users
	open usercursor
	fetch next from usercursor into @tmp_trade_user
	while(@@fetch_status=0)
	begin
		exec pr_calc_real_fund_hedge @Match_Id,@Market_Id,@Match_Type,@tmp_trade_user,1,@Exchange_Back_Lay_Id,@tmp_trade_user
		fetch next from usercursor into @tmp_trade_user
	end	
END
GO