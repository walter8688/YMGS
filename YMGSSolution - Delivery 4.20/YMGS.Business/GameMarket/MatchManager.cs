using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.GameMarket;
using YMGS.Data.Presentation;
using YMGS.Data.Common;
using YMGS.Data.Entity;
using YMGS.Business.EventManage;
using YMGS.Business.MemberShip;
using System.Transactions;
using YMGS.Data.Resources;

namespace YMGS.Business.GameMarket
{
    public class MatchManager : BrBase
    {
        #region 查询比赛信息

        /// <summary>
        /// 查询比赛信息
        /// </summary>
        /// <param name="matchName">比赛名称</param>
        /// <param name="matchEventItem">赛事项目</param>
        /// <param name="matchEventZone">赛事区域</param>
        /// <param name="eventName">赛事名称</param>
        /// <param name="beginMatchDate">开始比赛时间</param>
        /// <param name="endMatchDate">结束比赛时间</param>
        /// <returns></returns>
        public static DsMatchList QueryMatchByParam(string matchName, Int32? matchEventItem, Int32? matchEventZone,
                            string eventName, DateTime? beginMatchDate, DateTime? endMatchDate)
        {
            return MatchDA.QueryMatchByParam(matchName, matchEventItem, matchEventZone,
                    eventName, beginMatchDate, endMatchDate);
        }

        /// <summary>
        /// 按照比赛ID查询比赛基本信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static DSMatch QueryMatchIncMarketById(int matchId)
        {
            DSMatch dsMatch = MatchDA.QueryMatchById(matchId);
            return dsMatch;
        }

        /// <summary>
        /// 按照比赛ID查询比赛基本信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static DSMatchMarket QueryMatchMarketById(int matchId)
        {
            return MatchDA.QueryMatchMarketById(matchId);
        }

        public static DSMatchMarket QueryMatchMarketByTmpId(int marketTmpId)
        {
            return MatchDA.QueryMatchMarketByTmpId(marketTmpId);
        }

        public static DSMatchMarket QueryMatchMarketByEventId(int EventId)
        {
            return MatchDA.QueryMatchMarketByEventId(EventId);
        }
        /// <summary>
        /// 查询当前可以参与下注的比赛和比赛市场信息[具体是否能下注，还需要参考玩法和配置信息]
        /// </summary>
        /// <returns></returns>
        public static DSMatchAndMarket QueryMatchAndMarketForBetting()
        {
            return MatchDA.QueryMatchAndMarketForBetting();
        }

        #endregion

        #region 终止比赛

        /// <summary>
        /// 终止比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void AbortMatch(int matchId, int curUserId)
        {
            MatchDA.AbortMatch(matchId, curUserId);
        }

        #endregion

        #region 激活比赛
        /// <summary>
        /// 激活比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void ActivateMatch(int matchId, int curUserId)
        {
            MatchDA.ActivateMatch(matchId, curUserId);
        }
        #endregion

        #region 开始下半场比赛
        /// <summary>
        /// 开始下半场比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        public static void SecHalfStartMatch(int matchId, int curUserId)
        {
            MatchDA.SecHalfStartMatch(matchId, curUserId);
        }
        #endregion

        #region 删除比赛

        /// <summary>
        /// 删除比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static void DeleteMatch(int matchId)
        {
            MatchDA.DeleteMatch(matchId);
        }

        #endregion

        #region 更新比赛时间

        /// <summary>
        /// 更新比赛时间
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="autoFreezeDate"></param>
        /// <param name="curUserId"></param>
        public static void ModifyMatchTime(int matchId, DateTime startDate, DateTime endDate, DateTime autoFreezeDate, int curUserId)
        {
            MatchDA.ModifyMatchTime(matchId,startDate,endDate,autoFreezeDate,curUserId);
        }

        #endregion

        #region 暂停比赛

        /// <summary>
        /// 暂停比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void SuspendMatch(int matchId, int curUserId)
        {
            MatchDA.SuspendMatch(matchId, curUserId);
        }

        #endregion

        #region 新增和更新比赛

