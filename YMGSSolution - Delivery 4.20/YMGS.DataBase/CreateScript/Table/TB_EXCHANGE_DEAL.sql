/***
CREATE DATE:2013-01-09
DESCRIPTION:���׺ͽ������ģ��/��϶���
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EXCHANGE_DEAL' ) 
    CREATE TABLE TB_EXCHANGE_DEAL
        (
          [EXCHANGE_DEAL_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [MATCH_ID] INT ,--����ID
          [MARKET_ID] INT ,--�����г�ID
          [EXCHANGE_BACK_ID] INT ,--Ͷע����ID
          [EXCHANGE_LAY_ID] INT ,--��ע����ID
          [DEAL_AMOUNT] DECIMAL(20, 2) ,--��Ͻ��
          [DEAL_TIME] DATETIME ,--���ʱ��
          [STATUS] INT	,--״̬
		  [MATCH_TYPE] INT,--��������
		  [ODDS] decimal(9,2)--����
        )
GO