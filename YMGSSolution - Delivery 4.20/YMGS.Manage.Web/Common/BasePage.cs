using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Entity;
using YMGS.Manage.Web.Common;
using database = YMGS.Data.DataBase;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.MasterPage;
using System.Web.Security;
using YMGS.Framework;

namespace YMGS.Manage.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        { 
            base.OnLoad(e);
            if (!IsPostBack)
            {
                //判断此页面是否有权限
                PageRight();
            }
        }

        #region property
        protected virtual int Func_PageId
        { get { return 0; } }
      
        #endregion

        public void PageRight()
        {
            if (!MySession.Accessable(Func_PageId))
                Response.Redirect(Default.Url());
        }

        protected int TopMenuID
        {
            get { return TopMenuIdAttribute.Get(GetType().BaseType); }
        }
        protected int LeftMenuID
        {
            get { return LeftMenuIdAttribute.Get(GetType().BaseType); }
        }

        public BetBase Master
        {
            get
            {
                return (base.Master as BetBase);
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (Master != null)
            {
                Master.TopMenuID = TopMenuID.ToString();
                Master.LeftMenuID = LeftMenuID.ToString();
            }

            if (!(HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity != null &&
                HttpContext.Current.User.Identity.IsAuthenticated))
            {
                Response.Redirect(Default.Url());                
            }
        }
    }
}