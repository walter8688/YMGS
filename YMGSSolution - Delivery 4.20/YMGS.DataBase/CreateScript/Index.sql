--TB_CHAMP_EVENT
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_CHAMP_EVENT]') AND name = N'IX_TB_CHAMP_EVENT_01')
BEGIN
	DROP INDEX IX_TB_CHAMP_EVENT_01 ON [dbo].[TB_CHAMP_EVENT] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_CHAMP_EVENT_01 ON [dbo].[TB_CHAMP_EVENT] 
(
	EVENT_ID	
)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_CHAMP_EVENT]') AND name = N'IX_TB_CHAMP_EVENT_02')
BEGIN
	DROP INDEX IX_TB_CHAMP_EVENT_02 ON [dbo].[TB_CHAMP_EVENT] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_CHAMP_EVENT_02 ON [dbo].[TB_CHAMP_EVENT] 
(
	CHAMP_EVENT_STARTDATE,
	CHAMP_EVENT_ENDDATE	
)
GO

--TB_CHAMP_EVENT_MEMBER
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_CHAMP_EVENT_MEMBER]') AND name = N'IX_TB_CHAMP_EVENT_MEMBER_01')
BEGIN
	DROP INDEX IX_TB_CHAMP_EVENT_MEMBER_01 ON [dbo].[TB_CHAMP_EVENT_MEMBER] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_CHAMP_EVENT_MEMBER_01 ON [dbo].[TB_CHAMP_EVENT_MEMBER] 
(
	CHAMP_EVENT_ID
)
GO


--TB_CHAMP_MARKET
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_CHAMP_MARKET]') AND name = N'IX_TB_CHAMP_MARKET_01')
BEGIN
	DROP INDEX IX_TB_CHAMP_MARKET_01 ON [dbo].[TB_CHAMP_MARKET] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_CHAMP_MARKET_01 ON [dbo].[TB_CHAMP_MARKET] 
(
	CHAMP_EVENT_ID,
	CHAMP_MEMBER_ID
)
GO

--TB_CHAMP_WIN_MEMBER
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_CHAMP_WIN_MEMBER]') AND name = N'IX_TB_CHAMP_WIN_MEMBER_01')
BEGIN
	DROP INDEX IX_TB_CHAMP_WIN_MEMBER_01 ON [dbo].[TB_CHAMP_WIN_MEMBER] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_CHAMP_WIN_MEMBER_01 ON [dbo].[TB_CHAMP_WIN_MEMBER] 
(
	CHAMP_EVENT_ID
)
GO

--TB_CHAMP_WIN_MEMBER
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EVENT]') AND name = N'IX_TB_EVENT_01')
BEGIN
	DROP INDEX IX_TB_EVENT_01 ON [dbo].[TB_EVENT] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EVENT_01 ON [dbo].[TB_EVENT] 
(
	START_DATE,
	END_DATE
)
GO

--TB_EXCHANGE_BACK
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_BACK]') AND name = N'IX_TB_EXCHANGE_BACK_01')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_BACK_01 ON [dbo].[TB_EXCHANGE_BACK] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_BACK_01 ON [dbo].[TB_EXCHANGE_BACK] (MATCH_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_BACK]') AND name = N'IX_TB_EXCHANGE_BACK_02')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_BACK_02 ON [dbo].[TB_EXCHANGE_BACK] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_BACK_02 ON [dbo].[TB_EXCHANGE_BACK] (MARKET_ID)
GO


--TB_EXCHANGE_DEAL
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_DEAL]') AND name = N'IX_TB_EXCHANGE_DEAL_01')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_DEAL_01 ON [dbo].[TB_EXCHANGE_DEAL] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_DEAL_01 ON [dbo].[TB_EXCHANGE_DEAL] (MATCH_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_DEAL]') AND name = N'IX_TB_EXCHANGE_DEAL_02')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_DEAL_02 ON [dbo].[TB_EXCHANGE_DEAL] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_DEAL_02 ON [dbo].[TB_EXCHANGE_DEAL] (EXCHANGE_BACK_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_DEAL]') AND name = N'IX_TB_EXCHANGE_DEAL_03')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_DEAL_03 ON [dbo].[TB_EXCHANGE_DEAL] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_DEAL_03 ON [dbo].[TB_EXCHANGE_DEAL] (EXCHANGE_LAY_ID)
GO


