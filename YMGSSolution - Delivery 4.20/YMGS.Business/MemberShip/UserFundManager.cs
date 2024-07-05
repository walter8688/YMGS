using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;
using YMGS.Data.Presentation;

namespace YMGS.Business.MemberShip
{
    public class UserFundManager
    {
        #region 获取用户资金账号信息
        /// <summary>
        /// 获取用户资金账号信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DSUserFund QueryUserFund(int userID)
        {
            return UserFundDA.QueryUserFund(userID);
        }
        #endregion

        #region 初始化用户资金账户
        /// <summary>
        /// 初始化用户资金账户
        /// </summary>
        /// <param name="userID"></param>
        public static void InitUserFund(int userID)
        {
            UserFundDA.InitUserFund(userID);
        }
        #endregion

        #region 更新用户资金账户
        /// <summary>
        /// 更新用户资金账户
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateUserFund(DSUserFund.TB_USER_FUNDRow row)
        {
            UserFundDA.UpdateUserFund(row);
        }
        #endregion

        #region 设置用户资金账户银行信息
        /// <summary>
        /// 设置用户资金账户银行信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bankName"></param>
        /// <param name="openBankName"></param>
        /// <param name="cardNo"></param>
        /// <param name="accoutHolder"></param>
        public static void SetUserFundBankInfo(int userId, string bankName, string openBankName, string cardNo, string accoutHolder)
        {
            UserFundDA.SetUserFundBankInfo(userId, bankName, openBankName, cardNo, accoutHolder);
        }
        #endregion

        #region 猎取我的交易
        /// <summary>
        /// 猎取我的交易
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="obj"></param>
        public static DSMyTrade QueryTrade(DateTime start, DateTime end, DSMyTrade.TB_MY_TRADERow obj)
        {
            return UserFundDA.QueryTrade(start, end, obj);
        }
        #endregion

        #region 获取用户资金明细
        public static DSFundHistory QueryFundHistory(int userId, DateTime startDate, DateTime endDate, int tradeType)
        {
            return UserFundDA.QueryFundHistory(userId, startDate, endDate, tradeType);
        }
        #endregion

        public static DSUserPayVCard QueryUserPayVCardDetail(string ordno)
        {
            return UserFundDA.QueryUserPayVCardDetail(ordno);
        }

        public static MyMatch QueryMyMatch(int TradeUser)
        {
            return UserFundDA.QueryMyMatch(TradeUser);
        }
    }
}
