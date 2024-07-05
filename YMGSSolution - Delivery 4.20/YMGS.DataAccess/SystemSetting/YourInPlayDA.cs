using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using System.Data;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.SystemSetting
{
    public class YourInPlayDA
    {
        public static void ManageYourInPlay(int userId, int matchId, int faved)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@User_Id", ParameterType= DbType.Int32, ParameterValue = userId},
                new ParameterData(){ ParameterName="@Match_Id", ParameterType= DbType.Int32, ParameterValue = matchId},
                new ParameterData(){ ParameterName="@Faved", ParameterType= DbType.Int32, ParameterValue = faved}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_manage_your_inplay", parameters);
        }

        public static DSYourInPlay QueryYourInPlay(int userId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@User_Id", ParameterType= DbType.Int32, ParameterValue = userId},
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSYourInPlay>("pr_get_your_inplay", parameters);
        }

        public static DSYourInPlay QueryAllYourInPlay()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSYourInPlay>("pr_get_all_your_inplay", null);
        }
    }
}
