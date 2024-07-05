IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_MATCH' ) 
    CREATE TABLE TB_MATCH
        (
          [MATCH_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [MATCH_NAME] NVARCHAR(100) ,	
		  [MATCH_NAME_EN] NVARCHAR(100),
          [MATCH_DESC] NVARCHAR(100) ,	
          [EVENT_ID] INT ,	
          [EVENT_HOME_TEAM_ID] INT ,	
          [EVENT_HOME_GUEST_ID] INT ,	
          [STARTDATE] DATETIME ,	
          [ENDDATE] DATETIME ,	
          [AUTO_FREEZE_DATE] DATETIME ,	
          [HOME_FIR_HALF_SCORE] INT ,	
          [GUEST_FIR_HALF_SCORE] INT ,	
          [HOME_SEC_HALF_SCORE] INT ,	
          [GUEST_SEC_HALF_SCORE] INT ,	
          [HOME_OVERTIME_SCORE] INT ,	
          [GUEST_OVERTIME_SCORE] INT ,	
          [HOME_POINT_SCORE] INT ,	
          [GUEST_POINT_SCORE] INT ,	
          [HOME_FULL_SCORE] INT ,	
          [GUEST_FULL_SCORE] INT ,	
          [STATUS] INT ,
		  [ADDITIONALSTATUS] INT,
          [RECOMMENDMATCH] BIT,
          [IS_ZOUDI] BIT DEFAULT 0,
          [CREATE_USER] INT ,	
          [CREATE_TIME] DATETIME ,
          [LAST_UPDATE_USER] INT ,	
          [LAST_UPDATE_TIME] DATETIME,	
		  [SETTLE_STATUS] INT DEFAULT 0,
		  [First_Half_End_Time] DATETIME,
		  [Sec_Half_Start_Time] DATETIME
        )
    GO