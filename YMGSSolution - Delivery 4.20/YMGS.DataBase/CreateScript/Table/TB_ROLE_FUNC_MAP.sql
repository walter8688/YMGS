/***
Create Date:2013-01-07
Description:用户和权限管理模块/角色功能映射
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_ROLE_FUNC_MAP' ) 
    BEGIN
        CREATE TABLE TB_ROLE_FUNC_MAP
            (
              [ROLE_FUNC_MAP_ID] INT PRIMARY KEY
                                     IDENTITY(1, 1) ,
              [ROLE_ID] INT ,
              [FUNC_ID] INT 
            )
    END
    GO
    
