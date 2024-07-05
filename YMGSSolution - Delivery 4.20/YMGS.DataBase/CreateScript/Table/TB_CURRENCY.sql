IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_CURRENCY' ) 
    CREATE TABLE TB_CURRENCY
        (
          [CURRENCY_ID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [CURRENCY_NAME] VARCHAR(50) ,
          [CURRENCY_EN] VARCHAR(50) 
        )
    GO