using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.SystemSetting;
using YMGS.Data.DataBase;
using System.Data;

namespace YMGS.Business.SystemSetting
{
    public class AgentManager
    {
        #region 获取所有代理人数据
        public static DSAgentList QueryAgent(int roleID, string userName,string agentName)
        {
            return AgentDA.QueryAgent(roleID, userName, agentName);
        }
        #endregion

        #region 设置代理人佣金率&会员人数
        public static void SetAgentDetail(DSAgentDetail.TB_AGENT_DETAILRow agentDetail)
        {
            AgentDA.SetAgentDetail(agentDetail);
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
            return AgentDA.QueryAgentMember(userId);
        }
        #endregion

        #region 取消代理
        public static void CancleAgent(int userId)
        {
            AgentDA.CancleAgent(userId);
        }
        #endregion

        #region 获取代理类型
        public static DataSet GetAllAgentType()
        {
            return AgentDA.GetAllAgentType();
        }
        #endregion

        #region 获取代理申请记录
        public static DSApplyAgent QueryApplyAgentDeatail(int? roleId, DateTime applyStartDate, DateTime applyEndDate, string userName, int? applyStatus)
        {
            return AgentDA.QueryApplyAgentDeatail(roleId, applyStartDate, applyEndDate, userName, applyStatus);
        }
        #endregion

        #region 审核申请代理
        public static void ApproveProcessApplyProxy(int applyProxyId)
        {
            AgentDA.ApproveProcessApplyProxy(applyProxyId);
        }
        #endregion

        #region 批准申请代理
        public static void ConfirmApplyProxy(int applyProxyId)
        {
            AgentDA.ConfirmApplyProxy(applyProxyId);
        }
        #endregion

        #region 拒绝申请代理
        public static void RejectApplyProxy(int applyProxyId)
        {
            AgentDA.RejectApplyProxy(applyProxyId);
        }
        #endregion

        #region 根据申请代理的ID获取该申请的详细信息
        public static DataSet GetApplyProxyDetailsByAPID(int applyProxyId)
        {
            return AgentDA.GetApplyProxyDetailsByAPID(applyProxyId);
        }
        #endregion
    }
}
