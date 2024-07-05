using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.Presentation;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.ReportCenter
{
    public class BetReportDA
    {
        public static DSBetReport QueryBetReport(DateTime startDate, DateTime endDate, int exchangeType, string betType, string eventItemName, string eventZoneName, string eventName)
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
            parameters.Add(new ParameterData() { ParameterName = "@Exchange_Type", ParameterType = DbType.Int32, ParameterValue = exchangeType });
            parameters.Add(new ParameterData() { ParameterName = "@Bet_Type", ParameterType = DbType.String, ParameterValue = betType });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Item_Name", ParameterType = DbType.String, ParameterValue = eventItemName });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Zone_Name", ParameterType = DbType.String, ParameterValue = eventZoneName });
            parameters.Add(new ParameterData() { ParameterName = "@Event_Name", ParameterType = DbType.String, ParameterValue = eventName });

            return SQLHelper.ExecuteStoredProcForDataSet<DSBetReport>("pr_get_bet_report", parameters);
        }

        public static DSMatchRpt QueryBLDReport(int matchtype, int matchid, int marketid)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                 new ParameterData(){ParameterName="@match_type",ParameterType=DbType.Int32,ParameterValue=matchtype},
                new ParameterData(){ParameterName="@MATCH_ID",ParameterType=DbType.Int32,ParameterValue=matchid},
                 new ParameterData(){ParameterName="@MARKET_ID",ParameterType=DbType.Int32,ParameterValue=marketid}
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSMatchRpt>("pr_match_rpt", parameters);
        }
    }
}
