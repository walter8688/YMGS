using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeTopNaviCtrl : System.Web.UI.UserControl
    {
        //public delegate void SetMidTopVisiable(string mark);
        //public SetMidTopVisiable MidTopVisiable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbtSports.PostBackUrl = Default.Url((int)PageIdEnum.Sports);
                lbtSports.Text = LangManager.GetString("sports");
                lblInPlay.PostBackUrl = Default.Url((int)PageIdEnum.InPlay);
                lblInPlay.Text = LangManager.GetString("InPlay");
                lblFootball.PostBackUrl = Default.Url((int)PageIdEnum.Football);
                lblFootball.Text = LangManager.GetString("football");
                lbtEntertainment.PostBackUrl = Default.Url((int)PageIdEnum.Entertaiment);
                lbtEntertainment.Text = LangManager.GetString("Entertainment");
            }
        }

        //protected void lbtSports_Click(object sender, EventArgs e)
        //{
        //    //if (MidTopVisiable == null)
        //    //    return;
        //    //MidTopVisiable("1");
        //}

        //protected void lbtEntertainment_Click(object sender, EventArgs e)
        //{
        //    //if (MidTopVisiable == null)
        //    //    return;
        //    //MidTopVisiable("2");
        //}

        //protected void lblInPlay_Click(object sender, EventArgs e)
        //{
        //    //if (MidTopVisiable == null)
        //    //    return;
        //    //MidTopVisiable("3");
        //}

        //protected void lblFootball_Click(object sender, EventArgs e)
        //{
        //    //if (MidTopVisiable == null)
        //    //    return;
        //    //MidTopVisiable("4");
        //}
    }
}