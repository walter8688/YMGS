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
    public class GameSettlementLogDA : DaBase
    {
        /// <summary>
        /// 建立比赛结算日志
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType"></param>
        /// <param name="calcFlag"></param>
        /// <param name="curUser"></param>
        /// <param name="matchOriStatus"></param>
        /// <returns></returns>
        public static int CreateMatchSettlementLog(int matchId,int matchType,int calcFlag,int curUser,int matchOriStatus)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Match_Type",ParameterType=DbType.Int32,ParameterValue=matchType},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=calcFlag},
                new ParameterData(){ParameterName="@Original_Status",ParameterType=DbType.Int32,ParameterValue=curUser},
                new ParameterData(){ParameterName="@Operator",ParameterType=DbType.Int32,ParameterValue=matchOriStatus}
            };

            var obj = SQLHelper.ExecuteForScalar("pr_add_settlement_log", parameters, CommandType.StoredProcedure);
            if (obj != null)
                return Convert.ToInt32(obj);
            else
                return -1;
        }
    }
}
