/***
CREATE DATE:2013-01-18
DESCRIPTION:赛事和比赛管理模块/赛事成员映射表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT_TEAM_MAP' ) 
    CREATE TABLE TB_EVENT_TEAM_MAP
        (
          EVENT_TEAM_ID INT PRIMARY KEY
                            IDENTITY(1, 1) ,
          TEAM_ID INT NOT NULL ,
          EVENT_ID INT NOT NULL
        )