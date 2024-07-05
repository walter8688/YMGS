IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_real_hedge_rollback]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_real_hedge_rollback]
GO
CREATE PROCEDURE [dbo].[pr_calc_real_hedge_rollback]
    (
		@Match_Id int,--����ID
		@Calc_Flag int, --0�볡���� 1 ȫ������
		@Match_Type int, --1 �������� 2 �ھ�����
		@CurUser int, --�û�ID
		@Log_Id int --������־ID
    )
AS

BEGIN

	--�볡����:ֻ��Ҫ�۳��볡�淨�����ĶԳ��ʽ�
	--ȫ������:�۳�ȫ���Գ��ʽ�
	
	declare @Tmp_User_Fund_Id int
	declare @Tmp_Trade_User int
	declare @Tmp_Match_Name nvarchar(max)
	declare @Tmp_Market_Name nvarchar(max)
	declare @Tmp_Hedge_Amounts decimal(18,2)
	declare @Tmp_Exchange_Back_Lay_Id int
	declare @Tmp_Hedge_Id int
	declare @Tmp_Name nvarchar(max)
	declare @Tmp_Dbl_Value decimal(18,2)
	
	declare @temp_hedge table
	(
		user_fund_Id int,
		match_name nvarchar(200),
		market_name nvarchar(200),
		hedge_amounts decimal(18,2),
		exchange_back_lay_id int,
		hedge_id int,
		trade_user int
	)
	
	--��������
	if(@Match_Type=1)
	begin	
		insert into @temp_hedge
		select D.user_fund_Id,E.match_name,B.market_name,A.hedge_amounts
			,A.exchange_back_lay_id,A.hedge_id,A.trade_user
		from tb_match_hedge_fund A
		left join tb_match_market B on A.match_id=B.match_id and A.market_id=B.market_id
		left join tb_market_template C on B.market_tmp_id=C.market_tmp_id
		left join tb_user_fund D on A.trade_user=D.user_id
		left join tb_match E on a.match_id=E.match_id
		where A.match_id=@Match_Id and A.match_type=@Match_Type and
		(
			(@Calc_Flag=0 and C.market_tmp_type=0)
			or
			(@Calc_Flag=1)
		)
		and A.status=0
	end
	
	--�ھ�����
	if(@Match_Type=2)
	begin	
		insert into @temp_hedge
		select E.user_fund_Id,
		D.champ_event_name match_name,
		C.champ_event_member_name market_name,
		A.hedge_amounts,
		A.exchange_back_lay_id,
		A.hedge_id,
		A.trade_user
		from tb_match_hedge_fund A
		left join tb_champ_market B  on A.match_id=B.champ_event_id and A.market_id=B.champ_market_id
		left join tb_champ_event_member C on B.champ_member_id=C.champ_event_member_id
		left join tb_champ_event D on A.match_id=D.champ_event_id
		left join tb_user_fund E on A.trade_user=E.user_id		
		where A.match_id=@Match_Id and A.match_type=@Match_Type
		and A.status=0
	end
	
	--��ʼ�۳��ͷŵĶԳ��ʽ�	
	declare hedge_cursor cursor FORWARD_ONLY for	
	select * from @temp_hedge
	open hedge_cursor
	fetch hedge_cursor into @Tmp_User_Fund_Id,@Tmp_Match_Name
		,@Tmp_Market_Name,@Tmp_Hedge_Amounts,@Tmp_Exchange_Back_Lay_Id,@Tmp_Hedge_Id,@Tmp_Trade_User
	while(@@fetch_status=0)
	begin
		
		set @Tmp_Name = isnull(@Tmp_Match_Name,'') + '[' + isnull(@Tmp_Match_Name,'') + ']'
						+ '�۳��Գ��ͷ��ʽ�'
		set @Tmp_Dbl_Value = @Tmp_Hedge_Amounts*-1.0
		--�ѿ۳��Գ��¼�����ʽ���ʷ�˺ű���
		--@Log_Id,@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund,@User_Id
		exec pr_calc_add_fundhistory_withlog @Log_Id,@Tmp_User_Fund_Id,11,@Tmp_Name,@Tmp_Exchange_Back_Lay_Id,@Tmp_Dbl_Value,@Tmp_Trade_User		

		--�����ʽ��
		update tb_user_fund set cur_fund=cur_fund-@Tmp_Hedge_Amounts where user_fund_id=@Tmp_User_Fund_Id
		
		
		--�����Ѿ��۳��Գ��ʽ�ĶԳ��¼	
		update tb_match_hedge_fund set status=1 where hedge_id=@Tmp_Hedge_Id

		--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
		--��ע��¼��־��Log_type_Id=7
		exec pr_calc_log @Log_Id,7,@Tmp_Trade_User,@Tmp_Hedge_Id,null,null,null,null,null,null	

				
		fetch hedge_cursor into @Tmp_User_Fund_Id,@Tmp_Match_Name
			,@Tmp_Market_Name,@Tmp_Hedge_Amounts,@Tmp_Exchange_Back_Lay_Id,@Tmp_Hedge_Id,@Tmp_Trade_User
	end
	deallocate hedge_cursor
	
END
GO