using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Business.GameSettle.SportMatchCalcRule
{
    /// <summary>
    /// 体育比赛的玩法结算结果
    /// </summary>
    public class MatchSettleResultInfo
    {
        /// <summary>
        /// 是否投注方赢:{1:买家赢;2:买家输;3:平局}
        /// </summary>
        public int IsBuyerWin
        {
            get;
            set;
        }

        /// <summary>
        /// 撮合交易ID
        /// </summary>
        public int ExchangeDealId
        {
            get;
            set;
        }

        /// <summary>
        /// 赌注交易结算标志 : 1 全注额计算 0.5 半注额结算
        /// </summary>
        public decimal BetCalculatePercent
        {
            get;
            set;
        }

        /// <summary>
        /// 赔率
        /// </summary>
        public decimal BetOdds
        {
            get;
            set;
        }
    }
}
