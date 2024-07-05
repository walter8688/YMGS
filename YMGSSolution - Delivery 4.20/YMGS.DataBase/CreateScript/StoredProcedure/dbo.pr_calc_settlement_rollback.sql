IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_settlement_rollback]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_settlement_rollback]
GO
CREATE PROCEDURE [dbo].[pr_calc_settlement_rollback]
    (
		@Match_Id int,--����ID
		@Calc_Flag int, --0�볡���� 1 ȫ������
		@Match_Type int, --1 �������� 2 �ھ�����
		@User_Id int, --��ǰ�����û�ID
		@Log_Id int -- ������־ID
    )
AS


BEGIN
SET XACT_ABORT ON
BEGIN TRANSACTION TradeTrans
	
	--�������
	declare @User_Fund_Id int --�û��ʽ��˺�ID
	declare @Tmp_Value decimal(18,2)
	
	--���������־�����
	declare @Settlement_Log table
	(
		log_type_id int,
		log_object int,
		log_int_data1 int,
		log_int_data2 int,
		log_dbl_data1 decimal(18,2),
		log_dbl_data2 decimal(18,2)		
	)
	
	--���������ܴ���Ӷ�𻺴��
	declare @Agent_Funds table
	(
		user_fund_id int,
		funds decimal(18,2)
	)

	--����û����ʽ�
	select @User_Fund_Id=user_fund_Id  from tb_user_fund where user_id=@User_Id
	
	--�����û�����ؽ�����־
	insert into @Settlement_Log
	select log_type_id,
			log_object,
			log_int_data1,
			log_int_data2,
			log_dbl_data1,
			log_dbl_data2
	from tb_settlement_log_detail
	where log_id=@Log_Id and trade_user=@User_Id
	
	--��ʼ���ݽ�����־������ؼ�¼
	
	--Step1 �����û���Ͷע��¼��״̬Ϊ����ǰ״̬
	update tb_exchange_back
	set status=case tb_exchange_back.match_amounts
				when 0 then 2
				else 1
				end
	from @Settlement_Log A
	where tb_exchange_back.exchange_back_id=A.log_object
	and A.log_type_id=1
	
	--Step2 �����û�����ע��¼��״̬Ϊ����ǰ״̬
	update tb_exchange_lay
	set status=case tb_exchange_lay.match_amounts
				when 0 then 2
				else 1
				end
	from @Settlement_Log A
	where tb_exchange_lay.exchange_lay_id=A.log_object
	and A.log_type_id=2
	
	--Step3 ���´�ϼ�¼״̬δδ����
	update tb_exchange_deal
	set status=1
	where match_id=@Match_Id
	and match_type=@Match_Type
	and exchange_deal_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=3
	)

	--Step4 ɾ�����û���ϼ�¼��Ӧ�Ľ����¼��
	delete from tb_exchange_settle
	where exchange_settle_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=4
	)

	--Step5 ɾ�����û���ϼ�¼��Ӧ�Ľ����¼��
	update tb_match_hedge_fund
	set status=0
	where match_id=@Match_Id 
	and match_type=@Match_Type
	and hedge_id in(
		select log_object
		from @Settlement_Log A
		where A.log_type_id=7
	)
	
	--Step6 ��ø��û����ʽ���ʷ��¼���������ʽ���ʷ��¼���ʽ��˺ŵ�Ӱ�졣
	--log_dbl_data1: 4 �ⶳ�ʽ� 5  ���� Ӯ�����������ı��� (ֱ�Ӹ�cur_fund�仯)
	--6 ���۳���Ӷ�� (ֱ�Ӹ�cur_fund�仯)	11 �Գ��¼ (ֱ�Ӹ�cur_fund�仯)				

	--Step6.1 �۳����ⶳ�ʽ�		
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=5 and A.log_int_data1=4
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_fund=cur_fund-@Tmp_Value,
	freezed_fund=freezed_fund+@Tmp_Value
	where user_fund_id=@User_Fund_Id

	--Step6.2 �ع�(��Ӯ�õ���������ı������۵�Ӷ���Լ��Գ��¼
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=5 and A.log_int_data1<>4
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_fund=cur_fund-@Tmp_Value
	where user_fund_id=@User_Fund_Id
	
	--Step7 ɾ�����û����ʽ���ʷ��¼��
	delete from tb_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=5
	)
	
	
	--Step8 �ع����û��Ļ�����ʷ��¼
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=8
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_user_fund
	set cur_integral=cur_integral-@Tmp_Value
	where user_fund_id=@User_Fund_Id

	--Step9 ɾ�����û��Ļ�����ʷ��¼��
	delete from tb_integral_history
	where integral_history_id in(
	select log_object from @Settlement_Log where log_type_id=8
	)

	--Step10 �����û������ɸ�������ܴ����Ӷ���¼��������Ӷ���¼�Դ�����ܴ����Ӱ�졣
	insert into @Agent_Funds
	select A.log_int_data2,isnull(sum(A.log_dbl_data1),0)
	from @Settlement_Log A
	where A.log_type_id=9
	group by A.log_int_data2
	
	update tb_user_fund
	set cur_fund=cur_fund-B.funds
	from tb_user_fund A
	inner join  @Agent_Funds B ON A.user_fund_id=B.user_fund_id

	--Step11 ɾ��������ܴ����Ӷ���Ӧ���ʽ���ʷ��¼
	delete from tb_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=9
	)
	
	--Step12 �ع�ϵͳ���ʽ�
	select @Tmp_Value = sum(isnull(A.log_dbl_data1,0))
	from @Settlement_Log A
	where A.log_type_id=6
	set @Tmp_Value = isnull(@Tmp_Value,0)
	
	update tb_system_main_fund
	set total_money=total_money-@Tmp_Value
	where main_func_id=1	
	
	--Step13 ɾ��ϵͳ�ʽ���ʷ��¼
	delete from tb_main_fund_history
	where fund_history_id in(
	select log_object from @Settlement_Log where log_type_id=6
	)

COMMIT TRANSACTION TradeTrans
END
GO