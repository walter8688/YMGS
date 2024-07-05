/***
CREATE DATE:2013-01-09
DESCRIPTION:赛事和比赛管理模块/市场模板表
***/
IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_MARKET_TEMPLATE' ) 
BEGIN
	CREATE TABLE TB_MARKET_TEMPLATE
		(
		  [MARKET_TMP_ID] INT PRIMARY KEY
							  IDENTITY(1, 1) ,
		  [MARKET_TMP_NAME] NVARCHAR(50) ,--市场模板名称	
		  [MARKET_TMP_NAME_EN] NVARCHAR(100),
		  [BET_TYPE_ID] INT ,--交易类型
		  [Market_Tmp_Type] INT,--半场 0/全场 1/半全场 2
		  [HOMESCORE] INT,--主队得分(波胆)
		  [AWAYSCORE] INT, --客队得分(波胆)
		  [GOALS] decimal(18,1), --进球数(大小球)
		  [SCOREA] decimal(18,1),--让球数A(让分盘)
		  [SCOREB] decimal(18,1),--让球数B(让分盘)
		  [CREATE_USER] INT ,--创建人
		  [CREATE_TIME] DATETIME ,--创建时间	
		  [LAST_UPDATE_USER] INT ,--最近更新人	
		  [LAST_UPDATE_TIME] DATETIME	--最近更新时间	
		)
END
GO