--TB_EXCHANGE_LAY
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_LAY]') AND name = N'IX_TB_EXCHANGE_LAY_01')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_LAY_01 ON [dbo].[TB_EXCHANGE_LAY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_LAY_01 ON [dbo].[TB_EXCHANGE_LAY] (MATCH_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_LAY]') AND name = N'IX_TB_EXCHANGE_LAY_02')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_LAY_02 ON [dbo].[TB_EXCHANGE_LAY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_LAY_02 ON [dbo].[TB_EXCHANGE_LAY] (MARKET_ID)
GO


--TB_EXCHANGE_SETTLE
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_EXCHANGE_SETTLE]') AND name = N'IX_TB_EXCHANGE_SETTLE_01')
BEGIN
	DROP INDEX IX_TB_EXCHANGE_SETTLE_01 ON [dbo].[TB_EXCHANGE_SETTLE] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_EXCHANGE_SETTLE_01 ON [dbo].[TB_EXCHANGE_SETTLE] (EXCHANGE_DEAL_ID)
GO


--TB_FUND_HISTORY
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_FUND_HISTORY]') AND name = N'IX_TB_FUND_HISTORY_01')
BEGIN
	DROP INDEX IX_TB_FUND_HISTORY_01 ON [dbo].[TB_FUND_HISTORY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_FUND_HISTORY_01 ON [dbo].[TB_FUND_HISTORY] (USER_FUND_ID)
GO


--TB_INTEGRAL_HISTORY
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_INTEGRAL_HISTORY]') AND name = N'IX_TB_INTEGRAL_HISTORY_01')
BEGIN
	DROP INDEX IX_TB_INTEGRAL_HISTORY_01 ON [dbo].[TB_INTEGRAL_HISTORY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_INTEGRAL_HISTORY_01 ON [dbo].[TB_INTEGRAL_HISTORY] (USER_FUND_ID)
GO


--TB_MAIN_FUND_HISTORY
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MAIN_FUND_HISTORY]') AND name = N'IX_TB_MAIN_FUND_HISTORY_01')
BEGIN
	DROP INDEX IX_TB_MAIN_FUND_HISTORY_01 ON [dbo].[TB_MAIN_FUND_HISTORY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MAIN_FUND_HISTORY_01 ON [dbo].[TB_MAIN_FUND_HISTORY] (COME_USER_FUND_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MAIN_FUND_HISTORY]') AND name = N'IX_TB_MAIN_FUND_HISTORY_02')
BEGIN
	DROP INDEX IX_TB_MAIN_FUND_HISTORY_02 ON [dbo].[TB_MAIN_FUND_HISTORY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MAIN_FUND_HISTORY_02 ON [dbo].[TB_MAIN_FUND_HISTORY] (EXCHANGE_SETTLE_ID)
GO


--TB_MATCH
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MATCH]') AND name = N'IX_TB_MATCH_01')
BEGIN
	DROP INDEX IX_TB_MATCH_01 ON [dbo].[TB_MATCH] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MATCH_01 ON [dbo].[TB_MATCH] (STARTDATE)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MATCH]') AND name = N'IX_TB_MATCH_02')
BEGIN
	DROP INDEX IX_TB_MATCH_02 ON [dbo].[TB_MATCH] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MATCH_02 ON [dbo].[TB_MATCH] (ENDDATE)
GO


--TB_MATCH_MARKET
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MATCH_MARKET]') AND name = N'IX_TB_MATCH_MARKET_01')
BEGIN
	DROP INDEX IX_TB_MATCH_MARKET_01 ON [dbo].[TB_MATCH_MARKET] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MATCH_MARKET_01 ON [dbo].[TB_MATCH_MARKET] (MATCH_ID)
GO

--TB_ROLE_FUNC_MAP
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_ROLE_FUNC_MAP]') AND name = N'IX_TB_ROLE_FUNC_MAP_01')
BEGIN
	DROP INDEX IX_TB_ROLE_FUNC_MAP_01 ON [dbo].[TB_ROLE_FUNC_MAP] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_ROLE_FUNC_MAP_01 ON [dbo].[TB_ROLE_FUNC_MAP] (ROLE_ID)
GO

