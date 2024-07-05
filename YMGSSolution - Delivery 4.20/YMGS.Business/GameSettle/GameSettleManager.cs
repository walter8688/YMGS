using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Business.GameMarket;
using YMGS.Data.Common;
using YMGS.Data.Presentation;
using YMGS.DataAccess.GameSettle;
using YMGS.Business.GameSettle.SportMatchCalcRule;
using System.Transactions;

namespace YMGS.Business.GameSettle
{
    public class GameSettleManager : BrBase
    {
        /// <summary>
        /// 验证是否可以进行比赛的结算
        /// </summary>
        /// <param name="dsMatch"></param>
        /// <returns></returns>
        private static string ValidSportMatch(DSMatch dsMatch,bool bIsHalfCalc)
        {
            if (dsMatch.TB_MATCH.Rows.Count == 0)
                return "未能查询到当前比赛详细信息!";
            var curMatch = dsMatch.TB_MATCH[0];

            MatchStatusEnum matchStatus = (MatchStatusEnum)curMatch.STATUS;
            MatchAdditionalStatusEnum matchAddiStatus = (MatchAdditionalStatusEnum)curMatch.ADDITIONALSTATUS;

            if (bIsHalfCalc)
            {
                
                if (!(matchStatus == MatchStatusEnum.HalfTimeFinished
                    || matchStatus == MatchStatusEnum.SecHalfStarted
                    || matchStatus == MatchStatusEnum.FullTimeFinished))                    
                {
                    return "只有半场已结束,并且全场没有结束时才能进行半场结算!";
                }
                if (curMatch.IsHOME_FIR_HALF_SCORENull() || curMatch.IsGUEST_FIR_HALF_SCORENull() ||
                    curMatch.HOME_FIR_HALF_SCORE == -1 || curMatch.GUEST_FIR_HALF_SCORE == -1)
                {
                    return "未输入半场比分，不能进行结算!";
                }

                if (!curMatch.IsSETTLE_STATUSNull())
                {
                    var settleStatus = (MatchSettleStatus)curMatch.SETTLE_STATUS;
                    if (settleStatus == MatchSettleStatus.HalfSettled || settleStatus == MatchSettleStatus.FullSettled)
                        return "半场已结算过，不能进行重复结算!";
                }

            }
            else
            {
                if (matchStatus != MatchStatusEnum.FullTimeFinished)
                {
                    return "只有全场结束时才能进行全场结算!";
                }
                if (curMatch.IsGUEST_SEC_HALF_SCORENull() || curMatch.IsHOME_SEC_HALF_SCORENull() ||
                    curMatch.GUEST_SEC_HALF_SCORE == -1 || curMatch.HOME_SEC_HALF_SCORE == -1)
                {
                    return "未输入全场比分，不能进行结算!";
                }

                if (!curMatch.IsSETTLE_STATUSNull())
                {
                    var settleStatus = (MatchSettleStatus)curMatch.SETTLE_STATUS;
                    if (settleStatus == MatchSettleStatus.FullSettled)
                        return "全场已结算过，不能进行重复结算!";
                }
            }

            if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
            {
                return "封盘或者暂停比赛不能进行结算!";
            }

            return string.Empty;
        }

