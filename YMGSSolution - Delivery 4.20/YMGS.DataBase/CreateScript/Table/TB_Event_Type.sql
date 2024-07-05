/***
CREATE DATE:2013-01-21
DESCRIPTION:���ºͱ�������ģ��/�������
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_EVENT_TYPE' ) 
    CREATE TABLE TB_EVENT_TYPE
        (
          EventType_ID INT PRIMARY KEY ,
          EventType_Name NVARCHAR(40) NOT NULL
                                      UNIQUE,
		  EventType_Name_En NVARCHAR(40)
        )
