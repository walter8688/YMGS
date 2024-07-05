IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_calc_realtime_football_result]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_calc_realtime_football_result]
END
GO

CREATE FUNCTION dbo.udf_calc_realtime_football_result
(
	@Match_Id int,
	@Market_Id int,
	@Match_Type int,
	@Trade_Flag int,
	@Deal_Amounts decimal(18,2),
	@Odds decimal(18,2),
	@Bet_Type_Id int,
	@Market_Tmp_Type int,
	@Market_Flag int
)
RETURNS @Result table(
	first_team_win_Funds decimal(18,2),
	last_team_win_funds decimal(18,2),
	half_funds decimal(18,2)
)
AS
BEGIN
	declare @tmp_first_win decimal(18,2)
	declare @tmp_last_win decimal(18,2)
	declare @tmp_half_funds decimal(18,2)
	declare @dblScoreA decimal(18,2)
	declare @dblScoreB decimal(18,2)				
	
	
	set @tmp_half_funds=0
	
	--如果是冠军比赛
	if(@Match_Type=2)
	begin
		--如果是投注
		if(@Trade_Flag =1)
		begin
			set @tmp_first_win = @Deal_Amounts*@Odds
			set @tmp_last_win = 0
		end
		
		--如果是受注
		if(@Trade_Flag=2)
		begin
			set @tmp_first_win = 0
			set @tmp_last_win = @Deal_Amounts*@Odds
		end
		
		--如果是自己投注和受注
		if(@Trade_Flag=3)
		begin
			set @tmp_first_win=@Deal_Amounts
			set @tmp_last_win=@Deal_Amounts
		end
	end
	
	--如果是足球比赛
	if(@Match_Type=1)
	begin
		--如果是投注
		if(@Trade_flag=1)
		begin
			--如果是标准盘、波胆、大小球
			if(@Bet_Type_Id=1 or @Bet_Type_Id=2 or @Bet_Type_Id=3)
			begin
				set @tmp_first_win = @Deal_Amounts*@Odds
				set @tmp_last_win = 0
			end
			
			--如果是让球盘
			if(@Bet_Type_Id = 4)
			begin				
				select @dblScoreA=SCOREA,@dblScoreB=SCOREB from tb_match_market
				where match_id=@Match_Id and market_id=@Market_Id
				if(@dblScoreA is null)--该让分盘有可能有平局
				begin
					--主队赢球
					set @tmp_first_win = @Deal_Amounts*@Odds
					
					--主队输球
					set @tmp_last_win = 0
					
					--主队和客队平局
					set @tmp_half_funds=@Deal_Amounts
				end
				else
				begin --普通让分盘
					--主队赢球
					set @tmp_first_win = @Deal_Amounts*@Odds
					
					--主队输球
					set @tmp_last_win = 0
					
					if(cast(@dblScoreB as int)=@dblScoreB)
					begin
						--主队半赢
						set @tmp_half_funds= @Deal_Amounts +(@Deal_Amounts*(@Odds-1)/2.0)
						
					end
					else
					begin
						--主队半输
						set @tmp_half_funds=@Deal_Amounts -(@Deal_Amounts*(@Odds-1)/2.0)
					end
				end
			end
		end
		
		--如果是受注
		if(@Trade_flag=2)
		begin
			--如果是标准盘、波胆、大小球
			if(@Bet_Type_Id=1 or @Bet_Type_Id=2 or @Bet_Type_Id=3)
			begin
				set @tmp_first_win = 0
				set @tmp_last_win = @Deal_Amounts*@Odds						
			end
			
			--如果是让球盘
			if(@Bet_Type_Id = 4)
			begin				
				select @dblScoreA=SCOREA,@dblScoreB=SCOREB from tb_match_market
				where match_id=@Match_Id and market_id=@Market_Id
				if(@dblScoreA is null)--该让分盘有可能有平局
				begin
					--主队赢球
					set @tmp_first_win = 0
					
					--主队输球
					set @tmp_last_win = @Deal_Amounts*@Odds
					
					--主队和客队平局
					set @tmp_half_funds=@Deal_Amounts
				end
				else
				begin --普通让分盘
					--主队赢球
					set @tmp_first_win = 0
					
					--主队输球
					set @tmp_last_win = @Deal_Amounts*@Odds
					
					if(cast(@dblScoreB as int)=@dblScoreB)
					begin
						--主队半赢
						set @tmp_half_funds= @Deal_Amounts -(@Deal_Amounts*(@Odds-1)/2.0)
						
					end
					else
					begin
						--主队半输
						set @tmp_half_funds=@Deal_Amounts +(@Deal_Amounts*(@Odds-1)/2.0)
					end
				end
			end
		end	
		
		--如果是自己投注和受注
		if(@Trade_Flag=3)
		begin
			set @tmp_first_win=@Deal_Amounts
			set @tmp_last_win=@Deal_Amounts
		end			
	end
	
	insert into @Result
	select isnull(@tmp_first_win,0),isnull(@tmp_last_win,0),isnull(@tmp_half_funds,0)
	return 
END
GO