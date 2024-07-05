/***
CREATE DATE:2013-01-09
DESCRIPTION:�û��ʽ��˻�ģ��/�û��˻���Ϣ
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_USER_FUND' ) 
    CREATE TABLE TB_USER_FUND
        (
          [USER_FUND_ID] INT PRIMARY KEY
                             IDENTITY(1, 1) ,
          [USER_ID] INT ,--�û�ID
          [BANK_NAME] NVARCHAR(40) ,--��������
          [OPEN_BANK_NAME] NVARCHAR(50) ,--��������
          [CARD_NO] NVARCHAR(30) ,--���п�����
          [ACCOUNT_HOLDER] NVARCHAR(40) ,--����������
          [CUR_FUND] DECIMAL(20, 2) ,--��ǰ�ʽ�
          [FREEZED_FUND] DECIMAL(20, 2) ,--��ǰ�����ʽ�
          [CUR_INTEGRAL] INT ,--��ǰ����
          [STATUS] INT ,--״̬
          [LAST_UPDATE_TIME] DATETIME	--�������ʱ��
        )
GO