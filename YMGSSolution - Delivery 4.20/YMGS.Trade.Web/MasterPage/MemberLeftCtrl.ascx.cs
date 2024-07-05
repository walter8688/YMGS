using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.MasterPage
{
    public partial class MemberLeftCtrl : System.Web.UI.UserControl
    {
        public int TopMenuId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void BindLeftMenu()
        {
            IList<ModuleMenuLink> moduleList = SystemMenuHelper.GetSystemMenus();
            UserAccess userAccess = PageHelper.GetCurrentUserAccess();

            IList<MenuLink> leftMenuList = new List<MenuLink>();

            foreach (var moduleInfo in moduleList)
            {
                if (moduleInfo.MenuId != TopMenuId)
                    continue;

                foreach (var menuInfo in moduleInfo.SubMenus)
                {
                    var clientUrl = PageHelper.MakeClientUrl(Request.ApplicationPath, @menuInfo.Url, Request.Params);
                    bool isAccessible = PageAccessProvider.IsAccessible(userAccess, clientUrl);
                    if (!isAccessible)
                        continue;

                    menuInfo.Url = clientUrl;
                    menuInfo.Selected = ((BasePage)this.Page).PageTitle == menuInfo.DisplayText;
                    leftMenuList.Add(menuInfo);
                }
            }

            repMenuItems.DataSource = leftMenuList;
            repMenuItems.DataBind();
        }
    }
}