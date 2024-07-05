IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_MATCH_HEDGE_FUND' ) 
    CREATE TABLE TB_MATCH_HEDGE_FUND
        (
          HEDGE_ID INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [MATCH_ID] INT ,	
          [MARKET_ID] INT ,	
          [TRADE_USER] INT ,	
		  [MATCH_TYPE] INT,
          [TRADE_FLAG] int ,	
          [EXCHANGE_BACK_LAY_ID] int ,	
          [HEDGE_AMOUNTS] decimal(18,2),
          [OPERATE_USER] INT ,	
          [OPERATE_TIME] DATETIME
        )
    GO