/***
CREATE DATE:2013-01-09
DESCRIPTION:�û��ʽ��˻�ģ��/�û��˻���ʷ��¼
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_FUND_HISTORY' ) 
    CREATE TABLE TB_FUND_HISTORY
        (
          [FUND_HISTORY_ID] INT PRIMARY KEY
                                IDENTITY(1, 1) ,
          [USER_FUND_ID] INT ,--�û��˻���Ϣ
          [TRADE_TYPE] INT ,--��������(��ֵ�����֣�Ͷע����ע����ϣ�����)
		  [TRADE_DESC] NVARCHAR(100),--��ע
          [TRADE_SERIAL_NO] INT ,--���׺�(���ݲ�ͬ�������ͣ��ͬ�ĵ���)
          [TRADE_FUND] DECIMAL(20, 2) ,--�����ʽ�
          [TRADE_DATE] DATETIME	,--����ʱ��
        )
GO