using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class Top3DataSource
    {
        public CenterTop3MarketObject matchSource { get; set; }
        public string titleName { get; set; }
    }

    public class CenterTop3MarketObject
    {
        public FootballMatchInfo MatchInfo { get; set; }
        public IList<FootballMarketInfo> MarketAllInfo { get; set; }
        public IList<FootballMarketInfo> MarketSummaryInfo { get; set; }
        public bool isShowAll { get; set; }
    }
}