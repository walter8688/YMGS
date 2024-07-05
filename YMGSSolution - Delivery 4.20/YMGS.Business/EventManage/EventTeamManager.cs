using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.DataAccess.EventManage;
using YMGS.Data.Common;
using System.Data;

namespace YMGS.Business.EventManage
{
    public class EventTeamManager
    {
        /// <summary>
        /// 查询参赛成员的数据集合
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DSTeamList QueryEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            return EventTeamDA.QueryEventTeam(row);
        }

        /// <summary>
        /// 根据赛事获取赛事成员
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static DSEventTeam QueryEventTeamByEventID(int eventID)
        {
            return EventTeamDA.QueryEventTeamByEventID(eventID);
        }

        /// <summary>
        /// 新增参赛成员
        /// </summary>
        /// <param name="row"></param>
        public static void AddEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            EventTeamDA.AddEventTeam(row);
        }

        /// <summary>
        /// 更新参赛成员
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateEventTeam(DSEventTeam.TB_EVENT_TEAMRow row)
        {
            EventTeamDA.UpdateEventTeam(row);
        }

        /// <summary>
        /// 删除参赛成员
        /// </summary>
        /// <param name="eventTeamID"></param>
        public static void DeleteEventTeam(int eventTeamID)
        {
            EventTeamDA.DeleteEventTeam(eventTeamID);
        }

        /// <summary>
        /// 更新赛事成员状态
        /// </summary>
        /// <param name="eventTeamID"></param>
        /// <param name="eventTeamStatus"></param>
        public static void UpdateEventTeamStatus(int eventTeamID, EventTeamStatusEnum eventTeamStatus)
        {
            EventTeamDA.UpdateEventTeamStatus(eventTeamID, eventTeamStatus);
        }

        public static DataTable GetEventTeamNameEN(int homeTeamId, int guestTeamId)
        {
            return EventTeamDA.GetEventTeamNameEN(homeTeamId, guestTeamId);
        }
    }
}
