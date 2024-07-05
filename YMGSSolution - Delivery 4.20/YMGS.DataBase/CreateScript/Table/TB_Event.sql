/***
CREATE DATE:2013-01-18
DESCRIPTION:���ºͱ�������ģ��/���±�
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT' ) 
    CREATE TABLE TB_EVENT
        (
          [EVENT_ID] INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [EVENTZONE_ID] INT ,  --��������ID
          [EVENT_NAME] NVARCHAR(100) ,	--��������
		  [EVENT_NAME_EN] NVARCHAR(100) ,	--��������
          [EVENT_DESC] NVARCHAR(100) ,	--��������	
          [START_DATE] DATETIME ,	--���¿�ʼʱ��
          [END_DATE] DATETIME ,	--���½���ʱ��	
          [STATUS] INT ,--����״̬(����/��ͣ/��ֹ)	
          [CREATE_USER] INT ,--������	
          [CREATE_TIME] DATETIME ,--����ʱ��	
          [LAST_UPDATE_USER] INT ,--	���������	
          [LAST_UPDATE_TIME] DATETIME	--�������ʱ��	
        )
    GO