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
    public class ChampMatchBL : CenterMatchMarketBase
    {
        public LanguageEnum Language { get; set; }

        private readonly string scriptString = "javascript:void(0)";

        public ChampMatchBL()
        {
        }

        public ChampMatchBL(LanguageEnum language)
        {
            this.Language = language;
        }

        #region 取cache数据
        /// <summary>
        /// 从cache里抓数据，并构造lst用于前台
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public FTMarketsObj GetChampMatchMarketInfoByEventID(int champEventID)
        {
            FTMarketsObj ftMarkets = new FTMarketsObj();
            //match
            ftMarkets.MatchInfo = GetMatchInfoByEventID(champEventID);
            //market
            ftMarkets.MarketInfo = GetMarketInfoByEventID(champEventID);

            return ftMarkets;
        }

        /// <summary>
        /// 从cache取match的数据
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        private FootballMatchInfo GetMatchInfoByEventID(int champEventID)
        {
            FootballMatchInfo matchBO = new FootballMatchInfo();
            var Champds = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            //Champds.ChampEventList   冠军比赛
            //Champds.ChampEventMarket 冠军比赛市场
            var ChampEventlst = Champds.ChampEventList.Where(x => x.Champ_Event_ID == champEventID);
            //var ChampMarketEventlst = Champds.ChampEventMarket.Where(x => x.CHAMP_EVENT_ID == champEventID);

            foreach (var item in ChampEventlst)
            {
                matchBO.EventId = item.Event_ID;
                matchBO.MatchType = 2;
                matchBO.MatchId = item.Champ_Event_ID;
                matchBO.MatchName = Language == LanguageEnum.Chinese ? "娱乐冠军" : "Entertainment Champion";
                matchBO.MatchName_CN = "娱乐冠军";
                matchBO.MatchName_EN = "Entertainment Champion";
                matchBO.MatchStatus = item.Champ_Event_Status;
                matchBO.isZouDi = false;
                matchBO.MADDStatus = item.Champ_Event_Status;
                matchBO.MStartDate = "";
                matchBO.MatchEndDate = UtilityHelper.DateToDateAndTimeStr(item.Champ_Event_EndDate);
                matchBO.HandicapHD = "";
                matchBO.HandicapFD = "";
                matchBO.ViewFullADD = scriptString;
            }
            return matchBO;
        }
        /// <summary>
        /// 从cache取market的数据
        /// </summary>
        /// <param name="champEventID"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetMarketInfoByEventID(int champEventID)
        {
            IList<FootballMarketInfo> ftMarketLst = GetChampInfoFromCache(champEventID);

            //把最优的3个投注的赔率以及金额加到list里去
            ftMarketLst = AddBackToChampMarketLst(ftMarketLst);
            //把最优的3个受注的赔率以及金额加到list里去
            ftMarketLst = AddLayToChampMarketLst(ftMarketLst);

            return ftMarketLst;
        }
        /// <summary>
        /// 构造lst
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> GetChampInfoFromCache(int champEventID)
        {
            IList<FootballMarketInfo> ftChampMarketLst = new List<FootballMarketInfo>();

            var Champds = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            //Champds.ChampEventList   冠军比赛
            //Champds.ChampEventMarket 冠军比赛市场
            var ChampEventlst = Champds.ChampEventList.Where(x => x.Champ_Event_ID == champEventID);
            var ChampMarketEventlst = Champds.ChampEventMarket.Where(x => x.CHAMP_EVENT_ID == champEventID);
            foreach (var item in ChampMarketEventlst)
            {
                FootballMarketInfo marketBO = new FootballMarketInfo();
                #region Add Market Info to List
                marketBO.MarketId = item.CHAMP_MARKET_ID;
                marketBO.MKName = Language == LanguageEnum.Chinese ? item.CHAMP_EVENT_MEMBER_NAME : item.IsCHAMP_EVENT_MEMBER_NAME_ENNull() ? "" : item.CHAMP_EVENT_MEMBER_NAME_EN;
                marketBO.MKName_CN = item.CHAMP_EVENT_MEMBER_NAME;
                marketBO.MKName_EN = item.IsCHAMP_EVENT_MEMBER_NAME_ENNull() ? "" : item.CHAMP_EVENT_MEMBER_NAME_EN;
                marketBO.MarketTmpId = -1;
                marketBO.MKTmpName = Language == LanguageEnum.Chinese ? ChampEventlst.FirstOrDefault().Champ_Event_Name : ChampEventlst.FirstOrDefault().CHAMP_EVENT_NAME_EN;
                marketBO.MKTmpName_CN = ChampEventlst.FirstOrDefault().Champ_Event_Name;
                marketBO.MKTmpName_EN = ChampEventlst.FirstOrDefault().CHAMP_EVENT_NAME_EN;
                marketBO.MarketTmpType = -1;
                marketBO.MarketFlag = -1;
                marketBO.ScoreA = -1;
                marketBO.ScoreB = -1;
                marketBO.BetTypeId = -1;
                marketBO.BetTypeName = "";
                marketBO.DealAmount = GetChampMarketDealAmountByEventID(item.CHAMP_MARKET_ID);
                marketBO.BackAT1 = "";
                marketBO.LayAT1 = "";
                marketBO.BackOT1 = "";
                marketBO.LayOT1 = "";
                marketBO.BackAT2 = "";
                marketBO.LayAT2 = "";
                marketBO.BackOT2 = "";
                marketBO.LayOT2 = "";
                marketBO.BackAT3 = "";
                marketBO.LayAT3 = "";
                marketBO.BackOT3 = "";
                marketBO.LayOT3 = "";
                marketBO.IsMClosed = isChampMatchClosed((ChampEventStatusEnum)ChampEventlst.FirstOrDefault().Champ_Event_Status);
                marketBO.IsMSuspend = IsMatchSuspend((MatchAdditionalStatusEnum)ChampEventlst.FirstOrDefault().Champ_Event_Status);
                marketBO.IsMFreezed = false;
                marketBO.DivCharacter = GetMatchLimitBetStatus(marketBO);
                marketBO.MStatusClass = GetMatchStatusClass(marketBO);
                marketBO.MatchAmountsSum = GetChampMarketMatchAmountsSumByMarketID(item.CHAMP_MARKET_ID);
                marketBO.RulesLinkAdd = GetRulesLinkAddress(-99);
                #endregion

                ftChampMarketLst.Add(marketBO);
            }
            return ftChampMarketLst;
        }

        /// <summary>
        /// 成交量
        /// </summary>
        /// <param name="marketID"></param>
        /// <returns></returns>
        private decimal GetChampMarketDealAmountByEventID(int champMarketID)
        {
            decimal backDealAmount = 0;
            var backDealAmounts = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCacheDealAmountBack;
            var tempBack = backDealAmounts.Where(e => e.MARKET_ID == champMarketID && e.MATCH_TYPE == 2).FirstOrDefault();
            if (tempBack != null)
            {
                backDealAmount = tempBack.BackDealAmount;
            }

            decimal layDealAmount = 0;
            var layDealAmounts = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCacheDealAmountLay;
            var tempLay = layDealAmounts.Where(e => e.MARKET_ID == champMarketID && e.MATCH_TYPE == 2).FirstOrDefault();
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
        private decimal GetChampMarketMatchAmountsSumByMarketID(int champMarketID)
        {
            decimal backMatchAmount = 0;
            var backMatchAmounts = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCacheDealAmountBack;
            var tempBack = backMatchAmounts.Where(e => e.MARKET_ID == champMarketID && e.MATCH_TYPE == 2).FirstOrDefault();
            if (tempBack != null)
            {
                backMatchAmount = tempBack.IsBackMatchAmountNull() ? 0 : tempBack.BackMatchAmount;
            }

            decimal layMatchAmount = 0;
            var layMatchAmounts = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCacheDealAmountLay;
            var tempLay = layMatchAmounts.Where(e => e.MARKET_ID == champMarketID && e.MATCH_TYPE == 2).FirstOrDefault();
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
        private string GetRulesLinkAddress(int champRuleID)
        {
            string linkAddress = string.Empty;
            DSHelper oddsDS = HelperManager.QueryHelper();
            var helpData = oddsDS.TB_HELPER;
            string rulesIDs = champRuleID.ToString();
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
        /// 把最优的3个投注的赔率以及金额加到list里去
        /// </summary>
        /// <param name="ftMarketLst"></param>
        /// <returns></returns>
        private IList<FootballMarketInfo> AddBackToChampMarketLst(IList<FootballMarketInfo> ftMarketLst)
        {
            var betBacks = (new CachedExchangeBack()).QueryCachedData<DSCachedExchangeBack>().TBCachedExchangeBack;
            foreach (var market in ftMarketLst)
            {
                foreach (var back in betBacks.Where(e => e.MATCH_TYPE == 2))
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
        private IList<FootballMarketInfo> AddLayToChampMarketLst(IList<FootballMarketInfo> ftMarketLst)
        {
            var betLays = (new CachedExchangeLay()).QueryCachedData<DSCachedExchangeLay>().TBCachedExchangeLay;
            foreach (var market in ftMarketLst)
            {
                foreach (var lay in betLays.Where(e => e.MATCH_TYPE == 2))
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

    }
}