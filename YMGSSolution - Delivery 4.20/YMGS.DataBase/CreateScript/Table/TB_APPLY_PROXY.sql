/***
CREATE DATE:2013-07-19
DESCRIPTION:会员总代理申请
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_APPLY_PROXY' ) 
    CREATE TABLE TB_APPLY_PROXY
        (
            [Apply_Proxy_ID] INT PRIMARY KEY IDENTITY(1, 1) ,
			[User_ID] [int] NULL, --申请总代理的会员的ID
			[Role_ID] [int] NULL,--申请总代理的ID
			[User_Telephone] [nvarchar](50) NULL,--手机号码
			[User_Country] [nvarchar](50) NULL,--所属国家
			[User_Province] [nvarchar](50) NULL,--所属州省
			[User_City] [nvarchar](50) NULL,--所属城市
			[User_BankAddress] [nvarchar](50) NULL,--开户行地址
			[User_BankNO] [nvarchar](50) NULL,--银行卡号
			[Apply_Status] [int] NULL,--申请状态
			[Apply_Date] [datetime] NULL,--系统消息发送时间
        )
    GO