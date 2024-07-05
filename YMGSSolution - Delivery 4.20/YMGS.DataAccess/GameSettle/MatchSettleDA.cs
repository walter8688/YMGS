using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.GameSettle
{
    public class MatchSettleDA : DaBase
    {
        /// <summary>
        /// 计算撮合交易记录
        /// </summary>
        /// <param name="iMatchId"></param>
        /// <param name="iExchangeDealId"></param>
        /// <param name="IsBuyerWin"></param>
        /// <param name="iExchangeBackId"></param>
        /// <param name="iExchangeLayId"></param>
        /// <param name="dblBetCalculateFlag"></param>
        /// <param name="dblDealAmount"></param>
        /// <param name="dblBetOdds"></param>
        public static void CalcSportGameMatchExchangeDeal(int iMatchId,int iExchangeDealId,int IsBuyerWin,
                int iExchangeBackId,int iExchangeLayId,decimal dblBetCalculateFlag,decimal dblDealAmount,decimal dblBetOdds,
                int iCurUserId, string matchName, string marketTmpName, string marketName, int logId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=iMatchId},
                new ParameterData(){ParameterName="@Exchange_Deal_Id",ParameterType=DbType.Int32,ParameterValue=iExchangeDealId},
                new ParameterData(){ParameterName="@Exchange_Back_Id",ParameterType=DbType.Int32,ParameterValue=iExchangeBackId},
                new ParameterData(){ParameterName="@Exchange_Lay_Id",ParameterType=DbType.Int32,ParameterValue=iExchangeLayId},
                new ParameterData(){ParameterName="@Is_Buyer_Win",ParameterType=DbType.Int32,ParameterValue=IsBuyerWin},
                new ParameterData(){ParameterName="@Bet_Calculate_Flag",ParameterType=DbType.Decimal,ParameterValue=dblBetCalculateFlag},
                new ParameterData(){ParameterName="@Deal_Amount",ParameterType=DbType.Decimal,ParameterValue=dblDealAmount},
                new ParameterData(){ParameterName="@Bet_Odds",ParameterType=DbType.Decimal,ParameterValue=dblBetOdds},
                new ParameterData(){ParameterName="@Cur_User_Id",ParameterType=DbType.Int32,ParameterValue=iExchangeBackId},
                new ParameterData(){ParameterName="@Match_Name",ParameterType=DbType.String,ParameterValue=matchName},
                new ParameterData(){ParameterName="@Market_Tmp_Name",ParameterType=DbType.String,ParameterValue=marketTmpName},
                new ParameterData(){ParameterName="@Market_Name",ParameterType=DbType.String,ParameterValue=marketName},
                new ParameterData(){ParameterName="@Log_Id",ParameterType=DbType.Int32,ParameterValue=logId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_calc_sport_match_settle", parameters);
        }
        
        /// <summary>
        /// 解冻体育比赛被冻结的资金
        /// </summary>
        /// <param name="iMatchId"></param>
        /// <param name="iCurUserId"></param>
        /// <param name="iCalcFlag">0半场结算 1 全场结算</param>
        /// <param name="logId">计算日志ID</param>
        public static void UnfreezeSportMatchDealFund(int iMatchId, int iCurUserId, int iCalcFlag,int logId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=iMatchId},
                new ParameterData(){ParameterName="@Cur_User_Id",ParameterType=DbType.Int32,ParameterValue=iCurUserId},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=iCalcFlag},
                new ParameterData(){ParameterName="@Log_Id",ParameterType=DbType.Int32,ParameterValue=logId}
            };

            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.SetTimeout(600);
                broker.ExecuteNonQuery("pr_calc_cancel_freeze_fund", parameters, CommandType.StoredProcedure);
            }
            finally
            {
                broker.Close();
            }
        } 

        /// <summary>
        /// 解冻冠军赛事被冻结的资金
        /// </summary>
        /// <param name="iChampEventId"></param>
        /// <param name="iCurUserId"></param>
        public static void UnfreezeChampEventDealFund(int iChampEventId,int iCurUserId,int logId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Champ_Event_Id",ParameterType=DbType.Int32,ParameterValue=iChampEventId},
                new ParameterData(){ParameterName="@Cur_User_Id",ParameterType=DbType.Int32,ParameterValue=iCurUserId},
                new ParameterData(){ParameterName="@Log_Id",ParameterType=DbType.Int32,ParameterValue=logId}
            };
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.SetTimeout(600);
                broker.ExecuteNonQuery("pr_calc_cancel_champ_match_freeze_fund", parameters, CommandType.StoredProcedure);
            }
            finally
            {
                broker.Close();
            }
        }

        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="settleStatus"></param>
        public static void UpdateMatchSettleStatus(int matchId, int settleStatus)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Match_Settle_Status",ParameterType=DbType.Int32,ParameterValue=settleStatus}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_update_match_settle_status", parameters);
        }


        /// <summary>
        /// 扣除实时计算时对冲的资金
        /// </summary>
        /// <param name="iMatchId"></param>
        /// <param name="iCurUserId"></param>
        /// <param name="iCalcFlag">0半场结算 1 全场结算</param>
        /// <param name="iMatchType">1 体育比赛 2 冠军赛事</param>
        public static void RollbackRealHedgeFund(int iMatchId, int iCurUserId, int iCalcFlag,int iMatchType,int iLogId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=iMatchId},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=iCalcFlag},
                new ParameterData(){ParameterName="@Match_Type",ParameterType=DbType.Int32,ParameterValue=iMatchType},
                new ParameterData(){ParameterName="@CurUser",ParameterType=DbType.Int32,ParameterValue=iCurUserId},
                new ParameterData(){ParameterName="@Log_Id",ParameterType=DbType.Int32,ParameterValue=iLogId}                
            };

            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.SetTimeout(600);
                broker.ExecuteNonQuery("pr_calc_real_hedge_rollback", parameters, CommandType.StoredProcedure);
            }
            finally
            {
                broker.Close();
            }
        } 
    }
}
