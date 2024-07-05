IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_SYSTEM_MAIN_FUND' ) 
    BEGIN
        CREATE TABLE TB_SYSTEM_MAIN_FUND
            (
              [MAIN_FUNC_ID] INT PRIMARY KEY,
              [TOTAL_MONEY] DECIMAL(18,2) ,
              [BANK_NAME] NVARCHAR(50) ,
              [BANK_ACCOUNT]	INT,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO