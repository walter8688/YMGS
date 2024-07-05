using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.GameMarket;
using YMGS.Framework;
using YMGS.DataAccess.AssistManage;

namespace YMGS.Business.Cache
{
    public class CachedHelp : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedHelp";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var helperDS = HelperDA.QueryHelper();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, helperDS);
        }
    }
}
