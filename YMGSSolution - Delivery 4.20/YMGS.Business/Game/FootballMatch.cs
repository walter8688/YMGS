using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Data.DataBase;
using YMGS.Data.Common;

namespace YMGS.Business.Game
{
    public class FootballMatch : MatchManagerBase,IMatchObject
    {
        public IList<MatchObject> GetMatchList()
        {
            var eventItemList = (new CachedEventItem()).QueryCachedData<DSEventItem>();
            var eventZoneList = (new CachedEventZone()).QueryCachedData<DSEventZone>();
            var eventList = (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            var matchMarketList = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var marketList = matchMarketList.Match_Market;
            //获取MarketId,MarketTmpId键值对
            var footballDefaultMarketList = from m in marketList
                                     where m.BET_TYPE_ID == (int)BetTypeEnum.MatchOdds
                                           && m.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.FullTime
                                     group m by m.MATCH_ID into ml
                                     select new
                                     {
                                         matchId = ml.Key,
                                         MARKET_TMP_ID = ml.Max(g => g.MARKET_TMP_ID)
                                     };

            Dictionary<int, int> marketIdTmpIdDic = new Dictionary<int, int>();
            foreach (var item in footballDefaultMarketList)
            {
                marketIdTmpIdDic.Add(item.matchId, item.MARKET_TMP_ID);
            }
            //获取足球赛事列表
            var footballList = from m in matchMarketList.Match_List
                               join e in eventList._DSEventTeamList
                               on m.EVENT_ID equals e.EVENT_ID.ToString()
                               join z in eventZoneList.TB_EVENT_ZONE
                               on e.EVENTZONE_ID equals z.EVENTZONE_ID
                               join t in eventItemList.TB_EVENT_ITEM
                               on z.EVENTITEM_ID equals t.EventItem_ID
                               orderby m.STARTDATE 
                               select new
                               {
                                   MatchList = m,
                                   MatchEventZoneId = z.EVENTZONE_ID,
                                   MatchEventItemId = t.EventItem_ID
                               };

            var footballMatchs = footballList;
            IList<MatchObject> footballMatchList = new List<MatchObject>();
            MatchObject footballObject = null;
            foreach (var football in footballMatchs)
            {
                footballObject = new MatchObject();
                footballObject.MatchEventItemId = football.MatchEventItemId;
                footballObject.MatchEventZoneId = football.MatchEventZoneId;
                footballObject.MatchEventItemName = football.MatchList.EVENTITEM_NAME;
                footballObject.MatchEventItemNameEN = football.MatchList.EVENTITEM_NAME_EN;
                footballObject.MatchEventZoneName = football.MatchList.EVENTZONE_NAME;
                footballObject.MatchEventZoneNameEN = football.MatchList.EVENTZONE_NAME_EN;
                footballObject.MatchEventId = football.MatchList.EVENT_ID;
                footballObject.MatchEventName = football.MatchList.EVENT_NAME;
                footballObject.MatchEventNameEN = football.MatchList.EVENT_NAME_EN;
                footballObject.MatchId = football.MatchList.MATCH_ID;
                footballObject.MatchName = football.MatchList.MATCH_NAME;
                footballObject.MatchNameEN = football.MatchList.MATCH_NAME_EN;
                footballObject.MatchStartDate = football.MatchList.STARTDATE;
                footballObject.HomeTeamName = football.MatchList.EVENT_HOME_TEAM_NAME;
                footballObject.HomeTeamNameEN = football.MatchList.EVENT_HOME_TEAM_NAME_EN;
                footballObject.GuestTeamName = football.MatchList.EVENT_GUEST_TEAM_NAME;
                footballObject.GuestTeamNameEN = football.MatchList.EVENT_GUEST_TEAM_NAME_EN;
                footballObject.HomeTeamScore = GetHomeTeamScore(football.MatchList);
                footballObject.GuestTeamScore = GetGuestTeamScore(football.MatchList);
                footballObject.MatchLink = GetMatchLink(football.MatchEventItemId, football.MatchEventZoneId, football.MatchList);
                footballObject.CurrentMatchStatus = GetMatchStatus((MatchStatusEnum)football.MatchList.STATUS);
                footballObject.IsZouDi = football.MatchList.IsIS_ZOUDINull() ? false : football.MatchList.IS_ZOUDI;
                footballObject.IsMatchComingSoon = IsMatchComingSoon(football.MatchList.AUTO_FREEZE_DATE);
                footballObject.IsMatchInPlay = IsMatchInPlay(football.MatchList);
                footballObject.IsMatchHT = IsMatchHT(football.MatchList);
                footballObject.IsMatchStarted = IsMatchStarted(football.MatchList);
                footballObject.IsMatchClosed = IsMatchClosed((MatchStatusEnum)football.MatchList.STATUS);
                footballObject.IsMatchSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)football.MatchList.ADDITIONALSTATUS);
                footballObject.IsMatchFreezed = IsMatchFreezed((MatchAdditionalStatusEnum)football.MatchList.ADDITIONALSTATUS);
                footballObject.IsMatchFaved = 0;
                footballObject.MatchFavedCalss = "stars";
                footballObject.MatchLimitBetStatus = GetMatchLimitBetStatus(footballObject);
                footballObject.MatchStatusClass = GetMatchStatusClass(footballObject);
                footballObject.CurrentScore = football.MatchList.STATUS >= (int)MatchStatusEnum.InMatching ? string.Format("{0} - {1}", footballObject.HomeTeamScore, footballObject.GuestTeamScore) : "v";
                footballObject.MatchStartedMinutes = GetMatchStartedMinutes(football.MatchList);
                footballObject.MatchStartingMinutes = GetMatchStartingMinutes(football.MatchList);
                if (marketIdTmpIdDic.ContainsKey(football.MatchList.MATCH_ID))
                    footballObject.DefaultMarketTmpId = marketIdTmpIdDic[football.MatchList.MATCH_ID];
                footballObject.CustomParam_1 = GetCustomParam_1(footballObject);
                footballMatchList.Add(footballObject);
            }
            return footballMatchList.OrderBy(m => m.MatchStartDate).ToList();
        }

        public string GetCustomParam_1(MatchObject football)
        {
            if (!football.IsZouDi)
                return football.MatchStartDate.ToString("HH:mm");
            if (football.IsMatchStarted)
                return string.Format("{0}'", football.MatchStartedMinutes);
            if (football.IsMatchHT)
                return "HT";
            if (football.IsMatchInPlay)
                return "In Play";
            if (football.IsMatchFreezed)
                return "Starting soon";
            if (football.IsMatchComingSoon)
                return string.Format("Starting in {0}'", Math.Abs(football.MatchStartingMinutes));
            return string.Empty;
        }
    }
}
