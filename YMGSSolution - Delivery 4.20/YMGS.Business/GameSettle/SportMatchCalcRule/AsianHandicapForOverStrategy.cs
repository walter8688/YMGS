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
    /// 让球(超过)
    /// </summary>
    public class AsianHandicapForOverStrategy : IMatchSettelStrategy
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
            if (!curExchangeDeal.IsMARKET_SCOREANull())
            {
                if (curExchangeDeal.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.HalfTime)
                {
                    if (curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREB > curMatch.GUEST_FIR_HALF_SCORE
                        && curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREA > curMatch.GUEST_FIR_HALF_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREB == curMatch.GUEST_FIR_HALF_SCORE
                        && curMatch.HOME_FIR_HALF_SCORE+curExchangeDeal.MARKET_SCOREA>curMatch.GUEST_FIR_HALF_SCORE
                        )
                    {
                        resultInfo.BetCalculatePercent = 0.5M;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREA == curMatch.GUEST_FIR_HALF_SCORE
                        && curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREB<curMatch.GUEST_FIR_HALF_SCORE
                        )
                    {
                        resultInfo.BetCalculatePercent = 0.5M;
                        resultInfo.IsBuyerWin = 2;
                    }
                    else
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 2;
                    }
                }
                else
                {
                    if (curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREB > curMatch.GUEST_FULL_SCORE
                        && curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREA > curMatch.GUEST_FULL_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREB == curMatch.GUEST_FULL_SCORE
                        && curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREA > curMatch.GUEST_FULL_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 0.5M;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREA == curMatch.GUEST_FULL_SCORE
                        && curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREB < curMatch.GUEST_FULL_SCORE
                        )
                    {
                        resultInfo.BetCalculatePercent = 0.5M;
                        resultInfo.IsBuyerWin = 2;
                    }
                    else
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 2;
                    }
                }
            }
            else
            {
                if (curExchangeDeal.MARKET_TMP_TYPE == (int)MarketTemplateTypeEnum.HalfTime)
                {
                    if (curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREB > curMatch.GUEST_FIR_HALF_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FIR_HALF_SCORE + curExchangeDeal.MARKET_SCOREB < curMatch.GUEST_FIR_HALF_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 2;
                    }
                    else
                    {
                        resultInfo.IsBuyerWin = 3;
                    }
                }
                else
                {
                    if (curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREB > curMatch.GUEST_FULL_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 1;
                    }
                    else if (curMatch.HOME_FULL_SCORE + curExchangeDeal.MARKET_SCOREB < curMatch.GUEST_FULL_SCORE)
                    {
                        resultInfo.BetCalculatePercent = 1;
                        resultInfo.IsBuyerWin = 2;
                    }
                    else
                    {
                        resultInfo.IsBuyerWin = 3;
                    }
                }
            }         

            resultInfo.ExchangeDealId = curExchangeDeal.EXCHANGE_DEAL_ID;
            resultInfo.BetOdds = curExchangeDeal.ODDS;
            return resultInfo;
        }
    }
}
