using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.DataAccess.GameSettle;
using YMGS.Data.DataBase;

namespace YMGS.Business.GameSettle
{
    public class CommissionManager
    {
        #region 获取可用的佣金积分率
        /// <summary>
        /// 获取可用的佣金积分率
        /// </summary>
        /// <returns></returns>
        public static DSBrokerageIntegral QueryBrokerageIntegral()
        {
            return CommissionDA.QueryBrokerageIntegral();
        }
        #endregion

        #region 新增佣金率
        /// <summary>
        /// 新增佣金率
        /// </summary>
        /// <param name="row"></param>
        public static void AddBrokerageIntegral(DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow row)
        {
            CommissionDA.AddBrokerageIntegral(row);
        }
        #endregion

         #region 删除佣金率
        /// <summary>
        /// 删除佣金率
        /// </summary>
        /// <param name="brokerageRageId"></param>
        public static void DelBrokerageIntegral(int brokerageRageId)
        {
            CommissionDA.DelBrokerageIntegral(brokerageRageId);
        }
        #endregion

        #region 更新佣金率
        /// <summary>
        /// 更新佣金率
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateBrokerageIntegral(DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow row)
        {
            CommissionDA.UpdateBrokerageIntegral(row);
        }

        /// <summary>
        /// 获取积分对应的佣金率
        /// </summary>
        /// <param name="integral"></param>
        /// <returns></returns>
        public static decimal QueryBrokerageByIntegral(int integral)
        {
            var brokerageDS = CommissionDA.QueryBrokerageByIntegral(integral);
            if (brokerageDS == null)
                return (decimal)0;
            return brokerageDS.TB_BROKERAGE_INTEGRAL_MAP[0].Brokerage_Rate;
        }
        #endregion
    }
}
