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
    public class SysAutoMegDA
    {
        /// <summary>
        /// 查看某条系统信息的详细信息
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public static DSSystemAutoMessage QuerySingleSystemAutoMessageByID(int msgID)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@MESSAGEID", ParameterType = DbType.Int32, ParameterValue = msgID });
            return SQLHelper.ExecuteStoredProcForDataSet<DSSystemAutoMessage>("pr_get_sysAutoMsgDetails", parameters);
        }

        /// <summary>
        /// 根据会员ID获取与自己相关的系统消息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DSSystemAutoMessage QuerySystemAutoMessage(DateTime startDate, DateTime endDate, int userId)
        {
            var parameters = new List<ParameterData>();
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Msg_SDate", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Msg_SDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@Msg_EDate", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Msg_EDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            parameters.Add(new ParameterData() { ParameterName = "@User_Id", ParameterType = DbType.Int32, ParameterValue = userId });
            return SQLHelper.ExecuteStoredProcForDataSet<DSSystemAutoMessage>("pr_get_sysAutoMsg", parameters);
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msgRow"></param>
        public static void AddSystemAutoMessage(DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow msgRow)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@SENDTO_USERID", ParameterType = DbType.Int32, ParameterValue = msgRow.SENDTO_USERID},
                new ParameterData(){ ParameterName = "@MESSAGE_CONTENT", ParameterType = DbType.String, ParameterValue = msgRow.MESSAGE_CONTENT},
                new ParameterData(){ ParameterName = "@MESSAGE_CONTENT_EN", ParameterType = DbType.String, ParameterValue = msgRow.MESSAGE_CONTENT_EN},
                new ParameterData(){ ParameterName = "@SENDBY_SYSTEMID", ParameterType = DbType.Int32, ParameterValue = msgRow.SENDBY_SYSTEMID}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_sysAutoMsg", parameters);
        }

        /// <summary>
        /// 根据消息ID，删除系统消息
        /// </summary>
        /// <param name="userID"></param>
        public static void DeleteSystemAutoMessageByMegID(int MESSAGEID)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@MESSAGEID", ParameterType = DbType.Int32, ParameterValue = MESSAGEID });
            SQLHelper.ExecuteStoredProcForScalar("pr_del_sysAutoMsg", parameters);
        }
    }
}
