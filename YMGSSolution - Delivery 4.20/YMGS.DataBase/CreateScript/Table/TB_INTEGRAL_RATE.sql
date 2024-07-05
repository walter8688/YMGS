/***
Create Date:2013-01-07
Description:系统参数模块/积分兑换率
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_INTEGRAL_RATE' ) 
    BEGIN
        CREATE TABLE TB_INTEGRAL_RATE
            (
              [INTEGRAL_RATE_ID] INT PRIMARY KEY
                                   IDENTITY(1, 1) ,
              [INTEGRAL_RATE] DECIMAL(4, 2) ,
              [FIT_FUND] NVARCHAR(40) ,
              [CREATE_USER] INT ,
              [CREATE_TIME] DATETIME ,
              [LAST_UPDATE_USER] INT ,
              [LAST_UPDATE_TIME] DATETIME
            )
    END
    GO