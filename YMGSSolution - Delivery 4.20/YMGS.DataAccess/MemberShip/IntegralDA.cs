using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;


namespace YMGS.DataAccess.MemberShip
{
    public class IntegralDA
    {
        public static DSIntegralHistory QueryIntegralHistory(int userId,DateTime startDate,DateTime endDate)
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
            parameters.Add(new ParameterData() { ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSIntegralHistory>("pr_get_integral_history", parameters);
        }
    }
}
