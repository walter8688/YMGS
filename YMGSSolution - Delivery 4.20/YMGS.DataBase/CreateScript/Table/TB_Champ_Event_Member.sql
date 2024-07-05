/***
CREATE DATE:2013-01-31
DESCRIPTION:冠军赛事成员表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_Champ_Event_Member' ) 
    CREATE TABLE TB_Champ_Event_Member
        (
          Champ_Event_Member_ID INT PRIMARY KEY
                                    IDENTITY(1, 1) ,
          Champ_Event_Member_Name NVARCHAR(100) ,
		  Champ_Event_Member_Name_En NVARCHAR(100) ,
          Champ_Event_ID INT
        )
        
        
