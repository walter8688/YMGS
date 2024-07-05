using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using YMGS.Data.Common;
using YMGS.Business.SystemSetting;
using YMGS.Manage.Web.Common;

namespace YMGS.Manage.Web
{
     [LeftMenuId(FunctionIdList.SystemManagement.RoleManagePage)]
     [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    public partial class testmaster : QueryBasePage
    {
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.RoleManagePage;
            }
        }

        protected override DataTable GetData()
        {
          string s=  CheckBoxList1.SelectedValue;
          string a = RadioButtonList1.SelectedValue;
            return RoleFuncMapManager.QueryData(1).TB_ROLE_FUNC_MAP; 
        }

       
       
      
         
    }
}