using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.DataAccess.EventManage;
using YMGS.DataAccess.GameMarket;
using YMGS.Data.Common;
using System.Transactions;
using YMGS.Business.GameMarket;

namespace YMGS.Business.EventManage
{
    public class EventManager
    {
        #region 根据条件查询赛事
        public static DSEventTeamList QueryEvent(int? eventZoneID, string eventName, string eventNameEn, string eventDesc, DateTime? startDate, DateTime? endDate, int? status, int? eventTeamID, string eventTeamName)
        {
            return EventDA.QueryEvent(eventZoneID, eventName, eventNameEn, eventDesc, startDate, endDate, status, eventTeamID, eventTeamName);
        }
        #endregion

        #region 根据赛事ID查询赛事
        public static DSEvent.TB_EVENTRow QueryEventByEventID(int? eventID)
        {
            var eventDS = EventDA.QueryEventByEventID(eventID);
            if (eventDS == null)
                return null;
            return (DSEvent.TB_EVENTRow)eventDS.Tables[0].Rows[0];
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
            EventDA.AddEvent(row, eventTeamIDS);
        }
        #endregion

        #region 删除赛事
        public static void DeleteEvent(int eventID)
        {
            EventDA.DeleteEvent(eventID);
        }
        #endregion

        #region 修改赛事
        public static void UpdateEvent(DSEvent.TB_EVENTRow row, string eventTeamIDS)
        {
            EventDA.UpdateEvent(row, eventTeamIDS);
        }
        #endregion

        #region 激活赛事
        public static void AvtivatedEvent(int eventID, int lastUpdateUserID)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0)))
                {
                    var matchList = EventDA.QueryMatchByEventID(eventID);
                    var activatedMatchList = matchList.TB_MATCH.Where(m => m.ADDITIONALSTATUS == (int)MatchAdditionalStatusEnum.Suspended);
                    foreach (DSMatch.TB_MATCHRow row in activatedMatchList)
                    {
                        MatchManager.NoramlMatch(row.MATCH_ID, lastUpdateUserID);
                    }
                    EventDA.ActivatedEvent(eventID, lastUpdateUserID);
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 暂停赛事
        public static void PauseEvent(int eventID, int lastUpdateUserID)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0)))
                {
                    var matchList = EventDA.QueryMatchByEventID(eventID);
                    //已激活，比赛已开始，半场已结束
                    var canPauseMatchList = matchList.TB_MATCH.Where(m => ((MatchAdditionalStatusEnum)m.ADDITIONALSTATUS == MatchAdditionalStatusEnum.Normal && ((MatchStatusEnum)m.STATUS == MatchStatusEnum.Activated || (MatchStatusEnum)m.STATUS == MatchStatusEnum.InMatching || (MatchStatusEnum)m.STATUS == MatchStatusEnum.HalfTimeFinished)));
                    foreach (DSMatch.TB_MATCHRow row in canPauseMatchList)
                    {
                        //更新比赛状态
                        MatchManager.SuspendMatch(row.MATCH_ID, lastUpdateUserID);
                    }
                    //更新赛事状态
                    EventDA.PauseEvent(eventID, lastUpdateUserID);
                    trans.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 终止赛事
        public static void AbortEvent(int eventID, int lastUpdateUserID)
        {
            try
            {
                //如果赛事下面有比赛已经开始，则不能终止赛事
                var matchList = EventDA.QueryMatchByEventID(eventID);
                IEnumerable<DSMatch.TB_MATCHRow> notAbortMatchList = matchList.TB_MATCH.Where(m => m.STATUS > 1);
                if (notAbortMatchList.GetEnumerator().Current != null || notAbortMatchList.Count() > 0)
                {
                    throw new Exception("赛事下属比赛中已有比赛开赛，不能终止!");
                }
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0)))
                {   
                    //已激活，暂停，比赛已开始，半场已结束
                    //var canAbortMatchList = matchList.TB_MATCH.Where(m => ((MatchStatusEnum)m.STATUS == MatchStatusEnum.Activated || (MatchStatusEnum)m.STATUS == MatchStatusEnum.InMatching || (MatchStatusEnum)m.STATUS == MatchStatusEnum.HalfTimeFinished));
                    //已激活
                    var canAbortMatchList = matchList.TB_MATCH.Where(m => ((MatchStatusEnum)m.STATUS == MatchStatusEnum.Activated)); 
                    foreach (DSMatch.TB_MATCHRow row in canAbortMatchList)
                    {
                        MatchManager.AbortMatch(row.MATCH_ID, lastUpdateUserID);
                    }
                    EventDA.AbortEvent(eventID, lastUpdateUserID);
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
