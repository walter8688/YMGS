using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Business.GameMarket;

namespace YMGS.Business.Cache
{
    public class CachedExchangeLay : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedExchangeLay";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsExchangeLay = MatchManager.GetCachedExchangeLay();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsExchangeLay);
        }
    }
}
