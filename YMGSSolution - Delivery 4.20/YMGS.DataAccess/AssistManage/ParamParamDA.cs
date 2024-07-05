using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Data.Common;

namespace YMGS.DataAccess.AssistManage
{
    public class ParamParamDA : DaBase
    {
        #region 获取参数
        public static DSParameter QueryParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameter = new List<ParameterData>()
                {
                    new ParameterData(){ ParameterName="@Param_Type", ParameterType= DbType.String, ParameterValue= row.PARAM_TYPE},
                    new ParameterData(){ ParameterName="@Param_Name", ParameterType= DbType.String, ParameterValue= row.PARAM_NAME}
                };
                var paramDS = persistBroker.ExecuteForDataSet<DSParameter>("pr_get_param", parameter, CommandType.StoredProcedure);
                return paramDS;
            }
            finally
            {
                persistBroker.Close();
            }
        }
        #endregion

        #region 新增参数
        public static int AddParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            IList<ParameterData> parameter = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Param_Type", ParameterType = DbType.Int32, ParameterValue = row.PARAM_TYPE},
                new ParameterData(){ ParameterName="@Param_Name", ParameterType = DbType.String, ParameterValue = row.PARAM_NAME},
                new ParameterData(){ ParameterName="@Is_Use", ParameterType = DbType.Int32,ParameterValue = row.IS_USE},
                new ParameterData(){ ParameterName="@Create_User", ParameterType = DbType.Int32,ParameterValue = row.CREATE_USER},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32,ParameterValue = row.LAST_UPDATE_USER}
            };
            return Convert.ToInt32(SQLHelper.ExecuteStoredProcForScalar("pr_add_param", parameter));
        }
        #endregion

        #region 更新参数
        public static int UpdateParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            IList<ParameterData> parameter = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Param_ID", ParameterType=DbType.Int32, ParameterValue=row.PARAM_ID},
                new ParameterData(){ ParameterName="@Param_Type", ParameterType=DbType.Int32, ParameterValue=row.PARAM_TYPE},
                new ParameterData(){ ParameterName="@Param_Name", ParameterType=DbType.String, ParameterValue=row.PARAM_NAME},
                new ParameterData(){ ParameterName="@Is_Use", ParameterType = DbType.Int32,ParameterValue = row.IS_USE},
                new ParameterData(){ ParameterName="@Last_Update_User", ParameterType = DbType.Int32,ParameterValue = row.LAST_UPDATE_USER}
            };
            return Convert.ToInt32(SQLHelper.ExecuteStoredProcForScalar("pr_up_param", parameter));
        }
        #endregion

        #region 删除参数
        public static int DelParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            IList<ParameterData> parameter = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Param_ID", ParameterType=DbType.Int32, ParameterValue=row.PARAM_ID}
            };
            return Convert.ToInt32(SQLHelper.ExecuteStoredProcForScalar("pr_del_param", parameter));
        }
        #endregion

        #region 参数排序
        public static int OrderParam(int paramID, OrderAction order)
        {
            IList<ParameterData> parameter = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName="@Param_ID", ParameterType = DbType.Int32, ParameterValue = paramID},
                new ParameterData(){ ParameterName="@Order_Tpye", ParameterType= DbType.Int32, ParameterValue = (int)order}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_order_param", parameter);
            return Convert.ToInt32(returnCode);
        }
        #endregion

        #region 获取参数类型
        public static DSParamType QueryParamType(DataHandlerEnum dataHandler)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
            new ParameterData(){ ParameterName="@Handler_Type", ParameterType= DbType.Int32, ParameterValue=(int)dataHandler},
            new ParameterData(){ ParameterName="@Param_Type_ID", ParameterType= DbType.Int32, ParameterValue=1},
            new ParameterData(){ ParameterName="@Param_Type_Name", ParameterType= DbType.String, ParameterValue=string.Empty}
            };
            var paramTypeDS = SQLHelper.ExecuteStoredProcForDataSet<DSParamType>("pr_handler_param_type", parameters);
            return paramTypeDS;
        }
        #endregion

        #region 新增，编辑，删除参数类型
        public static int HandlerParamType(DSParamType.TB_PARAM_TYPERow row,DataHandlerEnum dataHandler)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
            new ParameterData(){ ParameterName="@Handler_Type", ParameterType= DbType.Int32, ParameterValue=(int)dataHandler},
            new ParameterData(){ ParameterName="@Param_Type_ID", ParameterType= DbType.Int32, ParameterValue=row.PARAM_TYPE_ID},
            new ParameterData(){ ParameterName="@Param_Type_Name", ParameterType= DbType.String, ParameterValue=row.PARAM_TYPE_NAME}
            };
            var returnCode = SQLHelper.ExecuteStoredProcForScalar("pr_handler_param_type", parameters);
            return Convert.ToInt32(returnCode);
        }
        #endregion

        #region 根据参数类型获取参数
        /// <summary>
        /// 根据参数类型获取参数
        /// </summary>
        /// <param name="paramType"></param>
        /// <returns></returns>
        public static DSParamParam QueryParamByType(ParamTypeEnum paramType)
        {
            IList<ParameterData> parameter = new List<ParameterData>();
            parameter.Add(new ParameterData() { ParameterName = "@ParamType", ParameterType = DbType.Int32, ParameterValue = (int)paramType });
            var paramDS = SQLHelper.ExecuteStoredProcForDataSet<DSParamParam>("pr_get_param_bytype", parameter);
            return paramDS;
        }
        #endregion
    }
}
