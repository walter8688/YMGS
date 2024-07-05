using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;
using YMGS.Business.SystemSetting;
using YMGS.Data.Presentation;

namespace YMGS.Business.MemberShip
{
    public class AgentAccountManager
    {
        #region 判断账户是否存在
        /// <summary>
        /// 判断账户是否存在
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public static bool CheckAccountNameExists(string accountName)
        {
            var AccountDS = AgentAccountDA.QueryAccountByName(accountName);
            if (AccountDS.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
        #endregion

        #region 判断邮件是否存在
        public static bool CheckEmailExists(string email)
        {
            DSSystemAccount saemail = SystemSettingManager.QueryData("", 0, email);
            if (saemail.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
        #endregion

        #region 根据总代理获取下级代理
        public static DSAgentList QueryAgentDetailByGeneralAgentId(int generalAgentId)
        {
            return AgentAccountDA.QueryAgentDetailByGeneralAgentId(generalAgentId);
        }

        public static DSAccount QueryAgentByGeneralId(int generalId)
        {
            return AgentAccountDA.QueryAgentByGeneralId(generalId);
        }
        #endregion

        #region 获取会员
        public static DSAccount QueryMembers()
        {
            return AgentAccountDA.QueryMembers();
        }
        #endregion

        #region 
        public static DSAccount QueryMembersByGeneralAgentId(int generalAgentId)
        {
            return AgentAccountDA.QueryMembersByGeneralAgentId(generalAgentId);
        }
        #endregion

        #region 设置代理
        public static void SetMemberAsAgent(int agentId, int userId)
        {
            AgentAccountDA.SetMemberAsAgent(agentId, userId);
        }
        #endregion

        /// <summary>
        /// 根据AgentID获取下属会员
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public static DSAccount QueryMembersByAgentId(int agentId)
        {
            return AgentAccountDA.QueryMembersByAgentId(agentId);
        }

        public static void CheckGrowMemberCount(int agentId)
        {
            AgentAccountDA.CheckGrowMemberCount(agentId);
        }
    }
}
