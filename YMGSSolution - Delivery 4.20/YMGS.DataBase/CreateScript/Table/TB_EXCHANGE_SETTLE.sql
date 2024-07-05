/***
CREATE DATE:2013-01-09
DESCRIPTION:���׺ͽ������ģ��/�������
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_SETTLE' ) 
    CREATE TABLE TB_EXCHANGE_SETTLE
        (
          [EXCHANGE_SETTLE_ID] INT PRIMARY KEY
                                   IDENTITY(1, 1) ,
          [EXCHANGE_DEAL_ID] INT ,--��Ͻ���ID
          [WIN_USER_ID] INT ,--WIN�û�
          [LOSE_USER_ID] INT ,--LOSE�û�
          [WIN_INTEGRAL] DECIMAL(20, 2) ,--WIN�û�����
          [LOSE_INTEGRAL] DECIMAL(20, 2) ,--LOSE�û�����
          [BROKERAGE] DECIMAL(20, 2) ,--ƽ̨Ӷ��
          [BROKERAGE_RATE] DECIMAL(20, 4) ,--ƽ̨Ӷ����
          [AGENT_COMMISSION_RATE] DECIMAL(20,4), --����Ӷ����
          [MAIN_AGENT_COMMISSION_RATE] DECIMAL(20,4), --�ܴ���Ӷ����
          [SETTLE_TIME] DATETIME, --����ʱ��
		  [EXCHANGE_WIN_FLAG] int --��Ӯ��־
        )
GO