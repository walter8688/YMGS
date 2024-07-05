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
    /// 默认策略
    /// </summary>
    public class DefaultStrategy : IMatchSettelStrategy
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
                resultInfo.IsBuyerWin = 1;//resultInfo.IsBuyerWin = true;
            resultInfo.ExchangeDealId = curExchangeDeal.EXCHANGE_DEAL_ID;
            resultInfo.BetOdds = 0;
            resultInfo.BetCalculatePercent = 0;            
            return resultInfo;
        }
    }
}
