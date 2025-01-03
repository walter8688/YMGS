IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_edit_Exchange_Lay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_edit_Exchange_Lay]
GO
CREATE PROCEDURE [dbo].[pr_edit_Exchange_Lay]
    ( 
	  @MATCHTYPE int,
      @STATUS nvarchar(10) ,
      @EXCHANGE_LAY_ID int,
      @MATCH_ID int, 
      @MARKET_ID int, 
      @ODDS decimal(6,2), 
      @BET_AMOUNTS decimal(20,2), 
      @MATCH_AMOUNTS decimal(20,2), 
      @TRADE_USER int
    )
AS 
BEGIN
SET XACT_ABORT ON
	--edit是下注 cancel是取消未撮合的下注金额
	if(@STATUS='edit')
	begin 
	DECLARE @BACKNEWID INT,@LAYNEWID INT,
	        @Amounts decimal(20,2),
	        @layODDS decimal(6,2),
	        @CreateTime DATETIME,
			@userfundid int,
			@sumFrozenFund decimal(20,2),
			@leftFund decimal(20,2)
	SET @CreateTime = getdate();
	declare cur_amounts cursor for 
	select ODDS,MATCH_AMOUNTS,EXCHANGE_BACK_ID from TB_Exchange_Back where  STATUS=1 and MATCH_TYPE=@MATCHTYPE  and Match_ID=@Match_ID and Market_ID=@Market_ID
     and ODDS<=@ODDS order by ODDS ASC,TRADE_TIME asc;
	if(@MATCHTYPE=1)
	begin
		if(not Exists( SELECT mm.MARKET_ID from TB_Match_Market mm
					left join  TB_Market_Template mt on mt.MARKET_TMP_ID=mm.MARKET_TMP_ID
					left join TB_MATCH mat on mat.MATCH_ID=mm.MATCH_ID
					WHERE  mm.MATCH_ID=@MATCH_ID and mm.MARKET_ID= @MARKET_ID
					and  mat.ADDITIONALSTATUS IN(1)
					AND 
					mm.MARKET_ID IN(select market_id from dbo.udf_get_canuse_market_for_betting())
		))
		begin
			RAISERROR ('999' , 16, 1) WITH NOWAIT
			return
		end
	end

	if(@MATCHTYPE=2)
	begin
		if(not Exists( SELECT mm.Champ_Market_ID
					from TB_CHAMP_MARKET mm
					INNER JOIN TB_CHAMP_EVENT B ON mm.CHAMP_EVENT_ID=B.CHAMP_EVENT_ID
					LEFT JOIN TB_EVENT D ON D.EVENT_ID=B.EVENT_ID
					WHERE B.Champ_Event_ID=@Match_Id and mm.Champ_Market_ID=@MARKET_ID and B.CHAMP_EVENT_STATUS=1))
		begin
			RAISERROR ('999' , 16, 1) WITH NOWAIT
			return
		end
	end

	--新增下注记录
	declare @home_team_score int
	declare @guest_team_score int
	select @home_team_score=home_full_score,@guest_team_score=guest_full_score from tb_match where match_id=@MATCH_ID
	insert into TB_Exchange_Lay(MATCH_TYPE,MATCH_ID,MARKET_ID,ODDS,BET_AMOUNTS,MATCH_AMOUNTS,TRADE_TIME,TRADE_USER,STATUS,HOME_TEAM_SCORE,GUEST_TEAM_SCORE)
	  VALUES(@MATCHTYPE,@MATCH_ID,@MARKET_ID,@ODDS,@BET_AMOUNTS,@MATCH_AMOUNTS,@CreateTime,@TRADE_USER,1,@home_team_score,@guest_team_score);
	  SELECT @LAYNEWID= SCOPE_IDENTITY();
	set @sumFrozenFund=0;
	set @leftFund=0;
	open cur_amounts 
	fetch next from cur_amounts
	into @layODDS,@Amounts,@BACKNEWID
	WHILE (@@FETCH_STATUS = 0)
	begin
	  --如果下注金额大于等于被撮合的金额则受注记录撮合成功，继续撮合下一条记录，否则投注成功
	 IF (@MATCH_AMOUNTS>=@Amounts) 
	 BEGIN	  
		SELECT @MATCH_AMOUNTS=@MATCH_AMOUNTS-@Amounts;
		select @sumFrozenFund=@sumFrozenFund+@Amounts*(@layODDS-1);
		select @leftFund=@leftFund+@Amounts;
		UPDATE TB_Exchange_Back SET MATCH_AMOUNTS=0,STATUS=2
		WHERE EXCHANGE_BACK_ID=@BACKNEWID and MATCH_TYPE=@MATCHTYPE;
		--记录撮合交易
		INSERT INTO DBO.TB_EXCHANGE_DEAL(MATCH_TYPE,ODDS,MATCH_ID,MARKET_ID,EXCHANGE_BACK_ID,EXCHANGE_LAY_ID,DEAL_AMOUNT,DEAL_TIME,[STATUS])
		VALUES(@MATCHTYPE,@layODDS,@MATCH_ID,@MARKET_ID,@BACKNEWID,@LAYNEWID,@Amounts,@CreateTime,1);

		IF(@MATCH_AMOUNTS>0)
			update TB_Exchange_Lay set MATCH_AMOUNTS=@MATCH_AMOUNTS where EXCHANGE_LAY_ID=@LAYNEWID  and MATCH_TYPE=@MATCHTYPE
		if(@MATCH_AMOUNTS=0)
		begin
			update TB_Exchange_Lay set MATCH_AMOUNTS=@MATCH_AMOUNTS,STATUS=2 where EXCHANGE_LAY_ID=@LAYNEWID  and MATCH_TYPE=@MATCHTYPE
			break
		end
	 END
	 ELSE
	 BEGIN
	     UPDATE TB_Exchange_Back SET MATCH_AMOUNTS=@Amounts-@MATCH_AMOUNTS
	     WHERE EXCHANGE_BACK_ID=@BACKNEWID  and MATCH_TYPE=@MATCHTYPE;
		  select @sumFrozenFund=@sumFrozenFund+@MATCH_AMOUNTS*(@layODDS-1);
		  select @leftFund=@leftFund+@MATCH_AMOUNTS;
	     INSERT INTO DBO.TB_EXCHANGE_DEAL(MATCH_TYPE,ODDS,MATCH_ID,MARKET_ID,EXCHANGE_BACK_ID,EXCHANGE_LAY_ID,DEAL_AMOUNT,DEAL_TIME,[STATUS])
	     VALUES(@MATCHTYPE,@layODDS,@MATCH_ID,@MARKET_ID,@BACKNEWID,@LAYNEWID,@MATCH_AMOUNTS,@CreateTime,1);
	     update TB_Exchange_Lay set  MATCH_AMOUNTS=0,STATUS=2 where EXCHANGE_LAY_ID=@LAYNEWID  and MATCH_TYPE=@MATCHTYPE;
	     break;
	 END
	 FETCH NEXT FROM cur_amounts INTO @layODDS,@Amounts,@BACKNEWID
   end
   close cur_amounts
   DEALLOCATE cur_amounts

	--增加冻结资金，减少活动资金
	update TB_User_Fund set CUR_FUND=CUR_FUND-@sumFrozenFund-(@BET_AMOUNTS-@leftFund)*(@ODDS-1),FREEZED_FUND=FREEZED_FUND+@sumFrozenFund+(@BET_AMOUNTS-@leftFund)*(@ODDS-1)
	where USER_ID=@TRADE_USER
	select @userfundid=USER_FUND_ID from   TB_User_Fund where USER_ID=@TRADE_USER
	insert into tb_fund_history(USER_FUND_ID,TRADE_TYPE,TRADE_DESC,TRADE_SERIAL_NO,TRADE_DATE,TRADE_FUND)
          values(@userfundid,3,N'减少活动资金:'+convert(varchar(20),@sumFrozenFund+(@BET_AMOUNTS-@leftFund)*(@ODDS-1)) +N',增加冻结资金:'+convert(varchar(20),@sumFrozenFund+(@BET_AMOUNTS-@leftFund)*(@ODDS-1)),@LAYNEWID,GETDATE(),(@sumFrozenFund+(@BET_AMOUNTS-@leftFund)*(@ODDS-1))*-1)

	--实时计算:对冲资金释放
	exec pr_calc_real_fund_hedge_main @MATCH_ID,@MARKET_ID,@MATCHTYPE,@TRADE_USER,2,@LAYNEWID,@TRADE_USER

	end

	--取消未撮合的下注金额
	if(@STATUS='cancel')
	begin
		DECLARE @leftAmounts decimal(20,2)
		select @leftAmounts=MATCH_AMOUNTS* (ODDS-1) from TB_Exchange_Lay where EXCHANGE_LAY_ID=@EXCHANGE_LAY_ID  and MATCH_TYPE=@MATCHTYPE
		update TB_Exchange_Lay set STATUS=6,MATCH_AMOUNTS=0 where EXCHANGE_LAY_ID=@EXCHANGE_LAY_ID  and MATCH_TYPE=@MATCHTYPE
		update TB_User_Fund set CUR_FUND=CUR_FUND+@leftAmounts,FREEZED_FUND=FREEZED_FUND-@leftAmounts
		where USER_ID=@TRADE_USER
		select @userfundid=USER_FUND_ID from   TB_User_Fund where USER_ID=@TRADE_USER
		insert into tb_fund_history(USER_FUND_ID,TRADE_TYPE,TRADE_DESC,TRADE_SERIAL_NO,TRADE_DATE,TRADE_FUND)
          values(@userfundid,4,N'取消未撮合金额',@EXCHANGE_LAY_ID,GETDATE(),@leftAmounts)
	end	
	
	EXEC pr_up_cache_object 12
END
GO