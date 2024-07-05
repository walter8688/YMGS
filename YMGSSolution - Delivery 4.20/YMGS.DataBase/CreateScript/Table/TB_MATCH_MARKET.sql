IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_MATCH_MARKET' ) 
    CREATE TABLE TB_MATCH_MARKET
        (
          [MARKET_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [MARKET_NAME] NVARCHAR(200) ,	--市场名称
		  [MARKET_NAME_EN] NVARCHAR(200) ,	--市场名称En
          [MATCH_ID] INT ,	--比赛ID
          [MARKET_TMP_ID] INT, --市场模板ID
          [MARKET_FLAG] INT, --市场标志
          [SCOREA] DECIMAL(18,1), --分数A
          [SCOREB] DECIMAL(18,1), --分数B
		  [MARKET_STATUS] int DEFAULT 1 not null
        )
    GO