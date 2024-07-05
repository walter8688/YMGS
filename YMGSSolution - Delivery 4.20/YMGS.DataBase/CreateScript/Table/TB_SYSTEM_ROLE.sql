/***
Create Date:2013-01-07
Description:用户和权限管理模块/系统角色 
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_SYSTEM_ROLE' ) 
    BEGIN
        CREATE TABLE TB_SYSTEM_ROLE
            (
              [ROLE_ID] INT PRIMARY KEY IDENTITY(1,1),
              [ROLE_NAME] NVARCHAR(40) ,
              [ROLE_DESC] NVARCHAR(100) ,
              [CREATE_USER] INT ,
              [CREATE_TIME] DATETIME ,
              [LAST_UPDATE_USER] INT ,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO