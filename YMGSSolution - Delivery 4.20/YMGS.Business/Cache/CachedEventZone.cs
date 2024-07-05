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
    /// 赛事区域缓存
    /// </summary>
    public class CachedEventZone : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedEventZone";
            }
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        public override void Refresh()
        {
            DSEventZone dsEventZone = new DSEventZone();
            DSEventZone.TB_EVENT_ZONERow queryRow = dsEventZone.TB_EVENT_ZONE.NewTB_EVENT_ZONERow();
            queryRow.EVENTITEM_ID = -1;
            queryRow.EVENTZONE_NAME = queryRow.EVENTZONE_NAME_EN = string.Empty;
            queryRow.EVENTZONE_DESC = string.Empty;
            queryRow.PARAM_ZONE_ID = -1;
            var eventZoneList = EventZoneManager.QueryEventZone(queryRow);
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, eventZoneList);
        }
    }
}
