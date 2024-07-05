using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.MemberShip
{
    public class AgentAccountDA
    {
        public static DSAccount QueryAccountByName(string accountName)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Account_Name", ParameterType = DbType.String, ParameterValue = accountName });
            var accountDS = SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_check_account_name", parameters);
            return accountDS;
        }

        public static DSAgentList QueryAgentDetailByGeneralAgentId(int generalAgentId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@General_Agent_Id", ParameterType = DbType.Int32, ParameterValue = generalAgentId });
            var agentDetailList = SQLHelper.ExecuteStoredProcForDataSet<DSAgentList>("pr_get_agent_by_generalAgentId", parameters);
            return agentDetailList;
        }

        public static DSAccount QueryMembers()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_get_members", null);
        }


        public static DSAccount QueryMembersByGeneralAgentId(int generalAgentId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@General_AgentId", ParameterType = DbType.Int32, ParameterValue = generalAgentId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_get_members_by_general_agentId", parameters);
        }

        public static DSAccount QueryMembersByAgentId(int agentId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@AgentId", ParameterType = DbType.Int32, ParameterValue = agentId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_get_members_by_agentId", parameters);
        }

        public static DSAccount QueryAgentByGeneralId(int generalId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@General_Agent_Id", ParameterType = DbType.Int32, ParameterValue = generalId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_get_agent_By_id", parameters);
        }

        public static void SetMemberAsAgent(int agentId, int userId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@AgentId", ParameterType = DbType.Int32, ParameterValue = agentId });
            parameters.Add(new ParameterData() { ParameterName = "@UserId", ParameterType = DbType.Int32, ParameterValue = userId });
            SQLHelper.ExecuteStoredProcForScalar("pr_set_members_as_agent", parameters);
        }

        public static void CheckGrowMemberCount(int agentId)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Agent_Id", ParameterType = DbType.Int32, ParameterValue = agentId });
            SQLHelper.ExecuteStoredProcForScalar("pr_check_grow_member_count", parameters);
        }
    }
}
