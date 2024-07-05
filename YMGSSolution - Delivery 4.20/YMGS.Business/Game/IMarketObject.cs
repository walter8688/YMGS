using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;

namespace YMGS.Business.Game
{
    public interface IMarketObject
    {
        IList<MarketObject> GetMarketList();
        IList<MarketObject> GetMarketList(int marketTmpId);
    }
}
