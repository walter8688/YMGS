using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class FootballMatchMarket
    {
        public int MatchId { get; set; }
        public int MarketId { get; set; }
        public int MarketFlag { get; set; }
        public int MarketTmpId { get; set; }
        public string MatchName { get; set; }
        public string MatchNameEn { get; set; }
        public string MarketTmpName { get; set; }
        public string MarketTmpNameEn { get; set; }
        public string MarketName { get; set; }
        public string MarketNameEn { get; set; }
        public decimal BackOdds { get; set; }
        public decimal LayOdds { get; set; }
        public decimal BackMatchAmouts { get; set; }
        public decimal LayMatchAmouts { get; set; }
    }
}