using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.GameControlService
{
    public class GameControlServiceDA
    {
        public static void AutoFreezeStartMatch(int matchId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_atuo_freeze_start_match", parameters);
        }

        public static DSMatch GetAutoFreezeStartMatchs()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSMatch>("pr_get_atuo_freeze_start_match", null);
        }
    }
}