        /// <summary>
        /// 新增比赛
        /// </summary>
        /// <param name="dsMatchInfo"></param>
        /// <param name="marketTemplateList"></param>
        /// <param name="homeTeamName"></param>
        /// <param name="clientTeamName"></param>
        public static int AddMatch(DSMatch dsMatchInfo, IList<int> marketTemplateList)
        {
            if(dsMatchInfo.TB_MATCH.Rows.Count == 0)
                throw new ArgumentException("请添加比赛详细信息!");

            DSMatch.TB_MATCHRow curMatch = dsMatchInfo.TB_MATCH[0];
            DSEventTeam dsEventTeamList = EventTeamManager.QueryEventTeamByEventID(curMatch.EVENT_ID);
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow = dsEventTeamList.TB_EVENT_TEAM.FindByEVENT_TEAM_ID(curMatch.EVENT_HOME_TEAM_ID);
            DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow = dsEventTeamList.TB_EVENT_TEAM.FindByEVENT_TEAM_ID(curMatch.EVENT_HOME_GUEST_ID);
            if (homeTeamRow == null || visitingTeamRow == null)
            {
                throw new ArgumentException("不能查询到当前比赛球队的详细信息!");
            }

            GenerateMatchMarket(dsMatchInfo, marketTemplateList, homeTeamRow, visitingTeamRow);
            return MatchDA.AddMatch(dsMatchInfo);
        }

        /// <summary>
        /// 更新比赛
        /// </summary>
        /// <param name="dsMatchInfo"></param>
        /// <param name="marketTemplateList"></param>
        /// <param name="homeTeamName"></param>
        /// <param name="clientTeamName"></param>
        public static void UpdateMatch(DSMatch dsMatchInfo, IList<int> marketTemplateList)
        {
            if (dsMatchInfo.TB_MATCH.Rows.Count == 0)
                throw new ArgumentException("请添加比赛详细信息!");

            DSMatch.TB_MATCHRow curMatch = dsMatchInfo.TB_MATCH[0];
            DSEventTeam dsEventTeamList = EventTeamManager.QueryEventTeamByEventID(curMatch.EVENT_ID);
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow = dsEventTeamList.TB_EVENT_TEAM.FindByEVENT_TEAM_ID(curMatch.EVENT_HOME_TEAM_ID);
            DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow = dsEventTeamList.TB_EVENT_TEAM.FindByEVENT_TEAM_ID(curMatch.EVENT_HOME_GUEST_ID);
            if (homeTeamRow == null || visitingTeamRow == null)
            {
                throw new ArgumentException("不能查询到当前比赛球队的详细信息!");
            }

            GenerateMatchMarket(dsMatchInfo, marketTemplateList, homeTeamRow, visitingTeamRow);
            MatchDA.UpdateMatch(dsMatchInfo);
        }
        
        /// <summary>
        /// 依据市场模板生成比赛的市场
        /// </summary>
        /// <param name="dsMatchInfo"></param>
        /// <param name="marketTemplateList"></param>
        private static void GenerateMatchMarket(DSMatch dsMatchInfo, IList<int> marketTemplateList,
                    DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            var templateDS = MarketTemplateManager.QueryMarketTemplateByParam(-1, string.Empty, -1);
            dsMatchInfo.TB_MATCH_MARKET.Clear();

            for (int i = 0; i < marketTemplateList.Count; i++)
            {
                GenerateMatchMarketItem(templateDS, dsMatchInfo, marketTemplateList[i], homeTeamRow, visitingTeamRow);
            }            
        }

