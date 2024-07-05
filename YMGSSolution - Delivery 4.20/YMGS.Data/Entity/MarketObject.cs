using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    [Serializable]
    public class MarketObject
    {
        public int BetTypeId { get; set; }
        public int MarketTmpId { get; set; }
        public int MarketFlag { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string MarketNameEN { get; set; }
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public string MatchNameEN { get; set; }
        public decimal ScoreA { get; set; }
        public decimal ScoreB { get; set; }
        public string MarketTmpName { get; set; }
        public string MarketTmpNameEN { get; set; }
        public string BackMatchAmouts { get; set; }
        public string LayMatchAmouts { get; set; }
        public string BackOdds { get; set; }
        public string LayOdds { get; set; }
        public bool MarketEnabled { get; set; }
    }
}
