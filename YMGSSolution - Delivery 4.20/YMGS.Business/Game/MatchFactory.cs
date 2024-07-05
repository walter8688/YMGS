using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;

namespace YMGS.Business.Game
{
    public class MatchFactory
    {
        public static IMatchObject GetMatchObjecj(MatchType type)
        {
            switch (type)
            {
                case MatchType.Football:
                    return new FootballMatch();
                case MatchType.Entertainment:
                    return new EntertainmentMatch();
                default:
                    return null;
            }
        }
    }
}
