using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    [Serializable]
    public class MatchObject
    {
        public int MatchEventItemId { get; set; }
        public string MatchEventItemName { get; set; }
        public string MatchEventItemNameEN { get; set; }
        public int MatchEventZoneId { get; set; }
        public string MatchEventZoneName { get; set; }
        public string MatchEventZoneNameEN { get; set; }
        public string MatchEventId { get; set; }
        public string MatchEventName { get; set; }
        public string MatchEventNameEN { get; set; }

        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public string MatchNameEN { get; set; }
        public DateTime MatchStartDate { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamNameEN { get; set; }
        public string GuestTeamName { get; set; }
        public string GuestTeamNameEN { get; set; }
        public int HomeTeamScore { get; set; }
        public int GuestTeamScore { get; set; }
        public string MatchLink { get; set; }
        public string CurrentMatchStatus { get; set; }
        public bool IsZouDi { get; set; }
        public bool IsMatchComingSoon { get; set; }
        public bool IsMatchInPlay { get; set; }
        public bool IsMatchHT{ get; set; }
        public bool IsMatchStarted { get; set; }
        public bool IsMatchClosed { get; set; }
        public bool IsMatchSuspend { get; set; }
        public bool IsMatchFreezed { get; set; }
        public int IsMatchFaved { get; set; }//0:未收藏;1:收藏
        public string MatchFavedCalss { get; set; }
        public string MatchLimitBetStatus { get; set; }
        public string MatchStatusClass { get; set; }
        public string CurrentScore { get; set; }
        public int MatchStartingMinutes { get; set; }//距离封盘还有多少时间
        public int MatchStartedMinutes { get; set; }//比赛进行了所长时间
        public string CustomParam_1 { get; set; }
        //默认显示在In-Play的市场模板Id
        public int DefaultMarketTmpId { get; set; }
    }
}
