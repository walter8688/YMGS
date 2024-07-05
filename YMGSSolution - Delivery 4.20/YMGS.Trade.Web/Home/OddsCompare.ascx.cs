using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.AssistManage;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Business.EventManage;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.Home
{
    public partial class OddsCompare : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadMatch();
                SetOddsCompare();
                DSADWords ds = (new CachedADWords()).QueryCachedData<DSADWords>();
                var data= ds.TB_AD_WORDS.Where(s=>s.TITLE.Trim()=="赔率比较网站");
                if(data!=null&& data.Count()>0)
                {
                    hlktentercompare.NavigateUrl = data.FirstOrDefault().WEBLINK;
                }
            }
        }

        private void loadMatch()
        {
            ddlitem.DataTextField = "MATCHNAME";
            ddlitem.DataValueField = "MATCHID";
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var oddsData = (new CachedOddsCompare()).QueryCachedData<DSODDSCOMPARE>();
            var data = (from s in oddsData.TB_ODDS_COMPARE
                        join d in ds.Match_List
                         on s.MATCHID equals d.MATCH_ID 
                        select new { s.MATCHID, s.MATCHNAME }).Distinct();
            ddlitem.DataSource = data;
            ddlitem.DataBind();
            if (ddlitem.Items.Count > 0)
            {
                ddlitem.SelectedIndex = 0;
                hfditem.Value = "0";
            }
            else
            {
                ddlitem.SelectedIndex = -1;
                hfditem.Value = "-1";
            }
            if (Request["matchid"] == null)
                return;
            string matchid = Request["matchid"].ToString();
            int matchindex = ddlitem.Items.IndexOf(ddlitem.Items.FindByValue(matchid));
            if (ddlitem.Items.Count > 0)
            {
                hfditem.Value = matchindex < 0 ? "0" : matchindex.ToString();
                ddlitem.SelectedIndex = matchindex < 0 ? 0 : matchindex;
            }
        }

        public int TotalItemNum
        {
            get { return ddlitem.Items.Count-1; }
        }
        public int LanguageMark { set; get; }

        private void Bindrpt(string matchid)
        {
            ibtpre.Visible = ibtnext.Visible = ddlitem.Items.Count == 1 ? false : true;
            var oddscompares = (new CachedOddsCompare()).QueryCachedData<DSODDSCOMPARE>();
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
        
            var eventds = (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            var results = from s in oddscompares.TB_ODDS_COMPARE.Where(s=>s.MATCHID.ToString()==matchid)
                          join x in ds.Match_List.Where(s =>((s.IsEVENTTYPE_NAMENull()?"":s.EVENTTYPE_NAME) == "体育类"))
                          on s.MATCHID equals x.MATCH_ID into resultmatch
                          from i in resultmatch
                          select new { TeamA = LanguageMark == 1 ? i.EVENT_HOME_TEAM_NAME : i.EVENT_HOME_TEAM_NAME_EN, TeamB = LanguageMark == 1 ? i.EVENT_GUEST_TEAM_NAME: i.EVENT_GUEST_TEAM_NAME_EN, s.MATCHID, s.MATCHNAME, s.PROFIT, s.CN_CORP, s.EN_CORP, i.EVENTTYPE_NAME, i.MATCH_NAME_EN, i.EVENT_ID, i.STARTDATE };
            var data = (from s in (results.Where(s =>(s.EVENTTYPE_NAME == "体育类")))
                        join e in eventds._DSEventTeamList
                        on s.EVENT_ID equals e.EVENT_ID.ToString() into recmatch
                        from i in recmatch
                        select new
                        {
                            tradeurl = string.Format("?Ent=0&item={0}&zone={1}&eventid={2}&itemdate={3}&matchid={4}", i.EventItem_ID.ToString(), i.EVENTZONE_ID.ToString(), i.EVENT_ID.ToString(), s.STARTDATE.ToString("yyyy-MM-dd"), s.MATCHID.ToString()),
                            MATCH_NAME = LanguageMark == 1 ? s.MATCHNAME : s.MATCH_NAME_EN,
                            PROFIT=s.PROFIT,
                            s.MATCHID,s.TeamA,s.TeamB,
                            STARTDATE = s.STARTDATE.ToString("yyyy-MM-dd"),
                            corp = LanguageMark == 1 ? s.CN_CORP : s.EN_CORP
                            
                        }).Distinct();
            
            string name = LanguageMark == 1 ? "必发必" : "bestabet";
          
            var betfairdata = from s in data
                              where s.corp.ToLower() == name
                              select s;
            if (betfairdata != null && betfairdata.Count() > 0)
            {
                hlktrade.NavigateUrl =Default.Url()+ betfairdata.FirstOrDefault().tradeurl;
                lblbetfair.Text = name;
                lblprofit.Text = betfairdata.FirstOrDefault().PROFIT.ToString();

                lblTeamA.Text = lblTeamAA.Text = betfairdata.FirstOrDefault().TeamA;
                lblTeamB.Text = betfairdata.FirstOrDefault().TeamB;
            }
            if (data!= null && data.Count() > 0)
            {
                rptoddscompare.DataSource = data.Where(s => s.corp.ToLower() != name);
                rptoddscompare.DataBind();
            }
        }

        public void SetOddsCompare()
        {
            if (ddlitem.Items.Count == 0)
            {
                pnloddscompare.Visible = false;
                return;
            }
            else
                pnloddscompare.Visible = true;
         
            Bindrpt(ddlitem.SelectedValue);
        }

        protected void ibtpre_Click(object sender, ImageClickEventArgs e)
        {
            SwitchMatch(false);
        }

        private void SwitchMatch(bool mark)
        {
            //ibtpre.Visible=ibtnext.Visible=ddlitem.Items.Count == 1?false:true;
            
            int index = int.Parse(hfditem.Value);
            index = mark ? index+1 :index-1;
            if (TotalItemNum < index)
                hfditem.Value = "0";
            if (index < 0)
            {
                hfditem.Value = TotalItemNum.ToString();
            }
            if (TotalItemNum >= index && index >= 0)
            {
                hfditem.Value = index.ToString();
            }
            int curIndex = int.Parse(hfditem.Value);

            ddlitem.SelectedIndex = curIndex;
            // int matchid =int.Parse( ddlitem.SelectedValue);
            Bindrpt(ddlitem.SelectedValue);
        }

        protected void ibtnext_Click(object sender, ImageClickEventArgs e)
        {
            SwitchMatch(true);
        }
    }
}