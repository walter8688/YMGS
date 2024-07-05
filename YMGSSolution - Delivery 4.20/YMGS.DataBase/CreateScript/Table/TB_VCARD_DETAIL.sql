IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_VCARD_DETAIL' ) 
    CREATE TABLE TB_VCARD_DETAIL
        (
          VCARD_ID INT PRIMARY KEY
                       IDENTITY(1, 1) ,
          VCARD_NO NVARCHAR(100) ,
          VCARD_ACTIVATE_NO NVARCHAR(100) ,
          VCARD_FACE_VALUE INT ,
          VCARD_STATUS INT ,
          CREATE_USER_ID INT ,
          CREATE_DATE DATETIME ,
          ACTIVATE_USER_ID INT ,
          ACTIVATE_DATE DATETIME
        )
    GO
    
    