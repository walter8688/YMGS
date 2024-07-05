IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_HELPER' ) 
    CREATE TABLE TB_HELPER
        (
          [ITEMID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [PITEMID] int,
          [CNITEMNAME] varchar(50),
		  [ENITEMNAME] varchar(50),
		  [WEBLINK] varchar(200),
		  [ENWEBLINK] varchar(200),
		  [OrderNO] INT
        )
    GO