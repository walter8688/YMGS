/***
CREATE DATE:2013-01-09
DESCRIPTION:用户资金账户模块/用户账户信息
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_USER_FUND' ) 
    CREATE TABLE TB_USER_FUND
        (
          [USER_FUND_ID] INT PRIMARY KEY
                             IDENTITY(1, 1) ,
          [USER_ID] INT ,--用户ID
          [BANK_NAME] NVARCHAR(40) ,--银行名称
          [OPEN_BANK_NAME] NVARCHAR(50) ,--开户银行
          [CARD_NO] NVARCHAR(30) ,--银行卡卡号
          [ACCOUNT_HOLDER] NVARCHAR(40) ,--开户人姓名
          [CUR_FUND] DECIMAL(20, 2) ,--当前资金
          [FREEZED_FUND] DECIMAL(20, 2) ,--当前冻结资金
          [CUR_INTEGRAL] INT ,--当前积分
          [STATUS] INT ,--状态
          [LAST_UPDATE_TIME] DATETIME	--最近更新时间
        )
GO