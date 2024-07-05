/***
Create Date:2013-01-07
Description:�û���Ȩ�޹���ģ��/��ɫ����ӳ��
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_SYSTEM_ACCESS' ) 
    BEGIN
        CREATE TABLE TB_SYSTEM_ACCESS
            (
              [ACCESS_ID] INT PRIMARY KEY
                              IDENTITY(1, 1) ,
              [USER_ID] INT ,
              [CLIENT_IP] NVARCHAR(20) ,
              [CLIENT_OS] NVARCHAR(20) ,
              [CLIENT_BROWSER] NVARCHAR(20) ,
              [VISIT_DATE] DATETIME
            )
    END