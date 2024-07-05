using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.DataAccess.AssistManage;
using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Data.Common;

namespace YMGS.Business.AssistManage
{
    public class ParamParamManager : BrBase
    {
        #region 获取参数
        public static DSParameter QueryParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            return ParamParamDA.QueryParam(row);
        }
        #endregion

        #region 新增参数
        public static int AddParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            return ParamParamDA.AddParam(row);
        }
        #endregion

        #region 更新参数
        public static int UpdateParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            return ParamParamDA.UpdateParam(row);
        }
        #endregion

        #region 删除参数
        public static int DelParam(DSParamParam.TB_PARAM_PARAMRow row)
        {
            return ParamParamDA.DelParam(row);
        }
        #endregion

        #region 参数排序
        public static int OrderParam(int paramID, OrderAction order)
        {
            return ParamParamDA.OrderParam(paramID, order);
        }
        #endregion

        #region 获取参数类型
        public static DSParamType QueryParamType(DataHandlerEnum dataHandler)
        {
            return ParamParamDA.QueryParamType(dataHandler);
        }
        #endregion

        #region 新增，编辑，删除参数类型
        public static int HandlerParamType(DSParamType.TB_PARAM_TYPERow row, DataHandlerEnum dataHandler)
        {
            return ParamParamDA.HandlerParamType(row, dataHandler);
        }
        #endregion

        #region
        public static DSParamParam QueryParamByType(ParamTypeEnum paramType)
        {
            return ParamParamDA.QueryParamByType(paramType);
        }
        #endregion
    }
}
