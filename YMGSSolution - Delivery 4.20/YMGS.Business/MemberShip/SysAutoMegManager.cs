using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;
using YMGS.Data.Presentation;

namespace YMGS.Business.MemberShip
{
    //public interface IAutoSendSystemMessage : IDisposable
    //{

    //    void AutoSendSystemMessage(DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow msgRow);
    //}

    public class SysAutoMegManager 
    {
        /// <summary>
        /// 查看某条系统信息的详细信息
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public static DSSystemAutoMessage QuerySingleSystemAutoMessageByID(int msgID)
        {
            return SysAutoMegDA.QuerySingleSystemAutoMessageByID(msgID);
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
            return SysAutoMegDA.QuerySystemAutoMessage(startDate, endDate, userId);
        }

        /// <summary>
        /// 添加系统消息
        /// </summary>
        /// <param name="msgRow"></param>
        public static void AddSystemAutoMessage(DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow msgRow)
        {
            SysAutoMegDA.AddSystemAutoMessage(msgRow);
        }

        /// <summary>
        /// 根据消息ID，删除系统消息
        /// </summary>
        /// <param name="MESSAGEID"></param>
        public static void DeleteSystemAutoMessageByMegID(int MESSAGEID)
        {
            SysAutoMegDA.DeleteSystemAutoMessageByMegID(MESSAGEID);
        }

        public virtual void AutoSendSystemMessage(DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow msgRow)
        {
            AddSystemAutoMessage(msgRow);
        }
    }
}
