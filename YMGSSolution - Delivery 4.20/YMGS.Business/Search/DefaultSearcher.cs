using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Data.DataBase;
using YMGS.Framework;

namespace YMGS.Business.Search
{
   public class DefaultSearcher
    {
       public static int LANGUAGE { get; set; }

        /// <summary>
        /// 赛事项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static DSEventItem GetEventItem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                //获得赛事项目
                return (new CachedEventItem()).QueryCachedData<DSEventItem>();
            }
            return null;
        }
        /// <summary>
        /// 赛事区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static object GetEventZone(string id)
        {
            var ds = (new CachedEventZone()).QueryCachedData<DSEventZone>();
            var data = (from s in ds.TB_EVENT_ZONE
                        where s.EVENTITEM_ID == int.Parse(id) orderby s.ZONE_NAME
                        select new { s.EVENTZONE_ID,EVENTZONE_NAME=LANGUAGE==1? s.EVENTZONE_NAME:s.EVENTZONE_NAME_EN }).Distinct();
            return data;
        }
        /// <summary>
        /// 赛事
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static object GetEvent(string id)
        {
            var ds = (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            var data = (from s in ds._DSEventTeamList
                        where s.EVENTZONE_ID == int.Parse(id)
                        select new { s.EVENT_ID, EVENT_NAME=LANGUAGE==1?s.EVENT_NAME:s.EVENT_NAME_EN }).Distinct();
            return data;
        }
        /// <summary>
        /// 赛事时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static object GetEventDate(string id)
        {
            var objTemp5 = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            //objTemp5.ChampEventList   冠军比赛
            //objTemp5.ChampEventMarket 冠军比赛市场

            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var data = (from s in ds.Match_List
                        where s.EVENT_ID == id
                        select new { STARTDATE = s.STARTDATE.ToString("yyyy-MM-dd"),ID = s.STARTDATE.ToString("yyyy-MM-dd"),MARK=1 }
                        ).Union(from t in objTemp5.ChampEventList
                                where t.Event_ID == int.Parse(id)
                                select new { STARTDATE = LANGUAGE==1?t.Champ_Event_Name:t.CHAMP_EVENT_NAME_EN, ID = t.Champ_Event_ID.ToString(), MARK = 2 }
                        ).Distinct();
            return data.OrderBy(i=>i.STARTDATE);
        }
       /// <summary>
       /// 娱乐冠军比赛
       /// </summary>
       /// <returns></returns>
       public static object GetChampionMatch()
       {
           var Champds = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
           //Champds.ChampEventList   冠军比赛
           //Champds.ChampEventMarket 冠军比赛市场
           var data = (from s in Champds.ChampEventList
                       where s.Champ_Event_Type == 2 && s.Champ_Event_Status==1
                       select new {s.Champ_Event_ID,Champ_Event_Name=LANGUAGE==1?s.Champ_Event_Name:s.CHAMP_EVENT_NAME_EN });
           return data;
       }
       

        /// <summary>
        /// 获取比赛
        /// </summary>
        /// <param name="id"></param>
        /// <param name="StartDate"></param>
        /// <returns></returns>
       public static object GetMATCH(string id, string StartDate)
        {
           // var objTemp5 = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            //objTemp5.ChampEventList   冠军比赛
           //.Union(
           //             from t in objTemp5.ChampEventMarket
           //             where t.CHAMP_EVENT_ID.ToString() == StartDate
           //             select new { MATCH_ID = t.CHAMP_MARKET_ID, MATCH_NAME = t.CHAMP_EVENT_MEMBER_NAME }
           //             )
            //objTemp5.ChampEventMarket 冠军比赛市场
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var data = (from s in ds.Match_List
                        where s.EVENT_ID == id && s.STARTDATE.ToString("yyyy-MM-dd") == StartDate
                        select new { MATCH_ID = s.MATCH_ID, MATCH_NAME=LANGUAGE==1?s.MATCH_NAME:s.MATCH_NAME_EN }).Distinct();
            return data;
        }
        //into recmatch
        //               from i in recmatch
       public static object GetRecommandMatch(string eventid,string startDate)
       {
           var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
           var eventds=(new CachedEvent()).QueryCachedData<DSEventTeamList>();
           var data = from s in (ds.Match_List.Where(s =>((s.IsEVENTTYPE_NAMENull()?"":s.EVENTTYPE_NAME) == "体育类") && ( s.RECOMMENDMATCH == true)))
                       join e in eventds._DSEventTeamList
                       on s.EVENT_ID equals e.EVENT_ID.ToString()
                      where UtilityHelper.DateToDateAndTimeStr(s.STARTDATE) == startDate && s.EVENT_ID == eventid
                      orderby s.STARTDATE
                       select new {
                         //  param=string.Format("?Ent=0&item={0}&zone={1}&eventid={2}&itemdate={3}&matchid={4}",i.EventItem_ID.ToString(),i.EVENTZONE_ID.ToString(),i.EVENT_ID.ToString(),s.STARTDATE.ToString("yyyy-MM-dd"),s.MATCH_ID.ToString()),
                           param = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}", e.EventItem_ID.ToString(), e.EVENTZONE_ID.ToString(), e.EVENT_ID.ToString(), s.STARTDATE.ToString("yyyy-MM-dd"), s.MATCH_ID.ToString()),
                           s.EVENT_ID,
                           EVENT_NAME=LANGUAGE==1?s.EVENT_NAME:s.EVENT_NAME_EN,
                           STARTDATE = UtilityHelper.DateToDateAndTimeStr(s.STARTDATE),
                           s.MATCH_ID,
                           MATCH_NAME=LANGUAGE==1?s.MATCH_NAME:s.MATCH_NAME_EN,
                           RaceTime=s.STARTDATE.ToString("HH:mm")};
           if (data != null && data.Count() > 0)
               return data.Distinct();
           return data;
       }
       public static object GetRecommandMatchHeader()
       {
           var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
           var data = (from s in (ds.Match_List.Where(s => ((s.IsEVENTTYPE_NAMENull()?"":s.EVENTTYPE_NAME) == "体育类") && (s.RECOMMENDMATCH == true)))
                       select new {s.EVENT_ID,
                           EVENT_NAME=LANGUAGE==1?s.EVENT_NAME:s.EVENT_NAME_EN,
                                   StartDate =  UtilityHelper.DateToDateAndTimeStr(s.STARTDATE)
                                   
                       }).Distinct().OrderBy(i => i.StartDate);
           return data;
       }


        /// <summary>
        /// 获取比赛市场
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static object GetMATCHMarket(string id)
        {
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var data = ((from s in ds.Match_Market
                         where (s.MATCH_ID == int.Parse(id) && !(new int?[] { 2, 4 }).Contains(s.BET_TYPE_ID))
                         select new { s.BET_TYPE_ID, s.MARKET_TMP_TYPE,MARKET_TMP_NAME=LANGUAGE==1?s.MARKET_TMP_NAME:s.MARKET_TMP_NAME_EN }
                       ).Union(
                        from s in ds.Match_Market
                        where s.MATCH_ID == int.Parse(id) && s.BET_TYPE_ID == 2
                        select new { s.BET_TYPE_ID, s.MARKET_TMP_TYPE, MARKET_TMP_NAME = s.MARKET_TMP_TYPE == 0 ? (LANGUAGE == 1 ? "半场波胆" : "Half Time Score") : (LANGUAGE == 1 ? "全场波胆" : "Correct Score") }
                        ).Union(
                        from s in ds.Match_Market
                        where s.MATCH_ID == int.Parse(id) && s.BET_TYPE_ID == 4
                        select new { s.BET_TYPE_ID, s.MARKET_TMP_TYPE, MARKET_TMP_NAME = s.MARKET_TMP_TYPE == 0 ? (LANGUAGE == 1 ? "半场让分盘" : "Half Asian Handicap") : (LANGUAGE == 1 ? "全场让分盘" : "Asian Handicap") }
                        )
                        ).Distinct();
            return data;
        }
    }
}
