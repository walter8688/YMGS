using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class BetInfoObject
    {
        public int matchId { get; set; }
        public int matchType { get; set; }
        public int marketId { get; set; }
        public int marketTmpId { get; set; }
        public decimal Odds { get; set; }
        public decimal matchAmounts { get; set; }
        public string matchName { get; set; }
        public string matchNameEn { get; set; }
        public string marketName { get; set; }
        public string marketNameEn { get; set; }
        public string marketTmpName { get; set; }
        public string marketTmpNameEn { get; set; }
    }
}