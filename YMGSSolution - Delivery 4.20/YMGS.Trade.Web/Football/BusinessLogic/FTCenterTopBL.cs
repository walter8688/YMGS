using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Common;
using YMGS.Trade.Web.Football.Model;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Trade.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Trade.Web.Football.Common;
using YMGS.Business.AssistManage;

namespace YMGS.Trade.Web.Football.BusinessLogic
{
    public class FTCenterTopBL : CenterMatchMarketBase
    {
        public LanguageEnum Language { get; set; }

        private readonly string scriptString = "javascript:void(0)";

        public FTCenterTopBL()
        {
        }


        public FTCenterTopBL(LanguageEnum language)
        {
            this.Language = language;
        }


        public FootballMatchInfo matchInfo { get; set; }
        public IList<FootballMarketInfo> fbList { get; set; }
        public FTCenterTopBL(int matchId, LanguageEnum language)
        {
            this.Language = language;
            matchInfo = GetMatchInfoByMatchID(matchId);
            fbList = GetMarketInfoByMatchID(matchId);
        }

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

        #region public 取cache数据
        /// <summary>
        /// 从cache取match的数据
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        private FootballMatchInfo GetMatchInfoByMatchID(int matchID)
        {
            FootballMatchInfo matchBO = new FootballMatchInfo();
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var match = dsMatchMarkets.Match_List.Where(x => x.MATCH_ID == matchID);
            foreach (var item in match)
            {
                matchBO.EventId = Convert.ToInt32(item.EVENT_ID);
                matchBO.MatchType = 1;
                matchBO.MatchId = item.MATCH_ID;
                matchBO.MatchName = Language == LanguageEnum.Chinese ? item.MATCH_NAME : item.MATCH_NAME_EN;
                matchBO.MatchName_CN = item.MATCH_NAME;
                matchBO.MatchName_EN = item.MATCH_NAME_EN;
                matchBO.MatchStatus = item.STATUS;
                matchBO.isZouDi = (item.IS_ZOUDI && (item.STATUS == (int)MatchStatusEnum.InMatching || item.STATUS == (int)MatchStatusEnum.HalfTimeFinished || item.STATUS == (int)MatchStatusEnum.SecHalfStarted));
                matchBO.MADDStatus = item.ADDITIONALSTATUS;
                matchBO.MStartDate = UtilityHelper.DateToDateAndTimeStr(item.STARTDATE);
                matchBO.MatchEndDate = "";
                matchBO.HandicapHD = item.IsHandicapHalfDefaultNull() ? "-1" : item.HandicapHalfDefault;
                matchBO.HandicapFD = item.IsHandicapHalfDefaultNull() ? "-1" : item.HandicapFullDefault;
                matchBO.ViewFullADD = GetViewFullMarketLinkAddress(matchBO.EventId, item.MATCH_ID, UtilityHelper.DateToStrOrDefault(item.STARTDATE, DateTime.Now.ToString()));
            }
            return matchBO;
        }
        /// <summary>
        /// 从cache里抓数据，并构造lst用于前台
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetMarketInfoByMatchID(int matchID)
        {
            IList<FootballMarketInfo> ftMarketLst = GetMatchMarketInfoByMatchIDFromCache(matchID);

            //把最优的3个投注的赔率以及金额加到list里去
            ftMarketLst = AddBackToMarketLst(ftMarketLst);
            //把最优的3个受注的赔率以及金额加到list里去
            ftMarketLst = AddLayToMarketLst(ftMarketLst);

            return ftMarketLst;
        }
        /// <summary>
        /// 从cache里取market的数据
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetMatchMarketInfoByMatchIDFromCache(int matchID)
        {
            IList<FootballMarketInfo> marketLst = new List<FootballMarketInfo>();
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var markets = dsMatchMarkets.Match_Market.Where(x => x.MATCH_ID == matchID);
            foreach (var item in markets)
            {
                FootballMarketInfo marketBO = new FootballMarketInfo();
                marketBO.MarketId = item.MARKET_ID;
                marketBO.MKName = Language == LanguageEnum.Chinese ? item.MARKET_NAME : item.MARKET_NAME_EN;
                marketBO.MKName_CN = item.MARKET_NAME;
                marketBO.MKName_EN = item.MARKET_NAME_EN;
                marketBO.MarketTmpId = item.MARKET_TMP_ID;
                marketBO.MKTmpName = Language == LanguageEnum.Chinese ? item.MARKET_TMP_NAME : item.MARKET_TMP_NAME_EN;
                marketBO.MKTmpName_CN = item.MARKET_TMP_NAME;
                marketBO.MKTmpName_EN = item.MARKET_TMP_NAME_EN;
                marketBO.MarketTmpType = item.MARKET_TMP_TYPE;
                marketBO.MarketFlag = item.MARKET_FLAG;
                marketBO.ScoreA = item.IsSCOREANull() ? -1 : item.SCOREA;
                marketBO.ScoreB = item.IsSCOREBNull() ? -1 : item.SCOREB;
                marketBO.BetTypeId = item.BET_TYPE_ID;
                marketBO.BetTypeName = Language == LanguageEnum.Chinese ? item.BET_TYPE_NAME : item.BET_TYPE_NAME_EN;
                marketBO.DealAmount = GetMarketDealAmountByMarketID(item.MARKET_ID);
                //marketBO.BackAT1 = "";
                //marketBO.LayAT1 = "";
                //marketBO.BackOT1 = "";
                //marketBO.LayOT1 = "";
                //marketBO.BackAT2 = "";
                //marketBO.LayAT2 = "";
                //marketBO.BackOT2 = "";
                //marketBO.LayOT2 = "";
                //marketBO.BackAT3 = "";
                //marketBO.LayAT3 = "";
                //marketBO.BackOT3 = "";
                //marketBO.LayOT3 = "";
                marketBO.IsMClosed = IsMatchClosed((MatchStatusEnum)item.STATUS);
                marketBO.IsMSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)item.ADDITIONALSTATUS);
                marketBO.IsMFreezed = IsMatchFreezed((MatchAdditionalStatusEnum)item.ADDITIONALSTATUS);
                marketBO.DivCharacter = GetMatchLimitBetStatus(marketBO);
                marketBO.MStatusClass = GetMatchStatusClass(marketBO);
                marketBO.MatchAmountsSum = GetMarketMatchAmountsSumByMarketID(item.MARKET_ID);
                marketBO.RulesLinkAdd = GetRulesLinkAddress(item.BET_TYPE_ID, item.MARKET_TMP_TYPE);
                marketLst.Add(marketBO);
            }
            return marketLst;
        }
        /// <summary>
        /// 成交量
        /// </summary>
        /// <param name="marketID"></param>
        /// <returns></returns>
        private decimal GetMarketDealAmountByMarketID(int marketID)
        {
            decimal backDealAmount = 0;
            var backDealAmounts = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCacheDealAmountBack;
            var tempBack = backDealAmounts.Where(e => e.MARKET_ID == marketID && e.MATCH_TYPE == 1).FirstOrDefault();
            if (tempBack != null)
            {
                backDealAmount = tempBack.BackDealAmount;
            }

            decimal layDealAmount = 0;
            var layDealAmounts = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCacheDealAmountLay;
            var tempLay = layDealAmounts.Where(e => e.MARKET_ID == marketID && e.MATCH_TYPE == 1).FirstOrDefault();
            if (tempLay != null)
            {
                layDealAmount = tempLay.LayDealAmount;
            }

            return backDealAmount + layDealAmount;
        }
        /// <summary>
        /// 挂单量
        /// </summary>
        /// <param name="marketID"></param>
        /// <returns></returns>
        private decimal GetMarketMatchAmountsSumByMarketID(int marketID)
        {
            decimal backMatchAmount = 0;
            var backMatchAmounts = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCacheDealAmountBack;
            var tempBack = backMatchAmounts.Where(e => e.MARKET_ID == marketID && e.MATCH_TYPE == 1).FirstOrDefault();
            if (tempBack != null)
            {
                backMatchAmount = tempBack.IsBackMatchAmountNull() ? 0 : tempBack.BackMatchAmount;
            }

            decimal layMatchAmount = 0;
            var layMatchAmounts = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCacheDealAmountLay;
            var tempLay = layMatchAmounts.Where(e => e.MARKET_ID == marketID && e.MATCH_TYPE == 1).FirstOrDefault();
            if (tempLay != null)
            {
                layMatchAmount = tempLay.IsLayMatchAmountNull() ? 0 : tempLay.LayMatchAmount;
            }

            return backMatchAmount + layMatchAmount;
        }
        /// <summary>
        /// Rules的规则链接地址
        /// </summary>
        /// <returns></returns>
        private string GetRulesLinkAddress(int BetTypeID, int MarketTmpType)
        {
            string linkAddress = string.Empty;
            DSHelper oddsDS = HelperManager.QueryHelper();
            var helpData = oddsDS.TB_HELPER;
            string rulesIDs = BetTypeID.ToString() + "-" + MarketTmpType.ToString();
            var search = helpData.Where(x => (x.IsRulesIDNull() ? "" : x.RulesID) == rulesIDs);
            if (search.Count() == 0)
            {
                linkAddress = scriptString;
            }
            else
            {
                linkAddress = Language == LanguageEnum.Chinese ? search.FirstOrDefault().WEBLINK : search.FirstOrDefault().ENWEBLINK;
            }
            return linkAddress;
        }

        /// <summary>
        /// top1的link
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="matchId"></param>
        /// <param name="matchStartDate"></param>
        /// <returns></returns>
        public string GetViewFullMarketLinkAddress(int eventId, int matchId, string matchStartDate)
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
        /// 把最优的3个投注的赔率以及金额加到list里去
        /// </summary>
        /// <param name="ftMarketLst"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> AddBackToMarketLst(IList<FootballMarketInfo> ftMarketLst)
        {
            var betBacks = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCachedExchangeBack;
            foreach (var market in ftMarketLst)
            {
                foreach (var back in betBacks.Where(e => e.MATCH_TYPE == 1))
                {
                    if (market.MarketId == back.MARKET_ID)
                    {
                        if (back.TopNO == 1)
                        {
                            market.BackAT1 = back.BackMATCH_AMOUNTS.ToString();
                            market.BackOT1 = back.BackODDS.ToString();
                        }
                        if (back.TopNO == 2)
                        {
                            market.BackAT2 = back.BackMATCH_AMOUNTS.ToString();
                            market.BackOT2 = back.BackODDS.ToString();
                        }
                        if (back.TopNO == 3)
                        {
                            market.BackAT3 = back.BackMATCH_AMOUNTS.ToString();
                            market.BackOT3 = back.BackODDS.ToString();
                        }
                    }
                }
            }
            return ftMarketLst;
        }
        /// <summary>
        /// 把最优的3个受注的赔率以及金额加到list里去
        /// </summary>
        /// <param name="ftMarketLst"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> AddLayToMarketLst(IList<FootballMarketInfo> ftMarketLst)
        {
            var betLays = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCachedExchangeLay;
            foreach (var market in ftMarketLst)
            {
                foreach (var lay in betLays.Where(e => e.MATCH_TYPE == 1))
                {
                    if (market.MarketId == lay.MARKET_ID)
                    {
                        if (lay.TopNO == 1)
                        {
                            market.LayAT1 = lay.LayMATCH_AMOUNTS.ToString();
                            market.LayOT1 = lay.LayODDS.ToString();
                        }
                        if (lay.TopNO == 2)
                        {
                            market.LayAT2 = lay.LayMATCH_AMOUNTS.ToString();
                            market.LayOT2 = lay.LayODDS.ToString();
                        }
                        if (lay.TopNO == 3)
                        {
                            market.LayAT3 = lay.LayMATCH_AMOUNTS.ToString();
                            market.LayOT3 = lay.LayODDS.ToString();
                        }
                    }
                }
            }
            return ftMarketLst;
        }
        #endregion

        #region top1
        /// <summary>
        /// top1数据(包括WebService)
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="betOrder"></param>
        /// <returns></returns>
        public FTMarketsObj GetWebServiceTop1Data(int matchID, MarketBetTypeOrderInfo betOrder)
        {
            FTMarketsObj ftMarkets = new FTMarketsObj();
            //match
            //ftMarkets.MatchInfo = GetMatchInfoByMatchID(matchID);
            ftMarkets.MatchInfo = matchInfo == null ? GetMatchInfoByMatchID(matchID) : matchInfo;
            //market
            ftMarkets.MarketInfo = GetFootballMarketLstByFilter(matchID, betOrder);

            return ftMarkets;
        }

        #endregion

        #region top3
        /// <summary>
        /// WebService - top3
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="Param"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public CenterTop3MarketObject GetWebServiceTop3DataAllAndSummary(int Match_ID, MatchTop3Parameter Param, bool isAll)
        {
            CenterTop3MarketObject webServiceMarket = GetAllAndSummaryTop3Data(Match_ID, Param);
            webServiceMarket.isShowAll = isAll;

            return webServiceMarket;
        }

        /// <summary>
        /// BEGIN:TOP3的数据，包括all以及summary的
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public CenterTop3MarketObject GetAllAndSummaryTop3Data(int Match_ID, MatchTop3Parameter Param)
        {
            CenterTop3MarketObject marketObject = new CenterTop3MarketObject();
            //marketObject.MatchInfo = GetMatchInfoByMatchID(Match_ID);
            marketObject.MatchInfo = matchInfo == null ? GetMatchInfoByMatchID(Match_ID) : matchInfo;
            marketObject.MarketAllInfo = GetTop3AllDataByParameter(Match_ID, Param);
            marketObject.MarketSummaryInfo = GetAsianSummaryLstByParam(Match_ID, Param);
            //亚洲盘时，默认显示summary的
            marketObject.isShowAll = !IsDisplayShowAllButton(Param);
            return marketObject;
        }
        /// <summary>
        /// 根据具体条件，获取top3所有数据
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<FootballMarketInfo> GetTop3AllDataByParameter(int Match_ID, MatchTop3Parameter Param)
        {
            IList<FootballMarketInfo> allLst = new List<FootballMarketInfo>();

            if (!string.IsNullOrEmpty(Param.MarketTmpID))
            {
                int marketTmpID = Convert.ToInt32(Param.MarketTmpID);
                allLst = GetEachTempMatchMarketTop3Lst(Match_ID, marketTmpID);
            }
            if (!string.IsNullOrEmpty(Param.OrderNO))
            {
                int orderNO = Convert.ToInt32(Param.OrderNO);
                allLst = GetSpecialMatchMarketTop3Lst(Match_ID, orderNO);
            }
            if (!string.IsNullOrEmpty(Param.BetTypeID) && !string.IsNullOrEmpty(Param.MarketTmpType))
            {
                int betTypeID = Convert.ToInt32(Param.BetTypeID);
                int marketTmpType = Convert.ToInt32(Param.MarketTmpType);
                allLst = GetTop3MatchMarketLstByBetType(Match_ID, betTypeID, marketTmpType);
            }
            return allLst;
        }

        /// <summary>
        /// 获取亚洲盘的summary数据
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<FootballMarketInfo> GetAsianSummaryLstByParam(int matchID, MatchTop3Parameter Param)
        {
            IList<FootballMarketInfo> summaryLst = new List<FootballMarketInfo>();

            if (!string.IsNullOrEmpty(Param.OrderNO))
            {
                int orderNO = Convert.ToInt32(Param.OrderNO);
                if (orderNO == 1 || orderNO == 2)//亚洲盘
                {
                    //IList<MarketBetTypeOrderInfo> marketBetTypeLst = FootballCommonFunction.QueryAllMarketBetTypeOrderInfo();
                    IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(matchID);
                    MarketBetTypeOrderInfo betOrder = marketBetTypeLst.Where(e => e.OrdNo == orderNO).FirstOrDefault();
                    summaryLst = GetAsianSummaryMatchMarketLst(matchID, betOrder.BetTypeId, betOrder.MarketTmpType);
                }
            }
            if (!string.IsNullOrEmpty(Param.BetTypeID) && !string.IsNullOrEmpty(Param.MarketTmpType))
            {
                int betTypeID = Convert.ToInt32(Param.BetTypeID);
                int marketTmpType = Convert.ToInt32(Param.MarketTmpType);
                if ((betTypeID == 4 && marketTmpType == 0) || (betTypeID == 4 && marketTmpType == 1))
                {
                    summaryLst = GetAsianSummaryMatchMarketLst(matchID, betTypeID, marketTmpType);
                }
            }
            return summaryLst;
        }

        /// <summary>
        /// 根据具体条件，获取top3控件的名称
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string GetTop3ControlTitle(int matchID,MatchTop3Parameter Param)
        {
            string titleName = string.Empty;
            if (!string.IsNullOrEmpty(Param.OrderNO))
            {
                int orderNO = Convert.ToInt32(Param.OrderNO);
                //IList<MarketBetTypeOrderInfo> marketBetTypeLst = FootballCommonFunction.QueryAllMarketBetTypeOrderInfo();
                IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(matchID);
                MarketBetTypeOrderInfo betOrder = marketBetTypeLst.Where(e => e.OrdNo == orderNO).FirstOrDefault();
                titleName = Language == LanguageEnum.Chinese ? betOrder.BetTypeName_CN : betOrder.BetTypeName_EN;
            }
            if (!string.IsNullOrEmpty(Param.BetTypeID) && !string.IsNullOrEmpty(Param.MarketTmpType))
            {
                int betTypeID = Convert.ToInt32(Param.BetTypeID);
                int marketTmpType = Convert.ToInt32(Param.MarketTmpType);
                if (betTypeID == 4)
                {
                    if (marketTmpType == 0)
                    {
                        titleName = Language == LanguageEnum.Chinese ? "半场让分盘" : "Half Asian Handicap";
                    }
                    if (marketTmpType == 1)
                    {
                        titleName = Language == LanguageEnum.Chinese ? "全场让分盘" : "Asian Handicap";
                    }
                }
                if (betTypeID == 2)
                {
                    if (marketTmpType == 0)
                    {
                        titleName = Language == LanguageEnum.Chinese ? "半场波胆" : "Half Time Score";
                    }
                    if (marketTmpType == 1)
                    {
                        titleName = Language == LanguageEnum.Chinese ? "全场波胆" : "Correct Score";
                    }
                }
            }
            if (!string.IsNullOrEmpty(Param.MarketTmpID))
            {
                int marketTmpID = Convert.ToInt32(Param.MarketTmpID);
                var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
                var market = dsMatchMarkets.Match_Market.Where(x => x.MARKET_TMP_ID == marketTmpID);
                titleName = Language == LanguageEnum.Chinese ? market.FirstOrDefault().MARKET_TMP_NAME : market.FirstOrDefault().MARKET_TMP_NAME_EN;
            }
            return titleName;
        }
        /// <summary>
        /// 根据条件，判断top3是否是亚洲盘，是的话则需显示summary/show all
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public bool IsDisplayShowAllButton(MatchTop3Parameter Param)
        {
            bool isDisplay = false;
            if (!string.IsNullOrEmpty(Param.OrderNO))
            {
                int orderNO = Convert.ToInt32(Param.OrderNO);
                if (orderNO == 1 || orderNO == 2)//亚洲盘
                {
                    isDisplay = true;
                }
            }
            if (!string.IsNullOrEmpty(Param.BetTypeID) && !string.IsNullOrEmpty(Param.MarketTmpType))
            {
                int betTypeID = Convert.ToInt32(Param.BetTypeID);
                if (betTypeID == 4)
                {
                    isDisplay = true;
                }
            }
            return isDisplay;
        }

        /// <summary>
        /// 取单个市场的top3数据
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="Market_Tmp_ID"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetEachTempMatchMarketTop3Lst(int Match_ID, int Market_Tmp_ID)
        {
            IList<FootballMarketInfo> eachSingleData = new List<FootballMarketInfo>();

            //IList<FootballMarketInfo> matchMarketAll = GetMarketInfoByMatchID(Match_ID);
            IList<FootballMarketInfo> matchMarketAll = fbList == null ? GetMarketInfoByMatchID(Match_ID) : fbList;
            eachSingleData = matchMarketAll.Where(e => e.MarketTmpId == Market_Tmp_ID).ToList<FootballMarketInfo>();

            return eachSingleData;
        }

        /// <summary>
        /// 从top1传去的top3数据,即OrderNO
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="MarketOrderNO"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetSpecialMatchMarketTop3Lst(int Match_ID, int OrderNO)
        {
            IList<FootballMarketInfo> specialData = new List<FootballMarketInfo>();

            //IList<MarketBetTypeOrderInfo> marketBetTypeLst = FootballCommonFunction.QueryAllMarketBetTypeOrderInfo();
            IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(Match_ID);
            MarketBetTypeOrderInfo betOrder = marketBetTypeLst.Where(e => e.OrdNo == OrderNO).FirstOrDefault();
            specialData = GetFootballMarketLstByFilter(Match_ID, betOrder);

            return specialData;
        }

        /// <summary>
        /// 亚洲盘、波胆
        /// </summary>
        /// <param name="Match_ID"></param>
        /// <param name="BetTypeID"></param>
        /// <param name="MarketType"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetTop3MatchMarketLstByBetType(int Match_ID, int BetTypeID, int MarketType)
        {
            IList<FootballMarketInfo> betTypeData = new List<FootballMarketInfo>();

            //IList<FootballMarketInfo> matchMarketAll = GetMarketInfoByMatchID(Match_ID);
            IList<FootballMarketInfo> matchMarketAll = fbList == null ? GetMarketInfoByMatchID(Match_ID) : fbList;
            betTypeData = matchMarketAll.Where(x => x.BetTypeId == BetTypeID && x.MarketTmpType == MarketType).ToList<FootballMarketInfo>();

            return betTypeData;
        }

        /// <summary>
        /// 获取top1/top3的数据
        /// </summary>
        /// <param name="betOrder"></param>
        /// <param name="isScore">判断是否是大小球，true-是，需要筛选比分；false-不是不需要比分</param>
        /// <param name="isEqual">判断是否需要等于比分 true:是等于 false:不等于</param>
        /// <returns></returns>
        public IList<FootballMarketInfo> GetFootballMarketLstByFilter(int matchID, MarketBetTypeOrderInfo betOrder)
        {
            bool isScore = false;
            bool isEqual = false;
            if (betOrder.BetTypeId == 3)
            {
                isScore = true;
                if (betOrder.OrdNo == 3 || betOrder.OrdNo == 4)
                {//大小球
                    isEqual = true;
                }
            }

            IList<FootballMarketInfo> matchMarketAll = fbList == null ? GetMarketInfoByMatchID(matchID) : fbList;
            if (isScore)
            {
                if (isEqual)
                {
                    matchMarketAll = matchMarketAll.Where(x => x.BetTypeId == betOrder.BetTypeId
                        && x.MarketTmpType == betOrder.MarketTmpType && (x.ScoreB == betOrder.GoalsEqualsOne || x.ScoreB == betOrder.GoalsEqualsTwo)
                        ).ToList<FootballMarketInfo>();
                }
                else
                {
                    matchMarketAll = matchMarketAll.Where(x => x.BetTypeId == betOrder.BetTypeId
                        && x.MarketTmpType == betOrder.MarketTmpType && x.ScoreB != betOrder.GoalsEqualsOne && x.ScoreB != betOrder.GoalsEqualsTwo
                        ).ToList<FootballMarketInfo>();
                }
            }
            else
            {
                matchMarketAll = matchMarketAll.Where(x => x.BetTypeId == betOrder.BetTypeId && x.MarketTmpType == betOrder.MarketTmpType
                   ).ToList<FootballMarketInfo>();
            }
            return matchMarketAll;
        }

        /// <summary>
        /// 设置亚洲盘的显示，根据default或者成交量来
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public IList<FootballMarketInfo> GetAsianSummaryMatchMarketLst(int matchID, int betTypeID, int marketTmpType)
        {
            IList<FootballMarketInfo> AsianSummaryLst = new List<FootballMarketInfo>();

            //FootballMatchInfo matchInfo = GetMatchInfoByMatchID(matchID);
            FootballMatchInfo mtInfo = matchInfo == null ? GetMatchInfoByMatchID(matchID) : matchInfo;
            //IList<FootballMarketInfo> matchMarketAll = GetMarketInfoByMatchID(matchID);
            IList<FootballMarketInfo> matchMarketAll = fbList == null ? GetMarketInfoByMatchID(matchID) : fbList;
            int defaultPort = 0;
            if (marketTmpType == (int)MarketTemplateTypeEnum.HalfTime)
            {
                defaultPort = Convert.ToInt32(mtInfo.HandicapHD);
            }
            if (marketTmpType == (int)MarketTemplateTypeEnum.FullTime)
            {
                defaultPort = Convert.ToInt32(mtInfo.HandicapFD);
            }
            matchMarketAll = matchMarketAll.Where(x => x.BetTypeId == betTypeID && x.MarketTmpType == marketTmpType).ToList<FootballMarketInfo>();

            AsianSummaryLst = GetAsianSummaryMatchMarketLstByFilter(matchMarketAll, defaultPort);

            return AsianSummaryLst;
        }

        /// <summary>
        /// 取summary数据，根据成交量或者default盘口来
        /// </summary>
        /// <param name="matchMarketAll"></param>
        /// <param name="defaultPort"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetAsianSummaryMatchMarketLstByFilter(IList<FootballMarketInfo> matchMarketAll, int defaultPort)
        {
            IList<FootballMarketInfo> summaryMatchMarketLst = new List<FootballMarketInfo>();
            if (matchMarketAll.Where(x => x.MatchAmountsSum != 0).Count() > 0)
            {//有成交量
                var amountLst = matchMarketAll.OrderByDescending(x => x.MatchAmountsSum);
                defaultPort = amountLst.First().MarketTmpId;
                //按ScoreB排序
                var sortLst = matchMarketAll.Where(x => x.MarketFlag == (int)MatchMarketFlagEnum.Over).OrderBy(x => x.ScoreB).ToList<FootballMarketInfo>();
                if (sortLst.Count <= 3)
                {//表示最多只有6条，summary跟all是一样的
                    summaryMatchMarketLst = matchMarketAll;
                }
                else
                {
                    summaryMatchMarketLst = GetAsianSummaryCountLarge3(matchMarketAll, defaultPort, sortLst);
                }
            }
            else
            {//没有成交量，按default排列
                //按ScoreB排序
                var sortLst = matchMarketAll.Where(x => x.MarketFlag == (int)MatchMarketFlagEnum.Over).OrderBy(x => x.ScoreB).ToList<FootballMarketInfo>();
                if (sortLst.Count <= 3)
                {//表示最多只有6条，summary跟all是一样的
                    summaryMatchMarketLst = matchMarketAll;
                }
                else
                {
                    summaryMatchMarketLst = GetAsianSummaryCountLarge3(matchMarketAll, defaultPort, sortLst);
                }
            }
            return summaryMatchMarketLst;
        }

        /// <summary>
        /// 数据量超过3条时的取相邻的上下2条记录
        /// </summary>
        /// <param name="matchMarketAll"></param>
        /// <param name="defaultPort"></param>
        /// <param name="sortLst"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetAsianSummaryCountLarge3(IList<FootballMarketInfo> matchMarketAll, int defaultPort, IList<FootballMarketInfo> sortLst)
        {
            IList<FootballMarketInfo> summaryCount3MatchMarketLst = new List<FootballMarketInfo>();
            int tmp1 = 0, tmp3 = 0;
            for (int i = 0; i < sortLst.Count; i++)
            {
                if (sortLst[i].MarketTmpId == defaultPort)
                {
                    if (i == 0)
                    {
                        tmp1 = sortLst[i + 1].MarketTmpId;
                        tmp3 = sortLst[i + 2].MarketTmpId;

                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[1]);
                    }
                    else if (i == (sortLst.Count - 1))
                    {
                        tmp1 = sortLst[i - 2].MarketTmpId;
                        tmp3 = sortLst[i - 1].MarketTmpId;

                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[1]);
                    }
                    else
                    {
                        tmp1 = sortLst[i - 1].MarketTmpId;
                        tmp3 = sortLst[i + 1].MarketTmpId;

                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp1).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == defaultPort).ToList<FootballMarketInfo>()[1]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[0]);
                        summaryCount3MatchMarketLst.Add(matchMarketAll.Where(x => x.MarketTmpId == tmp3).ToList<FootballMarketInfo>()[1]);
                    }
                }
            }
            return summaryCount3MatchMarketLst;
        }
        #endregion
    }
}