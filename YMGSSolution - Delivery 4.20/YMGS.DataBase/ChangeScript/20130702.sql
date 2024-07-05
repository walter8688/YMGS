--增加Your In-Play表
IF NOT EXISTS ( SELECT  * FROM    SYSOBJECTS WHERE   XTYPE = 'U' AND UPPER(NAME) = 'TB_YOUR_INPLAY' ) 
    CREATE TABLE TB_YOUR_INPLAY
        (
          [YOUR_INPLAY_ID] INT PRIMARY KEY IDENTITY(1, 1) ,
          [USER_ID] INT ,
          [MATCH_ID] INT ,
          [IS_FAV] INT
        )
    GO
    