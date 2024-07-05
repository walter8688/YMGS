﻿--添加货币表
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_CURRENCY' ) 
    CREATE TABLE TB_CURRENCY
        (
          [CURRENCY_ID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [CURRENCY_NAME] VARCHAR(50) ,
          [CURRENCY_EN] VARCHAR(50) 
        )
    GO

--添加时区表
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_TIMEZONE' ) 
    CREATE TABLE TB_TIMEZONE
        (
          [TIMEZONE_ID] INT PRIMARY KEY
                            IDENTITY(1,1),
          [TIMEZONE_NAME] VARCHAR(50)
        )
    GO


--初始化时区
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('UK')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('ET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('PT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('CET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('CT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('MT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-12')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-11')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-10')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-9')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-8')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-7')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-6')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-4')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-3')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-2')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-1')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+1')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+2')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+3')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+4')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+6')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+7')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+8')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+9')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+9.5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+10')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+10.5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+11')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+12')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+13')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('EET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('POR')

--初始化货币
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '澳币', 'Australian Dollar (AUD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '美金', 'US Dollar (USD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '人民币', 'China Yuan (CNY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '欧元', 'Euro (EUR)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '英镑', 'Great Britain Pound (GBP)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '日元', 'Japan Yen (JPY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '新币', 'Singapore Dollar (SGD)' )

--用户表添加货币ID
ALTER TABLE dbo.TB_SYSTEM_ACCOUNT ADD CURRENCY_ID INT
--用户表添加时区ID
ALTER TABLE dbo.TB_SYSTEM_ACCOUNT ADD TIMEZONE_ID INT