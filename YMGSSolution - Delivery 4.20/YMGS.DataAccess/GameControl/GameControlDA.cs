using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.GameControl
{
    public class GameControlDA
    {
        /// <summary>
        /// 清理市场
        /// </summary>
        /// <param name="matchId"></param>
        public static void ClearMarket(int matchId,int match_Type)
        {
            var param = new List<ParameterData>();
            param.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            param.Add(new ParameterData() { ParameterName = "@Match_Type", ParameterType = DbType.Int32, ParameterValue = match_Type });
            SQLHelper.ExecuteStoredProcForScalar("pr_clear_market", param);
        }

        public static DSMarketTmp GetMarketByMatchId(int? matchId)
        {
            var param = new List<ParameterData>();
            param.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSMarketTmp>("pr_get_market", param);
        }

        public static void UpdateMatchMarketStatus(int? matchId, string marketTmpIdList)
        {
            var param = new List<ParameterData>();
            param.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            param.Add(new ParameterData() { ParameterName = "@Market_Tmp_Id", ParameterType = DbType.String, ParameterValue = marketTmpIdList });
            SQLHelper.ExecuteStoredProcForScalar("pr_update_market_status", param);
        }

        public static void ClearMatchOverUnderMarketByMarketIds(int? matchId, int matchType, string marketTmpIds)
        {
            var param = new List<ParameterData>();
            param.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            param.Add(new ParameterData() { ParameterName = "@Match_Type", ParameterType = DbType.Int32, ParameterValue = matchType });
            param.Add(new ParameterData() { ParameterName = "@Market_Tmp_Ids", ParameterType = DbType.String, ParameterValue = marketTmpIds });
            SQLHelper.ExecuteStoredProcForScalar("pr_clear_market_by_matkettmpids", param);
        }
    }
}
