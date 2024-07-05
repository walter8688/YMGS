/***
CREATE DATE:2013-01-31
DESCRIPTION:冠军赛事市场表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_Champ_Market' ) 
    CREATE TABLE TB_Champ_Market
        (
          Champ_Market_ID INT PRIMARY KEY
                              IDENTITY(1, 1) ,
          Champ_Event_ID INT ,
          Champ_Member_ID INT,
        )
        
        