using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.MasterPage
{
    public partial class MemberShipMaster : System.Web.UI.MasterPage
    {
        #region 公共属性

        public ToolkitScriptManager ToolkitScriptManager
        {
            get
            {
                return ScriptManager1;
            }
        }

        public Label PageTitleElement
        {
            get
            {
                return lblTitle;
            }
        }
        public string PageTitleFormat
        {
            get { return "{0} - " + Global.CompanyName; }
        }

        public string PageTitle
        {
            get
            {
                return PageTitleElement.Text;
            }
            set
            {
                PageTitleElement.Text = value;
            }
        }

        public int TopMenuId
        {
            get;
            set;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    //Page.Title = string.Format(CommonConstant.PageTitleFormat, Global.ProductName, Global.CompanyName);
            //}
            Page.Title = LangManager.GetString("ProductName");
        }


        protected override void OnPreRender(EventArgs e)
        {
            PerformDataBind();
            base.OnPreRender(e);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PerformDataBind();
            base.OnDataBinding(e);
        }

        private bool dataBound;
        private void PerformDataBind()
        {
            if (dataBound)
                return;
            dataBound = true;

            topMenu.TopMenuId = TopMenuId;
            topMenu.BindTopMenu();
            var curModuleInfo = topMenu.CurModuleLinkInfo;
            string curModuleName = string.Empty;
            if(curModuleInfo != null)
                curModuleName = curModuleInfo.DisplayText;
            lblNaviTitle.Text = LangManager.GetString("Yourlocation") + string.Format(CommonConstant.MemberShipPageNaviTitleFormat, curModuleName, PageTitle);
            leftMenu.TopMenuId = TopMenuId;
            leftMenu.BindLeftMenu();
        }

        protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
        }
    }
}