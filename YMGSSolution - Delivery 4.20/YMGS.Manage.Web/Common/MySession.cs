using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using System.Web;
using YMGS.Data.DataBase;

namespace YMGS.Manage.Web.Common
{
    public class MySession
    {
        public static UserDetail CurrentUser
        {
            get
            {
                if (!ExistSession())
                    HttpContext.Current.Response.Redirect("~/Default.aspx");
                return HttpContext.Current.Session["CurrentUser"] as UserDetail;
            }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }

        private static bool ExistSession()
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                HttpContext.Current.Session.Clear();
                return false;
            }

            return true;
        }

        public static void ClearSessions()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }

        public static bool Accessable(int Func_Id)
        {
            if (Func_Id == 0)
                return false;
            DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable tb = CurrentUser.ROLE_FUNC;
            if (tb == null)
                return false;
            return tb.Where(s => s.FUNC_ID == Func_Id).Count() > 0 ? true : false;
        }
    }
}