using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Manage.Web.Common
{
    public class CommonConstant
    {
        public static int PageErrorCode = -1;
        public static string DropDownListNullKey = "";
        public static string DropDownListNullValue = "-1";
        #region 辅助管理模块
        #region 区域管理页面
        public static readonly string AssistantManagePage_AddParamZone_Msg = "请维护子区域后再做相应操作";
        #endregion

        #region 参数管理页面
        public static readonly string ParameterManagePage_InUse = "是";
        public static readonly string ParameterManagePage_ExistsParamName_Msg = "参数名称已经存在!";
        public static readonly string ParameterManagePage_ParamType_ID = "PARAM_TYPE_ID";
        public static readonly string ParameterManagePage_ParamType_Name = "PARAM_TYPE_NAME";
        #endregion
        #endregion

        #region 赛事管理模块
        #region 赛事区域管理页面
        public static readonly string EventZoneManagePage_EventItem_ID = "EventItem_ID";
        public static readonly string EventZoneManagePage_EventItem_Name = "EventItem_Name";
        #endregion
        #endregion

        public static readonly string MemberShipPageNaviTitleFormat = "您当前所在位置： &nbsp;首页 &nbsp;&gt; &nbsp;{0} &nbsp;&gt; &nbsp;{1}";
        public static readonly string NavigateDefaultPage = "~/Default.aspx";
    }
}
