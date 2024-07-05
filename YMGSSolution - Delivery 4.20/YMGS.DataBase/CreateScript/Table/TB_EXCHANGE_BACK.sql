/***
CREATE DATE:2013-01-09
DESCRIPTION:���׺ͽ������ģ��/Ͷע����
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_BACK' ) 
    CREATE TABLE TB_EXCHANGE_BACK
        (
          [EXCHANGE_BACK_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [MATCH_ID] INT ,--����ID
          [MARKET_ID] INT ,--�����г�ID
          [ODDS] DECIMAL(6, 2) ,--����
          [BET_AMOUNTS] DECIMAL(20, 2) ,--��ע���
          [MATCH_AMOUNTS] DECIMAL(20, 2) ,--���ʣ����
          [TRADE_TIME] DATETIME ,--����ʱ��
          [TRADE_USER] INT ,--��ע�û�
          [STATUS] INT,	--��ע״̬
		  [MATCH_TYPE] INT, --��������
		  [HOME_TEAM_SCORE] INT, --���ӱȷ�
		  [GUEST_TEAM_SCORE] INT --�Ͷӱȷ�
        )
GO