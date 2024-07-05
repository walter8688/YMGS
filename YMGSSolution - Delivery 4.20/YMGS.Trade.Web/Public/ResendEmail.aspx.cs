using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.Public
{
    public partial class ResendEmail: SimplePageBase
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("UserRegister");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ResendEmail), "Public").AbsoluteUri;
        }

        public static string Url(int iUserId)
        {
            return UrlHelper.BuildUrl(typeof(ResendEmail), "Public","id",iUserId.ToString()).AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              

            }
        }

        protected void btnyes_Click(object sender, EventArgs e)
        {  string id = Request.QueryString["id"];
                DSSystemAccount sa = SystemSettingManager.QueryData("", int.Parse(id), "");
                DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = sa.TB_SYSTEM_ACCOUNT[0];

                string activeUrl = "RegisterResult.aspx?resend=1&id=" + obj.USER_ID.ToString();
                Response.Redirect(activeUrl);
        }

    }
}