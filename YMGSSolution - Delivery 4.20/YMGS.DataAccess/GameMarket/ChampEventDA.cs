using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Framework;
using YMGS.Data.Common;

namespace YMGS.DataAccess.GameMarket
{
    public class ChampEventDA
    {

        #region 查询冠军赛事
        /// <summary>
        /// 查询冠军赛事
        /// </summary>
        /// <param name="chanmpEventType"></param>
        /// <param name="champEvnetName"></param>
        /// <param name="champEventDesc"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DSChampEvent QueryChampEvent(int? chanmpEventType, string champEvnetName,string champEventNameEn, string champEventDesc, DateTime? startDate, DateTime? endDate)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Champ_Event_Type", ParameterType = DbType.Int32, ParameterValue = chanmpEventType},
                new ParameterData(){ ParameterName = "@Champ_Event_Name", ParameterType = DbType.String, ParameterValue = champEvnetName},
                new ParameterData(){ ParameterName = "@Champ_Event_Name_EN", ParameterType = DbType.String, ParameterValue = champEventNameEn},
                new ParameterData(){ ParameterName = "@Champ_Event_Desc", ParameterType = DbType.String, ParameterValue = champEventDesc}
            };
            if (startDate == null)
                parameters.Add(new ParameterData() { ParameterName = "@Champ_Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Champ_Start_Date", ParameterType = DbType.DateTime, ParameterValue = startDate });
            if (endDate == null)
                parameters.Add(new ParameterData() { ParameterName = "@Champ_End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Champ_End_Date", ParameterType = DbType.DateTime, ParameterValue = endDate });
            var champEventList = SQLHelper.ExecuteStoredProcForDataSet<DSChampEvent>("pr_get_champ_event", parameters);
            return champEventList;
        }
        #endregion

        #region 依据冠军赛事主键查询冠军赛事
        /// <summary>
        ///依据冠军赛事主键查询冠军赛事
        /// </summary>
        /// <param name="iChampEventId"></param>
        /// <param name="champEvnetName"></param>
        /// <param name="champEventDesc"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DSChampEvent QueryChampEventByKey(int iChampEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = iChampEventId}
            };
            var champEventList = SQLHelper.ExecuteStoredProcForDataSet<DSChampEvent>("pr_get_champ_event_by_key", parameters);
            return champEventList;
        }
        #endregion

        #region 新增冠军赛事
        /// <summary>
        /// 新增冠军赛事
        /// </summary>
        /// <param name="champEvent"></param>
        public static void AddChampEvent(DSChampEvent.TB_Champ_EventRow champEvent, string eventMembers)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Champ_Event_Type", ParameterType = DbType.Int32, ParameterValue = champEvent.Champ_Event_Type},
                new ParameterData(){ ParameterName = "@Event_Id", ParameterType = DbType.Int32, ParameterValue = champEvent.Event_ID},
                new ParameterData(){ ParameterName = "@Champ_Event_Name", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Name},
                new ParameterData(){ ParameterName = "@Champ_Event_Name_EN", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Name_En},
                new ParameterData(){ ParameterName = "@Champ_Event_Desc", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Desc},
                new ParameterData(){ ParameterName = "@Champ_Event_StartDate", ParameterType = DbType.DateTime, ParameterValue = champEvent.Champ_Event_StartDate},
                new ParameterData(){ ParameterName = "@Champ_Event_EndDate", ParameterType = DbType.DateTime, ParameterValue = champEvent.Champ_Event_EndDate},
                new ParameterData(){ ParameterName = "@Champ_Event_Status", ParameterType = DbType.Int32, ParameterValue = champEvent.Champ_Event_Status},
                new ParameterData(){ ParameterName = "@Create_User", ParameterType = DbType.Int32, ParameterValue = champEvent.Create_User},
                new ParameterData(){ ParameterName = "@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = champEvent.Last_Update_User},
                new ParameterData(){ ParameterName = "@Champ_Event_Members", ParameterType = DbType.String, ParameterValue = eventMembers}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_champ_event", parameters);
        }
        #endregion

        #region 根据冠军赛事ID获取冠军赛事成员
        /// <summary>
        /// 获取冠军赛事成员
        /// </summary>
        /// <param name="champEventId"></param>
        /// <returns></returns>
        public static DSChampEventMember QueryChampEventMemberById(int champEventId)
        {
            IList<ParameterData> parameter = new List<ParameterData>();
            parameter.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            var memberList = SQLHelper.ExecuteStoredProcForDataSet<DSChampEventMember>("pr_get_champ_member_by_id", parameter);
            return memberList;
        }
        #endregion

        #region 更新冠军赛事
        /// <summary>
        /// 更新冠军赛事
        /// </summary>
        /// <param name="champEvent"></param>
        /// <param name="eventMembers"></param>
        public static void UpdateChampEvent(DSChampEvent.TB_Champ_EventRow champEvent, string eventMembers)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEvent.Champ_Event_ID},
                new ParameterData(){ ParameterName = "@Champ_Event_Name", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Name},
                new ParameterData(){ ParameterName = "@Champ_Event_Name_EN", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Name_En},
                new ParameterData(){ ParameterName = "@Champ_Event_Desc", ParameterType = DbType.String, ParameterValue = champEvent.Champ_Event_Desc},
                new ParameterData(){ ParameterName = "@Champ_Event_StartDate", ParameterType = DbType.DateTime, ParameterValue = champEvent.Champ_Event_StartDate},
                new ParameterData(){ ParameterName = "@Champ_Event_EndDate", ParameterType = DbType.DateTime, ParameterValue = champEvent.Champ_Event_EndDate},
                new ParameterData(){ ParameterName = "@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = champEvent.Last_Update_User},
                new ParameterData(){ ParameterName = "@Champ_Event_Members", ParameterType = DbType.String, ParameterValue = eventMembers}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_update_champ_event", parameters);
        }
        #endregion

        #region 删除冠军赛事
        /// <summary>
        /// 删除冠军赛事
        /// </summary>
        /// <param name="champEventId"></param>
        public static void DeleteChampEvent(int champEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            SQLHelper.ExecuteStoredProcForScalar("pr_del_champ_event", parameters);
        }
        #endregion

        #region 激活冠军赛事
        public static void ActiveChampEvent(int champEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            SQLHelper.ExecuteStoredProcForScalar("pr_active_champ_event", parameters);
        }
        #endregion

        #region 暂停冠军赛事
        public static void ParseChampEvent(int champEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            SQLHelper.ExecuteStoredProcForScalar("pr_pause_champ_event", parameters);
        }
        #endregion

        #region 终止冠军赛事
        public static void AbortChampEvent(int champEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            SQLHelper.ExecuteStoredProcForScalar("pr_abort_champ_event", parameters);
        }
        #endregion

        #region 结束冠军比赛
        public static void FinishChampEvent(int champEventId)
        {
            var parameters =new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            SQLHelper.ExecuteStoredProcForScalar("pr_finish_champ_event", parameters);
        }
        #endregion

        #region 维护获胜成员
        public static void RecordWinMembers(int champEventId, string winMemberIds)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Win_Members", ParameterType = DbType.String, ParameterValue = winMemberIds });
            SQLHelper.ExecuteStoredProcForScalar("pr_add_champ_win_members", parameters);
        }
        #endregion

        #region 获取冠军赛事获胜成员
        /// <summary>
        /// 获取冠军赛事获胜成员
        /// </summary>
        /// <param name="champEventId"></param>
        /// <returns></returns>
        public static DSChampWinMemList QueryChampWinMemList(int champEventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Champ_Event_Id", ParameterType = DbType.Int32, ParameterValue = champEventId });
            var winMemList = SQLHelper.ExecuteStoredProcForDataSet<DSChampWinMemList>("pr_get_champ_event_winmembers", parameters);
            return winMemList;
        }
        #endregion

        #region 查询当前激活的冠军赛事和冠军市场信息

        /// <summary>
        /// 查询当前激活的冠军赛事和冠军市场信息
        /// </summary>
        /// <returns></returns>
        public static DSChampionEventAndMarket QueryCurActivatedChampionEventAndMarket()
        {
            var champEventList = SQLHelper.ExecuteStoredProcForDataSet<DSChampionEventAndMarket>("pr_get_champion_market_for_betting", null);
            return champEventList;
        }

        #endregion
    }
}
