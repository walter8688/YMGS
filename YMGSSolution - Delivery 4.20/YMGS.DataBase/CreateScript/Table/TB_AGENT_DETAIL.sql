/***
CREATE DATE:2013-01-31
DESCRIPTION:´úÀíÏêÏ¸±í
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_AGENT_DETAIL' ) 
    CREATE TABLE TB_AGENT_DETAIL
        (
          Agent_User_ID INT PRIMARY KEY ,
          Brokerage DECIMAL(4, 4) ,
          Member_Count INT
        )
        

