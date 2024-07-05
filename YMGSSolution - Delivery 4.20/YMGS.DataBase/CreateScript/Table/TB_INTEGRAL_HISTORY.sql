IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_INTEGRAL_HISTORY' ) 
    CREATE TABLE TB_INTEGRAL_HISTORY
        (
          [INTEGRAL_HISTORY_ID] INT PRIMARY KEY
                                 IDENTITY(1, 1) ,
          [USER_FUND_ID] INT ,--�û��˺�ID
          [EXCHANGE_DEAL_ID] int,--��Ͻ���ID
          [DEALED_FUND] DECIMAL(20, 2) ,--���׽��
          [GOT_INTEGRAL] int ,--��û���
          [TRADE_DATE] DATETIME --����ʱ��
        )
GO