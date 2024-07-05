using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.GameSettle
{
    public class CommissionDA
    {
        #region 获取可用的佣金积分率
        /// <summary>
        /// 获取可用的佣金积分率
        /// </summary>
        /// <returns></returns>
        public static DSBrokerageIntegral QueryBrokerageIntegral()
        {
            var brokerageIntegralDS = SQLHelper.ExecuteStoredProcForDataSet<DSBrokerageIntegral>("pr_get_brokerage_integral_map", null);
            return brokerageIntegralDS;
        }
        #endregion

        #region 新增佣金率
        /// <summary>
        /// 新增佣金率
        /// </summary>
        /// <param name="row"></param>
        public static void AddBrokerageIntegral(DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Brokerage_Rage", ParameterType=DbType.Decimal, ParameterValue= row.Brokerage_Rate},
                new ParameterData(){ ParameterName="@Min_Integral", ParameterType=DbType.Int32, ParameterValue= row.Min_Integral},
                new ParameterData(){ ParameterName="@Max_Integral", ParameterType=DbType.Int32, ParameterValue= row.Max_Integral},
                new ParameterData(){ ParameterName="@Cur_User", ParameterType=DbType.Int32, ParameterValue= row.Create_User}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_add_brokerage_integral_map", parameters);
        }
        #endregion

        #region 删除佣金率
        /// <summary>
        /// 删除佣金率
        /// </summary>
        /// <param name="brokerageRageId"></param>
        public static void DelBrokerageIntegral(int brokerageRageId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Brokerage_Rage_Id", ParameterType = DbType.Decimal, ParameterValue = brokerageRageId });
            SQLHelper.ExecuteStoredProcForScalar("pr_del_brokerage_integral_map", parameters);
        }
        #endregion

        #region 更新佣金率
        /// <summary>
        /// 更新佣金率
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateBrokerageIntegral(DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow row)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Brokerage_Rage_Id", ParameterType=DbType.Int32, ParameterValue= row.Brokerage_Rate_ID},
                new ParameterData(){ ParameterName="@Brokerage_Rage", ParameterType=DbType.Decimal, ParameterValue= row.Brokerage_Rate},
                new ParameterData(){ ParameterName="@Min_Integral", ParameterType=DbType.Int32, ParameterValue= row.Min_Integral},
                new ParameterData(){ ParameterName="@Max_Integral", ParameterType=DbType.Int32, ParameterValue= row.Max_Integral},
                new ParameterData(){ ParameterName="@Cur_User", ParameterType=DbType.Int32, ParameterValue= row.Create_User}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_up_brokerage_integral_map", parameters);
        }
        #endregion

        public static DSBrokerageIntegral QueryBrokerageByIntegral(int integral)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Integral", ParameterType = DbType.Int32, ParameterValue = integral });
            return SQLHelper.ExecuteStoredProcForDataSet<DSBrokerageIntegral>("pr_get_brokerage_by_integral", parameters);
        }
    }
}
