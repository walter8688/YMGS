using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.GameMarket;
using YMGS.Data.Presentation;

namespace YMGS.Business.GameMarket
{
    public class ChampEventManager
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
        public static DSChampEvent QueryChampEvent(int? chanmpEventType, string champEvnetName, string champEventNameEn, string champEventDesc, DateTime? startDate, DateTime? endDate)
        {
            return ChampEventDA.QueryChampEvent(chanmpEventType, champEvnetName, champEventNameEn, champEventDesc, startDate, endDate);
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
            return ChampEventDA.QueryChampEventByKey(iChampEventId);        
        }

        #endregion

        #region 新增冠军赛事
        /// <summary>
        /// 新增冠军赛事
        /// </summary>
        /// <param name="champEvent"></param>
        public static void AddChampEvent(DSChampEvent.TB_Champ_EventRow champEvent, string eventMembers)
        {
            ChampEventDA.AddChampEvent(champEvent, eventMembers);
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
            var dsEventMember = ChampEventDA.QueryChampEventMemberById(champEventId);
            foreach (var eventMemberRow in dsEventMember.TB_Champ_Event_Member)
            {
                if (eventMemberRow.IsChamp_Event_Member_Name_EnNull())
                    continue;
                if (!string.IsNullOrEmpty(eventMemberRow.Champ_Event_Member_Name_En))
                    eventMemberRow.Champ_Event_Member_Name = eventMemberRow.Champ_Event_Member_Name + "|" + eventMemberRow.Champ_Event_Member_Name_En;
            }
            return dsEventMember;
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
            ChampEventDA.UpdateChampEvent(champEvent, eventMembers);
        }
        #endregion

        #region 删除冠军赛事
        /// <summary>
        /// 删除冠军赛事
        /// </summary>
        /// <param name="champEventId"></param>
        public static void DeleteChampEvent(int champEventId)
        {
            ChampEventDA.DeleteChampEvent(champEventId);
        }
        #endregion

        #region 激活冠军赛事
        /// <summary>
        /// 激活冠军赛事
        /// </summary>
        /// <param name="champEventId"></param>
        public static void ActiveChampEvent(int champEventId)
        {
            ChampEventDA.ActiveChampEvent(champEventId);
        }
        #endregion

        #region 暂停冠军赛事
        /// <summary>
        /// ParseChampEvent
        /// </summary>
        /// <param name="champEventId"></param>
        public static void ParseChampEvent(int champEventId)
        {
            ChampEventDA.ParseChampEvent(champEventId);
        }
        #endregion

        #region 结束冠军比赛
        public static void FinishChampEvent(int champEventId)
        {
            ChampEventDA.FinishChampEvent(champEventId);
        }
        #endregion

        #region 终止冠军赛事
        /// <summary>
        /// 终止冠军赛事
        /// </summary>
        /// <param name="champEventId"></param>
        public static void AbortChampEvent(int champEventId)
        {
            ChampEventDA.AbortChampEvent(champEventId);
        }
        #endregion

        #region 维护获胜成员
        /// <summary>
        /// 维护获胜成员
        /// </summary>
        /// <param name="champEventId"></param>
        /// <param name="winMemberIds"></param>
        public static void RecordWinMembers(int champEventId, string winMemberIds)
        {
            ChampEventDA.RecordWinMembers(champEventId, winMemberIds);
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
            return ChampEventDA.QueryChampWinMemList(champEventId);
        }
        #endregion

        #region 查询当前激活的冠军赛事和冠军市场信息

        /// <summary>
        /// 查询当前激活的冠军赛事和冠军市场信息
        /// </summary>
        /// <returns></returns>
        public static DSChampionEventAndMarket QueryCurActivatedChampionEventAndMarket()
        {
            return ChampEventDA.QueryCurActivatedChampionEventAndMarket();
        }

        #endregion

    }
}
