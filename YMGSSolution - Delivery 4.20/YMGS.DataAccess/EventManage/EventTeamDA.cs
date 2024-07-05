using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.EventManage
{
    public class EventTeamDA
    {
        /// <summary>
        /// 查询参赛成员的数据集合
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DSTeamList QueryEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Item_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENT_ITEM_ID},
                new ParameterData(){ ParameterName="@Event_Team_Name", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME},
                new ParameterData(){ ParameterName="@Event_Team_Name_EN", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME_EN},
                new ParameterData(){ ParameterName="@Event_Team_Type1", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE1},
                new ParameterData(){ ParameterName="@Event_Team_Type2", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE2},
                new ParameterData(){ ParameterName="@Status", ParameterType = DbType.Int32, ParameterValue = row.STATUS},
                new ParameterData(){ ParameterName="@Param_Zone_ID", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var eventTeamListDS = SQLHelper.ExecuteStoredProcForDataSet<DSTeamList>("pr_get_event_team", parameters);
            return eventTeamListDS;
        }

        /// <summary>
        /// 根据赛事获取赛事成员
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static DSEventTeam QueryEventTeamByEventID(int eventID)
        {
            IList<ParameterData> parameter = new List<ParameterData>();
            parameter.Add(new ParameterData() { ParameterName = "@Event_ID", ParameterType = DbType.Int32, ParameterValue = eventID });
            var eventTeamDS = SQLHelper.ExecuteStoredProcForDataSet<DSEventTeam>("pr_get_eventTeam_by_EventID", parameter);
            return eventTeamDS;
        }

        /// <summary>
        /// 新增参赛成员
        /// </summary>
        /// <param name="row"></param>
        public static void AddEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Item_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENT_ITEM_ID},
                new ParameterData(){ ParameterName="@Event_Team_Name", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME},
                new ParameterData(){ ParameterName="@Event_Team_Name_EN", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME_EN},
                new ParameterData(){ ParameterName="@Event_Team_Type1", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE1},
                new ParameterData(){ ParameterName="@Event_Team_Type2", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE2},
                new ParameterData(){ ParameterName="@Status", ParameterType = DbType.Int32, ParameterValue = row.STATUS},
                new ParameterData(){ ParameterName="@Create_User", ParameterType = DbType.Int32, ParameterValue = row.CREATE_USER},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName="@Param_Zone_Id", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var retunCode = SQLHelper.ExecuteStoredProcForScalar("pr_add_event_team", parameters);
        }

        /// <summary>
        /// 更新参赛成员
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Team_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_ID},
                new ParameterData(){ ParameterName="@Event_Item_ID", ParameterType = DbType.Int32, ParameterValue = row.EVENT_ITEM_ID},
                new ParameterData(){ ParameterName="@Event_Team_Name", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME},
                new ParameterData(){ ParameterName="@Event_Team_Name_EN", ParameterType = DbType.String, ParameterValue = row.EVENT_TEAM_NAME_EN},
                new ParameterData(){ ParameterName="@Event_Team_Type1", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE1},
                new ParameterData(){ ParameterName="@Event_Team_Type2", ParameterType = DbType.Int32, ParameterValue = row.EVENT_TEAM_TYPE2},
                new ParameterData(){ ParameterName="@Status", ParameterType = DbType.Int32, ParameterValue = row.STATUS},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = row.LAST_UPDATE_USER},
                new ParameterData(){ ParameterName="@Param_Zone_Id", ParameterType = DbType.Int32, ParameterValue = row.PARAM_ZONE_ID}
            };
            var retunCode = SQLHelper.ExecuteStoredProcForScalar("pr_up_event_team", parameters);
        }

        /// <summary>
        /// 删除参赛成员
        /// </summary>
        /// <param name="eventTeamID"></param>
        public static void DeleteEventTeam(int eventTeamID)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Event_Team_ID", ParameterType = DbType.Int32, ParameterValue = eventTeamID}
            };
            var retunCode = SQLHelper.ExecuteStoredProcForScalar("pr_del_event_team", parameters);
        }

        /// <summary>
        /// 更新赛事成员状态
        /// </summary>
        /// <param name="eventTeamID"></param>
        /// <param name="eventTeamStatus"></param>
        public static void UpdateEventTeamStatus(int eventTeamID, EventTeamStatusEnum eventTeamStatus)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Event_Team_ID", ParameterType = DbType.Int32, ParameterValue = eventTeamID });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Team_Status", ParameterType = DbType.Int32, ParameterValue = (int)eventTeamStatus });
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_up_event_team_status", parameters);
        }

        public static DataTable GetEventTeamNameEN(int homeTeamId, int guestTeamId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@HomeTeamId", ParameterType = DbType.Int32, ParameterValue = homeTeamId });
            parameters.Add(new ParameterData() { ParameterName = "@GuestTeamId", ParameterType = DbType.Int32, ParameterValue = guestTeamId });
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            var dt = broker.ExecuteForDataTable("pr_event_team_name_en", parameters, CommandType.StoredProcedure);
            broker.Close();
            return dt;
        }
    }
}
