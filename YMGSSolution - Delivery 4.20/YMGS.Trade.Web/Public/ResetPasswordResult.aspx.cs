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
    public partial class ResetPasswordResult : SimplePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }
    }
}