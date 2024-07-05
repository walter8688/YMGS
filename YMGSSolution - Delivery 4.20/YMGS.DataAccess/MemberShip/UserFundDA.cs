using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.MemberShip
{
    public class UserFundDA
    {
        #region 获取用户资金账号信息
        /// <summary>
        /// 获取用户资金账号信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DSUserFund QueryUserFund(int userID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@UserID", ParameterType = DbType.Int32, ParameterValue = userID });
            var userFundDS = SQLHelper.ExecuteStoredProcForDataSet<DSUserFund>("pr_get_userfund_by_userid", parameters);
            return userFundDS;
        }
        #endregion

        #region 初始化用户资金账户
        /// <summary>
        /// 初始化用户资金账户
        /// </summary>
        /// <param name="userID"></param>
        public static void InitUserFund(int userID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@UserID", ParameterType = DbType.Int32, ParameterValue = userID });
            SQLHelper.ExecuteStoredProcForScalar("pr_inti_userfund", parameters);

        }
        #endregion

        #region 更新用户资金账户
        /// <summary>
        /// 更新用户资金账户
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateUserFund(DSUserFund.TB_USER_FUNDRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@User_Fund_ID", ParameterType = DbType.Int32, ParameterValue = row.USER_FUND_ID},
                new ParameterData(){ ParameterName = "@Bank_Name", ParameterType = DbType.String, ParameterValue = row.BANK_NAME},
                new ParameterData(){ ParameterName = "@Open_Bank_Name", ParameterType = DbType.String, ParameterValue = row.OPEN_BANK_NAME},
                new ParameterData(){ ParameterName = "@Card_No", ParameterType = DbType.String, ParameterValue = row.CARD_NO},
                new ParameterData(){ ParameterName = "@Account_Holder", ParameterType = DbType.String, ParameterValue = row.ACCOUNT_HOLDER},
                new ParameterData(){ ParameterName = "@Current_Fund", ParameterType = DbType.Decimal, ParameterValue = row.CUR_FUND},
                new ParameterData(){ ParameterName = "@Freezed_Fund", ParameterType = DbType.Decimal, ParameterValue = row.FREEZED_FUND},
                new ParameterData(){ ParameterName = "@Current_Intergral", ParameterType = DbType.Int32, ParameterValue = row.CUR_INTEGRAL},
                new ParameterData(){ ParameterName = "@Status", ParameterType = DbType.Int32, ParameterValue = row.STATUS}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_up_userfund", parameters);
        }
        #endregion

        #region 我的交易
        /// <summary>
        /// 获取我的交易
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DSMyTrade QueryTrade(DateTime start, DateTime end, DSMyTrade.TB_MY_TRADERow obj)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@BetType", ParameterType = DbType.Int32, ParameterValue = obj.IsBETTYPENull()?0:obj.BETTYPE });
            parameters.Add(new ParameterData() { ParameterName = "@BetStatus", ParameterType = DbType.Int32, ParameterValue = obj.IsSTATUSNull()?0:obj.STATUS });
            parameters.Add(new ParameterData() { ParameterName = "@startDateTime", ParameterType = DbType.DateTime, ParameterValue = start });
            parameters.Add(new ParameterData() { ParameterName = "@EndDateTime", ParameterType = DbType.DateTime, ParameterValue = end });
            parameters.Add(new ParameterData() { ParameterName = "@TRADEUSER", ParameterType = DbType.Int32, ParameterValue = obj.TRADE_USER });
            var mytrade = SQLHelper.ExecuteStoredProcForDataSet<DSMyTrade>("pr_get_mytrade", parameters);
            return mytrade;
        }
        #endregion

        #region 更新银行信息
        /// <summary>
        /// 更新银行信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bankName"></param>
        /// <param name="openBankName"></param>
        /// <param name="cardNo"></param>
        /// <param name="accoutHolder"></param>
        public static void SetUserFundBankInfo(int userId,string bankName,string openBankName,string cardNo,string accoutHolder)
        {
            var parameters = new List<ParameterData>() 
            {
                new ParameterData(){ ParameterName="@User_Id", ParameterType = DbType.Int32, ParameterValue = userId},
                new ParameterData(){ ParameterName="@Bank_Name", ParameterType = DbType.String, ParameterValue = bankName},
                new ParameterData(){ ParameterName="@Open_Bank_Name", ParameterType = DbType.String, ParameterValue = openBankName},
                new ParameterData(){ ParameterName="@Card_No", ParameterType = DbType.String, ParameterValue = cardNo},
                new ParameterData(){ ParameterName="@Account_Holder", ParameterType = DbType.String, ParameterValue = accoutHolder}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_set_userfund_bankinfo", parameters);
        }
        #endregion

        #region 获取用户资金明细
        public static DSFundHistory QueryFundHistory(int userId, DateTime startDate, DateTime endDate, int tradeType)
        {
            IList<ParameterData> parameters = new List<ParameterData>();

            parameters.Add(new ParameterData() { ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userId });
            if (startDate == DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = startDate });
            if (endDate == DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = endDate });
            parameters.Add(new ParameterData() { ParameterName = "@Trade_Type", ParameterType = DbType.Int32, ParameterValue = tradeType });

            return SQLHelper.ExecuteStoredProcForDataSet<DSFundHistory>("pr_get_user_fund_detail", parameters);
        }
        #endregion

        public static DSUserPayVCard QueryUserPayVCardDetail(string ordno)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Order_Id", ParameterType = DbType.String, ParameterValue = ordno });
            return SQLHelper.ExecuteStoredProcForDataSet<DSUserPayVCard>("pr_get_userpay_succeed_detail", parameters);
        }

        public static MyMatch QueryMyMatch(int TradeUser)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Trade_User", ParameterType = DbType.Int32, ParameterValue = TradeUser });
            return SQLHelper.ExecuteStoredProcForDataSet<MyMatch>("pr_get_my_match", parameters);
        }
    }
}
