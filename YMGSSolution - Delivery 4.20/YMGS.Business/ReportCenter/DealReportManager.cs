using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.ReportCenter;

namespace YMGS.Business.ReportCenter
{
    public class DealReportManager
    {
        public static DSDealReport QueryDealReport(DateTime startDate, DateTime endDate, string betType, string eventItemName, string eventZoneName, string eventName)
        {
            return DealReportDA.QueryDealReport(startDate, endDate, betType, eventItemName, eventZoneName, eventName);
        }
    }
}