--TB_SYSTEM_ACCOUNT
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_SYSTEM_ACCOUNT]') AND name = N'IX_TB_SYSTEM_ACCOUNT_01')
BEGIN
	DROP INDEX IX_TB_SYSTEM_ACCOUNT_01 ON [dbo].[TB_SYSTEM_ACCOUNT] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_SYSTEM_ACCOUNT_01 ON [dbo].[TB_SYSTEM_ACCOUNT] (LOGIN_NAME)
GO


--TB_USER_FUND
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_USER_FUND]') AND name = N'IX_TB_USER_FUND_01')
BEGIN
	DROP INDEX IX_TB_USER_FUND_01 ON [dbo].[TB_USER_FUND] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_USER_FUND_01 ON [dbo].[TB_USER_FUND] (USER_ID)
GO

--TB_USER_PAY
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_USER_PAY]') AND name = N'IX_TB_USER_PAY_01')
BEGIN
	DROP INDEX IX_TB_USER_PAY_01 ON [dbo].[TB_USER_PAY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_USER_PAY_01 ON [dbo].[TB_USER_PAY] (USER_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_USER_PAY]') AND name = N'IX_TB_USER_PAY_02')
BEGIN
	DROP INDEX IX_TB_USER_PAY_02 ON [dbo].[TB_USER_PAY] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_USER_PAY_02 ON [dbo].[TB_USER_PAY] (VCARD_ID)
GO

--TB_USER_WITHDRAW
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_USER_WITHDRAW]') AND name = N'IX_TB_USER_WITHDRAW_01')
BEGIN
	DROP INDEX IX_TB_USER_WITHDRAW_01 ON [dbo].[TB_USER_WITHDRAW] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_USER_WITHDRAW_01 ON [dbo].[TB_USER_WITHDRAW] (USER_ID)
GO

--TB_MATCH_HEDGE_FUND
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MATCH_HEDGE_FUND]') AND name = N'IX_TB_MATCH_HEDGE_FUND_01')
BEGIN
	DROP INDEX IX_TB_MATCH_HEDGE_FUND_01 ON [dbo].[TB_MATCH_HEDGE_FUND] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MATCH_HEDGE_FUND_01 ON [dbo].[TB_MATCH_HEDGE_FUND] (MATCH_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_MATCH_HEDGE_FUND]') AND name = N'IX_TB_MATCH_HEDGE_FUND_02')
BEGIN
	DROP INDEX IX_TB_MATCH_HEDGE_FUND_02 ON [dbo].[TB_MATCH_HEDGE_FUND] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_MATCH_HEDGE_FUND_02 ON [dbo].[TB_MATCH_HEDGE_FUND] (MARKET_ID)
GO

--TB_SETTLEMENT_LOG
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_SETTLEMENT_LOG]') AND name = N'IX_TB_SETTLEMENT_LOG_01')
BEGIN
	DROP INDEX IX_TB_SETTLEMENT_LOG_01 ON [dbo].[TB_SETTLEMENT_LOG] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_SETTLEMENT_LOG_01 ON [dbo].[TB_SETTLEMENT_LOG] (MATCH_ID)
GO

--TB_SETTLEMENT_LOG_DETAIL
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_SETTLEMENT_LOG_DETAIL]') AND name = N'IX_TB_SETTLEMENT_LOG_DETAIL_01')
BEGIN
	DROP INDEX IX_TB_SETTLEMENT_LOG_DETAIL_01 ON [dbo].[TB_SETTLEMENT_LOG_DETAIL] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_SETTLEMENT_LOG_DETAIL_01 ON [dbo].[TB_SETTLEMENT_LOG_DETAIL] (LOG_ID)
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = 
	OBJECT_ID(N'[dbo].[TB_SETTLEMENT_LOG_DETAIL]') AND name = N'IX_TB_SETTLEMENT_LOG_DETAIL_02')
BEGIN
	DROP INDEX IX_TB_SETTLEMENT_LOG_DETAIL_02 ON [dbo].[TB_SETTLEMENT_LOG_DETAIL] 
END
GO

CREATE NONCLUSTERED INDEX IX_TB_SETTLEMENT_LOG_DETAIL_02 ON [dbo].[TB_SETTLEMENT_LOG_DETAIL] (TRADE_USER)
GO