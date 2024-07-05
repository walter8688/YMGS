using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Public;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;
using YMGS.Business.Cache;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeTopHelpCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  hyAbout.NavigateUrl = AboutFrm.Url();
            
            HomeBasePage basePage = (HomeBasePage)this.Page;
            if (basePage.Language == LanguageEnum.Chinese)
            {
                lbLanguage.Text = "English";
            }
            else
            {
                lbLanguage.Text = "中文";
            }
            if (!IsPostBack)
            {
                DSHelper ds = (new CachedHelp()).QueryCachedData<DSHelper>();
                var data = from i in ds.TB_HELPER.Where(s => s.PITEMID.ToString() == "0")
                           select new { i.ITEMID, name = basePage.Language == LanguageEnum.Chinese ? i.CNITEMNAME : i.ENITEMNAME };
                rpthelper.DataSource = data;
                    rpthelper.DataBind();
            }
        }



        protected void lbLanguage_Click(object sender, EventArgs e)
        {
            HomeBasePage basePage = (HomeBasePage)this.Page;
            if (basePage.Language == LanguageEnum.Chinese)
                basePage.Language = LanguageEnum.English;
            else
                basePage.Language = LanguageEnum.Chinese;

            Response.Redirect(Default.Url());
        }
    }
}