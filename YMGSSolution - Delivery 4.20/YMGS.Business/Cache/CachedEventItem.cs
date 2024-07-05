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
    /// 赛事项目缓存
    /// </summary>
    public class CachedEventItem : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedEventItem";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            var dsEventItem = EventZoneManager.QueryEventItem();
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsEventItem);
        }
    }
}
