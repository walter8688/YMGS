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
    public class ChampionGameSettleManager : BrBase
    {
        /// <summary>
        /// 验证是否可以进行比赛的结算
        /// </summary>
        /// <param name="dsMatch"></param>
        /// <returns></returns>
        private static string ValidChampEventMatch(DSChampEvent dsChampEvent,
                                    DSChampWinMemList dsChampWinMember)
        {
            if (dsChampEvent.TB_Champ_Event.Rows.Count == 0)
                return "未能查询到当前冠军赛事的详细信息!";

            var curChampEvent = dsChampEvent.TB_Champ_Event[0];
            //判断当前状态是否属于激活中
            if (curChampEvent.Champ_Event_Status != (int)ChampEventStatusEnum.Finished)
            {
                return "只有结束状态的冠军赛事才能进行比赛计算!";
            }

            if (dsChampWinMember._DSChampWinMemList.Rows.Count == 0)
            {
                return "只有录入冠军赛事的冠军成绩后才能进行比赛结算!";
            }

            return string.Empty;
        }

        /// <summary>
        /// 冠军赛事比赛结算
        /// </summary>
        /// <param name="iChampEventId">冠军赛事Id</param>
        /// <param name="iCurrentUserId">当前用户Id</param>
        /// <returns>返回信息</returns>
        public static string CalcChampEventGame(int iChampEventId,int iCurrentUserId,
             InitProgressDelegate initPgbDelegate, UpdateProgressDelegate updatePgbDelegate, bool isExecuteInitPgbDelegate)
        {
            DSChampEvent dsChampEvent = ChampEventManager.QueryChampEventByKey(iChampEventId);
            DSChampWinMemList dsChampWinMember = ChampEventManager.QueryChampWinMemList(iChampEventId);

            var curChampEvent = dsChampEvent.TB_Champ_Event[0];
            string strMessage = ValidChampEventMatch(dsChampEvent, dsChampWinMember);
            if (!string.IsNullOrEmpty(strMessage))
                return strMessage;
            
            //获取当前冠军赛事的比赛的撮合交易记录
            LogHelper.LogInformation("获取当前冠军赛事的比赛的撮合交易记录!");                        
            DSChampionExchangeDealList dsExchangeDeal = MatchExchangeDealManager.QueryChampionEventExchangeDeal(iChampEventId);
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
                //可能有些操作相当费时，故超时设置为30
                transOption.Timeout = new TimeSpan(0, 30, 0);
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required,transOption))
                {
                    //建立结算日志
                    logId = GameSettlementLogDA.CreateMatchSettlementLog(curChampEvent.Champ_Event_ID, 2, 1, iCurrentUserId, curChampEvent.Champ_Event_Status);

                    #region 计算：按照撮合交易记录依次进行结算
                    foreach (var curExchangeDeal in dsExchangeDeal.EXCHANGE_DEAL_LIST)
                    {
                        try
                        {
                            //bool bIsBuyerWin = false;
                            int bIsBuyerWin = 2;
                            var winList = dsChampWinMember._DSChampWinMemList
                                .Where(r=>r.Champ_Event_Member_ID==curExchangeDeal.CHAMP_MEMBER_ID)
                                .ToList();
                            if(winList.Count>0)
                                //bIsBuyerWin = true;
                                bIsBuyerWin = 1;

                            //计算该撮合交易数据
                            MatchSettleDA.CalcSportGameMatchExchangeDeal(iChampEventId, curExchangeDeal.EXCHANGE_DEAL_ID,
                                               bIsBuyerWin, curExchangeDeal.EXCHANGE_BACK_ID, curExchangeDeal.EXCHANGE_LAY_ID,
                                                1, curExchangeDeal.DEAL_AMOUNT, curExchangeDeal.ODDS,
                                                iCurrentUserId, curChampEvent.Champ_Event_Name, string.Empty,
                                                curExchangeDeal.CHAMP_EVENT_MEMBER_NAME,logId);
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
                            //System.Threading.Thread.Sleep(2000);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError("当前撮合交易记录ID" + curExchangeDeal.EXCHANGE_DEAL_ID.ToString()+
                                                ex.Message);
                            throw ex;
                        }
                    }
                    #endregion

                    updatePgbDelegate("正在取消未撮合成功的交易!", 81);                                        
                    LogHelper.LogInformation("开始取消未撮合成功的交易!");
                    MatchSettleDA.UnfreezeChampEventDealFund(iChampEventId, iCurrentUserId,logId);

                    updatePgbDelegate("正在扣除对冲释放资金", 90);
                    LogHelper.LogInformation("正在扣除对冲释放资金");
                    MatchSettleDA.RollbackRealHedgeFund(iChampEventId, iCurrentUserId, 1, 2,logId);

                    transaction.Complete();
                }
                updatePgbDelegate("结算完成!", 100);

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
