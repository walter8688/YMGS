using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeADWords : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindrptADWords();
            }
        }
        public int LanguageMark { set; get; }

        private void BindrptADWords()
        {
            var data = from s in (new CachedADWords()).QueryCachedData<DSADWords>().TB_AD_WORDS
                       select new {
                           title = LanguageMark == 1 ? s.TITLE.Substring(0, (s.TITLE.Length > 20 ? 20 : s.TITLE.Length)) + (s.TITLE.Length > 20 ? "..." : "") : s.TITLE_EN.Substring(0, (s.TITLE_EN.Length > 20 ? 20 : s.TITLE_EN.Length )) + (s.TITLE_EN.Length > 20 ? "..." : ""),
                           desc = LanguageMark == 1 ? s.DESC.Substring(0, s.DESC.Length > 20 ? 20 : s.DESC.Length ) + (s.DESC.Length > 20 ? "..." : "") : s.DESC_EN.Substring(0, s.DESC_EN.Length > 20 ? 20 : s.DESC_EN.Length ) + (s.DESC_EN.Length > 20 ? "..." : ""),
                           s.WEBLINK
                       };

            rptADWords.DataSource = data;
            rptADWords.DataBind();
        }
    }
}