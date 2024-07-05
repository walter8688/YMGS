using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.DataAccess.GameSettle;

namespace YMGS.Business.GameSettle
{
    public class MatchExchangeDealManager
    {
        /// <summary>
        /// 查询撮合交易记录
        /// </summary>
        /// <param name="iMatchId"></param>
        /// <param name="iQueryFlag">
        /// 0 半场结算撮合交易记录
        /// 1 全场结算撮合交易记录
        /// </param>
        /// <returns></returns>
        public static DSExchangeDealList QueryExchangeDeal(int iMatchId, int iQueryFlag)
        {
            return MatchExchangeDealDA.QueryExchangeDeal(iMatchId, iQueryFlag);
        }

        /// <summary>
        /// 查询冠军赛事的撮合交易记录
        /// </summary>
        /// <param name="iChampEventId">冠军</param>
        /// <returns></returns>
        public static DSChampionExchangeDealList QueryChampionEventExchangeDeal(int iChampEventId)
        {
            return MatchExchangeDealDA.QueryChampionEventExchangeDeal(iChampEventId);
        } 
    }
}
