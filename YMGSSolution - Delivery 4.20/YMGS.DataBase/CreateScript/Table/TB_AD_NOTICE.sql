IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_AD_NOTICE' ) 
    CREATE TABLE TB_AD_NOTICE
        (
          [PID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [TITLE] VARCHAR(100) ,
          [CONTENT] VARCHAR(300),
		  [ENTITLE] VARCHAR(100) ,
          [ENCONTENT] VARCHAR(300),
		  ISV INT
        )
    GO