/***
CREATE DATE:2013-01-09
DESCRIPTION:用户资金账户模块/用户账户历史记录
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_FUND_HISTORY' ) 
    CREATE TABLE TB_FUND_HISTORY
        (
          [FUND_HISTORY_ID] INT PRIMARY KEY
                                IDENTITY(1, 1) ,
          [USER_FUND_ID] INT ,--用户账户信息
          [TRADE_TYPE] INT ,--交易类型(充值、提现，投注，受注，撮合，结算)
		  [TRADE_DESC] NVARCHAR(100),--备注
          [TRADE_SERIAL_NO] INT ,--交易号(根据不同交易类型，填不同的单号)
          [TRADE_FUND] DECIMAL(20, 2) ,--交易资金
          [TRADE_DATE] DATETIME	,--交易时间
        )
GO