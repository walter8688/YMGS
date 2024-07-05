IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_TIMEZONE' ) 
    CREATE TABLE TB_TIMEZONE
        (
          [TIMEZONE_ID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [TIMEZONE_NAME] VARCHAR(50)
        )
    GO