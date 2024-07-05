using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.DataAccess.GameControl;
using YMGS.Data.Presentation;

namespace YMGS.Business.GameControl
{
    public class GameControlManager
    {
        /// <summary>
        /// 清理市场
        /// </summary>
        /// <param name="matchId"></param>
        public static void ClearMarket(int matchId, int match_Type)
        {
            GameControlDA.ClearMarket(matchId, match_Type);
        }

        public static DSMarketTmp GetMarketByMatchId(int? matchId)
        {
            return GameControlDA.GetMarketByMatchId(matchId);
        }

        public static void UpdateMatchMarketStatus(int? matchId, string marketTmpIdList)
        {
            GameControlDA.UpdateMatchMarketStatus(matchId, marketTmpIdList);
        }

        public static void ClearMatchOverUnderMarketByMarketIds(int? matchId, int matchType, string marketTmpIds)
        {
            GameControlDA.ClearMatchOverUnderMarketByMarketIds(matchId, matchType, marketTmpIds);
        }
    }
}
