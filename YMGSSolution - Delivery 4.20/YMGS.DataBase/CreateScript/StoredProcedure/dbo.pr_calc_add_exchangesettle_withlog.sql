IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_add_exchangesettle_withlog]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_add_exchangesettle_withlog]
GO
CREATE PROCEDURE [dbo].[pr_calc_add_exchangesettle_withlog]
    (
		@Log_Id int,
		@Exchange_Deal_Id int,
		@Win_User_Id int,
		@Lose_User_Id int,
		@Win_Integral decimal(18,2),
		@Lose_Integral decimal(18,2),
		@Brokerage decimal(18,2),
		@Brokerage_Rate decimal(20,4),
		@Exchange_Win_Flag int,
		@Exchange_Settle_Id int output
    )
AS
BEGIN

	declare @Tmp_Identity_Id int
	
	INSERT INTO TB_EXCHANGE_SETTLE(EXCHANGE_DEAL_ID,WIN_USER_ID,LOSE_USER_ID,WIN_INTEGRAL,LOSE_INTEGRAL,BROKERAGE,BROKERAGE_RATE,SETTLE_TIME,EXCHANGE_WIN_FLAG)
	VALUES(@Exchange_Deal_Id,@Win_User_Id,@Lose_User_Id,@Win_Integral,@Lose_Integral,@Brokerage,@Brokerage_Rate,getdate(),@Exchange_Win_Flag)
	
	select @Exchange_Settle_Id = SCOPE_IDENTITY()
	
	--增加结算日志
	--@Log_Id,@Log_Type_Id,@Trade_User,@Log_Object,@Log_Int_Data1,@Log_Int_Data2,@Log_Dbl_Data1,@Log_Dbl_Data2,@Log_Str_Data,@Description
	--用户结算记录日志的Log_type_Id=4
	exec pr_calc_log @Log_Id,4,@Win_User_Id,@Exchange_Settle_Id,@Exchange_Deal_Id,null,null,null,null,null
END
GO