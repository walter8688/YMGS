using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.Pay
{
    public class PayDA
    {
        /// <summary>
        /// 新增充值记录
        /// </summary>
        /// <param name="userPay"></param>
        public static void AddUserPay(DSUserPay.TB_USER_PAYRow userPay)
        {
            var parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userPay.USER_ID },
                new ParameterData(){ ParameterName = "@Mer_Id", ParameterType = DbType.String, ParameterValue = userPay.MER_ID },
                new ParameterData(){ ParameterName = "@Order_Id", ParameterType = DbType.String, ParameterValue = userPay.ORDER_ID },
                new ParameterData(){ ParameterName = "@Trans_Amt", ParameterType = DbType.Decimal, ParameterValue = userPay.TRAN_AMOUNT },
                new ParameterData(){ ParameterName = "@Trans_Status", ParameterType = DbType.Int32, ParameterValue = userPay.TRAN_STATUS },
                new ParameterData(){ ParameterName = "@Vcard_Id", ParameterType = DbType.Int32, ParameterValue = userPay.VCARD_ID },
                new ParameterData(){ ParameterName = "@Trans_Type", ParameterType = DbType.Int32, ParameterValue = userPay.TRAN_TYPE }
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_user_pay", parameters);
        }

        public static DSUserPay QueryUserPay(DateTime startDate, DateTime endDate,int userId)
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

            parameters.Add(new ParameterData() { ParameterName = "@User_ID", ParameterType = DbType.Int32, ParameterValue = userId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSUserPay>("pr_get_user_pay", parameters);
        }

        public static DataSet QueryMaxOrderId(string transDate)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Trans_Date", ParameterType = DbType.String, ParameterValue = transDate });
            return SQLHelper.ExecuteStoredProcForDataSet<DataSet>("pr_get_userpay_orderid", parameters);
        }

        public static void UserPaySuccessed(string orderId, int VCardId)
        {
            var parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@Order_Id", ParameterType = DbType.String, ParameterValue = orderId },
                new ParameterData(){ ParameterName = "@Vcard_Id", ParameterType = DbType.Int32, ParameterValue = VCardId }
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_user_pay_successed", parameters);
        }
    }
}
