/***
CREATE DATE:2013-01-31
DESCRIPTION:冠军赛事表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_Champ_Win_Member' ) 
    CREATE TABLE TB_Champ_Win_Member
        (
          Champ_Win_Member_ID INT PRIMARY KEY
                                  IDENTITY(1, 1) ,
          Champ_Event_Member_ID INT ,
          Champ_Event_ID INT
        )
        
