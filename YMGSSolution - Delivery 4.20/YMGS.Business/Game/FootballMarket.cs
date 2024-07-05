using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using YMGS.Data.Presentation;
using YMGS.Business.GameMarket;

namespace YMGS.Business.Game
{
    public class FootballMarket:IMarketObject
    {
        public IList<MarketObject> GetMarketList(int marketTmpId)
        {
            var markets = MatchManager.QueryMatchMarketByTmpId(marketTmpId);
            IList<MarketObject> marketList = new List<MarketObject>();
            MarketObject market = null;
            foreach (var m in markets.TB_MATCH_MARKET)
            {
                market = new MarketObject();
                market.BetTypeId = m.BET_TYPE_ID;
                market.MarketTmpId = m.MARKET_TMP_ID;
                market.MarketFlag = m.MARKET_FLAG;
                market.MarketId = m.MARKET_ID;
                market.MarketName = m.MARKET_NAME;
                market.MarketNameEN = m.MARKET_NAME_EN;
                market.MatchId = m.MATCH_ID;
                market.MatchName = m.MATCH_NAME;
                market.MatchNameEN = m.MATCH_NAME_EN;
                market.ScoreA = m.IsSCOREANull() ? 0 : m.SCOREA;
                market.ScoreB = m.IsSCOREBNull() ? 0 : m.SCOREB;
                market.MarketTmpName = m.MARKET_TMP_NAME;
                market.MarketTmpNameEN = m.MARKET_TMP_NAME_EN;
                market.BackMatchAmouts = m.backMATCH_AMOUNTS;
                market.LayMatchAmouts = m.layMATCH_AMOUNTS;
                market.BackOdds = m.backodds;
                market.LayOdds = m.layodds;
                market.MarketEnabled = true;
                marketList.Add(market);
            }
            return marketList;
        }

        public IList<MarketObject> GetMarketList()
        {
            throw new NotImplementedException();
        }
    }
}