        /// <summary>
        /// 生成市场项
        /// </summary>
        /// <param name="dsTemplate"></param>
        /// <param name="dsMatchInfo"></param>
        /// <param name="templateId"></param>
        private static void GenerateMatchMarketItem(DSMarketTemplate dsTemplate, DSMatch dsMatchInfo, int templateId,
                DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            var templateItem = dsTemplate.TB_MARKET_TEMPLATE.Where(r => r.MARKET_TMP_ID == templateId).FirstOrDefault();
            if (templateItem == null)
                return;
            var betType = (BetTypeEnum)templateItem.BET_TYPE_ID;

            switch (betType)
            {
                case BetTypeEnum.MatchOdds:
                    GenerateMatchOddsMarket(templateItem, dsMatchInfo, templateId, homeTeamRow, visitingTeamRow);
                    break;
                case BetTypeEnum.CorrectScore:
                    GenerateCorrectScoreMarket(templateItem, dsMatchInfo, templateId, homeTeamRow, visitingTeamRow);               
                    break;
                case BetTypeEnum.OverUnderGoal:
                    GenerateOverUnderMarket(templateItem, dsMatchInfo, templateId, homeTeamRow, visitingTeamRow);                                   
                    break;
                case BetTypeEnum.AsianHandicap:
                    GenerateAsianHandicapMarket(templateItem, dsMatchInfo, templateId, homeTeamRow, visitingTeamRow);               
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 生成标准盘的市场
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dsMatchInfo"></param>
        /// <param name="templateId"></param>
        private static void GenerateMatchOddsMarket(DSMarketTemplate.TB_MARKET_TEMPLATERow template, DSMatch dsMatchInfo, int templateId,
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            string homeTeamCnName = homeTeamRow.EVENT_TEAM_NAME;
            string homeTeamEnName = homeTeamRow.IsEVENT_TEAM_NAME_ENNull()?string.Empty:homeTeamRow.EVENT_TEAM_NAME_EN;
            string visitingTeamCnName = visitingTeamRow.EVENT_TEAM_NAME;
            string visitingTeamEnName = visitingTeamRow.IsEVENT_TEAM_NAME_ENNull()?string.Empty:visitingTeamRow.EVENT_TEAM_NAME_EN;

            var marketTmpType = (MarketTemplateTypeEnum)template.Market_Tmp_Type;
            if (marketTmpType == MarketTemplateTypeEnum.FullTime || marketTmpType == MarketTemplateTypeEnum.HalfTime)
            {
                var homeWinItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                homeWinItem.MARKET_NAME = homeTeamCnName;
                homeWinItem.MARKET_NAME_EN = homeTeamEnName;
                homeWinItem.MARKET_TMP_ID = templateId;
                homeWinItem.MARKET_FLAG = (int)MatchMarketFlagEnum.HomeTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(homeWinItem);

                var clientWinItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                clientWinItem.MARKET_NAME = visitingTeamCnName;
                clientWinItem.MARKET_NAME_EN = visitingTeamEnName;
                clientWinItem.MARKET_TMP_ID = templateId;
                clientWinItem.MARKET_FLAG = (int)MatchMarketFlagEnum.VisitingTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(clientWinItem);

                var theDrawWinItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                theDrawWinItem.MARKET_NAME = CommConstant.TheDrawString;
                theDrawWinItem.MARKET_NAME_EN = CommConstant.TheDrawEnString;
                theDrawWinItem.MARKET_TMP_ID = templateId;
                theDrawWinItem.MARKET_FLAG = (int)MatchMarketFlagEnum.TheDraw;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(theDrawWinItem);
            }

            #region 半场/全场标准盘, 需要生成9个市场
            if (marketTmpType == MarketTemplateTypeEnum.HalfAndFullTime)
            {
                var marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = homeTeamCnName + "/" + homeTeamCnName;
                marketItem.MARKET_NAME_EN = homeTeamEnName + "/" + homeTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.HomeTeamWin_HomeTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = homeTeamCnName + "/" + visitingTeamCnName;
                marketItem.MARKET_NAME_EN = homeTeamEnName + "/" + visitingTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.HomeTeamWin_VisitingTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = homeTeamCnName + "/" + CommConstant.TheDrawString;
                marketItem.MARKET_NAME_EN = homeTeamEnName + "/" + CommConstant.TheDrawEnString;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.HomeTeamWin_TheDraw;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = visitingTeamCnName + "/" + homeTeamCnName;
                marketItem.MARKET_NAME_EN = visitingTeamEnName + "/" + homeTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.VisitingTeamWin_HomeTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = visitingTeamCnName + "/" + visitingTeamCnName;
                marketItem.MARKET_NAME_EN = visitingTeamEnName + "/" + visitingTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.VisitingTeamWin_VisitingTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = visitingTeamCnName + "/" + CommConstant.TheDrawString;
                marketItem.MARKET_NAME_EN = visitingTeamEnName + "/" + CommConstant.TheDrawEnString;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.VisitingTeamWin_TheDraw;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = CommConstant.TheDrawString + "/" + homeTeamCnName;
                marketItem.MARKET_NAME_EN = CommConstant.TheDrawEnString + "/" + homeTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.TheDraw_HomeTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = CommConstant.TheDrawString + "/" + visitingTeamCnName;
                marketItem.MARKET_NAME_EN = CommConstant.TheDrawEnString + "/" + visitingTeamEnName;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.TheDraw_VisitingTeamWin;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

                marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
                marketItem.MARKET_NAME = CommConstant.TheDrawString + "/" + CommConstant.TheDrawString;
                marketItem.MARKET_NAME_EN = CommConstant.TheDrawEnString + "/" + CommConstant.TheDrawEnString;
                marketItem.MARKET_TMP_ID = templateId;
                marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.TheDraw_TheDraw;
                dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);
            }
            #endregion
        }

        /// <summary>
        /// 生成波胆的市场
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dsMatchInfo"></param>
        /// <param name="templateId"></param>
        private static void GenerateCorrectScoreMarket(DSMarketTemplate.TB_MARKET_TEMPLATERow template, DSMatch dsMatchInfo, int templateId,
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            var marketTmpType = (MarketTemplateTypeEnum)template.Market_Tmp_Type;
            var marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
            marketItem.MARKET_NAME = template.MARKET_TMP_NAME;
            marketItem.MARKET_NAME_EN = template.IsMARKET_TMP_NAME_ENNull() ? string.Empty : template.MARKET_TMP_NAME_EN;
            marketItem.MARKET_TMP_ID = templateId;
            marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.PreciseScore;
            marketItem.SCOREA = template.HOMESCORE;
            marketItem.SCOREB = template.AWAYSCORE;
            dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);
        }

