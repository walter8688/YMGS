IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_AD_WORDS' ) 
    CREATE TABLE TB_AD_WORDS
        (
          [AD_WORDS_ID] INT PRIMARY KEY
                            IDENTITY(1, 1) ,
          [TITLE] NVARCHAR(30) ,
          [TITLE_EN] NVARCHAR(30),
          [DESC] NVARCHAR(100),
          [DESC_EN] NVARCHAR(100),
          [WEBLINK] NVARCHAR(100)
        )
    GO