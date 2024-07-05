/***
CREATE DATE:2013-01-09
DESCRIPTION:赛事和比赛管理模块/下注类型表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_BET_TYPE' ) 
    CREATE TABLE TB_BET_TYPE
        (
          [BET_TYPE_ID] INT PRIMARY KEY ,
          [BET_TYPE_NAME] NVARCHAR(40) ,--下注类型名称
		  [BET_TYPE_NAME_EN] NVARCHAR(100),
          [BET_BEFORE_GAME] BIT ,--是否赛前下注
          [BET_GAMING] BIT ,--是否赛中下注
          [CREATE_USER] INT ,--创建人	
          [CREATE_TIME] DATETIME ,--创建时间	
          [LAST_UPDATE_USER] INT ,--	最近更新人	
          [LAST_UPDATE_TIME] DATETIME	--最近更新时间	

        )
    GO