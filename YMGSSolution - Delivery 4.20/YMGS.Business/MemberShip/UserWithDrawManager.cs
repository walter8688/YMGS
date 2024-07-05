using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;
using YMGS.Data.Presentation;

namespace YMGS.Business.MemberShip
{
    public class UserWithDrawManager
    {
        /// <summary>
        /// 新增用户提现记录
        /// </summary>
        /// <param name="userWD"></param>
        public static void AddUserWithDraw(DSUserWithDraw.TB_USER_WITHDRAWRow userWD)
        {
            UserWithDrawDA.AddUserWithDraw(userWD);
        }

        /// <summary>
        /// 查询用户提现记录
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DSUserWithDraw QueryUserWithDraw(DateTime startDate, DateTime endDate, int userId)
        {
            return UserWithDrawDA.QueryUserWithDraw(startDate, endDate, userId);
        }

        /// <summary>
        /// 取消提现记录
        /// </summary>
        /// <param name="userWithDrawId"></param>
        public static void CancleUserWithDraw(int userWithDrawId)
        {
            UserWithDrawDA.CancleUserWithDraw(userWithDrawId);
        }

        //根据多条件查询用户提现数据
        public static DSAccountWithDraw QueryAllUserWDInfo(DateTime startDate, DateTime endDate, decimal WDFromAmt, decimal WDToAmt, DSAccountWithDraw.TB_USER_WITHDRAWRow userWD)
        {
            return UserWithDrawDA.QueryAllUserWDInfo(startDate, endDate, WDFromAmt, WDToAmt, userWD);
        }

        /// <summary>
        /// 确认提现申请
        /// </summary>
        /// <param name="userWithDrawId"></param>
        public static void ConfirmUserWithDraw(int userWithDrawId)
        {
            UserWithDrawDA.ConfirmUserWithDraw(userWithDrawId);
        }

        /// <summary>
        /// 拒绝提现申请
        /// </summary>
        /// <param name="userWithDrawId"></param>
        public static void RejectUserWithDraw(int userWithDrawId)
        {
            UserWithDrawDA.RejectUserWithDraw(userWithDrawId);
        }

        /// <summary>
        /// 转账提现申请
        /// </summary>
        /// <param name="userWithDrawId"></param>
        public static void TransferUserWithDraw(int userWithDrawId,string transId)
        {
            UserWithDrawDA.TransferUserWithDraw(userWithDrawId,transId);
        }

    }
}
