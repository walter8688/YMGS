/***
Create Date:2013-01-07
Description:ϵͳ����ģ��/����
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_TYPE' ) 
    BEGIN
        CREATE TABLE TB_PARAM_TYPE
            (
              [PARAM_TYPE_ID] INT PRIMARY KEY,
              [PARAM_TYPE_NAME] NVARCHAR(50)
            )
    END
    GO