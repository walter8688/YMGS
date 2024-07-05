using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Business.AssistManage;
using YMGS.Data.Presentation;

namespace YMGS.Trade.Web
{
    public partial class TopRacePic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string lan = Request["lan"].ToString();
                if (lan == "1")
                    LanguageMark = 1;
                else
                    LanguageMark = 2;
                bindrpt(LanguageMark);
            }
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(TopRacePic), string.Empty).AbsoluteUri;
        }
        public int LanguageMark { set; get; }
        private void bindrpt(int mark)
        {
            var data = from s in TopRaceManager.QueryTopRace().TB_AD_TOPRACE
                       select s;

            if (data.Count() < 1)
                return;
            DSTopRace.TB_AD_TOPRACERow row = data.FirstOrDefault();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(mark == 1 ? row.CNPIC : row.ENPIC);
        }
    }
}