IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_edit_odds_compare]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_edit_odds_compare]
    GO
CREATE PROCEDURE pr_edit_odds_compare
    (
      @op_flag INT ,
      @matchid INT ,
      @matchname VARCHAR(100) ,
      @CN_CORP VARCHAR(100) ,
      @EN_CORP VARCHAR(100) ,
      @PROFIT DECIMAL(18, 2)
    )
AS 
    BEGIN
        IF ( @op_flag = 0 ) 
            BEGIN
                SELECT  MATCHID ,
                        MATCHNAME ,
                        CN_CORP ,
                        EN_CORP ,
                        PROFIT
                FROM    TB_ODDS_COMPARE
            END
        IF ( @op_flag = 1 ) 
            BEGIN
                INSERT  INTO TB_ODDS_COMPARE
                        ( MATCHID ,
                          MATCHNAME ,
                          CN_CORP ,
                          EN_CORP ,
                          PROFIT
                        )
                VALUES  ( @matchid ,
                          @matchname ,
                          @CN_CORP ,
                          @EN_CORP ,
                          @PROFIT
                        ) ;
				--更新缓存对象表
				exec pr_up_cache_object 9
            END
        IF ( @op_flag = 2 ) 
            BEGIN
                DELETE  FROM TB_ODDS_COMPARE
                WHERE   MATCHID = @matchid ;
            END
        IF ( @op_flag = 3 ) 
            BEGIN
                UPDATE  TB_ODDS_COMPARE
                SET     PROFIT = @PROFIT
                WHERE   MATCHID = @matchid
                        AND CN_CORP = @CN_CORP
                        AND EN_CORP = @EN_CORP
 				--更新缓存对象表
				exec pr_up_cache_object 9
           END
        IF ( @op_flag = 4 ) 
            BEGIN
                DELETE  FROM TB_ODDS_COMPARE
                WHERE   MATCHID = @matchid
                        AND CN_CORP = @CN_CORP
                        AND EN_CORP = @EN_CORP ;
				--更新缓存对象表
				exec pr_up_cache_object 9
            END
    END
GO