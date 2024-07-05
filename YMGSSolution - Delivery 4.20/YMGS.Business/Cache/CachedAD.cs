using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.GameMarket;
using YMGS.Framework;
using YMGS.DataAccess.SystemSetting;

namespace YMGS.Business.Cache
{
    public class CachedADPic : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedADPic";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsadpic = ADDA.QueryADPic();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsadpic);
        }
    }

    public class CachedADWords : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedADWords";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsadwords = ADDA.QueryDSADWords();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsadwords);
        }
    }
}
