/***
Create Date:2013-01-07
Description:系统参数模块/网站公告
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_BILLBOARD' ) 
    BEGIN
        CREATE TABLE TB_PARAM_BILLBOARD
            (
              [BILLBOARD_ID] INT PRIMARY KEY
                               IDENTITY(1, 1) ,
              [BILLBOARD_TITLE] NVARCHAR(100) ,
              [BILLBOARD_CONTENT] NVARCHAR(1000) ,
              [START_PLAY_TIME] DATETIME ,
              [END_PLAY_TIME] DATETIME ,
              [STATUS] INT ,
              [CREATE_USER] INT ,
              [CREATE_TIME] DATETIME ,
              [LAST_UPDATE_USER] INT ,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO