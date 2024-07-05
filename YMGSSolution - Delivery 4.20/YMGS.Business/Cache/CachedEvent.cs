using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemSetting;
using YMGS.Business.EventManage;

namespace YMGS.Business.Cache
{
    /// <summary>
    /// 赛事
    /// </summary>
    public class CachedEvent : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedEvent";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsEvent = EventManager.QueryEvent(-1, string.Empty, string.Empty, string.Empty, null, null, 0, -1, string.Empty);
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsEvent);
        }
    }
}
