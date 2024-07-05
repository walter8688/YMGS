using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;

namespace YMGS.DataAccess.EventManage
{
    public class EventZoneDA
    {
        #region 获取赛事项目
        public static DSEventItem QueryEventItem()
        {
            var eventItemDS = SQLHelper.ExecuteStoredProcForDataSet<DSEventItem>("pr_get_event_item", null);
            return eventItemDS;
        }
        #endregion

        #region 获取赛事区域
        public static DSEventZone QueryEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Item_ID", ParameterType= DbType.Int32, ParameterValue = row.EVENTITEM_ID},
                new ParameterData(){ ParameterName="@Event_Zone_Name", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME},
                new ParameterData(){ ParameterName="@Event_Zone_Name_En", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME_EN},
                new ParameterData(){ ParameterName="@Event_Zone_Desc", ParameterType = DbType.String, ParameterValue = row.EVENTZONE_DESC},
                new ParameterData(){ ParameterName="@Param_Zone_Id", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var eventZoneDS = SQLHelper.ExecuteStoredProcForDataSet<DSEventZone>("pr_get_event_zone", parameters);
            return eventZoneDS;
        }
        #endregion

        #region 新增赛事区域
        public static int AddEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@EventItem_ID", ParameterType= DbType.Int32, ParameterValue = row.EVENTITEM_ID},
                new ParameterData(){ ParameterName="@EventZone_Name", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME},
                new ParameterData(){ ParameterName="@EventZone_Name_En", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME_EN},
                new ParameterData(){ ParameterName="@EventZone_Desc", ParameterType = DbType.String, ParameterValue = row.EVENTZONE_DESC},
                new ParameterData(){ ParameterName="@Create_User", ParameterType = DbType.Int32, ParameterValue = row.CREATE_USER},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName="@Param_Zone_Id", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_add_event_zone", parameters);
            return Convert.ToInt32(returnCode);
        }
        #endregion

        #region 更新赛事区域
        public static int UpdateEventZone(DSEventZone.TB_EVENT_ZONERow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Zone_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENTZONE_ID},
                new ParameterData(){ ParameterName="@EventItem_ID", ParameterType= DbType.Int32, ParameterValue = row.EVENTITEM_ID},
                new ParameterData(){ ParameterName="@EventZone_Name", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME},
                new ParameterData(){ ParameterName="@EventZone_Name_En", ParameterType = DbType.String, ParameterValue= row.EVENTZONE_NAME_EN},
                new ParameterData(){ ParameterName="@EventZone_Desc", ParameterType = DbType.String, ParameterValue = row.EVENTZONE_DESC},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName="@Param_Zone_Id", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_up_event_zone", parameters);
            return Convert.ToInt32(returnCode);
        }
        #endregion

        #region 删除赛事区域
        public static int DeleteEventZone(int eventZoneID)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Zone_ID", ParameterType = DbType.Int32, ParameterValue = eventZoneID}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_del_event_zone", parameters);
            return Convert.ToInt32(returnCode);
        }
        #endregion
    }
}
