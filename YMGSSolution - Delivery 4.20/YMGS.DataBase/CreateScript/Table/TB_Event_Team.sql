/***
CREATE DATE:2013-01-18
DESCRIPTION:赛事和比赛管理模块/参赛成员表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT_TEAM' ) 
    CREATE TABLE TB_EVENT_TEAM
        (
          [EVENT_TEAM_ID] INT PRIMARY KEY
                              IDENTITY(1, 1) ,
          [EVENT_ITEM_ID] INT,
          [EVENT_TEAM_NAME] NVARCHAR(50) ,
		  [EVENT_TEAM_NAME_EN] NVARCHAR(100) ,
          [EVENT_TEAM_TYPE1] INT ,--类型1(国家/职业) 
          [EVENT_TEAM_TYPE2] INT ,--类型2(男子/女子) 
          [PARAM_ZONE_ID] INT,
          [STATUS] INT ,--启用/禁用
          [CREATE_USER] INT ,
          [CREATE_TIME] DATETIME ,
          [LAST_UPDATE_USER] INT ,
          [LAST_UPDATE_TIME] DATETIME
        )
    GO
    
