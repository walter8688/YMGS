using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Business.AssistManage;
using YMGS.Framework;

namespace YMGS.Business.Cache
{
    public class CachedOddsCompare : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedOddsCompare";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsEvent = OddsCompareManager.QueryOddsCompare2();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsEvent);
        }
    }
}

