IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_match_market_by_matchid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_match_market_by_matchid]
GO
CREATE PROCEDURE [dbo].[pr_get_match_market_by_matchid]
    (
      @Match_Id int
    )
AS 
BEGIN
	SELECT mt.BET_TYPE_ID,mt.Market_Tmp_Type,mm.MARKET_ID,mm.MARKET_NAME,isnull(mm.MARKET_NAME_EN,mm.MARKET_NAME) MARKET_NAME_EN ,mm.MATCH_ID,mm.MARKET_TMP_ID,mm.MARKET_FLAG,mm.SCOREA,mm.SCOREB,
case when mt.Market_Tmp_Type=0 and mt.BET_TYPE_ID=2 then 'Half Time Score'
     when mt.Market_Tmp_Type=1 and mt.BET_TYPE_ID=2 then 'Correct Score'
     when mt.Market_Tmp_Type=0 and mt.BET_TYPE_ID=4 then 'Half Asian Handicap'
      when mt.Market_Tmp_Type=1 and mt.BET_TYPE_ID=4 then 'Asian Handicap'  else mt.MARKET_TMP_NAME_EN end as MARKET_TMP_NAME_EN,
	   case when mt.Market_Tmp_Type=0 and mt.BET_TYPE_ID=2 then '半场波胆'
     when mt.Market_Tmp_Type=1 and mt.BET_TYPE_ID=2 then '全场波胆'
     when mt.Market_Tmp_Type=0 and mt.BET_TYPE_ID=4 then '半场让分盘'
      when mt.Market_Tmp_Type=1 and mt.BET_TYPE_ID=4 then '全场让分盘'  else mt.MARKET_TMP_NAME end as MARKET_TMP_NAME,
           mat.MATCH_NAME,mat.MATCH_NAME_EN,mat.STARTDATE STARTDATE,isnull(back.MATCH_AMOUNTS,0) backMATCH_AMOUNTS,
           isnull(back.ODDS,0) backodds,isnull(lay.MATCH_AMOUNTS,0) layMATCH_AMOUNTS,isnull(lay.ODDS,0) layodds
 from TB_Match_Market mm
left join  TB_Market_Template mt on mt.MARKET_TMP_ID=mm.MARKET_TMP_ID
left join TB_MATCH mat on mat.MATCH_ID=mm.MATCH_ID
left join 
(select back.MARKET_ID, back.ODDS,sum(back.MATCH_AMOUNTS) MATCH_AMOUNTS from TB_EXCHANGE_BACK back
 where exists
       (select MARKET_ID from (
                               select MARKET_ID,min(ODDS) ODDS FROM TB_EXCHANGE_BACK 
							   where STATUS=1  and MATCH_TYPE=1
                               group by MARKET_ID
							   ) eb where eb.MARKET_ID=back.MARKET_ID and eb.ODDS=back.ODDS
	   ) and MATCH_TYPE=1
       group by back.MARKET_ID,back.ODDS
) back on back.MARKET_ID=mm.MARKET_ID
left join 
(select lay.MARKET_ID, lay.ODDS,sum(lay.MATCH_AMOUNTS) MATCH_AMOUNTS from TB_EXCHANGE_LAY lay
 where exists
       (select MARKET_ID from (
                            select MARKET_ID,max(ODDS) ODDS FROM TB_EXCHANGE_LAY 
							where STATUS=1  and MATCH_TYPE=1
                            group by MARKET_ID
		                       ) la where la.MARKET_ID=lay.MARKET_ID and la.ODDS=lay.ODDS
        ) and MATCH_TYPE=1
        group by lay.MARKET_ID,lay.ODDS
) lay on lay.MARKET_ID=mm.MARKET_ID

WHERE  mm.MATCH_ID=@Match_Id
and  mm.MARKET_ID IN(select market_id from dbo.udf_get_canuse_market_for_betting())
END
GO