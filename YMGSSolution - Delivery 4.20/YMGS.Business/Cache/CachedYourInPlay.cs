using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Business.SystemSetting;

namespace YMGS.Business.Cache
{
    public class CachedYourInPlay : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedYourInPlay";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsYourInPlay = YourInPlayManager.QueryAllYourInPlay();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsYourInPlay);
        }
    }
}
