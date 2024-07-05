/***
CREATE DATE:2013-07-19
DESCRIPTION:��Ա�ܴ�������
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_APPLY_PROXY' ) 
    CREATE TABLE TB_APPLY_PROXY
        (
            [Apply_Proxy_ID] INT PRIMARY KEY IDENTITY(1, 1) ,
			[User_ID] [int] NULL, --�����ܴ���Ļ�Ա��ID
			[Role_ID] [int] NULL,--�����ܴ����ID
			[User_Telephone] [nvarchar](50) NULL,--�ֻ�����
			[User_Country] [nvarchar](50) NULL,--��������
			[User_Province] [nvarchar](50) NULL,--������ʡ
			[User_City] [nvarchar](50) NULL,--��������
			[User_BankAddress] [nvarchar](50) NULL,--�����е�ַ
			[User_BankNO] [nvarchar](50) NULL,--���п���
			[Apply_Status] [int] NULL,--����״̬
			[Apply_Date] [datetime] NULL,--ϵͳ��Ϣ����ʱ��
        )
    GO