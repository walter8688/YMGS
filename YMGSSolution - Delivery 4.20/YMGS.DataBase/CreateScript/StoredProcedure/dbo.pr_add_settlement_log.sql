IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_settlement_log]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_settlement_log]
GO
CREATE PROCEDURE [dbo].[pr_add_settlement_log]
    (
      @Match_Id int,
      @Match_Type int,
      @Calc_Flag int,
      @Original_Status int,
      @Operator int
    )
AS 
BEGIN
	--如果是冠军赛事:产生新的日志
	--如果是半场重新结算:产生新的日志
	--如果是全场重新结算:
	--					已进行过半场结算:则使用半场结算时的日志
	--					未进行过半场结算:产生新的日志
	declare @Is_Create bit
	declare @Log_Id int

	set @Is_Create = 1
	
	if(@Match_Type=1 and @Calc_Flag=1)
	begin
		if(not exists(select top 1 log_id from tb_settlement_log
				where match_id=@Match_Id and match_type=@Match_Type and calc_flag=1))
		begin
			select top 1 @Log_Id = log_id from tb_settlement_log
			where match_id=@Match_Id and match_type=@Match_Type and calc_flag=0
			order by begintime desc
			set @log_id = isnull(@Log_Id,-1)
			if(@log_id != -1)
				set @Is_Create = 0			
		end
	end
	

	if(@Is_Create=1)
	begin
		insert into tb_settlement_log(match_id,match_type,calc_flag,original_status,
			status,operator,begintime)
		values(@Match_Id,@Match_Type,@Calc_Flag,@Original_Status,
			0,@Operator,getdate())		
		SELECT SCOPE_IDENTITY()
	end
	else
	begin
		update tb_settlement_log
		set calc_flag=@Calc_Flag,
		original_status=@Original_Status,
		begintime=getdate(),
		endtime=null
		where log_id=@Log_Id
		select @Log_Id
	end
END
GO