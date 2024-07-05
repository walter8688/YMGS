using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Presentation;
using YMGS.Framework;

namespace YMGS.DataAccess.ReportCenter
{
    public class FundReportDA
    {
        public static DSFundReport QueryFundReport(DateTime startDate, DateTime endDate, int fundReportType)
        {
            var parameters = new List<ParameterData>();
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            parameters.Add(new ParameterData() { ParameterName = "@Fund_Type", ParameterType = DbType.Int32, ParameterValue = fundReportType });
            return SQLHelper.ExecuteStoredProcForDataSet<DSFundReport>("pr_get_fund_report", parameters);
        }
    }
}
