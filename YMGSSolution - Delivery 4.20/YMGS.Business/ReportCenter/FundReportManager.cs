using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.ReportCenter;

namespace YMGS.Business.ReportCenter
{
    public class FundReportManager
    {
        public static DSFundReport QueryFundReport(DateTime startDate, DateTime endDate, int fundReportType)
        {
            return FundReportDA.QueryFundReport(startDate, endDate, fundReportType);
        }
    }
}
