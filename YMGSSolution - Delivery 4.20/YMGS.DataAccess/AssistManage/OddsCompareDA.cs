using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.AssistManage
{
   public class OddsCompareDA
    {
       public static DSODDSCOMPARE QueryOddsCompare()
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@matchid",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@matchname",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@CN_CORP",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@EN_CORP",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@PROFIT",ParameterType=DbType.Decimal,ParameterValue=0}
            };
            var result = SQLHelper.ExecuteStoredProcForDataSet<DSODDSCOMPARE>("pr_edit_odds_compare", parameters);
            return result;
        }

       public static void EditOddsCompare(DSODDSCOMPARE.TB_ODDS_COMPARERow compareRow, int flag)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@matchid",ParameterType=DbType.Int32,ParameterValue=compareRow.MATCHID},
                 new ParameterData(){ParameterName="@matchname",ParameterType=DbType.String,ParameterValue=compareRow.MATCHNAME},
                new ParameterData(){ParameterName="@CN_CORP",ParameterType=DbType.String,ParameterValue=compareRow.CN_CORP},
                new ParameterData(){ParameterName="@EN_CORP",ParameterType=DbType.String,ParameterValue=compareRow.EN_CORP},
                new ParameterData(){ParameterName="@PROFIT",ParameterType=DbType.Decimal,ParameterValue=compareRow.PROFIT}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_edit_odds_compare", parameters);
        }

    }
}
