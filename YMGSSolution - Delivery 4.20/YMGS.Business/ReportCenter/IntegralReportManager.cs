using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.ReportCenter;

namespace YMGS.Business.ReportCenter
{
    public class IntegralReportManager
    {
        public static DSBrokerageReport QueryBrokerageReport(DateTime startDate, DateTime endDate)
        {
            return IntegralReportDA.QueryBrokerageReport(startDate, endDate);
        }

        public static DSCommissionReport QueryCommissionReport(DateTime startDate, DateTime endDate)
        {
            return IntegralReportDA.QueryCommissionReport(startDate, endDate);
        }
    }
}
