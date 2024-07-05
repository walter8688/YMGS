/***
CREATE DATE:2013-01-09
DESCRIPTION:交易和结算管理模块/撮合对象
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_DEAL' ) 
    CREATE TABLE TB_EXCHANGE_DEAL
        (
          [EXCHANGE_DEAL_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [MATCH_ID] INT ,--比赛ID
          [MARKET_ID] INT ,--比赛市场ID
          [EXCHANGE_BACK_ID] INT ,--投注对象ID
          [EXCHANGE_LAY_ID] INT ,--受注对象ID
          [DEAL_AMOUNT] DECIMAL(20, 2) ,--撮合金额
          [DEAL_TIME] DATETIME ,--撮合时间
          [STATUS] INT	,--状态
		  [MATCH_TYPE] INT,--比赛分类
		  [ODDS] decimal(9,2)--赔率
        )
GO