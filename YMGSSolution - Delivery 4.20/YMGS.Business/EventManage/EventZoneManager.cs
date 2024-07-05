using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.EventManage;

namespace YMGS.Business.EventManage
{
    public class EventZoneManager
    {
        #region 获取赛事项目
        public static DSEventItem QueryEventItem()
        {
            return EventZoneDA.QueryEventItem();
        }
        #endregion

        #region 获取赛事区域
        public static DSEventZone QueryEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            return EventZoneDA.QueryEventZone(row);
        }
        #endregion

        #region 新增赛事区域
        public static int AddEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            return EventZoneDA.AddEventZone(row);
        }
        #endregion

        #region 更新赛事区域
        public static int UpdateEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            return EventZoneDA.UpdateEventZone(row);
        }
        #endregion

        #region 删除赛事区域
        public static int DeleteEventZone(int eventZoneID)
        {
            return EventZoneDA.DeleteEventZone(eventZoneID);
        }
        #endregion

    }
}
