IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_USER_WITHDRAW' ) 
    CREATE TABLE TB_USER_WITHDRAW
        (
          [USER_WD_ID] INT PRIMARY KEY
                           IDENTITY(1, 1) ,
          [USER_ID] INT ,
          [TRANS_ID] NVARCHAR(15) ,
          [WD_STATUS] INT ,
          [WD_DATE] DATETIME ,
          [WD_AMOUNT] DECIMAL(18, 2) ,
          [WD_BANK_NAME] NVARCHAR(40) ,
          [WD_CARD_NO] NVARCHAR(30) ,
          [WD_ACCOUNT_HOLDER] NVARCHAR(40) ,
          [REMARK] NVARCHAR(100)
        )
    GO
    
    