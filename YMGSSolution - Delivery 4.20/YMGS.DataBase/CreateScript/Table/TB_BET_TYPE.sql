/***
CREATE DATE:2013-01-09
DESCRIPTION:���ºͱ�������ģ��/��ע���ͱ�
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_BET_TYPE' ) 
    CREATE TABLE TB_BET_TYPE
        (
          [BET_TYPE_ID] INT PRIMARY KEY ,
          [BET_TYPE_NAME] NVARCHAR(40) ,--��ע��������
		  [BET_TYPE_NAME_EN] NVARCHAR(100),
          [BET_BEFORE_GAME] BIT ,--�Ƿ���ǰ��ע
          [BET_GAMING] BIT ,--�Ƿ�������ע
          [CREATE_USER] INT ,--������	
          [CREATE_TIME] DATETIME ,--����ʱ��	
          [LAST_UPDATE_USER] INT ,--	���������	
          [LAST_UPDATE_TIME] DATETIME	--�������ʱ��	

        )
    GO