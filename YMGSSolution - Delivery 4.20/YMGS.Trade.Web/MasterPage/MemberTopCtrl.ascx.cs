using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.MasterPage
{
    public partial class MemberTopCtrl : System.Web.UI.UserControl
    {
        public int TopMenuId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHomePage.NavigateUrl = Default.Url();
                BasePage basePage = (BasePage)this.Page;
                if (basePage.IsLogin)
                    btnLogout.Visible = true;
                else
                    btnLogout.Visible = false;
            }
        }

        ModuleMenuLink curModuleMenu;
        /// <summary>
        /// 依据目前业务规则，只加载当前模块基本信息
        /// </summary>
        public void BindTopMenu()
        {
            
            IList<ModuleMenuLink> moduleList = SystemMenuHelper.GetSystemMenus();
            foreach (var moduleInfo in moduleList)
            {
                if (moduleInfo.MenuId == TopMenuId)
                {
                    moduleInfo.IsAccessible = false;
                    moduleInfo.Url = PageHelper.MakeClientUrl(Request.ApplicationPath, @moduleInfo.Url, Request.Params);
                    curModuleMenu = moduleInfo;
                    break;
                }
            }
 
        }

        /// <summary>
        /// 返回当前模块信息
        /// </summary>
        public ModuleMenuLink CurModuleLinkInfo
        {
            get
            {
                return curModuleMenu;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session[CommonConstant.CurrentLoginUserSessionKey] = null;
            Response.Redirect(Default.Url(), true);
        }
    }
}