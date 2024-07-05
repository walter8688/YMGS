IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_COUNTRY' ) 
    CREATE TABLE TB_COUNTRY
        (
          [COUNTRY_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [COUNTRY_NAME_CN] NVARCHAR(100) ,
          [COUNTRY_NAME_EN] NVARCHAR(100) 
        )
    GO