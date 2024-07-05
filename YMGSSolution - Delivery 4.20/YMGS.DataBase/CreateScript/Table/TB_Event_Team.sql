/***
CREATE DATE:2013-01-18
DESCRIPTION:���ºͱ�������ģ��/������Ա��
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
          [EVENT_TEAM_TYPE1] INT ,--����1(����/ְҵ) 
          [EVENT_TEAM_TYPE2] INT ,--����2(����/Ů��) 
          [PARAM_ZONE_ID] INT,
          [STATUS] INT ,--����/����
          [CREATE_USER] INT ,
          [CREATE_TIME] DATETIME ,
          [LAST_UPDATE_USER] INT ,
          [LAST_UPDATE_TIME] DATETIME
        )
    GO
    
