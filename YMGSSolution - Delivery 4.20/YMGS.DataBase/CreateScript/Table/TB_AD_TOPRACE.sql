IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_AD_TOPRACE' ) 
    CREATE TABLE TB_AD_TOPRACE
        (
          [TOPRACEID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [MARCHID] INT ,
          [CNPIC] varbinary(max),
		  [ENPIC] varbinary(max),
          [CNTITLE] VARCHAR(100),
		  [ENTITLE] VARCHAR(100),
		  [CNCONTENT] VARCHAR(300),
		  [ENCONTENT] VARCHAR(300)
        )
    GO