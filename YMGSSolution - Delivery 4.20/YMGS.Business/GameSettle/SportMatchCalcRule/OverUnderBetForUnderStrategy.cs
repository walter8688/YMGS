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
    /// 大小球(不超过)
    /// </summary>
    public class OverUnderBetForUnderStrategy : IMatchSettelStrategy
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

            if (curExchangeDeal.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.HalfTime)
                resultInfo.IsBuyerWin = (curMatch.HOME_FIR_HALF_SCORE + curMatch.GUEST_FIR_HALF_SCORE < curExchangeDeal.MARKET_SCOREB) ? 1 : 2;
            else
                resultInfo.IsBuyerWin = (curMatch.HOME_FULL_SCORE + curMatch.GUEST_FULL_SCORE < curExchangeDeal.MARKET_SCOREB) ? 1 : 2;
            resultInfo.ExchangeDealId = curExchangeDeal.EXCHANGE_DEAL_ID;
            resultInfo.BetOdds = curExchangeDeal.ODDS;
            resultInfo.BetCalculatePercent = 1;
            return resultInfo;
        }
    }
}
