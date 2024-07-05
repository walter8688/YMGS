using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.Cache;
using YMGS.Data.DataBase;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.Business.Navigator
{
    public class NavigatorManager
    {
        public LanguageEnum Language { get; set; }
        private readonly string scriptString = "javascript:void(0)";
        private NavigatorObject navigator;

        public Dictionary<int, int> EventToEventZoneDic
        {
            get
            {
                Dictionary<int, int> EventToEventZoneDic = new Dictionary<int, int>();
                var events = new CachedEvent().QueryCachedData<DSEventTeamList>()._DSEventTeamList.Distinct().OrderBy(e => e.EVENT_ID);
                foreach (var item in events)
                {
                    EventToEventZoneDic.Add(item.EVENT_ID, item.EVENTZONE_ID);
                }
                return EventToEventZoneDic;
            }
        }

        public Dictionary<int, int> EventZoneToEventItemDic
        {
            get
            {
                Dictionary<int, int> EventZoneToEventItemDic = new Dictionary<int, int>();
                var eventZones = new CachedEventZone().QueryCachedData<DSEventZone>().TB_EVENT_ZONE.Distinct().OrderBy(e => e.EVENTZONE_ID);
                foreach (var item in eventZones)
                {
                    EventZoneToEventItemDic.Add(item.EVENTZONE_ID, item.EVENTITEM_ID);
                }
                return EventZoneToEventItemDic;
            }
        }

        private IList<NavigatorSearchObject> searchConditonList = new List<NavigatorSearchObject>();

        /// <summary>
        /// 获取所有的Navigators
        /// </summary>
        /// <param name="navSearchConditionList"></param>
        /// <returns></returns>
        public IList<NavigatorObject> GetLeftNavigators(IList<NavigatorSearchObject> navSearchConditionList,LanguageEnum Language)
        {
            if (navSearchConditionList == null)
                return null;
            this.Language = Language;
            IList<NavigatorObject> leftNavigatorList = new List<NavigatorObject>();
            IList<NavigatorObject> tempLeftNavigatorList = new List<NavigatorObject>();
            IEnumerable<NavigatorObject> needRemovedLeftNavigatorList = new List<NavigatorObject>();
            foreach (var navSeachObj in navSearchConditionList)
            {
                var navigatorTypeId = navSeachObj.NavigatorTypeId;
                var navigatorId = navSeachObj.NavigatorId;
                if (tempLeftNavigatorList != null)
                {
                    if (navigatorTypeId == (int)LeftNavigatorTypeEnum.MatchStartDate)
                    {
                        needRemovedLeftNavigatorList = from t in tempLeftNavigatorList
                                                       where (t.NavigatorTypeId == navSeachObj.NavigatorTypeId && t.NavigatorId != navSeachObj.NavigatorId) || t.NavigatorTypeId == (int)LeftNavigatorTypeEnum.ChampionMatch
                                                       select t;
                    }
                    else if (navigatorTypeId == (int)LeftNavigatorTypeEnum.ChampionMatch)
                    {
                        needRemovedLeftNavigatorList = from t in tempLeftNavigatorList
                                                       where (t.NavigatorTypeId == navSeachObj.NavigatorTypeId && t.NavigatorId != navSeachObj.NavigatorId) || t.NavigatorTypeId == (int)LeftNavigatorTypeEnum.MatchStartDate
                                                       select t;
                    }
                    else
                    {
                        needRemovedLeftNavigatorList = from t in tempLeftNavigatorList where t.NavigatorTypeId == navSeachObj.NavigatorTypeId && t.NavigatorId != navSeachObj.NavigatorId select t;
                    }
                }
                foreach (var item in needRemovedLeftNavigatorList)
                    leftNavigatorList.Remove(item);
                IList<NavigatorObject> navigatorTempList = GetLeftNavigatorsByParam(navigatorTypeId, navigatorId);
                tempLeftNavigatorList = navigatorTempList;
                foreach (var nav in navigatorTempList)
                {
                    leftNavigatorList.Add(nav);
                }
            }
            return leftNavigatorList;
        }

        /// <summary>
        /// 根据不同的类别来构造Navigator
        /// </summary>
        /// <param name="navigatorTypeId"></param>
        /// <param name="navigatorId"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParam(int navigatorTypeId, string navigatorId)
        {
            switch ((LeftNavigatorTypeEnum)navigatorTypeId)
            {
                case LeftNavigatorTypeEnum.Sports:
                    return GetLeftNavigatorsByParamSports();
                case LeftNavigatorTypeEnum.EventItem:
                    return GetLeftNavigatorsByParamEventItem(navigatorId);
                case LeftNavigatorTypeEnum.EventZone:
                    return GetLeftNavigatorsByParamEventZone(navigatorId);
                case LeftNavigatorTypeEnum.Event:
                    return GetLeftNavigatorsByParamEvent(navigatorId);
                case LeftNavigatorTypeEnum.MatchStartDate:
                    return GetLeftNavigatorsByParamMatchStartDateAndEventId(navigatorId);
                case LeftNavigatorTypeEnum.ChampionMatch:
                    return null;
                case LeftNavigatorTypeEnum.Match:
                    return GetLeftNavigatorsByParamMatchId(navigatorId);
                case LeftNavigatorTypeEnum.BetType:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 根据体育类别加载赛事类别
        /// </summary>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamSports()
        {
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Sports, NavigatorId = "0" });
            var navigatorList = new List<NavigatorObject>();
            var eventItems = (new CachedEventItem()).QueryCachedData<DSEventItem>();
            foreach (var item in eventItems.TB_EVENT_ITEM)
            {
                var tempSearchConditionList = new List<NavigatorSearchObject>();
                tempSearchConditionList.AddRange(searchConditonList);
                tempSearchConditionList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventItem, NavigatorId = item.EventItem_ID.ToString() });
                navigator = new NavigatorObject
                {
                    NavigatorName = Language == LanguageEnum.Chinese ? item.EventItem_Name : item.EventItem_Name_En,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventItem,
                    NavigatorId = item.EventItem_ID.ToString(),
                    NavigatorLinkAddress = scriptString,
                    SearchCondition = tempSearchConditionList
                };
                navigatorList.Add(navigator);
            }
            return navigatorList;
        }

        /// <summary>
        /// 根据赛事类别加载赛事区域
        /// </summary>
        /// <param name="eventItemId"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamEventItem(string eventItemId)
        {
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventItem, NavigatorId = eventItemId });
            var editKey = Convert.ToInt32(eventItemId);
            var navigatorList = new List<NavigatorObject>();
            var eventZones = (new CachedEventZone()).QueryCachedData<DSEventZone>().TB_EVENT_ZONE.Where(e => e.EVENTITEM_ID == editKey);
            foreach (var item in eventZones)
            {
                var tempSearchConditionList = new List<NavigatorSearchObject>();
                tempSearchConditionList.AddRange(searchConditonList);
                tempSearchConditionList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventZone, NavigatorId = item.EVENTZONE_ID.ToString() });
                navigator = new NavigatorObject
                {
                    NavigatorName = Language == LanguageEnum.Chinese ? item.EVENTZONE_NAME : item.EVENTZONE_NAME_EN,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventZone,
                    NavigatorId = item.EVENTZONE_ID.ToString(),
                    NavigatorLinkAddress = scriptString,
                    SearchCondition = tempSearchConditionList
                };
                navigatorList.Add(navigator);
            }
            return navigatorList;
        }

        /// <summary>
        /// 根据赛事区域加载赛事
        /// </summary>
        /// <param name="eventZoneId"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamEventZone(string eventZoneId)
        {
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventZone, NavigatorId = eventZoneId });
            var editKey = Convert.ToInt32(eventZoneId);
            var navigatorList = new List<NavigatorObject>();
            var events = (new CachedEvent()).QueryCachedData<DSEventTeamList>()._DSEventTeamList.Where(e => e.EVENTZONE_ID == editKey);
            foreach (var item in events)
            {
                var tempSearchConditionList = new List<NavigatorSearchObject>();
                tempSearchConditionList.AddRange(searchConditonList);
                tempSearchConditionList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Event, NavigatorId = item.EVENT_ID.ToString() });
                navigator = new NavigatorObject
                {
                    NavigatorName = Language == LanguageEnum.Chinese ? item.EVENT_NAME : item.EVENT_NAME_EN,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.Event,
                    NavigatorId = item.EVENT_ID.ToString(),
                    NavigatorLinkAddress = scriptString,
                    SearchCondition = tempSearchConditionList
                };
                navigatorList.Add(navigator);
            }
            return navigatorList;
        }

        /// <summary>
        /// 根据赛事加载冠军&日期
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamEvent(string eventId)
        {
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Event, NavigatorId = eventId });
            var editKey = Convert.ToInt32(eventId);
            var navigatorList = new List<NavigatorObject>();
            var champions = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>().ChampEventList.Where(m => m.Event_ID == editKey).OrderBy(m => m.Champ_Event_StartDate);
            var matchs = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>().Match_List.Where(m => m.EVENT_ID == eventId.ToString()).OrderBy(m => m.STARTDATE);

            //加载比赛日期
            var matchsByDate = from m in matchs
                               group m by new
                               {
                                   MatchStartDate = UtilityHelper.DateToStr(m.STARTDATE)
                               }
                                   into g
                                   select new
                                   {
                                       MatchStartDate = g.Key.MatchStartDate
                                   };
            foreach (var item in matchsByDate)
            {
                var tempSearchConditionList = new List<NavigatorSearchObject>();
                tempSearchConditionList.AddRange(searchConditonList);
                tempSearchConditionList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.MatchStartDate, NavigatorId = string.Format("{0}@{1}", eventId, item.MatchStartDate) });
                navigator = new NavigatorObject
                {
                    NavigatorName = item.MatchStartDate,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.MatchStartDate,
                    NavigatorId = string.Format("{0}@{1}", eventId, item.MatchStartDate),
                    NavigatorLinkAddress = scriptString,
                    SearchCondition = tempSearchConditionList
                };
                navigatorList.Add(navigator);
            }
            //加载冠军
            foreach (var item in champions)
            {
                navigator = new NavigatorObject
                {
                    NavigatorName = Language == LanguageEnum.Chinese ? item.Champ_Event_Name : item.CHAMP_EVENT_NAME_EN,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.ChampionMatch,
                    NavigatorId = item.Champ_Event_ID.ToString(),
                    NavigatorLinkAddress = GetNavigatorChampionLinkAddress(editKey, item.Champ_Event_ID),
                    SearchCondition = searchConditonList
                };
                navigatorList.Add(navigator);
            }
            return navigatorList;
        }

        /// <summary>
        /// 根据日期加载比赛
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamMatchStartDateAndEventId(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new List<NavigatorObject>();
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.MatchStartDate, NavigatorId = key });
            var eventId = Convert.ToInt32(key.Split('@')[0]);
            var matchStartDate = key.Split('@')[1];
            var navigatorList = new List<NavigatorObject>();
            var matchs = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>().Match_List.Where(m => m.EVENT_ID == eventId.ToString() && UtilityHelper.DateToStr(m.STARTDATE) == matchStartDate).OrderBy(m => m.STARTDATE);
            foreach (var item in matchs)
            {
                var tempSearchConditionList = new List<NavigatorSearchObject>();
                tempSearchConditionList.AddRange(searchConditonList);
                tempSearchConditionList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Match, NavigatorId = item.MATCH_ID.ToString() });
                navigator = new NavigatorObject
                {
                    NavigatorName = Language == LanguageEnum.Chinese ? item.MATCH_NAME : item.MATCH_NAME_EN,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.Match,
                    NavigatorId = item.MATCH_ID.ToString(),
                    NavigatorLinkAddress = GetNavigatorMatchLinkAddress(eventId, item.MATCH_ID, matchStartDate),
                    SearchCondition = tempSearchConditionList
                };
                navigatorList.Add(navigator);
            }

            return navigatorList;
        }

        /// <summary>
        /// 根据比赛加载玩法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private IList<NavigatorObject> GetLeftNavigatorsByParamMatchId(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new List<NavigatorObject>();
            searchConditonList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Match, NavigatorId = key });

            string matchStartDate = string.Empty;
            bool isZouDi = false;
            var matchId = int.Parse(key);
            var navigatorList = new List<NavigatorObject>();
            var temp = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var matchs = temp.Match_List.Where(m => m.MATCH_ID == matchId);
            foreach (var item in matchs)
            {
                matchStartDate = UtilityHelper.DateToStr(item.STARTDATE);
                isZouDi = item.IS_ZOUDI;
            }

            var matchMarkets = temp.Match_Market.Where(m => m.MATCH_ID == matchId);
            var markets = (from s in matchMarkets
                            where (!(new int?[] { 2, 4 }).Contains(s.BET_TYPE_ID))
                           select new { s.EVENT_ID, s.BET_TYPE_ID, s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, STATUS = s.STATUS }
                       ).Distinct();

            foreach (var item in markets)
            {
                navigator = new NavigatorObject
                {
                    NavigatorName = item.MARKET_TMP_NAME,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.BetType,
                    NavigatorId = string.Format("{0}@{1}", matchId, item.MARKET_TMP_ID),
                    NavigatorLinkAddress = GetNavigatorMarketLinkAddressForTmpId(item.EVENT_ID, matchId, matchStartDate, item.MARKET_TMP_ID),
                    isZouDi = (isZouDi && item.STATUS == (int)MatchStatusEnum.InMatching || item.STATUS == (int)MatchStatusEnum.HalfTimeFinished || item.STATUS == (int)MatchStatusEnum.SecHalfStarted) ? true : false,
                    SearchCondition = searchConditonList
                };
                navigatorList.Add(navigator);
            }
            var marketList = ((from s in matchMarkets
                               where s.BET_TYPE_ID == 2
                               select new { s.EVENT_ID, s.BET_TYPE_ID, s.MARKET_TMP_TYPE, MARKET_TMP_NAME = s.MARKET_TMP_TYPE == 0 ? (Language == LanguageEnum.Chinese ? "半场波胆" : "Half Time Score") : (Language == LanguageEnum.Chinese ? "全场波胆" : "Correct Score"), STATUS = s.STATUS }
                        ).Union(
                        from s in matchMarkets
                        where s.BET_TYPE_ID == 4
                        select new { s.EVENT_ID, s.BET_TYPE_ID, s.MARKET_TMP_TYPE, MARKET_TMP_NAME = s.MARKET_TMP_TYPE == 0 ? (Language == LanguageEnum.Chinese ? "半场让分盘" : "Half Asian Handicap") : (Language == LanguageEnum.Chinese ? "全场让分盘" : "Asian Handicap"), STATUS = s.STATUS }
                        )).Distinct();
            foreach (var item in marketList)
            {
                navigator = new NavigatorObject
                {
                    NavigatorName = item.MARKET_TMP_NAME,
                    NavigatorTypeId = (int)LeftNavigatorTypeEnum.BetType,
                    NavigatorId = string.Format("{0}@{1}@{2}", matchId, item.BET_TYPE_ID, item.MARKET_TMP_TYPE),
                    NavigatorLinkAddress = GetNavigatorMarketLinkAddressForTmpType(item.EVENT_ID, matchId, matchStartDate, item.BET_TYPE_ID, item.MARKET_TMP_TYPE),
                    isZouDi = (isZouDi && item.STATUS == (int)MatchStatusEnum.InMatching || item.STATUS == (int)MatchStatusEnum.HalfTimeFinished || item.STATUS == (int)MatchStatusEnum.SecHalfStarted) ? true : false,
                    SearchCondition = searchConditonList
                };
                navigatorList.Add(navigator);
            }

            return navigatorList;
        }

        /// <summary>
        /// 获取比赛链接地址
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="matchId"></param>
        /// <param name="matchStartDate"></param>
        /// <returns></returns>
        private string GetNavigatorMatchLinkAddress(int eventId, int matchId, string matchStartDate)
        {
            try
            {
                var eventZoneId = EventToEventZoneDic[eventId];
                var eventItemId = EventZoneToEventItemDic[eventZoneId];
                return string.Format("Default.aspx?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}", eventItemId, eventZoneId, eventId, matchStartDate, matchId);
            }
            catch
            {
                return scriptString;
            }
        }

        /// <summary>
        /// 获取玩法链接地址
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="matchId"></param>
        /// <param name="matchStartDate"></param>
        /// <param name="marketTmpType"></param>
        /// <returns></returns>
        private string GetNavigatorMarketLinkAddressForTmpId(int eventId, int matchId, string matchStartDate, int marketTmpId)
        {
            try
            {
                var eventZoneId = EventToEventZoneDic[eventId];
                var eventItemId = EventZoneToEventItemDic[eventZoneId];
                return string.Format("Default.aspx?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}&MarketTmpId={5}", eventItemId, eventZoneId, eventId, matchStartDate, matchId, marketTmpId);
            }
            catch
            {
                return scriptString;
            }
        }

        /// <summary>
        /// 获取玩法链接地址
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="matchId"></param>
        /// <param name="matchStartDate"></param>
        /// <param name="marketTmpType"></param>
        /// <returns></returns>
        private string GetNavigatorMarketLinkAddressForTmpType(int eventId, int matchId, string matchStartDate,int betTypeId, int marketTmpType)
        {
            try
            {
                var eventZoneId = EventToEventZoneDic[eventId];
                var eventItemId = EventZoneToEventItemDic[eventZoneId];
                return string.Format("Default.aspx?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}&BetTypeId={5}&MarketTmpType={6}", eventItemId, eventZoneId, eventId, matchStartDate, matchId, betTypeId, marketTmpType);
            }
            catch
            {
                return scriptString;
            }
        }

        /// <summary>
        /// 获取冠军比赛链接地址
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="championEventId"></param>
        /// <returns></returns>
        private string GetNavigatorChampionLinkAddress(int eventId, int championEventId)
        {
            try
            {
                var eventZoneId = EventToEventZoneDic[eventId];
                var eventItemId = EventZoneToEventItemDic[eventZoneId];
                return string.Format("Default.aspx?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&ChampionEventId={3}", eventItemId, eventZoneId, eventId, championEventId);
            }
            catch
            {
                return scriptString;
            }
        }
    }
}
