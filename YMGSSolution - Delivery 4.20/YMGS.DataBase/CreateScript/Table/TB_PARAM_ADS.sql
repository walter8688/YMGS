/***
Create Date:2013-01-07
Description:系统参数模块/广告位
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_PARAM_ADS' ) 
    BEGIN
        CREATE TABLE TB_PARAM_ADS
            (
              [ADS_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
              [ADS_TYPE] INT ,
              [ADS_TITLE] NVARCHAR(100) ,
              [ADS_CONTENT] NTEXT ,
              [ADS_ORDER] INT ,
              [STATUS] INT ,
              [URL] NVARCHAR(200)
            )
    END
    go

