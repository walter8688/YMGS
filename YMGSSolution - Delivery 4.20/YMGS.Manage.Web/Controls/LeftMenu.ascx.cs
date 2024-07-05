using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using System.Web.UI.HtmlControls;

namespace YMGS.Manage.Web.Controls
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            
            }
        }
        public void GetLeftMenu(List<RoleMenu> rmList)
        {
            rptLeftMenu.DataSource = rmList;
            rptLeftMenu.DataBind();
        }
        protected void rptLeftMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                RoleMenu rm = e.Item.DataItem as RoleMenu;
                if (rm.NodeSelected)
                {
                    HtmlControl hc = e.Item.FindControl("leftmenu1") as HtmlControl;
                    HtmlControl hcBlank = e.Item.FindControl("leftmenu2") as HtmlControl;
                    hc.Attributes.Remove("class");
                    hc.Attributes.Add("class", "leftselectedfone");
                    hcBlank.Attributes.Remove("class");
                    hcBlank.Attributes.Add("class", "lineliBlank");
                }
            }
        }
    }
}