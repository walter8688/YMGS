using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;
using YMGS.Data.Presentation;

namespace YMGS.Business.MemberShip
{
    public class AccountApplyProxyManager
    {
        /// <summary>
        /// 查看自己的申请代理记录
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DSApplyProxy QueryAccountApplyProxyByUserID(DateTime startDate, DateTime endDate, int status, int userId)
        {
            return AccountApplyProxyDA.QueryAccountApplyProxyByUserID(startDate, endDate, status, userId);
        }

        /// <summary>
        /// 代理申请
        /// </summary>
        /// <param name="msgRow"></param>
        public static void AddAccountApplyProxy(DSApplyProxy.TB_APPLY_PROXYRow proxyRow)
        {
            AccountApplyProxyDA.AddAccountApplyProxy(proxyRow);
        }

        /// <summary>
        /// 删除代理申请
        /// </summary>
        /// <param name="MESSAGEID"></param>
        public static void DeleteAccountApplyProxyByProxyID(int Apply_Proxy_ID)
        {
            AccountApplyProxyDA.DeleteAccountApplyProxyByProxyID(Apply_Proxy_ID);
        }

        /// <summary>
        /// 取消代理申请
        /// </summary>
        /// <param name="Apply_Proxy_ID"></param>
        public static void CancelUserApplyProxy(int Apply_Proxy_ID)
        {
            AccountApplyProxyDA.CancelUserApplyProxy(Apply_Proxy_ID);
        }
    }
}
