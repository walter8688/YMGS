IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champion_match_market_by_eventid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champion_match_market_by_eventid]
GO
CREATE PROCEDURE [dbo].[pr_get_champion_match_market_by_eventid]
    (
      @Match_Id int
    )
AS 
BEGIN
SELECT 1 BET_TYPE_ID,1 Market_Tmp_Type,mm.Champ_Market_ID MARKET_ID,C.Champ_Event_Member_Name MARKET_NAME,isnull(C.Champ_Event_Member_Name_En,C.Champ_Event_Member_Name) MARKET_NAME_EN,
B.Champ_Event_ID MATCH_ID,mm.Champ_Event_ID MARKET_TMP_ID,1 MARKET_FLAG,null SCOREA,null SCOREB,
 B.Champ_Event_Name  MARKET_TMP_NAME,B.Champ_Event_Name_En  MARKET_TMP_NAME_EN,
          case B.EVENT_ID when -1 then 'ÓéÀÖ¹Ú¾ü' else  D.EVENT_NAME end MATCH_NAME,case B.EVENT_ID when -1 then 'Entertainment Champion' else  D.EVENT_NAME_EN end MATCH_NAME_EN,
           --convert(varchar(30),B.Champ_Event_StartDate,111) STARTDATE,
           Champ_Event_StartDate STARTDATE,
           isnull(back.MATCH_AMOUNTS,0) backMATCH_AMOUNTS,
           isnull(back.ODDS,0) backodds,
           isnull(lay.MATCH_AMOUNTS,0) layMATCH_AMOUNTS,
           isnull(lay.ODDS,0) layodds
 from TB_CHAMP_MARKET mm
 INNER JOIN TB_CHAMP_EVENT B ON mm.CHAMP_EVENT_ID=B.CHAMP_EVENT_ID
 LEFT JOIN TB_CHAMP_EVENT_MEMBER C ON C.CHAMP_EVENT_MEMBER_ID=mm.Champ_Member_ID
 LEFT JOIN TB_EVENT D ON D.EVENT_ID=B.EVENT_ID
left join 
(select back.MARKET_ID, back.ODDS,sum(back.MATCH_AMOUNTS) MATCH_AMOUNTS from TB_EXCHANGE_BACK back
 where exists
       (select MARKET_ID from (
                               select MARKET_ID,min(ODDS) ODDS FROM TB_EXCHANGE_BACK 
							   where STATUS=1 and MATCH_TYPE=2
                               group by MARKET_ID
							   ) eb where eb.MARKET_ID=back.MARKET_ID and eb.ODDS=back.ODDS
	   ) and MATCH_TYPE=2
       group by back.MARKET_ID,back.ODDS
) back on back.MARKET_ID=mm.Champ_Market_ID
left join 
(select lay.MARKET_ID, lay.ODDS,sum(lay.MATCH_AMOUNTS) MATCH_AMOUNTS from TB_EXCHANGE_LAY lay
 where exists
       (select MARKET_ID from (
                            select MARKET_ID,max(ODDS) ODDS FROM TB_EXCHANGE_LAY 
							where STATUS=1  and MATCH_TYPE=2
                            group by MARKET_ID
		                       ) la where la.MARKET_ID=lay.MARKET_ID and la.ODDS=lay.ODDS
        ) and MATCH_TYPE=2
        group by lay.MARKET_ID,lay.ODDS
) lay on lay.MARKET_ID=mm.Champ_Market_ID
WHERE   B.CHAMP_EVENT_STATUS=1  and B.Champ_Event_ID=@Match_Id
END
GO