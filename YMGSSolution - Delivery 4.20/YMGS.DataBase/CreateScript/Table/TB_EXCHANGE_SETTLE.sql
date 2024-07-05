/***
CREATE DATE:2013-01-09
DESCRIPTION:交易和结算管理模块/结算对象
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_SETTLE' ) 
    CREATE TABLE TB_EXCHANGE_SETTLE
        (
          [EXCHANGE_SETTLE_ID] INT PRIMARY KEY
                                   IDENTITY(1, 1) ,
          [EXCHANGE_DEAL_ID] INT ,--撮合交易ID
          [WIN_USER_ID] INT ,--WIN用户
          [LOSE_USER_ID] INT ,--LOSE用户
          [WIN_INTEGRAL] DECIMAL(20, 2) ,--WIN用户积分
          [LOSE_INTEGRAL] DECIMAL(20, 2) ,--LOSE用户积分
          [BROKERAGE] DECIMAL(20, 2) ,--平台佣金
          [BROKERAGE_RATE] DECIMAL(20, 4) ,--平台佣金率
          [AGENT_COMMISSION_RATE] DECIMAL(20,4), --代理佣金率
          [MAIN_AGENT_COMMISSION_RATE] DECIMAL(20,4), --总代理佣金率
          [SETTLE_TIME] DATETIME, --计算时间
		  [EXCHANGE_WIN_FLAG] int --输赢标志
        )
GO