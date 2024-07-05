/***
Create Date:2013-01-07
Description:系统参数模块/赔率对比
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_ODDS' ) 
    BEGIN
        CREATE TABLE TB_PARAM_ODDS
            (
              [ODDS_ID] INT PRIMARY KEY
                            IDENTITY(1, 1) ,
              [MATCH_ID] INT NOT NULL ,
              [COMPANY1] NVARCHAR(100) ,
              [ODDS1] DECIMAL(4, 2) ,
              [COMPANY2] NVARCHAR(100) ,
              [ODDS2] DECIMAL(4, 2) ,
              [COMPANY3] NVARCHAR(100) ,
              [ODDS3] DECIMAL(4, 2) ,
              [COMPANY4] NVARCHAR(100) ,
              [ODDS4] DECIMAL(4, 2)
            )
    END
    GO