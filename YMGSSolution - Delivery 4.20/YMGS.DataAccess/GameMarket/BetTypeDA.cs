using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.GameMarket
{
    public class BetTypeDA:DaBase
    {
        /// <summary>
        /// 获得所有的交易类型
        /// </summary>
        /// <returns></returns>
        public static DSBetType QueryAllBetType()
        {
            var resultDS = SQLHelper.ExecuteStoredProcForDataSet<DSBetType>("pr_get_bet_type", null);
            return resultDS;
        }

        /// <summary>
        /// 保存交易类型
        /// </summary>
        /// <param name="betDT"></param>
        public static void SaveBetType(DSBetType.TB_BET_TYPERow betRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Bet_Type_Id",ParameterType=DbType.Int32,ParameterValue=betRow.BET_TYPE_ID},
                new ParameterData(){ParameterName="@Bet_Type_Name",ParameterType=DbType.String,ParameterValue=betRow.BET_TYPE_NAME},
                new ParameterData(){ParameterName="@Bet_Before_Game",ParameterType=DbType.Byte,ParameterValue=betRow.BET_BEFORE_GAME},
                new ParameterData(){ParameterName="@Bet_Gaming",ParameterType=DbType.Byte,ParameterValue=betRow.BET_GAMING},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.String,ParameterValue=betRow.LAST_UPDATE_USER}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_up_bet_type", parameters);
        }

        public static DSDeal QueryDeal(int BETTYPE, int BETID, int MATCH_ID, int MATCH_TYPE)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@BETTYPE",ParameterType=DbType.Int32,ParameterValue=BETTYPE},
                new ParameterData(){ParameterName="@BETID",ParameterType=DbType.Int32,ParameterValue=BETID},
                new ParameterData(){ParameterName="@MATCH_ID",ParameterType=DbType.Int32,ParameterValue=MATCH_ID},
                new ParameterData(){ParameterName="@MATCH_TYPE",ParameterType=DbType.Int32,ParameterValue=MATCH_TYPE}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DSDeal>("pr_get_deal", parameters);
        }
    }
}