        /// <summary>
        /// 生成大小球的市场
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dsMatchInfo"></param>
        /// <param name="templateId"></param>
        private static void GenerateOverUnderMarket(DSMarketTemplate.TB_MARKET_TEMPLATERow template, DSMatch dsMatchInfo, int templateId,
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            var marketTmpType = (MarketTemplateTypeEnum)template.Market_Tmp_Type;
            var marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
            marketItem.MARKET_NAME = string.Format(CommConstant.MarketOverFormatString, template.GOALS);
            marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketOverEnFormatString, template.GOALS);
            marketItem.MARKET_TMP_ID = templateId;
            marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.Over;
            marketItem.SCOREB = template.GOALS;
            dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

            marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
            marketItem.MARKET_NAME = string.Format(CommConstant.MarketUnderFormatString, template.GOALS);
            marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketUnderEnFormatString, template.GOALS);
            marketItem.MARKET_TMP_ID = templateId;
            marketItem.SCOREB = template.GOALS; 
            marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.Under;
            dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);
        }

        /// <summary>
        /// 生成让球的市场
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dsMatchInfo"></param>
        /// <param name="templateId"></param>
        private static void GenerateAsianHandicapMarket(DSMarketTemplate.TB_MARKET_TEMPLATERow template, DSMatch dsMatchInfo, int templateId,
            DSEventTeam.TB_EVENT_TEAMRow homeTeamRow, DSEventTeam.TB_EVENT_TEAMRow visitingTeamRow)
        {
            string homeTeamCnName = homeTeamRow.EVENT_TEAM_NAME;
            string homeTeamEnName = homeTeamRow.IsEVENT_TEAM_NAME_ENNull() ? string.Empty : homeTeamRow.EVENT_TEAM_NAME_EN;
            string visitingTeamCnName = visitingTeamRow.EVENT_TEAM_NAME;
            string visitingTeamEnName = visitingTeamRow.IsEVENT_TEAM_NAME_ENNull() ? string.Empty : visitingTeamRow.EVENT_TEAM_NAME_EN;

            var marketTmpType = (MarketTemplateTypeEnum)template.Market_Tmp_Type;
            var marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
            if (template.IsSCOREANull())
            {
                marketItem.MARKET_NAME = string.Format(CommConstant.MarketAsianHandicapFormatOneString, homeTeamCnName, template.SCOREB.ToString());
                marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketAsianHandicapFormatOneString, homeTeamEnName, template.SCOREB.ToString());
                marketItem.SCOREB = template.SCOREB;
            }
            else
            {
                marketItem.MARKET_NAME = string.Format(CommConstant.MarketAsianHandicapFormatTwoString, homeTeamCnName, template.SCOREA.ToString(), template.SCOREB.ToString());
                marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketAsianHandicapFormatTwoString, homeTeamEnName, template.SCOREA.ToString(), template.SCOREB.ToString());
                marketItem.SCOREA = template.SCOREA;
                marketItem.SCOREB = template.SCOREB;
            }
            marketItem.MARKET_TMP_ID = templateId;
            marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.Over;
            dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);

            marketItem = dsMatchInfo.TB_MATCH_MARKET.NewTB_MATCH_MARKETRow();
            if (template.IsSCOREANull())
            {
                marketItem.SCOREB = template.SCOREB*-1;
                marketItem.MARKET_NAME = string.Format(CommConstant.MarketAsianHandicapFormatOneString, visitingTeamCnName, (template.SCOREB * -1).ToString());
                marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketAsianHandicapFormatOneString, visitingTeamEnName, (template.SCOREB * -1).ToString());
            }
            else
            {
                marketItem.SCOREA = template.SCOREA*-1;
                marketItem.SCOREB = template.SCOREB*-1;
                marketItem.MARKET_NAME = string.Format(CommConstant.MarketAsianHandicapFormatTwoString, visitingTeamCnName, (template.SCOREA * -1).ToString(), (template.SCOREB * -1).ToString());
                marketItem.MARKET_NAME_EN = string.Format(CommConstant.MarketAsianHandicapFormatTwoString, visitingTeamEnName, (template.SCOREA * -1).ToString(), (template.SCOREB * -1).ToString());
            }
            marketItem.MARKET_TMP_ID = templateId;
            marketItem.MARKET_FLAG = (int)MatchMarketFlagEnum.Under;
            dsMatchInfo.TB_MATCH_MARKET.AddTB_MATCH_MARKETRow(marketItem);
        }
        #endregion

        #region 推荐比赛或取消比赛

        /// <summary>
        ///推荐比赛或取消比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void RecommendOrCancelMatch(int matchId, int curUserId, bool bIsRecommend)
        {
            MatchDA.RecommendOrCancelMatch(matchId, curUserId, bIsRecommend);
        }

        #endregion

        #region 正常比赛
        /// <summary>
        /// 正常比赛
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void NoramlMatch(int matchID, int lastUpdateUserID)
        {
            MatchDA.NoramlMatch(matchID, lastUpdateUserID);
        }
        #endregion

        #region 下注
        /// <summary>
        /// 下注
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="CurrentLoginUser">当前用户</param>
        /// <param name="MatchMarcketInfolist">投注</param>
        /// <param name="layMatchMarcketInfoList">受注</param>
        /// <returns></returns>
        public static string PlaceBet(decimal amount, int CurUserid, HashSet<MatchMarcketInfo> MatchMarcketInfolist, HashSet<MatchMarcketInfo> layMatchMarcketInfoList)
        {
            MatchMarcketInfo exceptionItem = null;
            int flag = 0;
            try
            {
                int userid = CurUserid;

                DSUserFund uf = UserFundManager.QueryUserFund(userid);
                if (uf.TB_USER_FUND[0].CUR_FUND < amount)
                {
                    return string.Format(BusinessResources.FundIsNotEnough, amount);
                }
                TransactionOptions transOption = new TransactionOptions();
                transOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOption))
                {                    
                    if (MatchMarcketInfolist.Count > 0)
                    {
                        foreach (MatchMarcketInfo item in MatchMarcketInfolist)
                        {
                            exceptionItem = item;
                            DSExchange_Back dsback = new DSExchange_Back();
                            DSExchange_Back.TB_EXCHANGE_BACKRow backrow = dsback.TB_EXCHANGE_BACK.NewTB_EXCHANGE_BACKRow();
                            backrow.EXCHANGE_BACK_ID = 0;
                            backrow.MARKET_ID = item.MARKET_ID;
                            backrow.MATCH_ID = item.MATCH_ID;
                            backrow.ODDS = decimal.Parse(item.odds);
                            backrow.BET_AMOUNTS = backrow.MATCH_AMOUNTS = decimal.Parse(item.AMOUNTS);
                            backrow.TRADE_USER = userid;
                            backrow.STATUS = 1;
                            backrow.MATCH_TYPE = item.MATCHTYPE.ToString();
                            MatchManager.ExchangeBack("edit", backrow);
                        }
                    }
                    flag = 1;
                    foreach (MatchMarcketInfo item in layMatchMarcketInfoList)
                    {
                        exceptionItem = item;
                        DSExchangeLay dslay = new DSExchangeLay();
                        DSExchangeLay.TB_EXCHANGE_LAYRow layrow = dslay.TB_EXCHANGE_LAY.NewTB_EXCHANGE_LAYRow();
                        layrow.EXCHANGE_LAY_ID = 0;
                        layrow.MARKET_ID = item.MARKET_ID;
                        layrow.MATCH_ID = item.MATCH_ID;
                        layrow.ODDS = decimal.Parse(item.odds);
                        layrow.BET_AMOUNTS = layrow.MATCH_AMOUNTS = decimal.Parse(item.AMOUNTS);
                        layrow.TRADE_USER = userid;
                        layrow.STATUS = 1;
                        layrow.MATCH_TYPE = item.MATCHTYPE.ToString();
                        MatchManager.ExchangeLay("edit", layrow);
                    }
                    transaction.Complete();

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                string exceptionstr = "[" + exceptionItem.MATCH_NAME + "-" + exceptionItem.MARKET_NAME + "(" + exceptionItem.MARKET_TMP_NAME + ")" + "]";
                return ex.Message.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0]+"," + exceptionstr;
            }
        }
        #endregion

        #region 投注
        /// <summary>
        /// 投注
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void ExchangeBack(string flag, DSExchange_Back.TB_EXCHANGE_BACKRow backrow)
        {
            MatchDA.ExchangeBack(flag, backrow);
        }
        #endregion

        #region 受注
        /// <summary>
        /// 受注
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void ExchangeLay(string flag, DSExchangeLay.TB_EXCHANGE_LAYRow layrow)
        {
            MatchDA.ExchangeLay(flag, layrow);
        }
        #endregion

        #region 录入比分
        /// <summary>
        /// 录入比分
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="score"></param>
        public static void RecordMatchScore(int matchId, FootBallMatchScore score)
        {
            MatchDA.RecordMatchScore(matchId, score);
        }
        #endregion

        #region 开始比赛
        /// <summary>
        /// 开始比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void StartMatch(int matchId, int lastUpdateUserId)
        {
            MatchDA.StartMatch(matchId, lastUpdateUserId);
        }
        #endregion

        #region 半场结束
        /// <summary>
        /// 比赛半场结束
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void HalfEndMatch(int matchId, int lastUpdateUserId)
        {
            MatchDA.HalfEndMatch(matchId, lastUpdateUserId);
        }
        #endregion

        #region 全场结束
        /// <summary>
        /// 比赛全场结束
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void FullEndMatch(int matchId, int lastUpdateUserId)
        {
            MatchDA.FullEndMatch(matchId, lastUpdateUserId);
        }
        #endregion

        #region 封盘比赛
        /// <summary>
        /// 封盘比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void FreezingMatch(int matchId, int lastUpdateUserId)
        {
            MatchDA.FreezingMatch(matchId, lastUpdateUserId);
        }
        #endregion

        #region 获取投注Top3
        public static DSCachedExchangeBack GetCachedExchangeBack()
        {
            return MatchDA.GetCachedExchangeBack();
        }
        #endregion

        #region 获取受注Top3
        public static DSCachedExchangeLay GetCachedExchangeLay()
        {
            return MatchDA.GetCachedExchangeLay();
        }
        #endregion
    }
}