        /// <summary>
        /// 体育类比赛半场结算
        /// </summary>
        /// <param name="iMatchId">比赛Id</param>
        /// <param name="iCurrentId">当前用户Id</param>
        /// <param name="bIsHalfCalc">是否半场结算</param>
        /// <returns>返回信息</returns>
        public static string CalcSportMatchGame(int iMatchId,int iCurrentId,bool bIsHalfCalc, 
             InitProgressDelegate initPgbDelegate, UpdateProgressDelegate updatePgbDelegate,bool isExecuteInitPgbDelegate)
        {
            DSMatch dsMatch = MatchManager.QueryMatchIncMarketById(iMatchId);
            var curMatch = dsMatch.TB_MATCH[0];
            string strMessage = ValidSportMatch(dsMatch, bIsHalfCalc);
            if (!string.IsNullOrEmpty(strMessage))
                return strMessage;
            
            LogHelper.LogInformation("获取当前需要进行半场结算的所有撮合交易记录");
            DSExchangeDealList dsExchangeDeal;
            if(bIsHalfCalc)
                dsExchangeDeal = MatchExchangeDealManager.QueryExchangeDeal(iMatchId, 0);
            else
                dsExchangeDeal = MatchExchangeDealManager.QueryExchangeDeal(iMatchId, 1);
            LogHelper.LogInformation("当前撮合交易记录条数:" + dsExchangeDeal.EXCHANGE_DEAL_LIST.Count.ToString());

            if (isExecuteInitPgbDelegate)
            {
                initPgbDelegate();
            }

            updatePgbDelegate("开始结算", 1);
            //System.Threading.Thread.Sleep(2000);

            try
            {
                DateTime preTime = DateTime.Now;
                DateTime curTime = DateTime.Now;
                int iTotalCount = dsExchangeDeal.EXCHANGE_DEAL_LIST.Count;
                int iCurCount = 0;
                int logId = -1;

                updatePgbDelegate("正在进行比赛结算", 1);
                LogHelper.LogInformation("计算:按照撮合交易记录依次进行结算");

                TransactionOptions transOption = new TransactionOptions();
                transOption.IsolationLevel = IsolationLevel.ReadUncommitted;
                transOption.Timeout = new TimeSpan(0, 30, 0);
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOption))
                {
                    logId = GameSettlementLogDA.CreateMatchSettlementLog(iMatchId, 1, bIsHalfCalc ? 0 : 1, iCurrentId,curMatch.STATUS);
                    #region 计算：按照撮合交易记录依次进行结算
                    foreach (DSExchangeDealList.EXCHANGE_DEAL_LISTRow curExchangeDeal in dsExchangeDeal.EXCHANGE_DEAL_LIST)
                    {
                        try
                        {
                            MatchMarketFlagEnum matchMarketFlag = (MatchMarketFlagEnum)curExchangeDeal.MARKET_FLAG;
                            BetTypeEnum betTypeEnum = (BetTypeEnum)curExchangeDeal.BET_TYPE_ID;
                            IMatchSettelStrategy matchSettleStrategy = GetMarketSettleStrategy(matchMarketFlag, betTypeEnum);

                            MatchSettleResultInfo matchSettleResult = matchSettleStrategy.CalculateMatch(curMatch, curExchangeDeal);
                            MatchSettleDA.CalcSportGameMatchExchangeDeal(curMatch.MATCH_ID, matchSettleResult.ExchangeDealId,
                                               matchSettleResult.IsBuyerWin, curExchangeDeal.EXCHANGE_BACK_ID, curExchangeDeal.EXCHANGE_LAY_ID,
                                                matchSettleResult.BetCalculatePercent, curExchangeDeal.DEAL_AMOUNT, curExchangeDeal.ODDS,
                                                iCurrentId, curMatch.MATCH_NAME, curExchangeDeal.MARKET_TMP_NAME, curExchangeDeal.MARKET_NAME,logId);
                            iCurCount++;
                            System.Threading.Thread.Sleep(10);
                            curTime = DateTime.Now;
                            var timeSpan = curTime.Subtract(preTime).TotalMilliseconds;
                            if (timeSpan > 500)
                            {
                                var dblPercent = iCurCount / (iTotalCount * 1.0);
                                int iPgbValue = (int)(dblPercent * 80);
                                updatePgbDelegate("正在进行比赛结算!", iPgbValue);
                                preTime = DateTime.Now;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError("当前撮合交易记录ID" + curExchangeDeal.EXCHANGE_DEAL_ID.ToString()+
                                                ex.Message);
                            throw ex;
                        }
                    }
                    #endregion

                    //更新结算状态
                    int settleStatus = bIsHalfCalc ? (int)MatchSettleStatus.HalfSettled : (int)MatchSettleStatus.FullSettled;
                    GameSettleManager.UpdateMatchSettleStatus(iMatchId, settleStatus);

                    updatePgbDelegate("正在取消未撮合成功的交易!", 81);                                        
                    LogHelper.LogInformation("开始取消未撮合成功的交易!");
                    MatchSettleDA.UnfreezeSportMatchDealFund(iMatchId, iCurrentId, bIsHalfCalc ? 0 : 1,logId);

                    updatePgbDelegate("正在扣除对冲释放资金",90);
                    LogHelper.LogInformation("正在扣除对冲释放资金");
                    MatchSettleDA.RollbackRealHedgeFund(iMatchId, iCurrentId, bIsHalfCalc ? 0 : 1, 1,logId);
                    transaction.Complete();
                }
                updatePgbDelegate("结算完成!", 100);
                LogHelper.LogInformation("结算完成!");
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得玩法对应的结算策略对象
        /// </summary>
        /// <param name="matchMarketFlag"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        private static IMatchSettelStrategy GetMarketSettleStrategy(MatchMarketFlagEnum matchMarketFlag, BetTypeEnum betType)
        {
            IMatchSettelStrategy matchSettleStrategy = null;
            switch (matchMarketFlag)
            {
                #region 半场标准盘、全场标准盘
                case MatchMarketFlagEnum.HomeTeamWin:
                    matchSettleStrategy = new HomeTeamWinStrategy();
                    break;
                case MatchMarketFlagEnum.VisitingTeamWin:
                    matchSettleStrategy = new VisitingTeamWinStrategy();
                    break;
                case MatchMarketFlagEnum.TheDraw:
                    matchSettleStrategy = new TheDrawStrategy();
                    break;
                #endregion

                #region 波胆
                case MatchMarketFlagEnum.PreciseScore:
                    matchSettleStrategy = new PreciseScoreStrategy();
                    break;
                #endregion

                #region 大小球和让球盘
                case MatchMarketFlagEnum.Over:
                    if (betType == BetTypeEnum.OverUnderGoal)
                        matchSettleStrategy = new OverUnderBetForOverStrategy();
                    else if (betType == BetTypeEnum.AsianHandicap)
                        matchSettleStrategy = new AsianHandicapForOverStrategy();
                    else
                        matchSettleStrategy = new DefaultStrategy();
                    break;
                case MatchMarketFlagEnum.Under:
                    if (betType == BetTypeEnum.OverUnderGoal)
                        matchSettleStrategy = new OverUnderBetForUnderStrategy();
                    else if (betType == BetTypeEnum.AsianHandicap)
                        matchSettleStrategy = new AsianHandicapForUnderStrategy();
                    else
                        matchSettleStrategy = new DefaultStrategy();
                    break;

                #endregion

                default:
                    matchSettleStrategy = new DefaultStrategy();
                    break;
            }
            return matchSettleStrategy;
        }

        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="settleStatus"></param>
        public static void UpdateMatchSettleStatus(int matchId, int settleStatus)
        {
            MatchSettleDA.UpdateMatchSettleStatus(matchId, settleStatus);
        }

    }
}
