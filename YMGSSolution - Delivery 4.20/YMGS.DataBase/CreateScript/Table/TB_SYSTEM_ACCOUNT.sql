/***
Create Date:2013-01-07
Description:用户和权限管理模块/系统用户 
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = N'TB_SYSTEM_ACCOUNT' ) 
    BEGIN
        CREATE TABLE TB_SYSTEM_ACCOUNT
            (
              [USER_ID] INT PRIMARY KEY
                            IDENTITY(1, 1) ,
              [USER_NAME] NVARCHAR(40) ,
              [BORN_YEAR] NVARCHAR(4) ,
              [BORN_MONTH] NVARCHAR(2) ,
              [BORN_DAY] NVARCHAR(2) ,
              [EMAIL_ADDRESS] NVARCHAR(50) ,
              [COUNTRY] INT ,
              [CITY] NVARCHAR(50) ,
              [ADDRESS] NVARCHAR(100) ,
              [ZIP_CODE] NVARCHAR(10) ,
              [PHONE_TYPE] INT ,
              [PHONE_ZONE] NVARCHAR(5) ,
              [PHONE_NUMBER] NVARCHAR(20) ,
              [LOGIN_NAME] NVARCHAR(20) NOT NULL ,
              [PASSWORD] NVARCHAR(200) NOT NULL ,
              [SQUESTION1] INT ,
              [SANSWER1] NVARCHAR(100) ,
              [ROLE_ID] INT ,
              [ACCOUNT_STATUS] INT ,
              [AGENT_ID] INT ,
			  [CURRENCY_ID] INT,
			  [TIMEZONE_ID] INT,	  
              [CREATE_DATE] DATETIME DEFAULT GETDATE()
            )
    END
    GO