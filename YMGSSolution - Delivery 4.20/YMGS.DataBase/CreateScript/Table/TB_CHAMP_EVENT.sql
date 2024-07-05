/***
CREATE DATE:2013-01-31
DESCRIPTION:冠军赛事表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_CHAMP_EVENT' ) 
    CREATE TABLE TB_CHAMP_EVENT
        (
          Champ_Event_ID INT PRIMARY KEY
                             IDENTITY(1, 1) ,
          Champ_Event_Type INT ,
          Event_ID INT ,
          Champ_Event_Name NVARCHAR(100) ,
		  Champ_Event_Name_En NVARCHAR(100) ,
          Champ_Event_Desc NVARCHAR(100) ,
          Champ_Event_StartDate DATETIME ,
          Champ_Event_EndDate DATETIME ,
          Champ_Event_Status INT ,
          Create_User INT ,
          Create_Time DATETIME ,
          Last_Update_User INT ,
          Last_Update_Time DATETIME
        )
        
