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
    public partial class TopMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        public void GetTopMenu(List<RoleMenu> rmList)
        {
            rptTopMenu.DataSource = rmList;
            rptTopMenu.DataBind();
        }

        protected void rptTopMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                RoleMenu rm = e.Item.DataItem as RoleMenu;
                if (rm.NodeSelected)
                {
                    HtmlControl hc = e.Item.FindControl("divTopMenu") as HtmlControl;
                    hc.Attributes.Remove("class");
                    hc.Attributes.Add("class", "caidaon");
                }
            }
        }

    }
}