using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.MemberShip;

namespace YMGS.Business.MemberShip
{
    public class IntegralManager
    {
        /// <summary>
        /// 查询积分历史
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DSIntegralHistory QueryIntegralHistory(int userId, DateTime startDate, DateTime endDate)
        {
            return IntegralDA.QueryIntegralHistory(userId, startDate, endDate);
        }
    }
}
