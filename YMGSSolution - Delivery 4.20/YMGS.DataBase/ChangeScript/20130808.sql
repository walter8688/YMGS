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

--增加缓存Object
IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='投注信息')
INSERT INTO TB_CACHE_OBJECT VALUES(11,'投注信息',GETDATE()) 
GO

IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='受注信息')
INSERT INTO TB_CACHE_OBJECT VALUES(12,'受注信息',GETDATE()) 
GO

IF NOT EXISTS
  (SELECT *
   FROM TB_CACHE_OBJECT
   WHERE CACHE_TYPE_DESC='你的走地盘')
INSERT INTO TB_CACHE_OBJECT VALUES(13,'你的走地盘',GETDATE()) 
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
insert into tb_settlement_log_type(log_type_id,log_type) values(1,'投注记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(2,'受注记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(3,'撮合记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(4,'结算记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(5,'资金历史记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(6,'系统资金历史记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(7,'对冲记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(8,'积分历史记录日志')
insert into tb_settlement_log_type(log_type_id,log_type) values(9,'代理佣金记录日志')
set identity_insert tb_settlement_log_type off

--增加比赛的默认盘口
ALTER TABLE dbo.TB_MATCH
ADD HandicapHalfDefault NVARCHAR(50) DEFAULT NULL,
    HandicapFullDefault NVARCHAR(50) DEFAULT NULL

-- 5003 重新结算比赛（权限）
delete from TB_SYSTEM_FUNC where func_id=5003
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5003,5001,'重新结算比赛',1,3,2,'');
-- 5012 重新结算冠军赛事（权限）
delete from TB_SYSTEM_FUNC where func_id=5012
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5012,5010,'重新结算冠军赛事',1,3,2,'');

--管理员默认结算权限
delete from TB_ROLE_FUNC_MAP where role_id=1 and func_id in(5003,5012)
INSERT  INTO dbo.TB_ROLE_FUNC_MAP ( ROLE_ID, FUNC_ID )VALUES  ( 1, 5003 )     
INSERT  INTO dbo.TB_ROLE_FUNC_MAP ( ROLE_ID, FUNC_ID )VALUES  ( 1, 5012 )

---文章管理
ALTER TABLE dbo.TB_HELPER
ADD OrderNO INT NULL
--文章管理：添加
SET IDENTITY_INSERT TB_HELPER ON
INSERT INTO TB_HELPER(ITEMID,PITEMID,CNITEMNAME,ENITEMNAME,WEBLINK,ENWEBLINK,OrderNO) 
					VALUES(0,-1,'帮助中心','Help Center',NULL,NULL,0)
SET IDENTITY_INSERT TB_HELPER OFF