IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_MAIN_FUND_HISTORY' ) 
    BEGIN
        CREATE TABLE TB_MAIN_FUND_HISTORY
            (
              [FUND_HISTORY_ID] INT PRIMARY KEY IDENTITY(1, 1),
              [COME_USER_FUND_ID] INT ,
              [FUND_AMOUNTS] DECIMAL(18,2) ,
              [FUND_MEMO]	NVARCHAR(200),
              [EXCHANGE_SETTLE_ID] INT,
              [TRADE_DATE] DATETIME
            )
    END
    GO