using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.GameSettle
{
    public class MatchExchangeDealDA : DaBase
    {
        /// <summary>
        /// 查询体育比赛的撮合交易记录
        /// </summary>
        /// <param name="iMatchId"></param>
        /// <param name="iQueryFlag">
        /// 0 半场结算撮合交易记录
        /// 1 全场结算撮合交易记录
        /// </param>
        /// <returns></returns>
        public static DSExchangeDealList QueryExchangeDeal(int iMatchId, int iQueryFlag)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=iMatchId},
                new ParameterData(){ParameterName="@Calc_Flag",ParameterType=DbType.Int32,ParameterValue=iQueryFlag}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DSExchangeDealList>("pr_calc_query_deal", parameters);
        }

        /// <summary>
        /// 查询冠军赛事的撮合交易记录
        /// </summary>
        /// <param name="iChampEventId">冠军</param>
        /// <returns></returns>
        public static DSChampionExchangeDealList QueryChampionEventExchangeDeal(int iChampEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Champ_Event_Id",ParameterType=DbType.Int32,ParameterValue=iChampEventId}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DSChampionExchangeDealList>("pr_calc_query_champion_deal", parameters);
        } 

    }
}
