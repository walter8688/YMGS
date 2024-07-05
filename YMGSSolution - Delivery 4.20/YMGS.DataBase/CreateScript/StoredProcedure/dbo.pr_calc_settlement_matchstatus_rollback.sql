IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_settlement_matchstatus_rollback]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_settlement_matchstatus_rollback]
    GO
CREATE PROCEDURE pr_calc_settlement_matchstatus_rollback
    (
      @Match_Id INT ,
	  @Match_Type int,
      @Log_Id int,
	  @Calc_Flag int
    )
AS 
    BEGIN
		declare @status int
		select @status=original_status from tb_settlement_log
		where log_id=@Log_Id

		update tb_settlement_log set status=2 where log_id=@Log_Id
		
		if(@Match_Type=1)
		begin		
			if(@Calc_Flag = 0)
			begin
				UPDATE  dbo.TB_MATCH
				SET	settle_status=0
				WHERE   MATCH_ID = @Match_Id
			end
			if(@Calc_Flag = 1)
			begin
				UPDATE  dbo.TB_MATCH
				SET     status = @status,
				settle_status=0
				WHERE   MATCH_ID = @Match_Id        
			end
		end
		
		if(@Match_Type=2)
		begin		
			UPDATE  dbo.tb_champ_event
			SET     champ_event_status = @status
			WHERE   champ_event_id = @Match_Id 
		end
    END
GO