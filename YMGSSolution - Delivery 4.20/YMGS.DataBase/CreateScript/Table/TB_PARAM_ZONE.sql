/***
Create Date:2013-01-07
Description:系统参数模块/区域
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_ZONE' ) 
    BEGIN
        CREATE TABLE TB_PARAM_ZONE
            (
              [ZONE_ID] INT PRIMARY KEY
                            IDENTITY(1, 1) ,
              [PARENT_ZONE_ID] INT ,
              [ZONE_NAME] NVARCHAR(40) ,
              [ZONE_ORDER] INT
            )
    END
    GO