IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_add_integralhistory_withlog]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_add_integralhistory_withlog]
GO
CREATE PROCEDURE [dbo].[pr_calc_add_integralhistory_withlog]
    (
		@Log_Id int,
		@User_Fund_Id int,
		@Exchange_Deal_Id int,
		@Deal_Fund decimal(18,2),
		@Got_Integral decimal(18,2),
		@Trade_Desc nvarchar(200),
		@User_Id int
    )
AS
BEGIN
	declare @Tmp_Identity_Id int
	
	--����ҵ�ӯ�����ָ������ʽ���ʷ�˺ű�
	INSERT INTO tb_integral_history(USER_FUND_ID,exchange_deal_id,dealed_fund,got_integral,TRADE_DATE)
	VALUES(@User_Fund_Id,@Exchange_Deal_Id,@Deal_Fund,@Got_Integral,getdate())
	--select * from tb_integral_history
	select @Tmp_Identity_Id = SCOPE_IDENTITY()
	
	--���ӽ�����־
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--�û�������ʷ��¼��־��Log_type_Id=8
	exec pr_calc_log @Log_Id,8,@User_Id,@Tmp_Identity_Id,@Exchange_Deal_Id,null,@Got_Integral,@Deal_Fund,null,@Trade_Desc	
	
END
GO