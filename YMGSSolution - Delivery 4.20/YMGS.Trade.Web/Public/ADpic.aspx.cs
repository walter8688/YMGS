using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Trade.Web.Common;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web
{
    public partial class ADpic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             string lan=   Request["lan"].ToString();
             if (lan == "1")
                 LanguageMark = 1;
             else
                 LanguageMark = 2;
             bindrpt(LanguageMark);
            }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ADpic), "Public").AbsoluteUri;
        }
        public int LanguageMark { set; get; }
        private void bindrpt(int mark)
        {
            var picData = (new CachedADPic()).QueryCachedData<DSADPic>();
            var data = from s in picData.TB_AD_PIC
                       select s;

            if (data.Count() < 1)
                return;
            DSADPic.TB_AD_PICRow row = data.FirstOrDefault();
             Response.ContentType = "image/gif";
             Response.BinaryWrite(mark == 1 ? row.PIC_ADDRESS : row.PIC_ADDRESS_EN);
        }
    }
}