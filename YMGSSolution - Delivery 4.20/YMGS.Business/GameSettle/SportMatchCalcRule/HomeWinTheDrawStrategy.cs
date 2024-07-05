using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Framework;
using YMGS.Business.GameSettle;

namespace YMGS.Business.GameSettle.SportMatchCalcRule
{
    /// <summary>
    /// 主队胜/平局(半/全场标准盘)
    /// </summary>
    public class HomeWinTheDrawStrategy : IMatchSettelStrategy
    {
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="curMatch">当前比赛</param>
        /// <param name="curExchangeDeal">当前撮合交易记录</param>
        /// <returns>结算结果</returns>
        public MatchSettleResultInfo CalculateMatch(DSMatch.TB_MATCHRow curMatch,
            DSExchangeDealList.EXCHANGE_DEAL_LISTRow curExchangeDeal)
        {
            MatchSettleResultInfo resultInfo = new MatchSettleResultInfo();

            resultInfo.IsBuyerWin = (curMatch.HOME_FIR_HALF_SCORE > curMatch.GUEST_FIR_HALF_SCORE
                && curMatch.HOME_FULL_SCORE == curMatch.GUEST_FULL_SCORE) ? 1 : 2;
            resultInfo.ExchangeDealId = curExchangeDeal.EXCHANGE_DEAL_ID;
            resultInfo.BetOdds = curExchangeDeal.ODDS;
            resultInfo.BetCalculatePercent = 1;
            return resultInfo;
        }
    }
}
