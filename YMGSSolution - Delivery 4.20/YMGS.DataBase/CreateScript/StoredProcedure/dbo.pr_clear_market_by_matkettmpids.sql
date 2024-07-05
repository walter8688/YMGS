IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_clear_market_by_matkettmpids]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_clear_market_by_matkettmpids]
GO
CREATE PROCEDURE [pr_clear_market_by_matkettmpids]
    (
      @Match_Id INT ,
      @Match_Type INT ,
      @Market_Tmp_Ids NVARCHAR(1000)
    )
AS 
    BEGIN
        SET XACT_ABORT ON
        BEGIN TRANSACTION
        DECLARE @Error_Msg NVARCHAR(100) ,
            @Cur_Freezed_Fund DECIMAL(20,2)
        DECLARE ClearMarketCur CURSOR FORWARD_ONLY
        FOR
				SELECT  Back.MATCH_AMOUNTS ,
						Back.TRADE_USER ,
						Back.EXCHANGE_BACK_ID AS Lay_Or_Back_Excange_Id, '0' as EXType
				FROM    dbo.TB_EXCHANGE_BACK Back inner join
				TB_MATCH_MARKET on Back.MARKET_ID = TB_MATCH_MARKET.MARKET_ID inner join 
				(SELECT [Value] as MARKET_TMP_ID
                            FROM  [dbo].[SplitString](@Market_Tmp_Ids,',', 1)) MarketTmp
                on TB_MATCH_MARKET.MARKET_TMP_ID = MarketTmp.MARKET_TMP_ID
				WHERE   Back.MATCH_ID = @Match_Id
						AND Back.MATCH_TYPE = @Match_Type
						AND Back.STATUS = 1
				UNION ALL
				SELECT  Lay.MATCH_AMOUNTS * ( ODDS - 1 ) ,
						Lay.TRADE_USER ,
						Lay.EXCHANGE_LAY_ID AS Lay_Or_Back_Excange_Id, '1' as EXType
				FROM    dbo.TB_EXCHANGE_LAY Lay inner join
				TB_MATCH_MARKET on Lay.MARKET_ID = TB_MATCH_MARKET.MARKET_ID inner join 
				(SELECT [Value] as MARKET_TMP_ID
                            FROM  [dbo].[SplitString](@Market_Tmp_Ids,',', 1)) MarketTmp
                on TB_MATCH_MARKET.MARKET_TMP_ID = MarketTmp.MARKET_TMP_ID
				WHERE   Lay.MATCH_ID = @Match_Id
						AND Lay.MATCH_TYPE = @Match_Type
						AND Lay.STATUS = 1
        DECLARE @Unfreeze_Fund DECIMAL(20,2) ,
            @User_Id INT ,
            @User_Fund_Id INT ,
            @Lay_Or_Back_Excange_Id INT ,
            @EXType NVARCHAR(1)
        OPEN ClearMarketCur
        FETCH NEXT FROM ClearMarketCur INTO @Unfreeze_Fund, @User_Id,
            @Lay_Or_Back_Excange_Id, @EXType
        WHILE @@FETCH_STATUS = 0 
            BEGIN
				--获取当前资金账户冻结的资金
                SELECT  @Cur_Freezed_Fund = FREEZED_FUND ,@User_Fund_Id = USER_FUND_ID
                FROM    dbo.TB_USER_FUND WHERE   [USER_ID] = @User_Id
                --验证解冻资金是否小于等于当前资金账户冻结的资金
                IF ( @Cur_Freezed_Fund < @Unfreeze_Fund ) 
                    BEGIN
                        SET @Error_Msg = '比赛ID:' + CONVERT(NVARCHAR(100),@Match_Id) + ';用户ID:' + CONVERT(NVARCHAR(100),@User_Id)+ ';可解冻资金不足,数据已回滚!'
                        RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                        ROLLBACK TRANSACTION
                        RETURN
                    END
                ELSE 
                    BEGIN
						IF @EXType = '0'
						BEGIN
							UPDATE TB_EXCHANGE_BACK SET STATUS = 6 WHERE EXCHANGE_BACK_ID = @Lay_Or_Back_Excange_Id 
						END
						ELSE
						BEGIN
							UPDATE TB_EXCHANGE_LAY SET STATUS = 6 WHERE EXCHANGE_LAY_ID = @Lay_Or_Back_Excange_Id 
						END
						--解冻资金
                        UPDATE  dbo.TB_USER_FUND
                        SET     CUR_FUND = CUR_FUND + @Unfreeze_Fund ,
                                FREEZED_FUND = FREEZED_FUND - @Unfreeze_Fund ,
                                LAST_UPDATE_TIME = GETDATE()
                        WHERE   [USER_ID] = @User_Id
                        --记录资金历史
                        INSERT  INTO dbo.TB_FUND_HISTORY
                                ( USER_FUND_ID ,
                                  TRADE_TYPE ,
                                  TRADE_DESC ,
                                  TRADE_SERIAL_NO ,
                                  TRADE_FUND ,
                                  TRADE_DATE
                                )
                        VALUES  ( @User_Fund_Id , -- USER_FUND_ID - int
                                  4 , -- TRADE_TYPE - int
                                  N'清理市场时未成交的投注解冻的资金' , -- TRADE_DESC - nvarchar(100)
                                  @Lay_Or_Back_Excange_Id , -- TRADE_SERIAL_NO - int
                                  @Unfreeze_Fund , -- TRADE_FUND - decimal
                                  GETDATE()  -- TRADE_DATE - datetime
                                )
                    END
                FETCH NEXT FROM ClearMarketCur INTO @Unfreeze_Fund, @user_id,
                    @Lay_Or_Back_Excange_Id, @EXType
            END        
        CLOSE ClearMarketCur
        DEALLOCATE ClearMarketCur
        
        --PRINT @@ERROR
        COMMIT TRANSACTION
    END
GO