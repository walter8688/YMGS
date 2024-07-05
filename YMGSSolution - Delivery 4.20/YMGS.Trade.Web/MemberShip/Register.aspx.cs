using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.MemberShip
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
                btnRegiester.Enabled = false;
            }
        }

        protected void BindYear()
        {
            for (int year = 1900; year <= GlobalBrManager.QueryCurrentDateTime().Year; year++)
            {
                ddlyear.Items.Add(year.ToString());
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbagree.Checked)
                btnRegiester.Enabled = ckbagree.Checked;
        }

    }
}