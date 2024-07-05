using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using System.IO;

namespace YMGS.Trade.Web.Home
{
    public partial class ADPic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgADPic.ImageUrl = ADpic.Url()+"?lan="+LanguageMark.ToString();
            }
        }
        public int LanguageMark { set; get; }
    }
}