using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Presentation;
using YMGS.Data.Common;
using YMGS.Trade.Web.Football.Model;
using YMGS.Framework;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.Football.BusinessLogic
{
    public class FootballBLBase
    {
        public virtual int GetHomeTeamScore(DSMatchAndMarket.Match_ListRow football)
        {
            int homeTeamScore = 0;
            if (!football.IsHOME_FIR_HALF_SCORENull())
                homeTeamScore += football.HOME_FIR_HALF_SCORE;
            if (!football.IsHOME_SEC_HALF_SCORENull())
                homeTeamScore += football.HOME_SEC_HALF_SCORE;
            if (!football.IsHOME_OVERTIME_SCORENull())
                homeTeamScore += football.HOME_OVERTIME_SCORE;
            if (!football.IsHOME_POINT_SCORENull())
                homeTeamScore += football.HOME_POINT_SCORE;
            return homeTeamScore;
        }

        public virtual int GetGuestTeamScore(DSMatchAndMarket.Match_ListRow match)
        {
            int guestTeamScore = 0;
            if (!match.IsGUEST_FIR_HALF_SCORENull())
                guestTeamScore += match.GUEST_FIR_HALF_SCORE;
            if (!match.IsGUEST_SEC_HALF_SCORENull())
                guestTeamScore += match.GUEST_SEC_HALF_SCORE;
            if (!match.IsGUEST_OVERTIME_SCORENull())
                guestTeamScore += match.GUEST_OVERTIME_SCORE;
            if (!match.IsGUEST_POINT_SCORENull())
                guestTeamScore += match.GUEST_POINT_SCORE;
            return guestTeamScore;
        }

        public virtual string GetMatchStatus(MatchStatusEnum status)
        {
            return string.Empty;
        }

        public virtual bool IsMatchComingSoon(DateTime matchStartDate)
        {
            if (matchStartDate.CompareTo(DateTime.Now.AddMinutes(15)) <= 0 && matchStartDate.CompareTo(DateTime.Now) > 0)
                return true;
            return false;
        }

        public virtual bool IsMatchInPlay(DSMatchAndMarket.Match_ListRow match)
        {
            if (match.STARTDATE.CompareTo(DateTime.Now.AddMinutes(15)) >= 0 && match.IS_ZOUDI == true)
                return true;
            return false;
        }

        public virtual bool IsMatchHT(DSMatchAndMarket.Match_ListRow match)
        {
            var curMatchStatus = (MatchStatusEnum)match.STATUS;
            var curMatchAddtionalStatus = (MatchAdditionalStatusEnum)match.ADDITIONALSTATUS;
            if (curMatchStatus == MatchStatusEnum.HalfTimeFinished && curMatchAddtionalStatus == MatchAdditionalStatusEnum.Normal)
                return true;
            return false;
        }

        public virtual bool IsMatchStarted(DSMatchAndMarket.Match_ListRow match)
        {
            var curMatchStatus = (MatchStatusEnum)match.STATUS;
            var curMatchAddtionalStatus = (MatchAdditionalStatusEnum)match.ADDITIONALSTATUS;
            if ((curMatchStatus == MatchStatusEnum.InMatching || curMatchStatus == MatchStatusEnum.SecHalfStarted) && curMatchAddtionalStatus == MatchAdditionalStatusEnum.Normal)
                return true;
            return false;
        }

        public virtual bool IsMatchClosed(MatchStatusEnum matchStatus)
        {
            return (matchStatus == MatchStatusEnum.FullTimeFinished || matchStatus == MatchStatusEnum.FinishedCalculation) ? true : false;
        }

        public virtual bool IsMatchSuspend(MatchAdditionalStatusEnum additionalStatus)
        {
            return (additionalStatus == MatchAdditionalStatusEnum.Suspended) ? true : false;
        }

        public virtual bool IsMatchFreezed(MatchAdditionalStatusEnum additionalStatus)
        {
            return (additionalStatus == MatchAdditionalStatusEnum.FreezingMatch) ? true : false;
        }
        public virtual bool IsMatchStartingFreezed(MatchStatusEnum matchStatus)
        {
            return (matchStatus != MatchStatusEnum.NotActivated || matchStatus != MatchStatusEnum.Activated) ? true : false;
        }
        //public virtual bool IsMatchFreezed(DSMatchAndMarket.Match_ListRow match)
        //{
        //    var curMatchStatus = (MatchStatusEnum)match.STATUS;
        //    var curMatchAddtionalStatus = (MatchAdditionalStatusEnum)match.ADDITIONALSTATUS;
        //    if ((curMatchStatus != MatchStatusEnum.NotActivated || curMatchStatus != MatchStatusEnum.Activated) && curMatchAddtionalStatus == MatchAdditionalStatusEnum.FreezingMatch)
        //        return false;
        //    return true;
        //}

        public virtual int GetMatchStartedMinutes(DSMatchAndMarket.Match_ListRow match)
        {
            int startedMin = 0;
            if (match.IsFirst_Half_End_TimeNull())
            {//上半场未结束
                TimeSpan ts = DateTime.Now.Subtract(match.STARTDATE);
                startedMin = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + 1;
                if (startedMin > 45)
                {
                    startedMin = 45;
                }
            }
            if (!match.IsSec_Half_Start_TimeNull())
            {//下半场已经开始
                TimeSpan ts = DateTime.Now.Subtract(match.Sec_Half_Start_Time);
                startedMin = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + 46;
                if (startedMin > 90)
                {
                    startedMin = 90;
                }
            }
            return startedMin;
        }

        public virtual int GetMatchStartingMinutes(DSMatchAndMarket.Match_ListRow match)
        {
            TimeSpan ts = DateTime.Now.Subtract(match.STARTDATE);
            return ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes - 1;
        }

        public virtual string GetMatchLink(int eventItemId, int eventZoneId, DSMatchAndMarket.Match_ListRow match)
        {
            return string.Format("Default.aspx?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}", eventItemId, eventZoneId, match.EVENT_ID, UtilityHelper.DateToStr(match.STARTDATE), match.MATCH_ID);
        }

        public virtual string GetMatchLimitBetStatus(FootballMatch match)
        {
            if (match.IsMatchFreezed)
                return LangManager.GetString("Suspend");
            if (match.IsMatchClosed)
                return LangManager.GetString("Suspend");
            if (match.IsMatchSuspend)
                return LangManager.GetString("Suspend");
            return string.Empty;
        }

        public virtual string GetMatchStatusClass(FootballMatch match)
        {
            if (!string.IsNullOrEmpty(match.MatchLimitBetStatus))
                return "status-overlay";
            return "";
        }

        public virtual string GetCustomParam_1(FootballMatch football)
        {
            if (!football.IsZouDi)
                return football.MatchStartDate.ToString("HH:mm");
            if (football.IsMatchStarted)
                return string.Format("{0}'", football.MatchStartedMinutes);
            if (football.IsMatchHT)
                return LangManager.GetString("HT");
            if (football.IsMatchInPlay)
                return LangManager.GetString("InPlayStatus");
            if (football.IsMatchFreezed)
                if (!football.IsMatchStartingFreezed)
                { return LangManager.GetString("StartingSoon"); }
            if (football.IsMatchComingSoon)
                return string.Format("{0} {1}'", LangManager.GetString("StartingSoon"), Math.Abs(football.MatchStartingMinutes));
            return string.Empty;
        }

        public virtual string GetCustomParam_1forDefault(FootballMatch football)
        {
            if (football.MatchStartDate.CompareTo(DateTime.Now.AddMinutes(5)) < 0 && football.MatchStartDate.CompareTo(DateTime.Now) >= 0)
                return LangManager.GetString("DedaultStartingSoon");
            if (football.MatchStartDate.Date > DateTime.Now.Date)
                return string.Format("{0} {1}", LangManager.GetString("Tomorrow"), football.MatchStartDate.ToString("HH:mm"));
            return football.MatchStartDate.ToString("HH:mm");
        }
    }
}