IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_MATCH_HEDGE_FUND' ) 
    CREATE TABLE TB_MATCH_HEDGE_FUND
        (
          HEDGE_ID INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [MATCH_ID] INT ,	
          [MARKET_ID] INT ,	
          [TRADE_USER] INT ,	
		  [MATCH_TYPE] INT,
          [TRADE_FLAG] int ,	
          [EXCHANGE_BACK_LAY_ID] int ,	
          [HEDGE_AMOUNTS] decimal(18,2),
          [OPERATE_USER] INT ,	
          [OPERATE_TIME] DATETIME,
		  [STATUS] INT
        )
    GO

alter table tb_odds_compare alter column MATCHID int not null
GO
alter table tb_odds_compare alter column CN_CORP nvarchar(100) not null
GO
alter table tb_odds_compare add constraint match_CN_Corp primary key(MATCHID,CN_CORP)
GO

--���ӻ���Object
IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='Ͷע��Ϣ')
INSERT INTO TB_CACHE_OBJECT VALUES(11,'Ͷע��Ϣ',GETDATE()) 
GO

IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='��ע��Ϣ')
INSERT INTO TB_CACHE_OBJECT VALUES(12,'��ע��Ϣ',GETDATE()) 
GO

IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='����ߵ���')
INSERT INTO TB_CACHE_OBJECT VALUES(13,'����ߵ���',GETDATE()) 
GO

IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_SETTLEMENT_LOG_TYPE' ) 
    CREATE TABLE TB_SETTLEMENT_LOG_TYPE
        (
          LOG_TYPE_ID INT PRIMARY KEY
                         IDENTITY(1, 1) ,
          [LOG_TYPE] nvarchar(100)	
        )
GO

IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_SETTLEMENT_LOG' ) 
    CREATE TABLE TB_SETTLEMENT_LOG
        (
          LOG_ID INT PRIMARY KEY
                         IDENTITY(1, 1) ,
		  MATCH_ID int,
		  MATCH_TYPE int,
		  CALC_FLAG int,
		  ORIGINAL_STATUS int,
		  STATUS int,
		  OPERATOR int,
		  BEGINTIME datetime,
		  ENDTIME datetime
        )
GO

IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_SETTLEMENT_LOG_DETAIL' ) 
    CREATE TABLE TB_SETTLEMENT_LOG_DETAIL
        (
          LOG_DETAIL_ID INT PRIMARY KEY
                         IDENTITY(1, 1) ,
		  LOG_ID int,
		  LOG_TYPE_ID int,
		  TRADE_USER int,
		  LOG_OBJECT int,
		  LOG_INT_DATA1 int,
		  LOG_INT_DATA2 int,
		  LOG_DBL_DATA1 decimal(18,2),
		  LOG_DBL_DATA2 decimal(18,2),
		  LOG_STR_DATA nvarchar(100),
		  [DESCRIPTION] nvarchar(200)
        )
GO

delete from tb_settlement_log_type
set identity_insert tb_settlement_log_type on
insert into tb_settlement_log_type(log_type_id,log_type) values(1,'Ͷע��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(2,'��ע��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(3,'��ϼ�¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(4,'�����¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(5,'�ʽ���ʷ��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(6,'ϵͳ�ʽ���ʷ��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(7,'�Գ��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(8,'������ʷ��¼��־')
insert into tb_settlement_log_type(log_type_id,log_type) values(9,'����Ӷ���¼��־')
set identity_insert tb_settlement_log_type off

--���ӱ�����Ĭ���̿�
ALTER TABLE dbo.TB_MATCH
ADD HandicapHalfDefault NVARCHAR(50) DEFAULT NULL,
    HandicapFullDefault NVARCHAR(50) DEFAULT NULL

-- 5003 ���½��������Ȩ�ޣ�
delete from TB_SYSTEM_FUNC where func_id=5003
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5003,5001,'���½������',1,3,2,'');
-- 5012 ���½���ھ����£�Ȩ�ޣ�
delete from TB_SYSTEM_FUNC where func_id=5012
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5012,5010,'���½���ھ�����',1,3,2,'');

--����ԱĬ�Ͻ���Ȩ��
delete from TB_ROLE_FUNC_MAP where role_id=1 and func_id in(5003,5012)
INSERT  INTO dbo.TB_ROLE_FUNC_MAP ( ROLE_ID, FUNC_ID )VALUES  ( 1, 5003 )     
INSERT  INTO dbo.TB_ROLE_FUNC_MAP ( ROLE_ID, FUNC_ID )VALUES  ( 1, 5012 )

---���¹���
ALTER TABLE dbo.TB_HELPER
ADD OrderNO INT NULL
--���¹������
SET IDENTITY_INSERT TB_HELPER ON
INSERT INTO TB_HELPER(ITEMID,PITEMID,CNITEMNAME,ENITEMNAME,WEBLINK,ENWEBLINK,OrderNO) 
					VALUES(0,-1,'��������','Help Center',NULL,NULL,0)
SET IDENTITY_INSERT TB_HELPER OFF