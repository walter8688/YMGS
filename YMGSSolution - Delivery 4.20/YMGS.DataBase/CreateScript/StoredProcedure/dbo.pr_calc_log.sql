IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_log]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_log]
GO
CREATE PROCEDURE [dbo].[pr_calc_log]
    (
		@Log_Id int,
		@Log_Type_Id int,
		@Trade_User int,
		@Log_Object int,
		@Log_Int_Data1 int,
		@Log_Int_Data2 int,
		@Log_Dbl_Data1 decimal(18,2),
		@Log_Dbl_Data2 decimal(18,2),
		@Log_Str_Data nvarchar(100),
		@Description nvarchar(200)
    )
AS
BEGIN
	insert into tb_settlement_log_detail(log_id,log_type_id,
	trade_user,log_object,log_int_data1,log_int_data2,
	log_dbl_data1,log_dbl_data2,log_str_data,description)
	values(@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,
	@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2
	,@Log_Str_Data,@Description)
	
END
GO