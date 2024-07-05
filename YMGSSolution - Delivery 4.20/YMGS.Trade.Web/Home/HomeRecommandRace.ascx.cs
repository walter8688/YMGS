using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Search;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeRecommandRace : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRecommandRace();
            }
        }

        public int LanguageMark { set; get; }

        private void BindRecommandRace()
        {
            DefaultSearcher.LANGUAGE=LanguageMark;
            rptRecRace.DataSource = DefaultSearcher.GetRecommandMatchHeader();
            rptRecRace.DataBind();
        }

        protected void rptRecRace_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfdid = e.Item.FindControl("hfdeventid") as HiddenField;
                Label lblstartDate = e.Item.FindControl("lblstartDate") as Label;
                Repeater rpt = e.Item.FindControl("rptsubRecRace") as Repeater;
                rpt.DataSource = DefaultSearcher.GetRecommandMatch(hfdid.Value, lblstartDate.Text);
                rpt.DataBind();
            }
        }

        protected void rptsubRecRace_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfdid = e.Item.FindControl("hfdparam") as HiddenField;
                HyperLink lbtmatch = e.Item.FindControl("hlkmatch") as HyperLink;
                lbtmatch.NavigateUrl = Default.Url() + hfdid.Value;
            }
        }
    }
}