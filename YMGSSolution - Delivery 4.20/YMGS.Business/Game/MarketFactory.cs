using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;

namespace YMGS.Business.Game
{
    public class MarketFactory
    {
        public static IMarketObject GetMarketObject(MatchType type)
        {
            switch (type)
            {
                case MatchType.Football:
                    return new FootballMarket();
                case MatchType.Entertainment:
                    return null;
                default:
                    return null;
            }
        }
    }
}
