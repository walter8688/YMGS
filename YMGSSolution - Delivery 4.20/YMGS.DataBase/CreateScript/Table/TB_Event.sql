/***
CREATE DATE:2013-01-18
DESCRIPTION:赛事和比赛管理模块/赛事表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT' ) 
    CREATE TABLE TB_EVENT
        (
          [EVENT_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [EVENTZONE_ID] INT ,  --赛事区域ID
          [EVENT_NAME] NVARCHAR(100) ,	--赛事名称
		  [EVENT_NAME_EN] NVARCHAR(100) ,	--赛事名称
          [EVENT_DESC] NVARCHAR(100) ,	--赛事描述	
          [START_DATE] DATETIME ,	--赛事开始时间
          [END_DATE] DATETIME ,	--赛事结束时间	
          [STATUS] INT ,--赛事状态(激活/暂停/终止)	
          [CREATE_USER] INT ,--创建人	
          [CREATE_TIME] DATETIME ,--创建时间	
          [LAST_UPDATE_USER] INT ,--	最近更新人	
          [LAST_UPDATE_TIME] DATETIME	--最近更新时间	
        )
    GO