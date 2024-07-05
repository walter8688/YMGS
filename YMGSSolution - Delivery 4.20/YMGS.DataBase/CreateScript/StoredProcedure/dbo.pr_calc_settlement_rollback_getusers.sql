IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_settlement_rollback_getusers]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_settlement_rollback_getusers]
GO
CREATE PROCEDURE [dbo].[pr_calc_settlement_rollback_getusers]
    (
		@Match_Id int,--比赛ID
		@Calc_Flag int, --0半场结算 1 全场结算
		@Match_Type int --1 体育比赛 2 冠军赛事
    )
AS

BEGIN

	declare @Log_Id int	--结算日志ID
	
	--首先获得当前比赛结算日志ID
	select top 1 @Log_Id = log_id from tb_settlement_log
	where match_id=@Match_Id and match_type=@Match_Type 
	and (
		(@Match_Type=2 and calc_flag=@Calc_Flag)	-- 冠军赛事
		or		
		(@Match_Type=1 and @Calc_Flag=0 and calc_flag=@Calc_Flag)	--体育赛事半场重新结算
		or
		(@Match_Type=1 and @Calc_Flag=1) --体育赛事全场重新结算
	)
	order by begintime desc	

	select @Log_Id Log_Id
	
	select distinct trade_user User_Id
	from tb_settlement_log_detail
	where log_id = @Log_Id
	
END
GO