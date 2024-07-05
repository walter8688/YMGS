IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_add_fundhistory_withlog]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_add_fundhistory_withlog]
GO
CREATE PROCEDURE [dbo].[pr_calc_add_fundhistory_withlog]
    (
		@Log_Id int,
		@User_Fund_Id int,
		@Trade_Type int,
		@Trade_Desc nvarchar(200),
		@Trade_Serial_No int,
		@Trade_Fund decimal(18,2),
		@User_Id int
    )
AS
BEGIN
	declare @Tmp_Identity_Id int
	
	--����ҵ�ӯ�����ָ������ʽ���ʷ�˺ű�
	INSERT INTO TB_FUND_HISTORY(USER_FUND_ID,TRADE_TYPE,TRADE_DESC,TRADE_SERIAL_NO,TRADE_FUND,TRADE_DATE)
	VALUES(@User_Fund_Id,@Trade_Type,@Trade_Desc,@Trade_Serial_No,@Trade_Fund ,getdate());
	
	select @Tmp_Identity_Id = SCOPE_IDENTITY()
	
	--���ӽ�����־
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--�û��ʽ���ʷ��¼��־��Log_type_Id=5
	exec pr_calc_log @Log_Id,5,@User_Id,@Tmp_Identity_Id,@Trade_Type,@Trade_Serial_No,@Trade_Fund,null,null,@Trade_Desc
END
GO