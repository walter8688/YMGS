using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Controls;
using AjaxControlToolkit;

namespace YMGS.Trade.Web.MasterPage
{
    public partial class HomeMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    //Page.Title = string.Format(CommonConstant.PageTitleFormat, Global.ProductName, Global.CompanyName);
            //}
            Page.Title = LangManager.GetString("ProductName");
        }

        protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
        }

        //public void RefreshHomeLoginPanel()
        //{
        //    homeLogin.Refresh();
        //}

        public ToolkitScriptManager ScriptManager
        {
            get
            {
                return ScriptManager1;
            }
        }
    }
}