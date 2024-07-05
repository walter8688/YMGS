using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Common;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;

namespace YMGS.Trade.Web.Football.BusinessLogic
{
    public class CenterMatchMarketBase
    {

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

        public virtual string GetMatchLimitBetStatus(FootballMarketInfo ftMarket)
        {
            if (ftMarket.IsMFreezed)
                return LangManager.GetString("Suspend");
            if (ftMarket.IsMClosed)
                return LangManager.GetString("Suspend");
            if (ftMarket.IsMSuspend)
                return LangManager.GetString("Suspend");
            return string.Empty;
        }

        public virtual string GetMatchStatusClass(FootballMarketInfo ftMarket)
        {
            if (!string.IsNullOrEmpty(ftMarket.DivCharacter))
                return "status-overlay";
            return "";
        }

        public virtual bool isChampMatchClosed(ChampEventStatusEnum champStatus)
        {
            return champStatus == ChampEventStatusEnum.Abort ? true : false;
        }

        public virtual bool isChampMatchSuspend(ChampEventStatusEnum champStatus)
        {
            return champStatus == ChampEventStatusEnum.Pause ? true : false;
        }
    }
}