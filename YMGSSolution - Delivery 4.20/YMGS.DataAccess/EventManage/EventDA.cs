using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Framework;

namespace YMGS.DataAccess.EventManage
{
    public class EventDA
    {
        #region 根据条件查询赛事
        /// <summary>
        /// 根据条件查询赛事
        /// </summary>
        /// <param name="eventZoneID"></param>
        /// <param name="eventName"></param>
        /// <param name="eventDesc"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="status"></param>
        /// <param name="eventTeamID"></param>
        /// <param name="eventTeamName"></param>
        /// <returns></returns>
        public static DSEventTeamList QueryEvent(int? eventZoneID, string eventName,string eventNameEn, string eventDesc, DateTime? startDate, DateTime? endDate, int? status, int? eventTeamID, string eventTeamName)
        {
            IList<ParameterData> parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@EVENTZONE_ID", ParameterType = DbType.Int32, ParameterValue = eventZoneID},
                new ParameterData(){ ParameterName = "@EVENT_NAME", ParameterType = DbType.String, ParameterValue = eventName},
                new ParameterData(){ ParameterName = "@EVENT_NAME_En", ParameterType = DbType.String, ParameterValue = eventNameEn},
                new ParameterData(){ ParameterName = "@EVENT_DESC", ParameterType = DbType.String, ParameterValue = eventDesc},
                new ParameterData(){ ParameterName = "@STATUS", ParameterType = DbType.Int32, ParameterValue = status},
                new ParameterData(){ ParameterName = "@EVENT_TEAM_ID", ParameterType = DbType.Int32, ParameterValue = eventTeamID},
                new ParameterData(){ ParameterName = "@EVENT_TEAM_NAME", ParameterType = DbType.String, ParameterValue = eventTeamName}            
            };
            if(startDate == null)
                parameters.Add(new ParameterData(){ ParameterName = "@START_DATE", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value});
            else
                parameters.Add(new ParameterData(){ ParameterName = "@START_DATE", ParameterType = DbType.DateTime, ParameterValue = startDate});
            if (endDate == null)
                parameters.Add(new ParameterData() { ParameterName = "@END_DATE", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@END_DATE", ParameterType = DbType.DateTime, ParameterValue = endDate });
            var eventList = SQLHelper.ExecuteStoredProcForDataSet<DSEventTeamList>("pr_get_event", parameters);

            return eventList;
        }
        #endregion

        #region 根据赛事ID查询赛事
        public static DSEvent QueryEventByEventID(int? eventID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Event_Id", ParameterType = DbType.Int32, ParameterValue = eventID });
            return SQLHelper.ExecuteStoredProcForDataSet<DSEvent>("pr_get_event_by_eventid", parameters);
        }
        #endregion

        #region 新增赛事
        /// <summary>
        /// 新增赛事
        /// </summary>
        /// <param name="row"></param>
        /// <param name="eventTeamIDS"></param>
        public static void AddEvent(DSEvent.TB_EVENTRow row, string eventTeamIDS)
        {
            IList<ParameterData> parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@Event_ZoneID", ParameterType = DbType.Int32, ParameterValue = row.EVENTZONE_ID},
                new ParameterData(){ ParameterName = "@Event_Name", ParameterType = DbType.String, ParameterValue = row.EVENT_NAME},
                new ParameterData(){ ParameterName = "@Event_Name_EN", ParameterType = DbType.String, ParameterValue = row.EVENT_NAME_EN},
                new ParameterData(){ ParameterName = "@Event_Desc", ParameterType = DbType.String, ParameterValue = row.EVENT_DESC},
                new ParameterData(){ ParameterName = "@Status", ParameterType = DbType.Int32, ParameterValue = row.STATUS},    
                new ParameterData(){ ParameterName = "@Create_User", ParameterType = DbType.Int32, ParameterValue = row.CREATE_USER},  
                new ParameterData(){ ParameterName = "@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName = "@Event_Team_IDS", ParameterType = DbType.String, ParameterValue = eventTeamIDS}  
            };
            if (row.START_DATE == DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = row.START_DATE });
            if (row.END_DATE == DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = row.END_DATE });
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_add_event", parameters);
        }
        #endregion

        #region 删除赛事
        /// <summary>
        /// 删除赛事
        /// </summary>
        /// <param name="eventID"></param>
        public static void DeleteEvent(int eventID)
        {
            IList<ParameterData> parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@EVENT_ID", ParameterType = DbType.Int32, ParameterValue = eventID}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_del_event", parameters);
        }
        #endregion

        #region 修改赛事
        /// <summary>
        /// 修改赛事
        /// </summary>
        /// <param name="row"></param>
        /// <param name="eventTeamIDS"></param>
        public static void UpdateEvent(DSEvent.TB_EVENTRow row, string eventTeamIDS)
        {
            IList<ParameterData> parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@Event_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENT_ID},
                new ParameterData(){ ParameterName = "@Event_ZoneID", ParameterType = DbType.Int32, ParameterValue = row.EVENTZONE_ID},
                new ParameterData(){ ParameterName = "@EVENT_NAME", ParameterType = DbType.String, ParameterValue = row.EVENT_NAME},
                new ParameterData(){ ParameterName = "@Event_Name_EN", ParameterType = DbType.String, ParameterValue = row.EVENT_NAME_EN},
                new ParameterData(){ ParameterName = "@EVENT_DESC", ParameterType = DbType.String, ParameterValue = row.EVENT_DESC},
                new ParameterData(){ ParameterName = "@START_DATE", ParameterType = DbType.DateTime, ParameterValue = row.START_DATE},
                new ParameterData(){ ParameterName = "@END_DATE", ParameterType = DbType.DateTime, ParameterValue = row.END_DATE},
                new ParameterData(){ ParameterName = "@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName = "@Event_Team_IDS", ParameterType = DbType.String, ParameterValue = eventTeamIDS}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_up_event", parameters);
        }
        #endregion 

        #region 赛事另存
        #endregion

        #region 激活赛事
        public static void ActivatedEvent(int eventID, int lastUpdateUserID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@EventID", ParameterType = DbType.Int32, ParameterValue = eventID });
            parameters.Add(new ParameterData() { ParameterName = "@LastUpdateUser", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserID });
            SQLHelper.ExecuteStoredProcForScalar("pr_active_event", parameters);
        }
        #endregion

        #region 暂停赛事
        public static void PauseEvent(int eventID, int lastUpdateUserID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@EventID", ParameterType = DbType.Int32, ParameterValue = eventID });
            parameters.Add(new ParameterData() { ParameterName = "@LastUpdateUser", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserID });
            SQLHelper.ExecuteStoredProcForScalar("pr_pause_event", parameters);
        }
        #endregion

        #region 终止赛事
        public static void AbortEvent(int eventID, int lastUpdateUserID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@EventID", ParameterType = DbType.Int32, ParameterValue = eventID });
            parameters.Add(new ParameterData() { ParameterName = "@LastUpdateUser", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserID });
            SQLHelper.ExecuteStoredProcForScalar("pr_abort_event", parameters);
        }
        #endregion

        #region 获取相关赛事比赛
        public static DSMatch QueryMatchByEventID(int eventID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@EventID", ParameterType = DbType.Int32, ParameterValue = eventID });
            var matchDS = SQLHelper.ExecuteStoredProcForDataSet<DSMatch>("pr_get_match_by_eventid", parameters);
            return matchDS;
        }
        #endregion
    }
}
