/***
Create Date:2013-01-07
Description:用户和权限管理模块/系统功能
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_SYSTEM_FUNC' ) 
    BEGIN
        CREATE TABLE TB_SYSTEM_FUNC
            (
              [FUNC_ID] INT PRIMARY KEY ,
              [PARENT_FUNC_ID] INT ,
              [FUNC_NAME] NVARCHAR(50) ,
              [FUNC_TYPE]	INT,
              [LEVELNO] INT ,
              [FUNC_ORDER] INT ,
              [URL] NVARCHAR(200) ,
              [CREATE_USER] INT ,
              [CREATE_TIME] DATETIME ,
              [LAST_UPDATE_USER] INT ,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO