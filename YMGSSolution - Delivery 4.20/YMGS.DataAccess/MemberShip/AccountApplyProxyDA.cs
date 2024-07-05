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
    public class AccountApplyProxyDA
    {

        /// <summary>
        /// 根据会员ID获取与自己相关的系统消息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DSApplyProxy QueryAccountApplyProxyByUserID(DateTime startDate, DateTime endDate, int status, int userId)
        {
            var parameters = new List<ParameterData>();
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Apply_SDate", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Apply_SDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@Apply_EDate", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Apply_EDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            parameters.AddRange(new ParameterData[] { 
                new ParameterData() { ParameterName = "@Apply_Status", ParameterType = DbType.Int32, ParameterValue = status },
                new ParameterData() { ParameterName = "@User_ID", ParameterType = DbType.Int32, ParameterValue = userId }
            });
            return SQLHelper.ExecuteStoredProcForDataSet<DSApplyProxy>("pr_get_proxyApplyByUserID", parameters);
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="msgRow"></param>
        public static void AddAccountApplyProxy(DSApplyProxy.TB_APPLY_PROXYRow applyProxyRow)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Apply_Proxy_ID",ParameterType = DbType.Int32,ParameterValue= ParameterDirection.Output},
                new ParameterData(){ ParameterName = "@User_ID", ParameterType = DbType.Int32, ParameterValue = applyProxyRow.User_ID},
                new ParameterData(){ ParameterName = "@Role_ID", ParameterType = DbType.Int32, ParameterValue = applyProxyRow.Role_ID},
                new ParameterData(){ ParameterName = "@User_Telephone", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_Telephone},
                new ParameterData(){ ParameterName = "@User_Country", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_Country},
                new ParameterData(){ ParameterName = "@User_Province", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_Province},
                new ParameterData(){ ParameterName = "@User_City", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_City},
                new ParameterData(){ ParameterName = "@User_BankAddress", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_BankAddress},
                new ParameterData(){ ParameterName = "@User_BankNO", ParameterType = DbType.String, ParameterValue = applyProxyRow.User_BankNO},
                new ParameterData(){ ParameterName = "@Apply_Status", ParameterType = DbType.Int32, ParameterValue = applyProxyRow.Apply_Status},
                new ParameterData(){ ParameterName = "@Apply_Date", ParameterType = DbType.DateTime, ParameterValue = applyProxyRow.Apply_Date},
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_proxyApply", parameters);
        }

        /// <summary>
        /// 删除代理申请
        /// </summary>
        /// <param name="userID"></param>
        public static void DeleteAccountApplyProxyByProxyID(int Apply_Proxy_ID)
        {
            //var parameters = new List<ParameterData>();
            //parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_ID", ParameterType = DbType.Int32, ParameterValue = Apply_Proxy_ID });
            //SQLHelper.ExecuteStoredProcForScalar("", parameters);
        }

        /// <summary>
        /// 取消代理申请
        /// </summary>
        /// <param name="Apply_Proxy_ID"></param>
        public static void CancelUserApplyProxy(int Apply_Proxy_ID)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_ID", ParameterType = DbType.Int32, ParameterValue = Apply_Proxy_ID });
            SQLHelper.ExecuteStoredProcForScalar("pr_cancel_userProxyApply", parameters);
        }
    }
}
