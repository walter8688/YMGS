using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.SystemSetting
{
    public class AgentDA
    {
        #region 获取所有代理人数据
        public static DSAgentList QueryAgent(int roleID, string userName,string agentName)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@Role_Id", ParameterType = DbType.Int32, ParameterValue = roleID},
                new ParameterData(){ ParameterName = "@User_Name", ParameterType = DbType.String, ParameterValue = userName},
                new ParameterData(){ ParameterName = "@Agent_Name", ParameterType = DbType.String, ParameterValue = agentName},
            };
            var agentList = SQLHelper.ExecuteStoredProcForDataSet<DSAgentList>("pr_get_all_agent", parameters);
            return agentList;
        }
        #endregion

        #region 设置代理人佣金率&会员人数
        public static void SetAgentDetail(DSAgentDetail.TB_AGENT_DETAILRow agentDetail)
        { 
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = agentDetail.Agent_User_ID},
                new ParameterData(){ ParameterName = "@Brokerage", ParameterType = DbType.Decimal, ParameterValue = agentDetail.Brokerage},
                new ParameterData(){ ParameterName = "@Member_Count", ParameterType = DbType.Int32, ParameterValue = agentDetail.Member_Count}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_set_agent_detail", parameters);
        }
        #endregion

        #region 获取下属会员
        /// <summary>
        /// 获取下属会员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DSAgentMember QueryAgentMember(int userId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSAgentMember>("pr_get_belong_member", parameters);
        }
        #endregion

        #region 取消代理
        public static void CancleAgent(int userId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@User_ID", ParameterType = DbType.Int32, ParameterValue = userId });
            SQLHelper.ExecuteStoredProcForScalar("pr_cancle_agent", parameters);
        }
        #endregion

        #region 获取代理类型
        public static DataSet GetAllAgentType()
        {
             return SQLHelper.ExecuteStoredProcForDataSet<DataSet>("pr_get_all_agenttype",null);
        }
        #endregion

        #region 获取代理申请记录
        public static DSApplyAgent QueryApplyAgentDeatail(int? roleId, DateTime applyStartDate, DateTime applyEndDate,string userName,int? applyStatus)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
             {
                 new ParameterData(){ ParameterName = "@Role_Id", ParameterType = DbType.Int32 , ParameterValue = roleId},
                 new ParameterData(){ ParameterName = "@User_Name", ParameterType = DbType.String , ParameterValue = userName},
                 new ParameterData(){ ParameterName = "@Apply_Status", ParameterType = DbType.Int32 , ParameterValue = applyStatus}
             };
            if (applyStartDate == DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Apply_Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Apply_Start_Date", ParameterType = DbType.DateTime, ParameterValue = applyStartDate });
            if (applyEndDate == DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@Apply_End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Apply_End_Date", ParameterType = DbType.DateTime, ParameterValue = applyEndDate });
            return SQLHelper.ExecuteStoredProcForDataSet<DSApplyAgent>("pr_get_apply_agent", parameters);
        }
        #endregion

        #region 审核申请代理
        public static void ApproveProcessApplyProxy(int applyProxyId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_Id", ParameterType = DbType.Int32, ParameterValue = applyProxyId });
            SQLHelper.ExecuteStoredProcForScalar("pr_approveprocess_apply_proxy", parameters);
        }
        #endregion

        #region 批准申请代理
        public static void ConfirmApplyProxy(int applyProxyId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_Id", ParameterType = DbType.Int32, ParameterValue = applyProxyId });
            SQLHelper.ExecuteStoredProcForScalar("pr_confirm_apply_proxy", parameters);
        }
        #endregion

        #region 拒绝申请代理
        public static void RejectApplyProxy(int applyProxyId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_Id", ParameterType = DbType.Int32, ParameterValue = applyProxyId });
            SQLHelper.ExecuteStoredProcForScalar("pr_reject_apply_proxy", parameters);
        }
        #endregion
        #region 根据申请代理的ID获取该申请的详细信息
        public static DataSet GetApplyProxyDetailsByAPID(int applyProxyId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Apply_Proxy_ID", ParameterType = DbType.Int32, ParameterValue = applyProxyId });
            return SQLHelper.ExecuteStoredProcForDataSet<DataSet>("pr_get_SingleApplyProxyByAPID", parameters);
        }
        #endregion
    }
}
