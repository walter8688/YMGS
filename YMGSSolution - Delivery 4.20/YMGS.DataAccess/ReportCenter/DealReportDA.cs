using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Presentation;
using YMGS.Framework;

namespace YMGS.DataAccess.ReportCenter
{
    public class DealReportDA
    {
        public static DSDealReport QueryDealReport(DateTime startDate, DateTime endDate, string betType, string eventItemName, string eventZoneName, string eventName)
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
            parameters.Add(new ParameterData() { ParameterName = "@Bet_Type", ParameterType = DbType.String, ParameterValue = betType });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Item_Name", ParameterType = DbType.String, ParameterValue = eventItemName });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Zone_Name", ParameterType = DbType.String, ParameterValue = eventZoneName });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Name", ParameterType = DbType.String, ParameterValue = eventName });

            return SQLHelper.ExecuteStoredProcForDataSet<DSDealReport>("pr_get_deal_report", parameters);
        }
    }
}
