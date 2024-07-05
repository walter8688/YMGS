using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.DataAccess.GameSettle;
using YMGS.Framework;
using System.Transactions;
using YMGS.Business.GameMarket;

namespace YMGS.Business.GameSettle
{
    public class RollbackSettlementManager
    {
        /// <summary>
        /// 验证体育比赛是否可以进行重新结算
        /// </summary>
        /// <param name="dsMatch"></param>
        /// <returns></returns>
        private static string CheckSportMatchRollbackSettlement(int matchId,int calcFlag)
        {
            var dsMatch = MatchManager.QueryMatchIncMarketById(matchId);
            if (dsMatch.TB_MATCH.Rows.Count == 0)
                return "未能查询到当前比赛详细信息!";
            var curMatch = dsMatch.TB_MATCH[0];

            MatchStatusEnum matchStatus = (MatchStatusEnum)curMatch.STATUS;
            MatchSettleStatus matchSettleStatus = (MatchSettleStatus)curMatch.SETTLE_STATUS;
            MatchAdditionalStatusEnum matchAddiStatus = (MatchAdditionalStatusEnum)curMatch.ADDITIONALSTATUS;

            if (calcFlag == 0)
            {
                if (!(matchSettleStatus == MatchSettleStatus.HalfSettled))
                {
                    return "只有半场已结算的比赛才能进行半场的重新结算";
                }
                if (curMatch.IsHOME_FIR_HALF_SCORENull() || curMatch.IsGUEST_FIR_HALF_SCORENull() ||
                    curMatch.HOME_FIR_HALF_SCORE == -1 || curMatch.GUEST_FIR_HALF_SCORE == -1)
                {
                    return "未输入半场比分，不能进行重新结算!";
                }
            }
            else
            {
                if (!(matchStatus == MatchStatusEnum.FinishedCalculation && matchSettleStatus == MatchSettleStatus.FullSettled))
                {
                    return "只有全场已结算的比赛才能进行重新结算";
                }
                if (curMatch.IsGUEST_SEC_HALF_SCORENull() || curMatch.IsHOME_SEC_HALF_SCORENull() ||
                    curMatch.GUEST_SEC_HALF_SCORE == -1 || curMatch.HOME_SEC_HALF_SCORE == -1)
                {
                    return "未输入全场比分，不能进行重新结算!";
                }
            }

            if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
            {
                return "封盘或者暂停比赛不能进行重新结算!";
            }

            return string.Empty;
        }

        private static string CheckChampEventRollbackSettlement(int champEventId)
        {
            DSChampEvent dsChampEvent = ChampEventManager.QueryChampEventByKey(champEventId);
            DSChampWinMemList dsChampWinMember = ChampEventManager.QueryChampWinMemList(champEventId);

            if (dsChampEvent.TB_Champ_Event.Rows.Count == 0)
                return "未能查询到当前冠军赛事的详细信息!";

            var curChampEvent = dsChampEvent.TB_Champ_Event[0];
            //判断当前状态是否属于激活中
            if (curChampEvent.Champ_Event_Status != (int)ChampEventStatusEnum.Calculated)
            {
                return "只有已结算状态的冠军赛事才能进行重新比赛结算!";
            }

            if (dsChampWinMember._DSChampWinMemList.Rows.Count == 0)
            {
                return "只有录入冠军赛事的冠军成绩后才能进行比赛结算!";
            }

            return string.Empty;
        }

        private static string CheckIsCanRollbackSettlement(int matchId, int matchType, int calcFlag)
        {
            if (matchType == 1)
                return CheckSportMatchRollbackSettlement(matchId, calcFlag);
            else
                return CheckChampEventRollbackSettlement(matchId);
        }

        /// <summary>
        /// 结算撤销(足球、冠军比赛)
        /// </summary>
        /// <param name="matchId">比赛ID</param>
        /// <param name="matchType">比赛类型: 1 体育比赛 2 冠军比赛</param>
        /// <param name="calcFlag">结算类型：　０　半场　１　全场</param>
        /// <param name="initPgbDelegate"></param>
        /// <param name="updatePgbDelegate"></param>
        /// <returns></returns>
        public static string RollbackSettlement(int matchId,int matchType,int calcFlag,
             InitProgressDelegate initPgbDelegate, UpdateProgressDelegate updatePgbDelegate)
        {
            LogHelper.LogInformation("开始重新结算");
            //首先判断该比赛是否可以进行重新结算
            LogHelper.LogInformation("首先判断该比赛是否可以进行重新结算!");
            string strMessage = CheckIsCanRollbackSettlement(matchId, matchType, calcFlag);
            if (!string.IsNullOrEmpty(strMessage))
                return strMessage;

            LogHelper.LogInformation("获得比赛结算时的员工列表!");
            var dsMatchInfo = MatchSettleRollbackDA.QuerySettlementRollbackUsers(matchId, calcFlag, matchType);
            if (dsMatchInfo.Log.Rows.Count == 0 ||
                    dsMatchInfo.Log[0].IsLog_IdNull())
                return "当前比赛没有记录详细的比赛日志，所以不能进行重新结算!";
            
            //显示进度条
            initPgbDelegate();
            updatePgbDelegate("开始已结算记录的撤销", 1);
            //System.Threading.Thread.Sleep(2000);
            try
            {
                DateTime preTime = DateTime.Now;
                DateTime curTime = DateTime.Now;
                int iTotalCount = dsMatchInfo.Users.Rows.Count;
                int iCurCount = 0;
                int logId = dsMatchInfo.Log[0].Log_Id;

                LogHelper.LogInformation(string.Format("结算撤销:当前待结算撤销员工数{0}",iCurCount.ToString()));
                LogHelper.LogInformation(string.Format("结算撤销:当前的日志ID为{0}", logId.ToString()));

                TransactionOptions transOption = new TransactionOptions();
                transOption.IsolationLevel = IsolationLevel.ReadUncommitted;
                //可能有些操作相当费时，故超时设置为30
                transOption.Timeout = new TimeSpan(0, 30, 0);
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOption))
                {
                    #region 依次对每个员工的已结算记录进行结算撤销
                    foreach (var curUser in dsMatchInfo.Users)
                    {
                        try
                        {
                            MatchSettleRollbackDA.RollbackSettlement(matchId, calcFlag, matchType, curUser.User_Id, logId);
                            iCurCount++;
                            System.Threading.Thread.Sleep(10);
                            curTime = DateTime.Now;
                            var timeSpan = curTime.Subtract(preTime).TotalMilliseconds;
                            if (timeSpan > 500)
                            {
                                var dblPercent = iCurCount / (iTotalCount * 1.0);
                                int iPgbValue = (int)(dblPercent * 90);
                                updatePgbDelegate("正在进行已结算记录的撤销", iPgbValue);
                                preTime = DateTime.Now;
                            }
                            //System.Threading.Thread.Sleep(2000);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError("结算撤销:当前员工ID" + curUser.User_Id.ToString() + ex.Message);
                            throw ex;
                        }
                    }
                    #endregion

                    updatePgbDelegate("更新比赛状态为结算前状态", 91);
                    MatchSettleRollbackDA.RollbackSettlementMatchStatus(matchId, matchType, logId, calcFlag);
                    transaction.Complete();
                }
                updatePgbDelegate("已结算记录的撤销完成!", 100);
                LogHelper.LogInformation("已结算记录的撤销完成!");
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
