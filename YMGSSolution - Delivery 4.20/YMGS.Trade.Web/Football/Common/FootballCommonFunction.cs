using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.Football.Model;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;

namespace YMGS.Trade.Web.Football.Common
{
    public class FootballCommonFunction
    {
        private static IList<MarketBetTypeOrderInfo> QueryAllMarketBetTypeOrderInfo()
        {
            var MarketBetTypeOrderInfo = new List<MarketBetTypeOrderInfo>();
            //标准盘：1 波胆：2 大小球：3 让球盘：4  半场0 全场1 半全场2
            MarketBetTypeOrderInfo.AddRange(new MarketBetTypeOrderInfo[]{
                //让球盘（全场所有）                                            
                new MarketBetTypeOrderInfo{isShowAllOrSummary = true,isOpen = true,BetTypeName_CN = "让球盘（全场所有）",BetTypeName_EN = "Asian Handicap(Full)",BetTypeId = 4,MarketTmpType = 1,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 1},
                //让球盘（半场所有）       
                new MarketBetTypeOrderInfo{isShowAllOrSummary = true,isOpen = true,BetTypeName_CN = "让球盘（半场所有）",BetTypeName_EN = "Asian Handicap(Half)",BetTypeId = 4,MarketTmpType = 0,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 2},
                //大小球（半场0.5、1.5）  
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "大小球（半场0.5、1.5）",BetTypeName_EN = "Over/Under Goals(Half 0.5 1.5)",BetTypeId = 3,MarketTmpType = 0,GoalsEqualsOne = 0.5,GoalsEqualsTwo = 1.5,OrdNo = 3},
                //大小球（全场1.5、2.5）   
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "大小球（全场1.5、2.5）",BetTypeName_EN = "Over/Under Goals(Full 1.5 2.5)",BetTypeId = 3,MarketTmpType = 1,GoalsEqualsOne = 1.5,GoalsEqualsTwo = 2.5,OrdNo = 4},
                //波胆（全场所有）        
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "波胆（全场所有）",BetTypeName_EN = "Correct Score(Full)",BetTypeId = 2,MarketTmpType = 1,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 5},
                //波胆（半场所有）        
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "波胆（半场所有）",BetTypeName_EN = "Correct Score(Half)",BetTypeId = 2,MarketTmpType = 0,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 6},
                //标准盘（全场）
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "标准盘（全场）",BetTypeName_EN = "Match Odds(Full)",BetTypeId = 1,MarketTmpType = 1,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 7},
                //标准盘（半场）           
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "标准盘（半场）",BetTypeName_EN = "Match Odds(Half)",BetTypeId = 1,MarketTmpType = 0,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 8},
                //标准盘（半/全场）       
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = true,BetTypeName_CN = "标准盘（半/全场）",BetTypeName_EN = "Match Odds(Half/Full)",BetTypeId = 1,MarketTmpType = 2,GoalsEqualsOne = -1,GoalsEqualsTwo = -1,OrdNo = 9},
                //大小球（半场剩余的）     
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = false,BetTypeName_CN = "大小球（半场剩余的）",BetTypeName_EN = "Over/Under Goals(Half Others)",BetTypeId = 3,MarketTmpType = 0,GoalsEqualsOne = 0.5,GoalsEqualsTwo = 1.5,OrdNo = 10},
                //大小球（全场剩余的）
                new MarketBetTypeOrderInfo{isShowAllOrSummary = false,isOpen = false,BetTypeName_CN = "大小球（全场剩余的）",BetTypeName_EN = "Over/Under Goals(Full Others)",BetTypeId = 3,MarketTmpType = 1,GoalsEqualsOne = 1.5,GoalsEqualsTwo = 2.5,OrdNo = 11}
            });
            return MarketBetTypeOrderInfo;
        }

        public IList<MarketBetTypeOrderInfo> GetMarketBetTypeOrderInfoByMatchID(int MatchID)
        {
            var dsMatchMarkets = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var market = dsMatchMarkets.Match_Market.Where(x => x.MATCH_ID == MatchID && x.BET_TYPE_ID == 3);
            IList<DSMatchAndMarket.Match_MarketRow> halfUnderGoals = new List<DSMatchAndMarket.Match_MarketRow>();
            IList<DSMatchAndMarket.Match_MarketRow> fullUnderGoals = new List<DSMatchAndMarket.Match_MarketRow>();
            halfUnderGoals = market.Where(x => x.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.HalfTime && x.MARKET_FLAG == (int)MatchMarketFlagEnum.Over).OrderBy(x => x.SCOREB).Take(2).ToList<DSMatchAndMarket.Match_MarketRow>();
            fullUnderGoals = market.Where(x => x.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.FullTime && x.MARKET_FLAG == (int)MatchMarketFlagEnum.Over).OrderBy(x => x.SCOREB).Take(2).ToList<DSMatchAndMarket.Match_MarketRow>();

            IList<MarketBetTypeOrderInfo> marketBetTypeLst = QueryAllMarketBetTypeOrderInfo();
            #region 半场
            //半场
            if (halfUnderGoals.Count == 0)
            {
                marketBetTypeLst[2].BetTypeName_CN = "大小球（半场0）";
                marketBetTypeLst[2].BetTypeName_EN = "Over/Under Goals(Half 0)";
                marketBetTypeLst[2].GoalsEqualsOne = 0;
                marketBetTypeLst[2].GoalsEqualsTwo = 0;
                marketBetTypeLst[9].GoalsEqualsOne = 0;
                marketBetTypeLst[9].GoalsEqualsTwo = 0;
            }
            else if (halfUnderGoals.Count == 1)
            {
                marketBetTypeLst[2].BetTypeName_CN = string.Format("大小球（半场{0}）", halfUnderGoals[0].SCOREB);
                marketBetTypeLst[2].BetTypeName_EN = string.Format("Over/Under Goals(Half {0})", halfUnderGoals[0].SCOREB);
                marketBetTypeLst[2].GoalsEqualsOne = halfUnderGoals[0].SCOREB;
                marketBetTypeLst[2].GoalsEqualsTwo = 0;
                marketBetTypeLst[9].GoalsEqualsOne = halfUnderGoals[0].SCOREB;
                marketBetTypeLst[9].GoalsEqualsTwo = 0;
            }
            else
            {
                marketBetTypeLst[2].BetTypeName_CN = string.Format("大小球（半场{0}、{1}）", halfUnderGoals[0].SCOREB, halfUnderGoals[1].SCOREB);
                marketBetTypeLst[2].BetTypeName_EN = string.Format("Over/Under Goals(Half {0} {1})", halfUnderGoals[0].SCOREB, halfUnderGoals[1].SCOREB);
                marketBetTypeLst[2].GoalsEqualsOne = halfUnderGoals[0].SCOREB;
                marketBetTypeLst[2].GoalsEqualsTwo = halfUnderGoals[1].SCOREB;
                marketBetTypeLst[9].GoalsEqualsOne = halfUnderGoals[0].SCOREB;
                marketBetTypeLst[9].GoalsEqualsTwo = halfUnderGoals[1].SCOREB;
            }
            #endregion
            #region 全场
            //全场
            if (fullUnderGoals.Count == 0)
            {
                marketBetTypeLst[3].BetTypeName_CN = "大小球（全场0）";
                marketBetTypeLst[3].BetTypeName_EN = "Over/Under Goals(Full 0)";
                marketBetTypeLst[3].GoalsEqualsOne = 0;
                marketBetTypeLst[3].GoalsEqualsTwo = 0;
                marketBetTypeLst[10].GoalsEqualsOne = 0;
                marketBetTypeLst[10].GoalsEqualsTwo = 0;
            }
            else if (fullUnderGoals.Count == 1)
            {
                marketBetTypeLst[3].BetTypeName_CN = string.Format("大小球（全场{0}）", fullUnderGoals[0].SCOREB);
                marketBetTypeLst[3].BetTypeName_EN = string.Format("Over/Under Goals(Full {0})", fullUnderGoals[0].SCOREB);
                marketBetTypeLst[3].GoalsEqualsOne = fullUnderGoals[0].SCOREB;
                marketBetTypeLst[3].GoalsEqualsTwo = 0;
                marketBetTypeLst[10].GoalsEqualsOne = fullUnderGoals[0].SCOREB;
                marketBetTypeLst[10].GoalsEqualsTwo = 0;
            }
            else
            {
                marketBetTypeLst[3].BetTypeName_CN = string.Format("大小球（全场{0}、{1}）", fullUnderGoals[0].SCOREB, fullUnderGoals[1].SCOREB);
                marketBetTypeLst[3].BetTypeName_EN = string.Format("Over/Under Goals(Full {0} {1})", fullUnderGoals[0].SCOREB, fullUnderGoals[1].SCOREB);
                marketBetTypeLst[3].GoalsEqualsOne = fullUnderGoals[0].SCOREB;
                marketBetTypeLst[3].GoalsEqualsTwo = fullUnderGoals[1].SCOREB;
                marketBetTypeLst[10].GoalsEqualsOne = fullUnderGoals[0].SCOREB;
                marketBetTypeLst[10].GoalsEqualsTwo = fullUnderGoals[1].SCOREB;
            }
            #endregion

            return marketBetTypeLst;
        }

        public static IList<FootballCalendar> GetFootballCalendar()
        {
            List<FootballCalendar> fbCalList = new List<FootballCalendar>();
            FootballCalendar fbCal = null;
            for (int i = 0; i < 7; i++)
            {
                fbCal = new FootballCalendar();
                if (i == 0)
                    fbCal.WeekCalendarName = LangManager.GetString("Today");
                else if (i == 1)
                    fbCal.WeekCalendarName = LangManager.GetString("Tomorrow");
                else
                    fbCal.WeekCalendarName = LangManager.GetString(DateTime.Now.AddDays(i).DayOfWeek.ToString());
                fbCal.CalendarDate = DateTime.Now.AddDays(i);
                fbCal.CalendarDateId = i + 1;
                fbCalList.Add(fbCal);
            }
            return fbCalList;
        }

        public static IList<MarketFlagObject> GenerateMarketFlagList()
        {
            var marketFlagList = new List<MarketFlagObject>();
            marketFlagList.AddRange(new MarketFlagObject[]{
                    new MarketFlagObject{ MarketFlagId = 1, MarketFlagName = LangManager.GetString(((MatchMarketFlagEnum)1).ToString())},
                    new MarketFlagObject{ MarketFlagId = 2, MarketFlagName = LangManager.GetString(((MatchMarketFlagEnum)2).ToString())},
                    new MarketFlagObject{ MarketFlagId = 3, MarketFlagName = LangManager.GetString(((MatchMarketFlagEnum)3).ToString())}
                });
            return marketFlagList;
        }

        public static IEnumerable<string> GenerateInplayTabList()
        {
            var inplayTabList = new List<string>();
            inplayTabList.Add(LangManager.GetString("InPlay"));
            inplayTabList.Add(LangManager.GetString("Today"));
            inplayTabList.Add(LangManager.GetString("Tomorrow"));
            inplayTabList.Add(LangManager.GetString("YourInPlay"));
            return inplayTabList;
        }
    }


}