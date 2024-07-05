/***
CREATE DATE:2013-01-16
DESCRIPTION:赛事和比赛管理模块/赛事区域
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT_ZONE' ) 
    CREATE TABLE TB_EVENT_ZONE
        (
          [EVENTZONE_ID] INT PRIMARY KEY
                             IDENTITY(1, 1) ,
          [EVENTITEM_ID] INT ,
          [EVENTZONE_NAME] NVARCHAR(40) NOT NULL
                                        UNIQUE ,
		  [EVENTZONE_NAME_EN] NVARCHAR(100),
          [EVENTZONE_DESC] NVARCHAR(100) ,
		  [PARAM_ZONE_ID] INT ,
          [CREATE_USER] INT ,
          [CREATE_TIME] DATETIME ,
          [LAST_UPDATE_USER] INT ,
          [LAST_UPDATE_TIME] DATETIME
        )
    GO