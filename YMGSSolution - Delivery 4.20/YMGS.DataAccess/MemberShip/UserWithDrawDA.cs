using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Framework;

namespace YMGS.DataAccess.MemberShip
{
    public class UserWithDrawDA
    {
        public static void AddUserWithDraw(DSUserWithDraw.TB_USER_WITHDRAWRow userWD)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userWD.USER_ID},
                new ParameterData(){ ParameterName = "@Trans_Id", ParameterType = DbType.String, ParameterValue = userWD.TRANS_ID},
                new ParameterData(){ ParameterName = "@WD_Status", ParameterType = DbType.Int32, ParameterValue = userWD.WD_STATUS},
                new ParameterData(){ ParameterName = "@WD_Amount", ParameterType = DbType.Decimal, ParameterValue = userWD.WD_AMOUNT},
                new ParameterData(){ ParameterName = "@Remark", ParameterType = DbType.String, ParameterValue = userWD.REMARK}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_user_withdraw", parameters);
        }

        public static DSUserWithDraw QueryUserWithDraw(DateTime startDate, DateTime endDate, int userId)
        {
            var parameters = new List<ParameterData>();
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@WD_SDate", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@WD_SDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@WD_EDate", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@WD_EDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            parameters.Add(new ParameterData() { ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSUserWithDraw>("pr_get_user_withdraw", parameters);
        }

        public static void CancleUserWithDraw(int userWithDrawId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_WD_Id", ParameterType = DbType.Int32, ParameterValue = userWithDrawId });
            SQLHelper.ExecuteStoredProcForScalar("pr_cancle_user_withdraw", parameters);
        }

        public static DSAccountWithDraw QueryAllUserWDInfo(DateTime startDate, DateTime endDate, decimal WDFromAmt, decimal WDToAmt, DSAccountWithDraw.TB_USER_WITHDRAWRow userWD)
        {
            var parameters = new List<ParameterData>();
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@WD_SDate", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@WD_SDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@WD_EDate", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@WD_EDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            parameters.AddRange(new ParameterData[] 
            { 
                new ParameterData() { ParameterName = "@User_Name", ParameterType = DbType.String, ParameterValue = userWD.LOGIN_NAME},
                new ParameterData() { ParameterName = "@WD_Status", ParameterType = DbType.Int32, ParameterValue = userWD.WD_STATUS},
                new ParameterData() { ParameterName = "@WD_From_Amt", ParameterType = DbType.Decimal, ParameterValue = WDFromAmt},
                new ParameterData() { ParameterName = "@WD_To_Amt", ParameterType = DbType.Decimal, ParameterValue = WDToAmt},
                new ParameterData() { ParameterName = "@WD_Bank_Name", ParameterType = DbType.String, ParameterValue = userWD.WD_BANK_NAME},
                new ParameterData() { ParameterName = "@WD_Card_No", ParameterType = DbType.String, ParameterValue = userWD.WD_CARD_NO},
                new ParameterData() { ParameterName = "@WD_ACCOUNT_HOLDER", ParameterType = DbType.String, ParameterValue = userWD.WD_ACCOUNT_HOLDER},
                new ParameterData() { ParameterName = "@Trans_Id", ParameterType = DbType.String, ParameterValue = userWD.TRANS_ID}
            });
            return SQLHelper.ExecuteStoredProcForDataSet<DSAccountWithDraw>("pr_query_user_withdraw", parameters);
        }

        public static void ConfirmUserWithDraw(int userWithDrawId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_WD_Id", ParameterType = DbType.Int32, ParameterValue = userWithDrawId });
            SQLHelper.ExecuteStoredProcForScalar("pr_confirm_user_withdraw", parameters);
        }

        public static void RejectUserWithDraw(int userWithDrawId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_WD_Id", ParameterType = DbType.Int32, ParameterValue = userWithDrawId });
            SQLHelper.ExecuteStoredProcForScalar("pr_reject_user_withdraw", parameters);
        }

        public static void TransferUserWithDraw(int userWithDrawId,string transId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_WD_Id", ParameterType = DbType.Int32, ParameterValue = userWithDrawId });
            parameters.Add(new ParameterData() { ParameterName = "@Trans_ID", ParameterType = DbType.String, ParameterValue = transId });
            SQLHelper.ExecuteStoredProcForScalar("pr_transfer_user_withdraw", parameters);
        }
    }
}
