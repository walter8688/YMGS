using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Business.Cache;
using YMGS.Trade.Web.Football.Common;
using YMGS.Trade.Web.Football.Model;


namespace YMGS.Trade.Web.Football.BusinessLogic
{
    public class InPlayFootballGameListBL : FootballBLBase
    {
        public LanguageEnum Language { get; set; }

        public InPlayFootballGameListBL() { }

        public InPlayFootballGameListBL(LanguageEnum language)
        {
            this.Language = language;
        }

        public IEnumerable<FootballMatch> GetFootballMatchList(int? curUserId)
        {
            var events = (new CachedEvent()).QueryCachedData<DSEventTeamList>()._DSEventTeamList;
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            if (dsMatchMarkets == null || dsMatchMarkets.Match_List.Count == 0)
                return null;
            var matchs = dsMatchMarkets.Match_List.Where(m => m.IS_ZOUDI == true).OrderBy(m => m.STARTDATE);
            var markets = dsMatchMarkets.Match_Market;
            var betBacks = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCachedExchangeBack;
            var betLays = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCachedExchangeLay;
            var yourInPlay = (new CachedYourInPlay()).QueryCachedData<DSYourInPlay>().TBYourInPlay.Where(m => m.USER_ID == curUserId);
            var defaultMarketTmpId = markets.OrderBy(m => m.MARKET_TMP_ID).FirstOrDefault().MARKET_TMP_ID;

            #region GetfbMatchList
            var matchListTemp = from m in matchs
                                join e in events
                                on m.EVENT_ID equals e.EVENT_ID.ToString()
                                where m.EVENT_ID == e.EVENT_ID.ToString()
                                orderby m.STARTDATE
                                select new
                                {
                                    MatchList = m,
                                    EventZoneId = e.EVENTZONE_ID,
                                    EventItemId = e.EventItem_ID
                                };

            var matchList = from m in matchListTemp
                            join y in yourInPlay
                            on m.MatchList.MATCH_ID equals y.MATCH_ID
                            into temp
                            from tt in temp.DefaultIfEmpty()
                            select new
                            {
                                MatchList = m.MatchList,
                                EventZoneId = m.EventZoneId,
                                EventItemId = m.EventItemId,
                                IsMatchFaved = tt == null ? 0 : tt.IS_FAV
                            };

            IList<FootballMatch> fbMatchList = new List<FootballMatch>();
            FootballMatch fbMatch = null;
            foreach (var item in matchList)
            {
                fbMatch = new FootballMatch();
                fbMatch.MatchEventItemId = item.EventItemId;
                fbMatch.MatchEventZoneId = item.EventZoneId;
                fbMatch.MatchEventId = item.MatchList.EVENT_ID;
                fbMatch.MatchId = item.MatchList.MATCH_ID;
                fbMatch.MatchName = Language == LanguageEnum.Chinese ? item.MatchList.MATCH_NAME : item.MatchList.MATCH_NAME_EN;
                fbMatch.MatchStartDate = item.MatchList.STARTDATE;
                fbMatch.HomeTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_HOME_TEAM_NAME : item.MatchList.EVENT_HOME_TEAM_NAME_EN;
                fbMatch.GuestTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_GUEST_TEAM_NAME : item.MatchList.EVENT_GUEST_TEAM_NAME_EN;
                fbMatch.HomeTeamScore = GetHomeTeamScore(item.MatchList);
                fbMatch.GuestTeamScore = GetGuestTeamScore(item.MatchList);
                fbMatch.MatchLink = GetMatchLink(item.EventItemId, item.EventZoneId, item.MatchList);
                fbMatch.CurrentMatchStatus = GetMatchStatus((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsZouDi = item.MatchList.IsIS_ZOUDINull() ? false : item.MatchList.IS_ZOUDI;
                fbMatch.IsMatchComingSoon = IsMatchComingSoon(item.MatchList.STARTDATE);
                fbMatch.IsMatchInPlay = IsMatchInPlay(item.MatchList);
                fbMatch.IsMatchHT = IsMatchHT(item.MatchList);
                fbMatch.IsMatchStarted = IsMatchStarted(item.MatchList);
                fbMatch.IsMatchClosed = IsMatchClosed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchFreezed = IsMatchFreezed((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchStartingFreezed = IsMatchStartingFreezed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchFaved = item.IsMatchFaved;
                fbMatch.MatchFavedCalss = item.IsMatchFaved == 1 ? "starchoosed" : "stars";
                fbMatch.MatchLimitBetStatus = GetMatchLimitBetStatus(fbMatch);
                fbMatch.MatchStatusClass = GetMatchStatusClass(fbMatch);
                fbMatch.CurrentScore = item.MatchList.STATUS >= (int)MatchStatusEnum.InMatching ? string.Format("{0} - {1}", fbMatch.HomeTeamScore, fbMatch.GuestTeamScore) : "v";
                fbMatch.MatchStartedMinutes = GetMatchStartedMinutes(item.MatchList);
                fbMatch.MatchStartingMinutes = GetMatchStartingMinutes(item.MatchList);
                fbMatch.FootballMatchMarkets = GetMatchMarkets(markets, betBacks, betLays, item.MatchList.MATCH_ID, defaultMarketTmpId);
                fbMatch.CustomParam_1 = GetCustomParam_1(fbMatch);
                fbMatchList.Add(fbMatch);
            }
            #endregion
            return fbMatchList;
        }

        public IList<FootballObject> GetFootballGameList(int? curUserId)
        {
            //var eventItems = (new CachedEventItem()).QueryCachedData<DSEventItem>().TB_EVENT_ITEM;
            //var eventZones= (new CachedEventZone()).QueryCachedData<DSEventZone>().TB_EVENT_ZONE;
            var events = (new CachedEvent()).QueryCachedData<DSEventTeamList>()._DSEventTeamList;
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            if (dsMatchMarkets == null || dsMatchMarkets.Match_List.Count == 0)
                return null;
            var matchs = dsMatchMarkets.Match_List.Where(m => m.IS_ZOUDI == true);
            var markets = dsMatchMarkets.Match_Market;
            var betBacks = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCachedExchangeBack;
            var betLays = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCachedExchangeLay;
            var yourInPlay = (new CachedYourInPlay()).QueryCachedData<DSYourInPlay>().TBYourInPlay.Where(m => m.USER_ID == curUserId);

            var defaultMarketTmpId = markets.OrderBy(m => m.MARKET_TMP_ID).FirstOrDefault().MARKET_TMP_ID;

            #region GetEventList
            var eventList = from ev in markets
                            join m in matchs
                            on ev.MATCH_ID equals m.MATCH_ID
                            orderby m.STARTDATE
                            group m by new
                            {
                                m.STARTDATE,
                                m.EVENT_ID
                            }
                                into g
                                select new
                                {
                                    EventId = g.Key.EVENT_ID,
                                    MatchStartDate = g.Key.STARTDATE,
                                    EventName = g.Max(p => p.EVENT_NAME),
                                    EventNameEN = g.Max(p => p.EVENT_NAME_EN)
                                };
            #endregion

            #region GetFootballObjectList
            //FootballObject 
            IList<FootballObject> fbObjList = new List<FootballObject>();
            FootballObject fbObj = null;
            foreach (var item in eventList)
            {
                fbObj = new FootballObject();
                fbObj.EventId = Convert.ToInt32(item.EventId);
                fbObj.EventName = Language == LanguageEnum.Chinese ? item.EventName : item.EventNameEN;
                fbObj.EventStartDate = item.MatchStartDate;
                //fbObj.MarketFlagList = FootballCommonFunction.GenerateMarketFlagList();
                fbObj.FootballMatchs = GetMatchs(events, matchs, item.EventId, item.MatchStartDate, markets, betBacks, betLays, defaultMarketTmpId, yourInPlay);
                fbObjList.Add(fbObj);
            }
            #endregion

            return fbObjList;
        }

        private IList<FootballMatch> GetMatchs(DSEventTeamList.DSEventTeamListDataTable events, IEnumerable<DSMatchAndMarket.Match_ListRow> matchs, string eventId,
            DateTime matchStartDate, DSMatchAndMarket.Match_MarketDataTable markets, DSCachedExchangeBack.TBCachedExchangeBackDataTable betBacks,
            DSCachedExchangeLay.TBCachedExchangeLayDataTable betLays, int defaultMarketTmpId, IEnumerable<DSYourInPlay.TBYourInPlayRow> yourInPlay)
        {
            var matchListTemp = from m in matchs
                                join e in events
                                on m.EVENT_ID equals e.EVENT_ID.ToString()
                                where m.EVENT_ID == eventId && m.STARTDATE == matchStartDate
                                orderby m.STARTDATE
                                select new
                                {
                                    MatchList = m,
                                    EventZoneId = e.EVENTZONE_ID,
                                    EventItemId = e.EventItem_ID
                                };

            #region GetfbMatchList
            var matchList = from m in matchListTemp
                            join y in yourInPlay
                            on m.MatchList.MATCH_ID equals y.MATCH_ID
                            into temp
                            from tt in temp.DefaultIfEmpty()
                            select new
                            {
                                MatchList = m.MatchList,
                                EventZoneId = m.EventZoneId,
                                EventItemId = m.EventItemId,
                                IsMatchFaved = tt == null ? 0 : tt.IS_FAV
                            };

            IList<FootballMatch> fbMatchList = new List<FootballMatch>();
            FootballMatch fbMatch = null;
            foreach (var item in matchList)
            {
                fbMatch = new FootballMatch();
                fbMatch.MatchEventItemId = item.EventItemId;
                fbMatch.MatchEventZoneId = item.EventZoneId;
                fbMatch.MatchEventId = item.MatchList.EVENT_ID;
                fbMatch.MatchId = item.MatchList.MATCH_ID;
                fbMatch.MatchName = Language == LanguageEnum.Chinese ? item.MatchList.MATCH_NAME : item.MatchList.MATCH_NAME_EN;
                fbMatch.MatchStartDate = item.MatchList.STARTDATE;
                fbMatch.HomeTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_HOME_TEAM_NAME : item.MatchList.EVENT_HOME_TEAM_NAME_EN;
                fbMatch.GuestTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_GUEST_TEAM_NAME : item.MatchList.EVENT_GUEST_TEAM_NAME_EN;
                fbMatch.HomeTeamScore = GetHomeTeamScore(item.MatchList);
                fbMatch.GuestTeamScore = GetGuestTeamScore(item.MatchList);
                fbMatch.MatchLink = GetMatchLink(item.EventItemId, item.EventZoneId, item.MatchList);
                fbMatch.CurrentMatchStatus = GetMatchStatus((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsZouDi = item.MatchList.IsIS_ZOUDINull() ? false : item.MatchList.IS_ZOUDI;
                fbMatch.IsMatchComingSoon = IsMatchComingSoon(item.MatchList.STARTDATE);
                fbMatch.IsMatchInPlay = IsMatchInPlay(item.MatchList);
                fbMatch.IsMatchHT = IsMatchHT(item.MatchList);
                fbMatch.IsMatchStarted = IsMatchStarted(item.MatchList);
                fbMatch.IsMatchClosed = IsMatchClosed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchFreezed = IsMatchFreezed((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchStartingFreezed = IsMatchStartingFreezed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchFaved = item.IsMatchFaved;
                fbMatch.MatchFavedCalss = item.IsMatchFaved == 1 ? "starchoosed" : "stars";
                fbMatch.MatchLimitBetStatus = GetMatchLimitBetStatus(fbMatch);
                fbMatch.MatchStatusClass = GetMatchStatusClass(fbMatch);
                fbMatch.CurrentScore = item.MatchList.STATUS >= (int)MatchStatusEnum.InMatching ? string.Format("{0} - {1}", fbMatch.HomeTeamScore, fbMatch.GuestTeamScore) : "v";
                fbMatch.MatchStartedMinutes = GetMatchStartedMinutes(item.MatchList);
                fbMatch.MatchStartingMinutes = GetMatchStartingMinutes(item.MatchList);
                fbMatch.FootballMatchMarkets = GetMatchMarkets(markets, betBacks, betLays, item.MatchList.MATCH_ID, defaultMarketTmpId);
                fbMatch.CustomParam_1 = GetCustomParam_1(fbMatch);
                fbMatchList.Add(fbMatch);
            }
            #endregion
            return fbMatchList;
        }

        private IList<FootballMatchMarket> GetMatchMarkets(DSMatchAndMarket.Match_MarketDataTable markets, DSCachedExchangeBack.TBCachedExchangeBackDataTable betBacks, DSCachedExchangeLay.TBCachedExchangeLayDataTable betLays, int matchId, int defaultMarketTmpId)
        {
            #region BetInfo
            var betMatchOddsBackList = from m in markets
                                       where m.MATCH_ID == matchId && m.MARKET_TMP_ID == defaultMarketTmpId
                                       join b in betBacks.Where(b => b.TopNO == 1 && b.MATCH_TYPE == 1)
                                       on m.MARKET_ID equals b.MARKET_ID
                                       into temp
                                       from tt in temp.DefaultIfEmpty()
                                       select new
                                       {
                                           MarketId = m.MARKET_ID,
                                           MarketFlag = m.MARKET_FLAG,
                                           MarketTmpId = m.MARKET_TMP_ID,
                                           MatchName = m.MATCH_NAME,
                                           MatchNameEn = m.MATCH_NAME_EN,
                                           MarketName = m.MARKET_NAME,
                                           MarketNameEn = m.MARKET_NAME_EN,
                                           MarketTmpName = m.MARKET_TMP_NAME,
                                           MarketTmpNameEn = m.MARKET_TMP_NAME_EN,
                                           BackOdds = tt == null ? 0 : tt.BackODDS,
                                           BackMatchAmounts = tt == null ? 0 : tt.BackMATCH_AMOUNTS
                                       };
            var betMatchOddsList = from m in betMatchOddsBackList
                                   join l in betLays.Where(b => b.TopNO == 1 && b.MATCH_TYPE == 1)
                                   on m.MarketId equals l.MARKET_ID
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       MarketId = m.MarketId,
                                       MarketFlag = m.MarketFlag,
                                       MarketTmpId = m.MarketTmpId,
                                       MatchName = m.MatchName,
                                       MatchNameEn = m.MatchNameEn,
                                       MarketName = m.MarketName,
                                       MarketNameEn = m.MarketNameEn,
                                       MarketTmpName = m.MarketTmpName,
                                       MarketTmpNameEn = m.MarketTmpNameEn,
                                       BackOdds = m.BackOdds,
                                       BackMatchAmounts = m.BackMatchAmounts,
                                       LayOdds = tt == null ? 0 : tt.LayODDS,
                                       LayMatchAmounts = tt == null ? 0 : tt.LayMATCH_AMOUNTS
                                   };
            #endregion

            IList<FootballMatchMarket> fbMatchMartketList = new List<FootballMatchMarket>();
            FootballMatchMarket fbMatchMartket = null;
            foreach (var item in betMatchOddsList.OrderBy(m => m.MarketFlag))
            {
                fbMatchMartket = new FootballMatchMarket();
                fbMatchMartket.MatchId = matchId;
                fbMatchMartket.MarketId = item.MarketId;
                fbMatchMartket.MarketFlag = item.MarketFlag;
                fbMatchMartket.MarketTmpId = item.MarketTmpId;
                fbMatchMartket.MatchName = item.MatchName;
                fbMatchMartket.MatchNameEn = item.MatchNameEn;
                fbMatchMartket.MarketName = item.MarketName;
                fbMatchMartket.MarketNameEn = item.MarketNameEn;
                fbMatchMartket.MarketTmpName = item.MarketTmpName;
                fbMatchMartket.MarketTmpNameEn = item.MarketTmpNameEn;
                fbMatchMartket.BackOdds = item.BackOdds;
                fbMatchMartket.BackMatchAmouts = item.BackMatchAmounts;
                fbMatchMartket.LayOdds = item.LayOdds;
                fbMatchMartket.LayMatchAmouts = item.LayMatchAmounts;
                fbMatchMartketList.Add(fbMatchMartket);
            }
            return fbMatchMartketList;
        }

        #region top1 and top3处的比赛标题
        public FootballMatch GetSingleMatchRealInfo(int MatchID)
        {
            FootballMatch matchReal = new FootballMatch();
            IEnumerable<FootballMatch> footMatchLst = GetFootballMatchList(-1);
            matchReal = footMatchLst.Where(x => x.MatchId == MatchID).FirstOrDefault<FootballMatch>();

            return matchReal;
        }
        #endregion

        #region 首页default的数据

        public IList<FootballObject> GetDefaultFootballGameList()
        {
            var events = (new CachedEvent()).QueryCachedData<DSEventTeamList>()._DSEventTeamList;
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            if (dsMatchMarkets == null || dsMatchMarkets.Match_List.Count == 0)
                return null;
            IEnumerable<DSMatchAndMarket.Match_ListRow> matchs = dsMatchMarkets.Match_List;
            if (DateTime.Now.Hour >= 11)
            {
                matchs = matchs.Where(m => ((m.STARTDATE >= DateTime.Now.Date.AddHours(11) && m.STARTDATE < DateTime.Now.Date.AddDays(1).AddHours(11) && m.STATUS == 1)));
            }
            else
            {
                matchs = matchs.Where(m => ((m.STARTDATE >= DateTime.Now.Date.AddDays(-1).AddHours(11) && m.STARTDATE < DateTime.Now.Date.AddHours(11) && m.STATUS == 1)));
            }
            var markets = dsMatchMarkets.Match_Market;
            var betBacks = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCachedExchangeBack;
            var betLays = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCachedExchangeLay;

            var defaultMarketTmpId = markets.OrderBy(m => m.MARKET_TMP_ID).FirstOrDefault().MARKET_TMP_ID;

            #region GetEventList
            var eventList = from ev in markets
                            join m in matchs
                            on ev.MATCH_ID equals m.MATCH_ID
                            orderby m.STARTDATE
                            group m by new
                            {
                                m.STARTDATE,
                                m.EVENT_ID
                            }
                                into g
                                select new
                                {
                                    EventId = g.Key.EVENT_ID,
                                    MatchStartDate = g.Key.STARTDATE,
                                    EventName = g.Max(p => p.EVENT_NAME),
                                    EventNameEN = g.Max(p => p.EVENT_NAME_EN)
                                };
            #endregion

            #region GetFootballObjectList
            //FootballObject 
            IList<FootballObject> fbObjList = new List<FootballObject>();
            FootballObject fbObj = null;
            foreach (var item in eventList)
            {
                fbObj = new FootballObject();
                fbObj.EventId = Convert.ToInt32(item.EventId);
                fbObj.EventName = Language == LanguageEnum.Chinese ? item.EventName : item.EventNameEN;
                fbObj.EventStartDate = item.MatchStartDate;
                fbObj.FootballMatchs = GetDefaultMatchs(events, matchs, item.EventId, item.MatchStartDate, markets, betBacks, betLays, defaultMarketTmpId);
                fbObjList.Add(fbObj);
            }
            #endregion

            return fbObjList;
        }

        private IList<FootballMatch> GetDefaultMatchs(DSEventTeamList.DSEventTeamListDataTable events, IEnumerable<DSMatchAndMarket.Match_ListRow> matchs, string eventId,
    DateTime matchStartDate, DSMatchAndMarket.Match_MarketDataTable markets, DSCachedExchangeBack.TBCachedExchangeBackDataTable betBacks,
    DSCachedExchangeLay.TBCachedExchangeLayDataTable betLays, int defaultMarketTmpId)
        {
            var matchListTemp = from m in matchs
                                join e in events
                                on m.EVENT_ID equals e.EVENT_ID.ToString()
                                where m.EVENT_ID == eventId && m.STARTDATE == matchStartDate
                                orderby m.STARTDATE
                                select new
                                {
                                    MatchList = m,
                                    EventZoneId = e.EVENTZONE_ID,
                                    EventItemId = e.EventItem_ID
                                };

            #region GetfbMatchList
            var matchList = from m in matchListTemp
                            select new
                            {
                                MatchList = m.MatchList,
                                EventZoneId = m.EventZoneId,
                                EventItemId = m.EventItemId
                            };

            IList<FootballMatch> fbMatchList = new List<FootballMatch>();
            FootballMatch fbMatch = null;
            foreach (var item in matchList)
            {
                fbMatch = new FootballMatch();
                fbMatch.MatchEventItemId = item.EventItemId;
                fbMatch.MatchEventZoneId = item.EventZoneId;
                fbMatch.MatchEventId = item.MatchList.EVENT_ID;
                fbMatch.MatchId = item.MatchList.MATCH_ID;
                fbMatch.MatchName = Language == LanguageEnum.Chinese ? item.MatchList.MATCH_NAME : item.MatchList.MATCH_NAME_EN;
                fbMatch.MatchStartDate = item.MatchList.STARTDATE;
                fbMatch.HomeTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_HOME_TEAM_NAME : item.MatchList.EVENT_HOME_TEAM_NAME_EN;
                fbMatch.GuestTeamName = Language == LanguageEnum.Chinese ? item.MatchList.EVENT_GUEST_TEAM_NAME : item.MatchList.EVENT_GUEST_TEAM_NAME_EN;
                fbMatch.HomeTeamScore = GetHomeTeamScore(item.MatchList);
                fbMatch.GuestTeamScore = GetGuestTeamScore(item.MatchList);
                fbMatch.MatchLink = GetMatchLink(item.EventItemId, item.EventZoneId, item.MatchList);
                fbMatch.CurrentMatchStatus = GetMatchStatus((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsZouDi = item.MatchList.IsIS_ZOUDINull() ? false : item.MatchList.IS_ZOUDI;
                fbMatch.IsMatchComingSoon = IsMatchComingSoon(item.MatchList.STARTDATE);
                fbMatch.IsMatchInPlay = IsMatchInPlay(item.MatchList);
                fbMatch.IsMatchHT = IsMatchHT(item.MatchList);
                fbMatch.IsMatchStarted = IsMatchStarted(item.MatchList);
                fbMatch.IsMatchClosed = IsMatchClosed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchFreezed = IsMatchFreezed((MatchAdditionalStatusEnum)item.MatchList.ADDITIONALSTATUS);
                fbMatch.IsMatchStartingFreezed = IsMatchStartingFreezed((MatchStatusEnum)item.MatchList.STATUS);
                fbMatch.IsMatchFaved = 1;
                fbMatch.MatchFavedCalss = "stars";
                fbMatch.MatchLimitBetStatus = GetMatchLimitBetStatus(fbMatch);
                fbMatch.MatchStatusClass = GetMatchStatusClass(fbMatch);
                fbMatch.CurrentScore = item.MatchList.STATUS >= (int)MatchStatusEnum.InMatching ? string.Format("{0} - {1}", fbMatch.HomeTeamScore, fbMatch.GuestTeamScore) : "v";
                fbMatch.MatchStartedMinutes = GetMatchStartedMinutes(item.MatchList);
                fbMatch.MatchStartingMinutes = GetMatchStartingMinutes(item.MatchList);
                fbMatch.FootballMatchMarkets = GetMatchMarkets(markets, betBacks, betLays, item.MatchList.MATCH_ID, defaultMarketTmpId);
                fbMatch.CustomParam_1 = GetCustomParam_1forDefault(fbMatch);
                fbMatchList.Add(fbMatch);
            }
            #endregion
            return fbMatchList;
        }

        #endregion
    }
}