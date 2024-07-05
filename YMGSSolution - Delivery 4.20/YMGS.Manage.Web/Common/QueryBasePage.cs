using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using YMGS.Manage.Web.MasterPage;

namespace YMGS.Manage.Web.Common
{
    public class QueryBasePage : BasePage
    {
   
        protected virtual void OnLoad(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                //判断此页面是否有权限
                BindGrid();
            }
        }
        private System.Web.UI.MasterPage GetMaster(System.Web.UI.MasterPage master)
        {
            if (master.Master != null)
            {
                return GetMaster(master.Master);
            }
            return master;
        }

        private System.Web.UI.MasterPage _Master_Page;

        public System.Web.UI.MasterPage Master_Page
        {
            get
            {
                if (_Master_Page == null)
                {
                    return GetMaster(this.Master);
                }
                return _Master_Page;
            }
        }

        private GridView _gdvMain;
        public virtual GridView gdvMain
        {
            get
            {
                if (_gdvMain != null)
                    return _gdvMain;

                Control mainList = Master_Page.FindControl("ListPlace").FindControl("gdvMain");
                if (mainList != null)
                {
                    _gdvMain = mainList as GridView;
                }
                return _gdvMain;
            }
        }
        private YMGS.Manage.Web.Controls.PageNavigator _Pager;
        public virtual YMGS.Manage.Web.Controls.PageNavigator Pager
        {
            get
            {
                if (_Pager != null)
                    return _Pager;

                Control pager = Master_Page.FindControl("ListPlace").FindControl("PageNavigator1");// GetCtrl("PageNavigator1");
                if (pager != null)
                {
                    _Pager = pager as YMGS.Manage.Web.Controls.PageNavigator;
                }
                return _Pager;
            }
        }
        protected virtual void PageNavigator1_PageIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        public virtual void BindGrid()
        {
            DataTable dt = GetData();
            ViewState["dt"] = dt;
            if (Pager != null)
            {
                Pager.databinds(dt, gdvMain);
            }
            else
            {
                gdvMain.DataSource = dt;
                gdvMain.DataBind();
            }

            AfterBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected virtual DataTable GetData()
        {
            return null;
        }

        protected virtual void AfterBind()
        {
            //object dt = ViewState["dt"];
            //if (dt != null)
            //{

            //    DataTable datatable = dt as DataTable;
            //    System.Web.UI.MasterPage master = Master_Page;
            //    Control topcontent = master.FindControl("TopContentPlaceHolder");
            //    Control export = topcontent.FindControl("export");
            //    //Control exportall = topcontent.FindControl("exportall");

            //    if (datatable.Rows.Count > 0)
            //    {
            //        export.Visible = true;
            //        //exportall.Visible = true;
            //    }
            //    else
            //    {
            //        export.Visible = false;
            //        //exportall.Visible = false;
            //    }
            //}
        }
    }
}