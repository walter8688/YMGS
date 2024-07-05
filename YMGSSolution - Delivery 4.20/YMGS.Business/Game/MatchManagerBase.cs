using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;

using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Data.Entity;

namespace YMGS.Business.Game
{
    public class MatchManagerBase
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

        public virtual bool IsMatchComingSoon(DateTime freezedStartDate)
        {
            if (freezedStartDate.CompareTo(DateTime.Now.AddMinutes(15)) < 0 && freezedStartDate.CompareTo(DateTime.Now) >= 0)
                return true;
            return false;
        }

        public virtual bool IsMatchInPlay(DSMatchAndMarket.Match_ListRow match)
        {
            if (match.AUTO_FREEZE_DATE.CompareTo(DateTime.Now.AddMinutes(15)) >= 0 && match.IS_ZOUDI == true)
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

        public virtual int GetMatchStartedMinutes(DSMatchAndMarket.Match_ListRow match)
        {
            int htTimeDiff = 0;
            if (!match.IsFirst_Half_End_TimeNull() && !match.IsSec_Half_Start_TimeNull())
            {
                var htts = match.Sec_Half_Start_Time.Subtract(match.First_Half_End_Time);
                htTimeDiff = match.Sec_Half_Start_Time.Subtract(match.First_Half_End_Time).Days * 24 * 60 + htts.Hours * 60 + htts.Minutes;
            }
            TimeSpan ts = DateTime.Now.Subtract(match.STARTDATE);
            return ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes - htTimeDiff;
        }

        public virtual int GetMatchStartingMinutes(DSMatchAndMarket.Match_ListRow match)
        {
            TimeSpan ts = DateTime.Now.Subtract(match.AUTO_FREEZE_DATE);
            return ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes ;
        }

        public virtual string GetMatchLink(int eventItemId, int eventZoneId, DSMatchAndMarket.Match_ListRow match)
        {
            return string.Format("~/Default.aspx?Ent=0&item={0}&zone={1}&eventid={2}&itemdate={3}&matchid={4}", eventItemId, eventZoneId, match.EVENT_ID, UtilityHelper.DateToStrOrDefault(match.STARTDATE, ""), match.MATCH_ID);
        }

        public virtual string GetMatchLimitBetStatus(MatchObject match)
        {
            if (match.IsMatchFreezed)
                return "Suspend";
            if (match.IsMatchClosed)
                return "Suspend";
            if (match.IsMatchSuspend)
                return "Suspend";
            return string.Empty;
        }

        public virtual string GetMatchStatusClass(MatchObject match)
        {
            if (!string.IsNullOrEmpty(match.MatchLimitBetStatus))
                return "status-overlay";
            return "";
        }
    }
}
