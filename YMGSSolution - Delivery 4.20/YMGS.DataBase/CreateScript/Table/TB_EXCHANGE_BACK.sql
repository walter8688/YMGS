/***
CREATE DATE:2013-01-09
DESCRIPTION:交易和结算管理模块/投注对象
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_BACK' ) 
    CREATE TABLE TB_EXCHANGE_BACK
        (
          [EXCHANGE_BACK_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [MATCH_ID] INT ,--比赛ID
          [MARKET_ID] INT ,--比赛市场ID
          [ODDS] DECIMAL(6, 2) ,--赔率
          [BET_AMOUNTS] DECIMAL(20, 2) ,--下注金额
          [MATCH_AMOUNTS] DECIMAL(20, 2) ,--撮合剩余金额
          [TRADE_TIME] DATETIME ,--交易时间
          [TRADE_USER] INT ,--下注用户
          [STATUS] INT,	--下注状态
		  [MATCH_TYPE] INT, --比赛分类
		  [HOME_TEAM_SCORE] INT, --主队比分
		  [GUEST_TEAM_SCORE] INT --客队比分
        )
GO