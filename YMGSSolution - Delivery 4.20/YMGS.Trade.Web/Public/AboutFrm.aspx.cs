using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.Public
{
    [TopMenuId(100000)]
    public partial class AboutFrm : MemberShipBasePage
    {
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(AboutFrm), "Public").AbsoluteUri;
        }

        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("AboutUs");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}