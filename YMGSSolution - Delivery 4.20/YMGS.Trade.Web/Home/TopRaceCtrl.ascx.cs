using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.AssistManage;
using YMGS.Data.Presentation;
using YMGS.Business.Cache;

namespace YMGS.Trade.Web.Home
{
    public partial class TopRaceCtrl : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                  DSTopRace ds = (new CachedTopRace()).QueryCachedData<DSTopRace>();
                  if (ds.TB_AD_TOPRACE.Rows.Count > 0)
                  {
                      DSTopRace.TB_AD_TOPRACERow row = ds.TB_AD_TOPRACE.FirstOrDefault();
                      hfdmarchid.Value = row.MARCHID.ToString();
                  }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                DSTopRace ds = (new CachedTopRace()).QueryCachedData<DSTopRace>();
                if (ds.TB_AD_TOPRACE.Rows.Count > 0)
                {
                    DSTopRace.TB_AD_TOPRACERow row = ds.TB_AD_TOPRACE.FirstOrDefault();
                    lbltitle.Text = LanguageMark == 1 ? row.CNTITLE : row.ENTITLE;
                    lblcontent.Text = LanguageMark == 1 ? row.CNCONTENT : row.ENCONTENT;
                    GetData(row.MARCHID);
                   
                    imgADPic.ImageUrl = TopRacePic.Url() + "?lan=" + LanguageMark.ToString();
                }
            }
        }

        public int Marchid
        {
            get { return int.Parse(hfdmarchid.Value); }
        }

        public void GetData(int matchid)
        {
            
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var eventds = (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            var data = (from s in (ds.Match_List.Where(s =>((s.IsEVENTTYPE_NAMENull()?"":s.EVENTTYPE_NAME) == "体育类") &&  (s.MATCH_ID == matchid) ))
                        join e in eventds._DSEventTeamList
                        on s.EVENT_ID equals e.EVENT_ID.ToString() into recmatch
                        from i in recmatch.DefaultIfEmpty()
                        select new
                        {
                            param = string.Format("?Ent=0&item={0}&zone={1}&eventid={2}&itemdate={3}&matchid={4}", i.EventItem_ID.ToString(), i.EVENTZONE_ID.ToString(), i.EVENT_ID.ToString(), s.STARTDATE.ToString("yyyy-MM-dd"), s.MATCH_ID.ToString()),
                            cnname = s.MATCH_NAME,
                            enname = s.MATCH_NAME_EN
                        }).Distinct().FirstOrDefault();
            if (data == null)
                return;
            
            lblmatchname.Text = LanguageMark == 1 ? data.cnname : data.enname;
            hlkbetNow.NavigateUrl = Default.Url() + data.param;
        }

        public bool isVisiable
        {
            get;
            set;
        }
        public int LanguageMark { set; get; }
    }
}