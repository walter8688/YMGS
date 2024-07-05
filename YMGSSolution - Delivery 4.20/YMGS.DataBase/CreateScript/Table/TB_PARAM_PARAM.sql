/***
Create Date:2013-01-07
Description:系统参数模块/参数
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_PARAM' ) 
    BEGIN
        CREATE TABLE TB_PARAM_PARAM
            (
              [PARAM_ID] INT PRIMARY KEY
                             IDENTITY(1, 1) ,
              [PARAM_TYPE] INT ,
              [PARAM_NAME] NVARCHAR(100) ,
              [PARAM_ORDER] INT,
			  [IS_USE] INT,
              [CREATE_USER] INT ,
              [CREATE_TIME] DATETIME ,
              [LAST_UPDATE_USER] INT ,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO