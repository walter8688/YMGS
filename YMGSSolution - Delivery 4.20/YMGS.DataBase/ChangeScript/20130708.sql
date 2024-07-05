--添加系统自发消息表
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_SYSTEM_AUTOMESSAGE' ) 
    CREATE TABLE TB_SYSTEM_AUTOMESSAGE
        (
          [MESSAGEID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [SENDTO_USERID] INT ,  --发送给会员的ID
          [MESSAGE_CONTENT] NVARCHAR(200) ,	--消息内容
		  [MESSAGE_CONTENT_EN] NVARCHAR(200) ,	--消息内容(英文)
          [SENDBY_SYSTEMID] INT ,	--发送者的ID	
          [MESSAGE_SEND_DATE] DATETIME 	--系统消息发送时间
        )
    GO