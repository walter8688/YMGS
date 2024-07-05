/***
CREATE DATE:2013-01-16
DESCRIPTION:赛事和比赛管理模块/赛事区域
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT_ITEM' ) 
    CREATE TABLE TB_EVENT_ITEM
        (
          EventItem_ID INT PRIMARY KEY ,
          EventType_ID INT ,
          EventItem_Name NVARCHAR(40) NOT NULL
                                      UNIQUE,
		  EventItem_Name_En NVARCHAR(100)
        )
