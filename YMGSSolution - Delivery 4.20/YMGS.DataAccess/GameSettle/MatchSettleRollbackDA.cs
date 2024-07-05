using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.GameSettle
{
    public class MatchSettleRollbackDA : DaBase
    {
        /// <summary>
        /// 查询比赛重新结算时需要回滚的用户列表
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="calcFlag"></param>
        /// <param name="matchType"></param>
        /// <returns></returns>
        public static DSSettlementRollbackUser QuerySettlementRollbackUsers(int matchId, int calcFlag, int matchType)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=calcFlag},
                new ParameterData(){ParameterName="@Match_Type",ParameterType=DbType.Int32,ParameterValue=matchType}
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSSettlementRollbackUser>("pr_calc_settlement_rollback_getusers",parameters);
        }

        /// <summary>
        /// 回滚结算
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="calcFlag"></param>
        /// <param name="matchType"></param>
        /// <param name="userId"></param>
        /// <param name="log_Id"></param>
        public static void RollbackSettlement(int matchId, int calcFlag, int matchType,int userId,int log_Id)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=calcFlag},
                new ParameterData(){ParameterName="@Match_Type",ParameterType=DbType.Int32,ParameterValue=matchType},
                new ParameterData(){ParameterName="@User_Id",ParameterType=DbType.Int32,ParameterValue=userId},
                new ParameterData(){ParameterName="@Log_Id",ParameterType=DbType.Int32,ParameterValue = log_Id}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_calc_settlement_rollback",parameters);
        }

        /// <summary>
        /// 回滚结算的比赛状态
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="Log_Id"></param>
        public static void RollbackSettlementMatchStatus(int matchId,int matchType,int logId,int calcFlag)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id", ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Match_Type", ParameterType=DbType.Int32,ParameterValue=matchType},
                new ParameterData(){ParameterName="@Log_Id", ParameterType=DbType.Int32,ParameterValue=logId},
                new ParameterData(){ParameterName="@Calc_Flag", ParameterType=DbType.Int32,ParameterValue=calcFlag}
            };
            
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_calc_settlement_matchstatus_rollback",parameters);
        }
    }
}
