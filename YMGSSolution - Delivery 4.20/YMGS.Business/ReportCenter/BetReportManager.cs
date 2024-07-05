using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.ReportCenter;
using YMGS.Data.DataBase;

namespace YMGS.Business.ReportCenter
{
    public class BetReportManager
    {
        public static DSBetReport QueryBetReport(DateTime startDate, DateTime endDate, int exchangeType, string betType, string eventItemName, string eventZoneName, string eventName)
        {
            return BetReportDA.QueryBetReport(startDate, endDate, exchangeType, betType, eventItemName, eventZoneName, eventName);
        }
        public static DSMatchRpt QueryBetReport(int matchtype, int matchid, int marketid)
        {
            return BetReportDA.QueryBLDReport(matchtype, matchid, marketid);
        }
    }
}
