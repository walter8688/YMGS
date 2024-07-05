IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_AD_PIC' ) 
    CREATE TABLE TB_AD_PIC
        (
          [AD_PIC_ID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [PIC_ADDRESS] varbinary(max) ,
          [PIC_ADDRESS_EN] varbinary(max)
        )
    GO