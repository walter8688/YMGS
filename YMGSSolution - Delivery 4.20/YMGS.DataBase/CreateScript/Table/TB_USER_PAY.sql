IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_USER_PAY' ) 
    CREATE TABLE TB_USER_PAY
        (
          [USER_PAY_ID] INT PRIMARY KEY
                            IDENTITY(1, 1) ,
          [USER_ID] INT ,
          [VCARD_ID] INT ,
          [MER_ID] NVARCHAR(15) ,
          [ORDER_ID] NVARCHAR(16) ,
          [TRAN_AMOUNT] DECIMAL(18, 2) ,
          [TRAN_DATE] DATETIME ,
          [TRAN_STATUS] INT ,
          [TRAN_TYPE] INT
        )
    GO
    
    
