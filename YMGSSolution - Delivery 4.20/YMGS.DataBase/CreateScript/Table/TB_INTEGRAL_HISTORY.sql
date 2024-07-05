IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_INTEGRAL_HISTORY' ) 
    CREATE TABLE TB_INTEGRAL_HISTORY
        (
          [INTEGRAL_HISTORY_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [USER_FUND_ID] INT ,--用户账号ID
          [EXCHANGE_DEAL_ID] int,--撮合交易ID
          [DEALED_FUND] DECIMAL(20, 2) ,--交易金额
          [GOT_INTEGRAL] int ,--获得积分
          [TRADE_DATE] DATETIME --交易时间
        )
GO