IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_mytrade]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_mytrade]
GO
CREATE PROCEDURE pr_get_mytrade
    (
    @BetType int,
      @BetStatus INT ,
      @startDateTime DateTime,
      @EndDateTime DateTime,
	  @TRADEUSER INT
    )
AS 
    BEGIN
  select main.BETTYPE,main.BETID,
 case main.MATCH_TYPE when 1 then main.MATCH_ID  else champion.MATCH_ID end MATCH_ID
 ,main.ODDS,main.BET_AMOUNTS,main.MATCH_AMOUNTS,main.TRADE_TIME,main.TRADE_USER,main.STATUS,main.MATCH_TYPE,
 case main.MATCH_TYPE when 1 then main.MARKET_ID else champion.MARKET_ID end MARKET_ID ,
 case main.MATCH_TYPE when 1 then mm.MARKET_NAME else champion.MARKET_NAME end MARKET_NAME,
 case main.MATCH_TYPE when 1 then m.MATCH_NAME  else champion.MATCH_NAME end MATCH_NAME,
 case main.MATCH_TYPE when 1 then mm.MARKET_NAME_EN else champion.MARKET_NAME_EN end MARKET_NAME_EN,
 case main.MATCH_TYPE when 1 then m.MATCH_NAME_EN  else champion.MATCH_NAME_EN end MATCH_NAME_EN,
 case main.MATCH_TYPE when 1 then TB_MARKET_TEMPLATE.Market_Tmp_Type else -1 end Market_Tmp_Type,
 case main.MATCH_TYPE when 1 then TB_BET_TYPE.BET_TYPE_NAME else '' end BET_TYPE_NAME,
 case main.MATCH_TYPE when 1 then TB_BET_TYPE.BET_TYPE_NAME_EN else '' end BET_TYPE_NAME_EN,
 sa.USER_NAME, main.HOME_TEAM_SCORE,main.GUEST_TEAM_SCORE,betResult.TRADE_FUND from (
                   select 1 as BETTYPE,EXCHANGE_BACK_ID as BETID, MATCH_ID, MARKET_ID, ODDS, BET_AMOUNTS, MATCH_AMOUNTS, TRADE_TIME, TRADE_USER, STATUS,MATCH_TYPE,HOME_TEAM_SCORE,GUEST_TEAM_SCORE
                   from dbo.TB_EXCHANGE_BACK WHERE TRADE_USER = @TRADEUSER
                   union
                   SELECT  2 AS BETTYPE,EXCHANGE_LAY_ID as BETID, MATCH_ID, MARKET_ID, ODDS, BET_AMOUNTS, MATCH_AMOUNTS, TRADE_TIME, TRADE_USER, STATUS,MATCH_TYPE,HOME_TEAM_SCORE,GUEST_TEAM_SCORE
                   FROM  dbo.TB_EXCHANGE_LAY WHERE TRADE_USER = @TRADEUSER ) main
                   left join 
                   TB_Match_Market mm on mm.MARKET_ID=main.MARKET_ID
                   left JOIN 
                   TB_MARKET_TEMPLATE ON mm.MARKET_TMP_ID = TB_MARKET_TEMPLATE.MARKET_TMP_ID
                   left JOIN 
                   TB_BET_TYPE ON TB_MARKET_TEMPLATE.BET_TYPE_ID = TB_BET_TYPE.BET_TYPE_ID
                   left join 
                   TB_Match m on m.MATCH_ID=main.MATCH_ID
                   left join
                   TB_System_Account sa on sa.USER_ID=main.TRADE_USER
                   left join 
                   (
                   SELECT mm.Champ_Market_ID MARKET_ID,C.Champ_Event_Member_Name MARKET_NAME,isnull(c.Champ_Event_Member_Name_En,C.Champ_Event_Member_Name) MARKET_NAME_EN,
B.Champ_Event_ID MATCH_ID,mm.Champ_Event_ID MARKET_TMP_ID,1 MARKET_FLAG,
 B.Champ_Event_Name  MARKET_TMP_NAME,B.Champ_Event_Name_En,
           isnull(B.Champ_Event_Name,D.EVENT_NAME) MATCH_NAME,isnull(B.Champ_Event_Name_En,D.EVENT_NAME_EN) MATCH_NAME_EN,
           convert(varchar(30),B.Champ_Event_StartDate,111) STARTDATE
 from TB_CHAMP_MARKET mm
 INNER JOIN TB_CHAMP_EVENT B ON mm.CHAMP_EVENT_ID=B.CHAMP_EVENT_ID
 LEFT JOIN TB_CHAMP_EVENT_MEMBER C ON C.CHAMP_EVENT_MEMBER_ID=mm.Champ_Member_ID
 LEFT JOIN TB_EVENT D ON D.EVENT_ID=B.EVENT_ID
 
                   ) champion on champion.MARKET_ID=main.MARKET_ID and main.MATCH_TYPE=2  
					LEFT JOIN 
                   (select 1 BETTYPE,betBackResult.BETID,SUM(TRADE_FUND) TRADE_FUND from (
select TB_EXCHANGE_BACK.EXCHANGE_BACK_ID BETID,TB_FUND_HISTORY.TRADE_FUND from TB_EXCHANGE_BACK 
inner join TB_EXCHANGE_DEAL on TB_EXCHANGE_BACK.EXCHANGE_BACK_ID = TB_EXCHANGE_DEAL.EXCHANGE_BACK_ID
inner join TB_FUND_HISTORY on TB_EXCHANGE_DEAL.EXCHANGE_DEAL_ID = TB_FUND_HISTORY.TRADE_SERIAL_NO
inner join TB_USER_FUND on TB_FUND_HISTORY.USER_FUND_ID = TB_USER_FUND.USER_FUND_ID
where TB_EXCHANGE_BACK.TRADE_USER = @TRADEUSER and TB_FUND_HISTORY.TRADE_TYPE = 5
and TB_USER_FUND.USER_ID = @TRADEUSER) betBackResult
group by betBackResult.BETID
UNION
select 2 BETTYPE,betLayResult.BETID,SUM(TRADE_FUND) TRADE_FUND from (
select TB_EXCHANGE_LAY.EXCHANGE_LAY_ID BETID,TB_FUND_HISTORY.TRADE_FUND from TB_EXCHANGE_LAY 
inner join TB_EXCHANGE_DEAL on TB_EXCHANGE_LAY.EXCHANGE_LAY_ID = TB_EXCHANGE_DEAL.EXCHANGE_LAY_ID
inner join TB_FUND_HISTORY on TB_EXCHANGE_DEAL.EXCHANGE_DEAL_ID = TB_FUND_HISTORY.TRADE_SERIAL_NO
inner join TB_USER_FUND on TB_FUND_HISTORY.USER_FUND_ID = TB_USER_FUND.USER_FUND_ID
where TB_EXCHANGE_LAY.TRADE_USER = @TRADEUSER and TB_FUND_HISTORY.TRADE_TYPE = 5
and TB_USER_FUND.USER_ID = @TRADEUSER) betLayResult
group by betLayResult.BETID) betResult
ON main.BETTYPE = betResult.BETTYPE AND main.BETID = betResult.BETID
         
                   where (main.BETTYPE=@BetType or @BetType=0) and 
                         main.TRADE_USER=@TRADEUSER and
                        ((main.STATUS in (3,4) and @BetStatus in(3,4))or(main.STATUS=@BetStatus or @BetStatus=0)) and
                         (main.TRADE_TIME between @startDateTime and  @EndDateTime) 
                         ORDER BY main.TRADE_TIME DESC
    END
GO