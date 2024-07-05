using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using YMGS.Trade.Web.Common;
using System.Security;
using System.Web.Caching;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using YMGS.Business.GameMarket;
using YMGS.Business.MemberShip;
using System.Web.Services;
using System.Web.Script.Services;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Data.Entity;
using System.Web.UI.HtmlControls;
using YMGS.Business.Search;
using YMGS.Data.Common;
using System.Transactions;
using YMGS.Trade.Web.Home;
using YMGS.Business.AssistManage;
using YMGS.Business.ReportCenter;
using YMGS.Business.Game;

namespace YMGS.Trade.Web
{
    public partial class Default : HomeBasePage
    {
        private const string DefaultValue = "0";
        private const string MatchMarcketInfoList = "MatchMarcketInfoList";
        private const string layMatchMarcketInfoList = "layMatchMarcketInfoList";
        private const string myleftmenu = "myleftmenu";
        private const string MatchInc = "MatchInc";
        private const string ET = "EventType";
        private const string SE = "se";

        public string NNN
        {
            get { return LangManager.GetString("NoteNotNull"); }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(Default), string.Empty).AbsoluteUri;
        }
        public static string Url(string eventtype)
        {
            return UrlHelper.BuildUrl(typeof(Default), string.Empty, ET, eventtype).AbsoluteUri;
        }
        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            oc.LanguageMark= TopRace.LanguageMark= Pic.LanguageMark = adwords.LanguageMark = RR.LanguageMark = (int)Language;            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DSTopRace ds = (new CachedTopRace()).QueryCachedData<DSTopRace>();
                if (ds.TB_AD_TOPRACE.Rows.Count > 0)
                {
                    hfdroprace.Value = "1";
                }
                else
                {
                    hfdroprace.Value = "0";
                }
                LoadEventItem(true);
                LoadNotice();
                lbllan.Text = hfdlan.Value = ((int)Language).ToString();
                SetMidVisiable(true);
                SetRightMenuVisiable(true);
                Toprecommand_OnClick(null, null);
                SetLocation();
                lbllan.Focus();
                if(hfdIsInPlay.Value != "1")
                    SetInPlayVisible(false);
                if (hdfIsFtCalander.Value != "1")
                    SetFootballVisble(false);
            }
            HomeTopNaviCtrl itme = this.Master.FindControl("homeTopNavi") as HomeTopNaviCtrl;
            itme.MidTopVisiable = SESwitch;
            homeGameListCtrl.refresh = LoadInPlay;
            homeGameListCtrl.betBack = betBack;
            homeGameListCtrl.betLay = betLay;
            footballGameListCtrl.betBack = betBack;
            footballGameListCtrl.betLay = betLay;
            GameListYourInPlay.betBack = GameListToday.betBack = GameListTomorrow.betBack = betBack;
            GameListYourInPlay.betLay = GameListToday.betLay = GameListTomorrow.betLay = betLay;
        }

        #region Save back and lay data into viewstate

        public void SaveBack(Repeater rpt)
        {
            HashSet<MatchMarcketInfo> MatchMarcketInfolist = new HashSet<MatchMarcketInfo>();
            foreach (RepeaterItem item in rpt.Items)
            {
                Repeater subrpt = item.FindControl("rptbackitem") as Repeater;
                foreach (RepeaterItem subitem in subrpt.Items)
                {
                    HiddenField hfdMATCH_ID = subitem.FindControl("hfdMATCH_ID") as HiddenField;
                    HiddenField hfdMARKET_ID = subitem.FindControl("hfdMARKET_ID") as HiddenField;
                    HiddenField hfdMARKET_TMP_ID = subitem.FindControl("hfdMARKET_TMP_ID") as HiddenField;
                    HiddenField hfdMATCHTYPE = subitem.FindControl("hfdMATCHTYPE") as HiddenField;
                    HiddenField hfdMARKET_TMP_NAME = subitem.FindControl("hfdMARKET_TMP_NAME") as HiddenField;
                    HiddenField hfdMARKET_NAME = subitem.FindControl("hfdMARKET_NAME") as HiddenField;
                    HiddenField hfdMATCH_NAME = subitem.FindControl("hfdMATCH_NAME") as HiddenField;
                    TextBox txtodds = subitem.FindControl("txtodds") as TextBox;
                    TextBox txtstake = subitem.FindControl("txtstake") as TextBox;

                    MatchMarcketInfo info = new MatchMarcketInfo();
                    info.MATCH_NAME = hfdMATCH_NAME.Value;
                    info.MARKET_ID = int.Parse(hfdMARKET_ID.Value);
                    info.MARKET_NAME = hfdMARKET_NAME.Value;
                    info.MARKET_TMP_NAME = hfdMARKET_TMP_NAME.Value;
                    info.MARKET_TMP_ID = int.Parse(hfdMARKET_TMP_ID.Value);
                    info.MATCH_ID = int.Parse(hfdMATCH_ID.Value);
                    info.MATCHTYPE = int.Parse(hfdMATCHTYPE.Value);
                    info.AMOUNTS = txtstake.Text;
                    info.odds = txtodds.Text;
                    MatchMarcketInfolist.Add(info);
                }
            }
            ViewState[MatchMarcketInfoList] = MatchMarcketInfolist;
        }

        public void SaveLay(Repeater rpt)
        {
            HashSet<MatchMarcketInfo> layMatchMarcketInfoLists = new HashSet<MatchMarcketInfo>();
            foreach (RepeaterItem item in rpt.Items)
            {
                Repeater subrpt = item.FindControl("rptlayitem") as Repeater;
                foreach (RepeaterItem subitem in subrpt.Items)
                {
                    HiddenField hfdMATCH_ID = subitem.FindControl("hfdMATCH_ID") as HiddenField;
                    HiddenField hfdMARKET_ID = subitem.FindControl("hfdMARKET_ID") as HiddenField;
                    HiddenField hfdMARKET_TMP_ID = subitem.FindControl("hfdMARKET_TMP_ID") as HiddenField;
                    HiddenField hfdMATCHTYPE = subitem.FindControl("hfdMATCHTYPE") as HiddenField;
                    HiddenField hfdMARKET_TMP_NAME = subitem.FindControl("hfdMARKET_TMP_NAME") as HiddenField;
                    HiddenField hfdMARKET_NAME = subitem.FindControl("hfdMARKET_NAME") as HiddenField;
                    HiddenField hfdMATCH_NAME = subitem.FindControl("hfdMATCH_NAME") as HiddenField;
                    TextBox txtodds = subitem.FindControl("txtodds") as TextBox;
                    TextBox txtstake = subitem.FindControl("txtstake") as TextBox;

                    MatchMarcketInfo info = new MatchMarcketInfo();
                    info.MATCH_NAME = hfdMATCH_NAME.Value;
                    info.MARKET_ID = int.Parse(hfdMARKET_ID.Value);
                    info.MARKET_NAME = hfdMARKET_NAME.Value;
                    info.MARKET_TMP_NAME = hfdMARKET_TMP_NAME.Value;
                    info.MARKET_TMP_ID = int.Parse(hfdMARKET_TMP_ID.Value);
                    info.MATCH_ID = int.Parse(hfdMATCH_ID.Value);
                    info.MATCHTYPE = int.Parse(hfdMATCHTYPE.Value);
                    info.AMOUNTS = txtstake.Text;
                    info.odds = txtodds.Text;
                    layMatchMarcketInfoLists.Add(info);
                }
            }
            ViewState[layMatchMarcketInfoList] = layMatchMarcketInfoLists;
        }

        #endregion

        private void SetInPlayVisible(bool mark)
        {
            pnlInPlay.Visible = mark;
            InPlayTimer.Enabled = mark;
            if (!mark)
            {
                GameTodayTimer.Enabled = false;
                GameTomorrowTimer.Enabled = false;
                GameYourInPlayTimer.Enabled = false;
            }
        }

        private void SetFootballVisble(bool mark)
        {
            pnlFootball.Visible = mark;
            footballGameListCtrl.IsAutoRefresh = mark;
        }

        private void SetUplScoreVisble(bool mark)
        {
            pnlScore.Visible = mark;
        }

        private void SetLeftVisiable(bool mark)
        {
            rptleftmenu.Visible = mark;
            rptEnt.Visible = !mark;
        }

        private void SetMidVisiable(bool mark)
        {
            bool flag = hfdroprace.Value == "1" ? true : false;
            if (mark)
                pnlMidTop.Visible = flag;
            else
                pnlMidTop.Visible = mark;
            bool isInPlay = hfdIsInPlay.Value == "1" ? true : false;
            bool isFbCal = hdfIsFtCalander.Value == "1" ? true : false;
            if(isInPlay || isFbCal)
                pnlMidM.Visible = false;
            else
                pnlMidM.Visible = !mark;
            ToprecommandTimer.Enabled = pnlMidTop.Visible;
            MarketTimer.Enabled = !pnlMidTop.Visible;
        }

        private void SetRightMenuVisiable(bool mark)
        {
            if (rptback.Items.Count > 0 || rptlay.Items.Count > 0)
                pnlsubbook.Visible = true;
            else
                pnlsubbook.Visible = !mark;
            pnlTJ.Visible = mark;
        }

        private void SetLocation()
        {
            SetURL();

        }

        private void SetURL()
        {
            if (Request["Ent"] == null)
            {
                return;
            }
            string isEnt = Request["Ent"].ToString();


            if (isEnt == "1")//娱乐
            {
                hfdIsChampian.Value = "1";
                SetLeftVisiable(false);
                LoadEntertainment();
                SetMidVisiable(false);
                hfdEntChampian.Value = "1";
                if (Request["EntChampian"] == null)
                {
                    return;
                }
                string EntChampian = Request["EntChampian"].ToString();

                foreach (RepeaterItem item in rptEnt.Items)
                {
                    LinkButton lbtChampMartch = item.FindControl("lbtChampMartch") as LinkButton;
                    if (lbtChampMartch.CommandArgument == EntChampian)
                    {
                        itemname_OnClick(lbtChampMartch, null);
                        break;
                    }
                }

            }
            else//体育
            {
                SetLeftVisiable(true);

                if (Request["item"] == null) return;
                string eventitemid = Request["item"] == null ? "0" : Request["item"];
                string eventzoneid = Request["zone"] == null ? "0" : Request["zone"];
                string eventid = Request["eventid"] == null ? "0" : Request["eventid"];
                string itemdate = Request["itemdate"] == null ? "0" : Request["itemdate"];
                string matchid = Request["matchid"] == null ? "0" : Request["matchid"];
                string markettempid = Request["markettempid"] == null ? "0" : Request["markettempid"];
                string IsChampian = Request["IsChampian"] == null ? "0" : Request["IsChampian"];
                string ChampionID = Request["ChampionID"] == null ? "0" : Request["ChampionID"];
                string Champeventid = Request["Champeventid"] == null ? "0" : Request["Champeventid"];
                hfdeventitemid.Value = eventitemid;
                hfdeventzoneid.Value = eventzoneid;
                hfdeventid.Value = eventid;
                hfditemdate.Value = itemdate;
                hfdmatchid.Value = matchid;
                hfdmarkettempid.Value = markettempid;
                hfdIsChampian.Value = IsChampian;
                hfdChampionID.Value = ChampionID;
                hfdChampeventid.Value = Champeventid;
                LoadLeftMenues();
            }
        }

        /// <summary>
        /// 加载左菜单项
        /// </summary>
        private void LoadLeftMenues()
        {
            bool chflag = false;
            try
            {
                int dateitem = Int32.Parse(hfditemdate.Value);//为冠军赛
                chflag = true;
            }
            catch
            { }
            LoadEventItem(false);
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                HtmlControl eventitemtr = item.FindControl("eventitemid") as HtmlControl;
                HtmlControl eventitem = item.FindControl("eventitem") as HtmlControl;
                Button itemname = item.FindControl("itemname") as Button;
                Repeater rpt = item.FindControl("rpteventzone") as Repeater;
                if (itemname.CommandArgument == hfdeventitemid.Value)
                {
                    eventitem.Visible = eventitemtr.Visible = true;
                    DefaultSearcher.LANGUAGE = (int)Language;
                    rpt.DataSource = DefaultSearcher.GetEventZone(hfdeventitemid.Value);
                    rpt.DataBind();
                    if (hfdeventzoneid.Value == "0") continue;
                    foreach (RepeaterItem zoneitem in rpt.Items)
                    {
                        HtmlControl eventzonetr = zoneitem.FindControl("eventzoneid") as HtmlControl;
                        HtmlControl eventzone = zoneitem.FindControl("eventzone") as HtmlControl;
                        Button zonebutton = zoneitem.FindControl("itemname") as Button;
                        if (zonebutton.CommandArgument == hfdeventzoneid.Value)
                        {
                            eventzone.Visible = eventzonetr.Visible = true;
                            Repeater rptevent = zoneitem.FindControl("rptevent") as Repeater;
                            DefaultSearcher.LANGUAGE = (int)Language;
                            rptevent.DataSource = DefaultSearcher.GetEvent(hfdeventzoneid.Value);
                            rptevent.DataBind();
                            if (hfdeventid.Value == "0") continue;
                            foreach (RepeaterItem eventsitem in rptevent.Items)
                            {

                                HtmlControl eventtr = eventsitem.FindControl("eventid") as HtmlControl;
                                HtmlControl events = eventsitem.FindControl("event") as HtmlControl;
                                Button eventbutton = eventsitem.FindControl("itemname") as Button;
                                if (eventbutton.CommandArgument == hfdeventid.Value)
                                {
                                    eventtr.Visible = events.Visible = true;
                                    Repeater rpteventdate = eventsitem.FindControl("rpteventdate") as Repeater;
                                    DefaultSearcher.LANGUAGE = (int)Language;
                                    rpteventdate.DataSource = DefaultSearcher.GetEventDate(hfdeventid.Value);
                                    rpteventdate.DataBind();
                                    if (hfditemdate.Value == "0") continue;
                                    foreach (RepeaterItem eventdateitem in rpteventdate.Items)
                                    {
                                        HtmlControl itemdatetr = eventdateitem.FindControl("itemdateid") as HtmlControl;
                                        HtmlControl itemdate = eventdateitem.FindControl("itemdate") as HtmlControl;
                                        Button eventdatebutton = eventdateitem.FindControl("itemname") as Button;
                                        //HiddenField hfdSTARTDATEID = eventdateitem.FindControl("hfdSTARTDATEID") as HiddenField;


                                        if (eventdatebutton.CommandArgument == hfditemdate.Value)
                                        {

                                            itemdatetr.Visible = true;

                                            if (chflag)
                                            {
                                                itemdate.Visible = false;
                                                LinkButton lbtitemname = eventdateitem.FindControl("lbtitemname") as LinkButton;
                                                if (IsChampion(lbtitemname, null))
                                                {
                                                    SetMidVisiable(false);
                                                    //return;
                                                }
                                                continue;
                                            }
                                            itemdate.Visible = true;
                                            Repeater rptmatch = eventdateitem.FindControl("rptmatch") as Repeater;
                                            DefaultSearcher.LANGUAGE = (int)Language;
                                            rptmatch.DataSource = DefaultSearcher.GetMATCH(hfdeventid.Value, hfditemdate.Value);
                                            rptmatch.DataBind();
                                            if (hfdmatchid.Value == "0") continue;
                                            foreach (RepeaterItem matchitem in rptmatch.Items)
                                            {
                                                HtmlControl matchtr = matchitem.FindControl("matchid") as HtmlControl;
                                                HtmlControl match = matchitem.FindControl("match") as HtmlControl;
                                                LinkButton matchbutton = matchitem.FindControl("itemname") as LinkButton;
                                                if (matchbutton.CommandArgument == hfdmatchid.Value)
                                                {
                                                    matchtr.Visible = match.Visible = true;
                                                    Repeater rptMatchMarket = matchitem.FindControl("rptMatchMarket") as Repeater;
                                                    DefaultSearcher.LANGUAGE = (int)Language;
                                                    rptMatchMarket.DataSource = DefaultSearcher.GetMATCHMarket(hfdmatchid.Value);
                                                    rptMatchMarket.DataBind();
                                                    if (hfdmarkettempid.Value == "0")
                                                    {
                                                        SetMidVisiable(false);
                                                        string matchid = matchbutton.CommandArgument;
                                                        hfdmatchid.Value = matchid;
                                                        hfdmarkettempid.Value = DefaultValue;
                                                        hfdEntChampian.Value = "0";
                                                        itemname_OnClick(matchbutton, null);
                                                        continue;
                                                    }


                                                    foreach (RepeaterItem MatchMarketItem in rptMatchMarket.Items)
                                                    {
                                                        LinkButton MatchMarketbutton = MatchMarketItem.FindControl("itemname") as LinkButton;
                                                        string matchMarketid = MatchMarketbutton.Text;
                                                        if (hfdmarkettempid.Value == matchMarketid)
                                                        {
                                                            SetMidVisiable(false);
                                                            itemname_OnClick(MatchMarketbutton, null);
                                                        }
                                                    }



                                                }
                                                else
                                                {
                                                    matchtr.Visible = match.Visible = false;
                                                }

                                            }

                                        }
                                        else
                                        {
                                            if (chflag)
                                            { itemdatetr.Visible = true; itemdate.Visible = false; }
                                            else
                                            { itemdatetr.Visible = itemdate.Visible = false; }
                                        }
                                    }
                                }
                                else
                                {
                                    eventtr.Visible = events.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            eventzone.Visible = eventzonetr.Visible = false;
                        }
                    }
                }
                else
                {
                    eventitem.Visible = eventitemtr.Visible = false;
                }
            }

            if(hfdmatchid.Value != "")
                DisplayMatchRealScore(int.Parse(hfdmatchid.Value));
        }

        private void LoadNotice()
        {
            var noticeData = (new CachedNotice()).QueryCachedData<DSNOTICE>();
            var data = from s in noticeData.TB_AD_NOTICE
                       select new { TITLE = Language == LanguageEnum.Chinese ? s.TITLE : s.ENTITLE, CONTENT = Language == LanguageEnum.Chinese ? s.CONTENT : s.ENCONTENT };

            rptNotice.DataSource = data;
            rptNotice.DataBind();
        }

        /// <summary>
        /// 加载体育项
        /// </summary>
        private void LoadEventItem(bool bIsDisplayEventZone)
        {
            DSEventItem ds = DefaultSearcher.GetEventItem("");
            var data = (from s in ds.TB_EVENT_ITEM
                        select new { EVENTITEM_IDs = s.EventItem_ID, EventItem_Names = Language == LanguageEnum.Chinese ? s.EventItem_Name : s.EventItem_Name_En }).Distinct();

            rptleftmenu.DataSource = data;
            rptleftmenu.DataBind();

            if (rptleftmenu.Items.Count > 0 && bIsDisplayEventZone)
            {
                var curLeftItem = rptleftmenu.Items[0];
                Button btnItem = curLeftItem.FindControl("itemname") as Button;
                if (btnItem != null)
                    eventitem_OnClick(btnItem, null);
            }
        }
        /// <summary>
        /// 加载娱乐
        /// </summary>
        private void LoadEntertainment()
        {
            DefaultSearcher.LANGUAGE = (int)Language;
            var data = DefaultSearcher.GetChampionMatch();

            this.rptEnt.DataSource = data;
            rptEnt.DataBind();
        }

        public void SESwitch(string mark)
        {
            hfdroprace.Value = "1";
            hfdEntChampian.Value = "0";
            hfdIsChampian.Value = "0";
            hfdIsInPlay.Value = "0";
            hdfIsFtCalander.Value = "0";
            if (mark == "1")
            {
                //加载体育大类
                SetUplScoreVisble(true);
                SetMidVisiable(true);
                SetLeftVisiable(true);
                SetInPlayVisible(false);
                SetFootballVisble(false);
                LoadEventItem(true);
            }
            if (mark == "2")
            {
                //加载娱乐大类
                SetUplScoreVisble(false);
                SetMidVisiable(true);
                SetLeftVisiable(false);
                SetInPlayVisible(false);
                SetFootballVisble(false);
                LoadEntertainment();
            }
            if (mark == "3")
            {
                //加载InPlay
                SetUplScoreVisble(false);
                hfdIsInPlay.Value = "1";
                SetMidVisiable(false);
                SetLeftVisiable(false);
                SetInPlayVisible(true);
                SetFootballVisble(false);
                LoadInPlay();
            }
            if (mark == "4")
            {
                SetUplScoreVisble(false);
                hdfIsFtCalander.Value = "1";
                SetMidVisiable(false);
                SetLeftVisiable(true);
                SetInPlayVisible(false);
                SetFootballVisble(true);
                LoadFootball();
            }
            if( pnlMidTop.Visible)
                ViewState[MatchInc] = MatchManager.QueryMatchMarketById(TopRace.Marchid); 
        }

        #region Football
        private void LoadFootball()
        {
            footballGameListCtrl.Language = Language;
            footballGameListCtrl.MatchDataSource = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().ToList<MatchObject>();
            footballGameListCtrl.DataBind();
        }
        #endregion

        #region InPlay
        /// <summary>
        /// 加载InPlay数据
        /// </summary>
        private void LoadInPlay()
        {
            InPlayTimer.Enabled = true;
            GameTodayTimer.Enabled = false;
            GameTomorrowTimer.Enabled = false;
            GameYourInPlayTimer.Enabled = false;
            homeGameListCtrl.Language = Language;
            homeGameListCtrl.AccordionTitle = LangManager.GetString("football");
            IList<MatchObject> matchList = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().Where(m => m.IsZouDi == true).ToList<MatchObject>();
            homeGameListCtrl.showViewAll = matchList.Count <= 10 || homeGameListCtrl.IsViewAll ? false : true;
            if (!homeGameListCtrl.IsViewAll)
                matchList = matchList.Take(10).ToList<MatchObject>();
            homeGameListCtrl.MatchDataSource = matchList;
            homeGameListCtrl.DataBind();
            uplInPlay.Update();
        }

        private void LoadGameToday()
        {
            InPlayTimer.Enabled = false;
            GameTodayTimer.Enabled = true;
            GameTomorrowTimer.Enabled = false;
            GameYourInPlayTimer.Enabled = false;
            GameListToday.Language = Language;
            var matchList = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().Where(m => ((m.MatchStartDate >= DateTime.Now.Date.AddHours(11) && m.MatchStartDate < DateTime.Now.Date.AddDays(1).AddHours(11)) && m.IsZouDi == true)).ToList<MatchObject>();
            GameListToday.MatchDataSource = matchList;
            GameListToday.DataBind();
            uplGameToday.Update();
        }

        private void LoadGameTomorrow()
        {
            InPlayTimer.Enabled = false;
            GameTodayTimer.Enabled = false;
            GameTomorrowTimer.Enabled = true;
            GameYourInPlayTimer.Enabled = false;
            GameListTomorrow.Language = Language;
            var matchList = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().Where(m => ((m.MatchStartDate >= DateTime.Now.Date.AddDays(1).AddHours(11) && m.MatchStartDate < DateTime.Now.Date.AddDays(2).AddHours(11)) && m.IsZouDi == true)).ToList<MatchObject>();
            GameListTomorrow.MatchDataSource = matchList;
            GameListTomorrow.DataBind();
            uplGameTomorrow.Update();
        }

        private void LoadGameYourInPlay()
        {
            InPlayTimer.Enabled = false;
            GameTodayTimer.Enabled = false;
            GameTomorrowTimer.Enabled = false;
            GameYourInPlayTimer.Enabled = true;
            GameListYourInPlay.Language = Language;
            var matchList = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().Where(m => m.IsZouDi == true).ToList<MatchObject>();
            GameListYourInPlay.IsYourInPlay = true;
            GameListYourInPlay.MatchDataSource = matchList;
            GameListYourInPlay.DataBind();
            uplYourGames.Update();
        }

        protected void LoadInPlay_Click(object sender, EventArgs e)
        {
            LoadInPlay();
        }

        protected void LoadGameToday_Click(object sender, EventArgs e)
        {
            LoadGameToday();
        }

        protected void LoadGameTomorrow_Click(object sender, EventArgs e)
        {
            LoadGameTomorrow();
        }

        protected void LoadYourInPlay_Click(object sender, EventArgs e)
        {
            LoadGameYourInPlay();
        }

        protected void InPlayTimer_OnClick(object sender, EventArgs e)
        {
            LoadInPlay();
            uplInPlay.Update();
        }

        protected void GameTodayTimer_OnClick(object sender, EventArgs e)
        {
            LoadGameToday();
            uplGameToday.Update();
        }

        protected void GameTomorrowTimer_OnClick(object sender, EventArgs e)
        {
            LoadGameTomorrow();
            uplGameTomorrow.Update();
        }

        protected void GameYourInPlayTimer_OnClick(object sender, EventArgs e)
        {
            LoadGameYourInPlay();
            uplYourGames.Update();
        }

        #endregion

        #region 绑定左边菜单

        protected void rptleftmenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void rpteventzone_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlControl eventzonetr = e.Item.FindControl("eventzone") as HtmlControl;
                eventzonetr.Visible = false;
            }
        }
        protected void rptevent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlControl eventtr = e.Item.FindControl("event") as HtmlControl;
                eventtr.Visible = false;
            }
        }
        protected void rpteventdate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlControl eventtr = e.Item.FindControl("itemdate") as HtmlControl;
                eventtr.Visible = false;
                HiddenField hfdSTARTDATEID = e.Item.FindControl("hfdSTARTDATEID") as HiddenField;
                Button itemname = e.Item.FindControl("itemname") as Button;
                LinkButton lbtitemname = e.Item.FindControl("lbtitemname") as LinkButton;
                if (hfdSTARTDATEID.Value == "1")
                {
                    lbtitemname.Visible = false;
                    itemname.Visible = true;
                }
                else
                {
                    lbtitemname.Visible = true;
                    itemname.Visible = false;
                }
            }
        }
        protected void rptmatch_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlControl matchtr = e.Item.FindControl("match") as HtmlControl;
                matchtr.Visible = false;
            }
        }

        #endregion

        #region 绑定中间的玩法

        protected void rptmain_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblname = e.Item.FindControl("lblname") as Label;
                Repeater rpt = e.Item.FindControl("rsm") as Repeater;              
                rpt.DataSource = GetMacket(lblname.Text);
                rpt.DataBind();
            }
        }
        protected void rptsubmain_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtMatchId = e.Item.FindControl("txtMatchId") as HtmlInputHidden;
                var btnBack = e.Item.FindControl("btnBack") as Button;
                if (CheckIsFreezeMatch(int.Parse(txtMatchId.Value)))
                {
                    btnBack.CssClass = "backBtnDisableCss";
                    btnBack.OnClientClick = "return false;";
                }
                else
                    btnBack.CssClass = "backBtnCss";
                var btnLay = e.Item.FindControl("btnLay") as Button;
                if (CheckIsFreezeMatch(int.Parse(txtMatchId.Value)))
                {
                    btnLay.CssClass = "layBtnDisableCss";
                    btnLay.OnClientClick = "return false;";
                }
                else
                    btnLay.CssClass = "layBtnCss";
            }
        }

        #endregion

        #region 押注列表绑定
        protected void rptback_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfdMATCH_ID = e.Item.FindControl("hfdMATCH_ID") as HiddenField;
                HiddenField hfdMATCHTYPE = e.Item.FindControl("hfdMATCHTYPE") as HiddenField;
                int matchid = int.Parse(hfdMATCH_ID.Value);
                Repeater rpt = e.Item.FindControl("rptbackitem") as Repeater;
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[MatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                rpt.DataSource = MatchMarcketInfolist.Where(s => s.MATCH_ID == matchid && s.MATCHTYPE == int.Parse(hfdMATCHTYPE.Value));
                rpt.DataBind();
            }
        }
        protected void rptbackitem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void rptlay_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfdMATCH_ID = e.Item.FindControl("hfdMATCH_ID") as HiddenField;
                HiddenField hfdMATCHTYPE = e.Item.FindControl("hfdMATCHTYPE") as HiddenField;
                int matchid = int.Parse(hfdMATCH_ID.Value);
                Repeater rpt = e.Item.FindControl("rptlayitem") as Repeater;
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[layMatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                rpt.DataSource = MatchMarcketInfolist.Where(s => s.MATCH_ID == matchid && s.MATCHTYPE == int.Parse(hfdMATCHTYPE.Value));
                rpt.DataBind();
            }
        }
        protected void rptlayitem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        #endregion

        #region leftMenu加载及相关事件
        public void BindRptMain()
        {
            if (hfdIsChampian.Value == "1")
            {
                if (hfdEntChampian.Value == "1")
                {
                    BindEntChampRpt(hfdChampeventid.Value);
                    DisplayMatchRealScore(null);
                    this.pnlMidM.Update();
                    return;
                }
                BindChampianRpt();
                DisplayMatchRealScore(null);
                this.pnlMidM.Update();
                return;
            }

            string matchid, markettmpid;
            int id = 0;
            if (hfdmarkettempid.Value == "0")
            {
                matchid = hfdmatchid.Value;
                id = int.Parse(matchid);
                rm.DataSource = GetMARKET_TMP_ID(id);
                rm.DataBind();
            }
            else
            {
                markettmpid = hfdmarkettempid.Value;
                matchid = hfdmatchid.Value;
                rm.DataSource = GetMARKET_TMP_ID(hfdmatchid.Value, markettmpid);
                rm.DataBind();
            }
            DisplayMatchRealScore(int.Parse(matchid));
            this.pnlMidM.Update();
        }

        public void BindEntChampRpt(string champEventid)
        {
            hfdIsChampian.Value = "1";
            var data = GetMARKET_TMP_ID(int.Parse(champEventid), int.Parse(champEventid));
            rm.DataSource = data;
            rm.DataBind();
        }

        #region Report
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            ToprecommandTimer.Enabled = true;
            pnlTopRace.Update();
        }
        protected void btnCancelM_OnClick(object sender, EventArgs e)
        {
            MarketTimer.Enabled = true;
            pnlMidM.Update();   
        }
        protected void ddlmarket_OnTextChanged(object sender, EventArgs e)
        {
            int flag = hfdIsChampian.Value == "0" ? 1 : 2;
            var data = BetReportManager.QueryBetReport(flag, int.Parse(hfdrptmatchid.Value), int.Parse(ddlmarket.SelectedValue));
            var cls = data.TB_BLD_RPT.Sum(s =>s.IsdealAmountNull()?0:s.dealAmount).ToString();
            lblcurracesum.Text = cls;
            rptmatchchart.DataSource = data;
            rptmatchchart.DataBind();
            mdlPopup.Show();
            pnlTopRace.Update();
        }
        protected void ddlmarketM_OnTextChanged(object sender, EventArgs e)
        {
            int flag = hfdIsChampian.Value == "0" ? 1 : 2;
            var data = BetReportManager.QueryBetReport(flag, int.Parse(hfdrptmatchidM.Value), int.Parse(ddlmarketM.SelectedValue));
            var cls = data.TB_BLD_RPT.Sum(s => s.IsdealAmountNull() ? 0 : s.dealAmount).ToString();
            this.lblrptsum.Text = cls;
            rptmatchchartM.DataSource = data;
            rptmatchchartM.DataBind();
            mdlPopupM.Show();
            pnlMidM.Update();
        }
        protected void btnrpt_OnClick(object sender, EventArgs e)
        {
            ImageButton ibtrpt = (ImageButton)sender;
            string[] strs=ibtrpt.CommandArgument.Split(new string[]{","},StringSplitOptions.None);
            string matchid = strs[0];
            hfdrptmatchid.Value = matchid;
            int flag = hfdIsChampian.Value == "0" ? 1 : 2;
            lblmatchname.Text = strs[2];
            loadddlmarket(ddlmarket, strs[1]);
            var data = BetReportManager.QueryBetReport(flag, int.Parse(matchid), int.Parse(ddlmarket.SelectedValue));
            // var data = BetReportManager.QueryBetReport(flag, int.Parse(matchid), 2499);
            var cls = data.TB_BLD_RPT.Sum(s => s.IsdealAmountNull() ? 0 : s.dealAmount).ToString();
            lblcurracesum.Text = cls;
            rptmatchchart.DataSource = data;
            rptmatchchart.DataBind();
            ToprecommandTimer.Enabled = false;
            mdlPopup.Show();
            pnlTopRace.Update();
        }
        protected void btnrptM_OnClick(object sender, EventArgs e)
        {
            ImageButton ibtrpt = (ImageButton)sender;
            string[] strs = ibtrpt.CommandArgument.Split(new string[] { "," }, StringSplitOptions.None);
            string matchid = strs[0];
            hfdrptmatchidM.Value = matchid;
            int flag = hfdIsChampian.Value == "0" ? 1 : 2;
            lblmatchnameM.Text = strs[2];
            loadddlmarket(ddlmarketM, strs[1]);
            var data = BetReportManager.QueryBetReport(flag, int.Parse(matchid), int.Parse(ddlmarketM.SelectedValue));
            // var data = BetReportManager.QueryBetReport(flag, int.Parse(matchid), 2499);
            var cls = data.TB_BLD_RPT.Sum(s => s.IsdealAmountNull() ? 0 : s.dealAmount).ToString();
            this.lblrptsum.Text = cls;
            rptmatchchartM.DataSource = data;
            rptmatchchartM.DataBind();
            MarketTimer.Enabled = false;
            mdlPopupM.Show();
            pnlMidM.Update();
        }
        #endregion

        private void loadddlmarket(DropDownList ddlmarket,string matchname)
        {
            ddlmarket.DataValueField = "MARKET_ID";
            ddlmarket.DataTextField = "MARKET_NAME";
            var data = GetMacket(matchname);

            ddlmarket.DataSource = data;
            ddlmarket.DataBind();
            if (data != null)
            {
                ddlmarket.SelectedIndex = 0;
            }
        }

        protected void Toprecommand_OnClick(object sender, EventArgs e)
        {
            hfdIsChampian.Value = "0";
            int id = TopRace.Marchid;

            string matchStatus = GetMatchStatusName(id);

            DSMatchMarket dsmm = MatchManager.QueryMatchMarketById(id);
            ViewState[MatchInc] = dsmm;
            var data = (from s in dsmm.TB_MATCH_MARKET
                        select new {s.MATCH_ID, 
                            MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), 
                            MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, 
                            s.STARTDATE, 
                            MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                            MATCH_STATUS = matchStatus
                        }).Distinct();
            rptTopRecommandMain.DataSource = data;
            rptTopRecommandMain.DataBind();
            DisplayMatchRealScore(id);
            pnlTopRace.Update();
        }
        protected void itemname_OnClick(object sender, EventArgs e)
        {
            DisplayMatchRealScore(null);

            #region 娱乐冠军
            if (hfdEntChampian.Value == "1")
            {
                string Champeventid = "";
                if (sender is System.Web.UI.Timer)
                {
                    Champeventid = hfdChampeventid.Value; 
                }
                else
                {
                    hfdChampeventid.Value = Champeventid = ((LinkButton)sender).CommandArgument;
                }
                BindEntChampRpt(Champeventid);
                return;
            }
            #endregion

            try
            {
                if (sender is Button)
                    return;
                int itemdate = Int32.Parse(hfditemdate.Value);//为冠军赛
                if (itemdate == 0)
                    return;
                BindChampianRpt();
                return;
            }
            catch
            { }


            hfdIsChampian.Value = "0";
            string matchid, markettmpid;
            int id = 0;
            if (hfdmarkettempid.Value == "0")
            {
                if (sender is System.Web.UI.Timer)
                {
                    matchid = hfdmatchid.Value;  //hdfmarketid.Value;
                }
                else
                {
                    matchid = ((LinkButton)sender).CommandArgument;
                }

                id = int.Parse(matchid);

                rm.DataSource = GetMARKET_TMP_ID(id);
                rm.DataBind();
            }
            else
            {
                if (sender is System.Web.UI.Timer)
                {
                    markettmpid = hfdmarkettempid.Value;
                }
                else
                {
                    markettmpid = ((LinkButton)sender).Text;
                }

                rm.DataSource = GetMARKET_TMP_ID(hfdmatchid.Value, markettmpid);
                rm.DataBind();
            }
            DisplayMatchRealScore(int.Parse(hfdmatchid.Value));
        }
        private void BindChampianRpt()
        {
            hfdIsChampian.Value = "1";

            rm.DataSource = GetMARKET_TMP_ID(int.Parse(hfditemdate.Value));
            rm.DataBind();
        }
        protected void eventitem_OnClick(object sender, EventArgs e)
        {
            SetMidVisiable(true);
            string eventitemid = ((Button)sender).CommandArgument;
            hfdeventitemid.Value = eventitemid;
            hfdeventzoneid.Value = DefaultValue;
            hfditemdate.Value = DefaultValue;
            hfdeventid.Value = DefaultValue;
            hfdmatchid.Value = DefaultValue;
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                HtmlControl eventitemtr = item.FindControl("eventitemid") as HtmlControl;
                HtmlControl eventitem = item.FindControl("eventitem") as HtmlControl;
                Button itemname = item.FindControl("itemname") as Button;
                Repeater rpt = item.FindControl("rpteventzone") as Repeater;
                if (itemname.CommandArgument == eventitemid)
                {
                    eventitem.Visible = eventitemtr.Visible = true;
                    DefaultSearcher.LANGUAGE = (int)Language;
                    rpt.DataSource = DefaultSearcher.GetEventZone(eventitemid);
                    rpt.DataBind();
                }
                else
                {
                    eventitem.Visible = eventitemtr.Visible = false;
                }
            }
        }
        protected void eventzone_OnClick(object sender, EventArgs e)
        {
            string eventzoneid = ((Button)sender).CommandArgument;

            hfdeventzoneid.Value = eventzoneid;
            hfditemdate.Value = DefaultValue;
            hfdeventid.Value = DefaultValue;
            hfdmatchid.Value = DefaultValue;
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                Button itemname = item.FindControl("itemname") as Button;
                if (itemname.CommandArgument == hfdeventitemid.Value)
                {
                    Repeater rpteventzone = item.FindControl("rpteventzone") as Repeater;
                    foreach (RepeaterItem zoneitem in rpteventzone.Items)
                    {
                        HtmlControl eventzonetr = zoneitem.FindControl("eventzoneid") as HtmlControl;
                        HtmlControl eventzone = zoneitem.FindControl("eventzone") as HtmlControl;
                        Button zonebutton = zoneitem.FindControl("itemname") as Button;
                        if (zonebutton.CommandArgument == hfdeventzoneid.Value)
                        {
                            eventzone.Visible = eventzonetr.Visible = true;
                            Repeater rptevent = zoneitem.FindControl("rptevent") as Repeater;
                            DefaultSearcher.LANGUAGE = (int)Language;
                            rptevent.DataSource = DefaultSearcher.GetEvent(hfdeventzoneid.Value);
                            rptevent.DataBind();
                        }
                        else
                        {
                            eventzone.Visible = eventzonetr.Visible = false;
                        }

                    }
                }
            }
        }
        protected void event_OnClick(object sender, EventArgs e)
        {
            string eventid = ((Button)sender).CommandArgument;

            hfditemdate.Value = DefaultValue;
            hfdeventid.Value = eventid;
            hfdmatchid.Value = DefaultValue;
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                Button itemname = item.FindControl("itemname") as Button;
                if (itemname.CommandArgument == hfdeventitemid.Value)
                {
                    Repeater rpteventzone = item.FindControl("rpteventzone") as Repeater;
                    foreach (RepeaterItem zoneitem in rpteventzone.Items)
                    {
                        Button zonebutton = zoneitem.FindControl("itemname") as Button;
                        if (zonebutton.CommandArgument == hfdeventzoneid.Value)
                        {
                            Repeater rptevent = zoneitem.FindControl("rptevent") as Repeater;
                            foreach (RepeaterItem eventitem in rptevent.Items)
                            {
                                HtmlControl eventtr = eventitem.FindControl("eventid") as HtmlControl;
                                HtmlControl events = eventitem.FindControl("event") as HtmlControl;
                                Button eventbutton = eventitem.FindControl("itemname") as Button;
                                if (eventbutton.CommandArgument == hfdeventid.Value)
                                {
                                    eventtr.Visible = events.Visible = true;
                                    Repeater rpteventdate = eventitem.FindControl("rpteventdate") as Repeater;
                                    DefaultSearcher.LANGUAGE = (int)Language;
                                    rpteventdate.DataSource = DefaultSearcher.GetEventDate(hfdeventid.Value);
                                    rpteventdate.DataBind();
                                }
                                else
                                {
                                    eventtr.Visible = events.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void eventDate_OnClick(object sender, EventArgs e)
        {
            IButtonControl obj = (IButtonControl)sender;
            //if(obj==null)
            //    obj = (LinkButton)sender;
            string eventDateid = obj.CommandArgument;

            hfditemdate.Value = eventDateid;
            if (IsChampion(sender, e))
            {
                SetMidVisiable(false);
                return;
            }
            hfdmatchid.Value = DefaultValue;
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                Button itemname = item.FindControl("itemname") as Button;
                if (itemname.CommandArgument == hfdeventitemid.Value)
                {
                    Repeater rpteventzone = item.FindControl("rpteventzone") as Repeater;
                    foreach (RepeaterItem zoneitem in rpteventzone.Items)
                    {
                        Button zonebutton = zoneitem.FindControl("itemname") as Button;
                        if (zonebutton.CommandArgument == hfdeventzoneid.Value)
                        {
                            Repeater rptevent = zoneitem.FindControl("rptevent") as Repeater;
                            foreach (RepeaterItem eventitem in rptevent.Items)
                            {

                                Button eventbutton = eventitem.FindControl("itemname") as Button;
                                if (eventbutton.CommandArgument == hfdeventid.Value)
                                {
                                    //eventtr.Visible = events.Visible = true;
                                    Repeater rpteventdate = eventitem.FindControl("rpteventdate") as Repeater;
                                    foreach (RepeaterItem eventdateitem in rpteventdate.Items)
                                    {
                                        HtmlControl itemdatetr = eventdateitem.FindControl("itemdateid") as HtmlControl;
                                        HtmlControl itemdate = eventdateitem.FindControl("itemdate") as HtmlControl;
                                        Button eventdatebutton = eventdateitem.FindControl("itemname") as Button;
                                        //HiddenField hfdSTARTDATEID = eventdateitem.FindControl("hfdSTARTDATEID") as HiddenField;
                                        //LinkButton lbtitemname = eventdateitem.FindControl("lbtitemname") as LinkButton;
                                        if (eventdatebutton.CommandArgument == hfditemdate.Value)
                                        {
                                            itemdatetr.Visible = itemdate.Visible = true;
                                            Repeater rptmatch = eventdateitem.FindControl("rptmatch") as Repeater;
                                            DefaultSearcher.LANGUAGE = (int)Language;
                                            rptmatch.DataSource = DefaultSearcher.GetMATCH(hfdeventid.Value, hfditemdate.Value);
                                            rptmatch.DataBind();

                                        }
                                        else
                                        {
                                            itemdatetr.Visible = itemdate.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsChampion(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                    return false;
                int itemdate = Int32.Parse(hfditemdate.Value);//为冠军赛
                if (itemdate == 0)
                    return false;
                hfdChampionID.Value = ((LinkButton)sender).CommandArgument;
                hfdEntChampian.Value = "0";
                BindChampianRpt();
                return true;
            }
            catch
            {
                return false;
            }
            // itemname_OnClick(sender, e);
        }
        protected void martch_OnClick(object sender, EventArgs e)
        {
            hfdIsInPlay.Value = "0";
            hdfIsFtCalander.Value = "0";
            SetUplScoreVisble(true);
            SetMidVisiable(false);
            SetInPlayVisible(false);
            SetFootballVisble(false);
            string matchid = ((LinkButton)sender).CommandArgument;
            hfdmatchid.Value = matchid;
            hfdmarkettempid.Value = DefaultValue;
            hfdEntChampian.Value = "0";
            itemname_OnClick(sender, e);
            foreach (RepeaterItem item in rptleftmenu.Items)
            {
                Button itemname = item.FindControl("itemname") as Button;
                if (itemname.CommandArgument == hfdeventitemid.Value)
                {
                    Repeater rpteventzone = item.FindControl("rpteventzone") as Repeater;
                    foreach (RepeaterItem zoneitem in rpteventzone.Items)
                    {
                        Button zonebutton = zoneitem.FindControl("itemname") as Button;
                        if (zonebutton.CommandArgument == hfdeventzoneid.Value)
                        {
                            Repeater rptevent = zoneitem.FindControl("rptevent") as Repeater;
                            foreach (RepeaterItem eventitem in rptevent.Items)
                            {

                                Button eventbutton = eventitem.FindControl("itemname") as Button;
                                if (eventbutton.CommandArgument == hfdeventid.Value)
                                {
                                    Repeater rpteventdate = eventitem.FindControl("rpteventdate") as Repeater;
                                    foreach (RepeaterItem eventdateitem in rpteventdate.Items)
                                    {
                                        Button eventdatebutton = eventdateitem.FindControl("itemname") as Button;
                                        if (eventdatebutton.CommandArgument == hfditemdate.Value)
                                        {
                                            Repeater rptmatch = eventdateitem.FindControl("rptmatch") as Repeater;
                                            foreach (RepeaterItem matchitem in rptmatch.Items)
                                            {
                                                HtmlControl matchtr = matchitem.FindControl("matchid") as HtmlControl;
                                                HtmlControl match = matchitem.FindControl("match") as HtmlControl;
                                                LinkButton matchbutton = matchitem.FindControl("itemname") as LinkButton;
                                                if (matchbutton.CommandArgument == hfdmatchid.Value)
                                                {
                                                    matchtr.Visible = match.Visible = true;
                                                    Repeater rptMatchMarket = matchitem.FindControl("rptMatchMarket") as Repeater;
                                                    DefaultSearcher.LANGUAGE = (int)Language;
                                                    rptMatchMarket.DataSource = DefaultSearcher.GetMATCHMarket(hfdmatchid.Value);
                                                    rptMatchMarket.DataBind();
                                                }
                                                else
                                                {
                                                    matchtr.Visible = match.Visible = false;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void martchMarket_OnClick(object sender, EventArgs e)
        {
            hfdIsInPlay.Value = "0";
            hdfIsFtCalander.Value = "0";
            hfdEntChampian.Value = "0";
            SetUplScoreVisble(true);
            SetMidVisiable(false);
            string matchMarketid = ((LinkButton)sender).Text;
            hfdmarkettempid.Value = matchMarketid;
            itemname_OnClick(sender, e);
        }
        /// <summary>
        /// 娱乐冠军
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChampMartch_OnClick(object sender, EventArgs e)
        {
            SetMidVisiable(false);
            //  string matchMarketid = ((LinkButton)sender).Text;
            hfdEntChampian.Value = "1";
            itemname_OnClick(sender, e);
        }

        #endregion

        #region 押注操作
        protected void betBack(IEnumerable<MatchMarcketInfo> data)
        {
            SetRightMenuVisiable(false);
            SaveBack(rptback);
            AddSession(data.First(), "back");
            bindRpt(rptback, "back");
            CheckRightMenu();
        }

        protected void betLay(IEnumerable<MatchMarcketInfo> data)
        {
            SetRightMenuVisiable(false);
            SaveLay(rptlay);
            AddSession(data.First(), "lay");
            bindRpt(rptlay, "lay");
            CheckRightMenu();
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            SetRightMenuVisiable(false);
            SaveBack(rptback);
            string MARKET_ID = ((Button)sender).CommandArgument;
            IEnumerable<MatchMarcketInfo> data = GetMacketbyid(int.Parse(MARKET_ID), true);

            AddSession(data.First(), "back");
            bindRpt(rptback, "back");
            CheckRightMenu();
        }
        protected void btnLay_OnClick(object sender, EventArgs e)
        {
            SetRightMenuVisiable(false);
            SaveLay(rptlay);
            string MARKET_ID = ((Button)sender).CommandArgument;
            IEnumerable<MatchMarcketInfo> data = GetMacketbyid(int.Parse(MARKET_ID), false);

            AddSession(data.First(), "lay");
            bindRpt(rptlay, "lay");
            CheckRightMenu();
        }
        protected void imbdelback_OnClick(object sender, ImageClickEventArgs e)
        {
            SaveBack(rptback);
            string[] arrayid = ((ImageButton)sender).CommandArgument.Split(new string[] { "," }, StringSplitOptions.None);
            string MARKET_ID = arrayid[0];
            string MATCHTYPEID = arrayid[1];
            HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[MatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            delSession(MatchMarcketInfolist.Where(s => s.MARKET_ID == int.Parse(MARKET_ID) && s.MATCHTYPE == int.Parse(MATCHTYPEID)).First(), "back");
            bindRpt(rptback, "back");
            CheckRightMenu();
        }
        protected void imbdellay_OnClick(object sender, ImageClickEventArgs e)
        {
            SaveLay(rptlay);
            string[] arrayid = ((ImageButton)sender).CommandArgument.Split(new string[] { "," }, StringSplitOptions.None);
            string MARKET_ID = arrayid[0];
            string MATCHTYPEID = arrayid[1];
            HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[layMatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            delSession(MatchMarcketInfolist.Where(s => s.MARKET_ID == int.Parse(MARKET_ID) && s.MATCHTYPE == int.Parse(MATCHTYPEID)).First(), "lay");
            bindRpt(rptlay, "lay");
            CheckRightMenu();
        }


        #endregion

        #region Session operate

        public void AddSession(MatchMarcketInfo item, string flag)
        {
            if (double.Parse(item.odds) < 2.00)
                item.odds = "2.00";

            if (flag == "back")
            {
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[MatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                MatchMarcketInfolist.Add(item);
                ViewState[MatchMarcketInfoList] = MatchMarcketInfolist;
            }
            if (flag == "lay")
            {
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[layMatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                MatchMarcketInfolist.Add(item);
                ViewState[layMatchMarcketInfoList] = MatchMarcketInfolist;
            }
        }
        public void delSession(MatchMarcketInfo item, string flag)
        {
            if (flag == "back")
            {
                if (ViewState[MatchMarcketInfoList] == null)
                    return;
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                MatchMarcketInfolist.Remove(item);
            }

            if (flag == "lay")
            {
                if (ViewState[layMatchMarcketInfoList] == null)
                    return;
                HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
                MatchMarcketInfolist.Remove(item);
            }
        }

        public void DelAllBackAndLayData()
        {
            ViewState[MatchMarcketInfoList] = null;
            ViewState[layMatchMarcketInfoList] = null;
        }

        private void CheckRightMenu()
        {
            HashSet<MatchMarcketInfo> backMatchMarcketInfolist = ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            HashSet<MatchMarcketInfo> layMatchMarcketInfolist = ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            rptback.Visible = backMatchMarcketInfolist == null ? false : backMatchMarcketInfolist.Count > 0 ? true : false;
            rptlay.Visible = layMatchMarcketInfolist == null ? false : layMatchMarcketInfolist.Count > 0 ? true : false;

            if (!rptback.Visible && !rptlay.Visible)
                SetRightMenuVisiable(true);
        }
        private void bindRpt(Repeater rpt, string flag)
        {
            HashSet<MatchMarcketInfo> MatchMarcketInfolist = null;
            if (flag == "back")
            {
                MatchMarcketInfolist = ViewState[MatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            }
            if (flag == "lay")
            {
                MatchMarcketInfolist = ViewState[layMatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            }

            rpt.DataSource = MatchMarcketInfolist.Select(s => new { s.MATCHTYPE, s.MATCH_ID, s.MATCH_NAME }).Distinct();
            rpt.DataBind();
            pnlbook.Update();
        }
        #endregion

        #region 获取对象
        /// <summary>
        /// 获取市场
        /// </summary>
        /// <param name="marketid">市场id</param>
        /// <param name="flag">flag为true时为下注，否则为受注</param>
        /// <returns></returns>
        public IEnumerable<MatchMarcketInfo> GetMacketbyid(int marketid, bool flag)
        {
            DSMatchMarket ds = ViewState[MatchInc] as DSMatchMarket;
            int matchtype = hfdIsChampian.Value == "1" ? 2 : 1;

            IEnumerable<MatchMarcketInfo> data = from s in ds.TB_MATCH_MARKET
                                                 where s.MARKET_ID == marketid
                                                 select new MatchMarcketInfo { MATCHTYPE = matchtype, MATCH_ID = s.MATCH_ID, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN, MARKET_ID = s.MARKET_ID, MARKET_NAME = Language == LanguageEnum.Chinese ? s.MARKET_NAME : s.MARKET_NAME_EN, MARKET_TMP_ID = s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, AMOUNTS = flag ? s.layMATCH_AMOUNTS : s.backMATCH_AMOUNTS, odds = flag ? s.layodds : s.backodds }
            ;
            return data;
        }

        public IEnumerable<MatchMarcketInfo> GetMacketbymatchid(int matchid)
        {
            DSMatchMarket ds = ViewState[MatchInc] as DSMatchMarket;
            IEnumerable<MatchMarcketInfo> data = from s in ds.TB_MATCH_MARKET
                                                 where s.MATCH_ID == matchid
                                                 select new MatchMarcketInfo { MATCH_ID = s.MATCH_ID, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN, MARKET_ID = s.MARKET_ID, MARKET_NAME = Language == LanguageEnum.Chinese ? s.MARKET_NAME : s.MARKET_NAME_EN, MARKET_TMP_ID = s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, AMOUNTS = s.backMATCH_AMOUNTS, odds = s.backodds }
            ;
            return data;
        }


        public object GetMARKET_TMP_ID(int matchid)
        {
            string matchStatusName = GetMatchStatusName(matchid);
            DSMatchMarket ds = GetMarket_DS(matchid.ToString());
            if (hfdIsChampian.Value == "0")
            {
                var data = (from s in ds.TB_MATCH_MARKET
                            select new { s.MATCH_ID,MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                            MATCH_STATUS=matchStatusName
                            }).Distinct();
                return data;
            }
            else
            {
                var data = (from s in ds.TB_MATCH_MARKET
                            where s.MARKET_TMP_ID.ToString() == hfdChampionID.Value
                            select new { s.MATCH_ID,MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                            MATCH_STATUS=matchStatusName}).Distinct();
                return data;
            }
        }

        public object GetMARKET_TMP_ID(int matchid, int champevnetid)
        {
            string matchStatusName = GetMatchStatusName(matchid);
            DSMatchMarket ds = GetMarket_DS(matchid.ToString());
            var data = (from s in ds.TB_MATCH_MARKET
                        where s.MARKET_TMP_ID == champevnetid
                        select new { s.MATCH_ID, MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                        MATCH_STATUS=matchStatusName}).Distinct();
            return data;
        }

        public object GetMARKET_TMP_ID(string marketid, int bettypeid, int markettmptype)
        {
            DSMatchMarket ds = GetMarket_DS(marketid);
            string matchStatusName = string.Empty;
            if (ds.TB_MATCH_MARKET.Rows.Count > 0)
            {
                matchStatusName = GetMatchStatusName(ds.TB_MATCH_MARKET[0].MATCH_ID);
            }

            var data = (from s in ds.TB_MATCH_MARKET
                        where s.BET_TYPE_ID == bettypeid && s.Market_Tmp_Type == markettmptype
                        select new { s.MATCH_ID, MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                        MATCH_STATUS=matchStatusName}).Distinct();
            return data;
        }
        public object GetMARKET_TMP_ID(string marketid, string markettmpname)
        {            
            DSMatchMarket ds = GetMarket_DS(marketid);
            string matchStatusName = string.Empty;
            if (ds.TB_MATCH_MARKET.Rows.Count > 0)
            {
                matchStatusName = GetMatchStatusName(ds.TB_MATCH_MARKET[0].MATCH_ID);
            }

            if (Language == LanguageEnum.Chinese)
            {
                var data = (from s in ds.TB_MATCH_MARKET
                            where s.MARKET_TMP_NAME == markettmpname
                            select new { s.MATCH_ID,MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                            MATCH_STATUS=matchStatusName}).Distinct();
                return data;
            }
            else
            {
                var data = (from s in ds.TB_MATCH_MARKET
                            where s.MARKET_TMP_NAME_EN == markettmpname
                            select new { s.MATCH_ID,MARKET_TMP_ID = s.BET_TYPE_ID.ToString() + "," + s.Market_Tmp_Type.ToString(), MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.STARTDATE, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN,
                            MATCH_STATUS=matchStatusName}).Distinct();
                return data;
            }
        }

        private DSMatchMarket GetMarket_DS(string marketid)
        {
            DSMatchMarket ds = hfdEntChampian.Value == "0" && hfdIsChampian.Value == "0" ? MatchManager.QueryMatchMarketById(int.Parse(marketid)) : MatchManager.QueryMatchMarketByEventId(int.Parse(marketid));
            ViewState[MatchInc] = ds;
            return ds;
        }
        public object GetMacket(int bettypeid, int markettmptype)
        {
            DSMatchMarket ds = ViewState[MatchInc] as DSMatchMarket;
            var data = from s in ds.TB_MATCH_MARKET
                       where s.BET_TYPE_ID == bettypeid && s.Market_Tmp_Type == markettmptype
                       select new { s.MATCH_ID, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN, s.MARKET_ID, MARKET_NAME = Language == LanguageEnum.Chinese ? s.MARKET_NAME : s.MARKET_NAME_EN, s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.layMATCH_AMOUNTS, s.layodds, s.backMATCH_AMOUNTS, s.backodds };
            return data;
        }
        public object GetMacket(string MARKETTMPNAME)
        {
            if (Language == LanguageEnum.Chinese)
            {
                DSMatchMarket ds = ViewState[MatchInc] as DSMatchMarket;
                var data = from s in ds.TB_MATCH_MARKET
                           where s.MARKET_TMP_NAME == MARKETTMPNAME
                           select new { s.MATCH_ID, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN, s.MARKET_ID, MARKET_NAME = Language == LanguageEnum.Chinese ? s.MARKET_NAME : s.MARKET_NAME_EN, s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.layMATCH_AMOUNTS, s.layodds, s.backMATCH_AMOUNTS, s.backodds };
                return data;
            }
            else
            {
                DSMatchMarket ds = ViewState[MatchInc] as DSMatchMarket;
                var data = from s in ds.TB_MATCH_MARKET
                           where s.MARKET_TMP_NAME_EN == MARKETTMPNAME
                           select new { s.MATCH_ID, MATCH_NAME = Language == LanguageEnum.Chinese ? s.MATCH_NAME : s.MATCH_NAME_EN, s.MARKET_ID, MARKET_NAME = Language == LanguageEnum.Chinese ? s.MARKET_NAME : s.MARKET_NAME_EN, s.MARKET_TMP_ID, MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MARKET_TMP_NAME : s.MARKET_TMP_NAME_EN, s.layMATCH_AMOUNTS, s.layodds, s.backMATCH_AMOUNTS, s.backodds };
                return data;
            }

        }
        #endregion

        #region 下注
        /// <summary>
        /// 下注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnpbets_Click(object sender, EventArgs e)
        {
            SaveLay(rptlay);
            SaveBack(rptback);
            if (CurrentUser == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('" + LangManager.GetString("loginfirst") + "');", true);
                return;
            }
            decimal amount = decimal.Parse(hfdtotal.Value);
            HashSet<MatchMarcketInfo> MatchMarcketInfolist = ViewState[MatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[MatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            HashSet<MatchMarcketInfo> layMatchMarcketInfolist = ViewState[layMatchMarcketInfoList] == null ? new HashSet<MatchMarcketInfo>() : ViewState[layMatchMarcketInfoList] as HashSet<MatchMarcketInfo>;
            string returnValue = MatchManager.PlaceBet(amount, this.CurrentUser.UserId, MatchMarcketInfolist, layMatchMarcketInfolist);
            if (string.IsNullOrEmpty(returnValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('" + LangManager.GetString("betSeccess") + "');", true);
                ViewState[MatchMarcketInfoList] = null;
                ViewState[layMatchMarcketInfoList] = null;

                layMatchMarcketInfolist = MatchMarcketInfolist = null;
            }
            else
            {            
                returnValue = returnValue.Replace("'", " ");
                string[] results=   returnValue.Split(new string[] { "," }, StringSplitOptions.None);
                if (results[0] == "999")
                {
                    string exceptionstring = LangManager.GetString("pleasecancelitem") + results[1];
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('" + exceptionstring + "');", true);//pleasecancelitem
                }
                else {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('" + LangManager.GetString("betFail") + "');", true);
                }
            }

            bindRpt(rptback, "back");
            bindRpt(rptlay, "lay");
            if (pnlMidTop.Visible)
            {
                Toprecommand_OnClick(null, null);
            }
            else
            {
                BindRptMain();
            }
            SetRightMenuVisiable(true);
            //刷新登录面板
            this.CurMasterPage.RefreshHomeLoginPanel();
        }
        #endregion

        /// <summary>
        /// 全部取消所投注选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelSelection_Click(object sender, EventArgs e)
        {
            DelAllBackAndLayData();
            bindRpt(rptback, "back");            
            bindRpt(rptlay, "lay");
            CheckRightMenu();
        }

        #region 公共方法
        private bool CheckIsFreezeMatch(int iMatchId)
        {
            if (!IsChampionMatch)
            {
                var curMatch = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>()
                                .Match_List.Where(r => r.MATCH_ID == iMatchId).FirstOrDefault();
                if (curMatch == null)
                    return false;

                if (curMatch.ADDITIONALSTATUS == (int)MatchAdditionalStatusEnum.FreezingMatch)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private string GetMatchStatusName(int iMatchId)
        {
            if (IsChampionMatch)
                return string.Empty;
            else
            {
                if (CheckIsFreezeMatch(iMatchId))
                    return LangManager.GetString("Rotaryheader");
                else
                    return string.Empty;
            }
        }

        private bool IsChampionMatch
        {
            get
            {
                return !(hfdEntChampian.Value == "0" && hfdIsChampian.Value == "0");
            }
        }
        #endregion

        #region 刷新

        protected void btnRefTopRace_Click(object sender, EventArgs e)
        {
            Toprecommand_OnClick(null, null);
        }

        protected void btnRefRac_Click(object sender, EventArgs e)
        {
            itemname_OnClick(MarketTimer, null);
        }
        
        #endregion

        #region 显示实时比分

        private void DisplayMatchRealScore(int? iMatchId)
        {
            string strScore = string.Empty;
            string strGreenColorStyle = "green";
            string strBlankColorStyle = "black";
            string strZouDiTemplate = " <font color={0}>{1}<br/>{2}</font> ";
            string strIsNotZouDiTemplate = " {0} ";
            if (!iMatchId.HasValue)
                return;
            else
            {
                var curMatch = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>()
                                .Match_List.Where(r=>r.MATCH_ID == iMatchId.Value).FirstOrDefault();
                if (curMatch != null)
                {
                    bool bIsMatchBeginPlay = false;
                    if (curMatch.STATUS == (int)MatchStatusEnum.Activated ||
                        curMatch.STATUS == (int)MatchStatusEnum.NotActivated)
                        bIsMatchBeginPlay = false;
                    else
                        bIsMatchBeginPlay = true;

                    int iHomeFullScore = curMatch.IsHOME_FULL_SCORENull() ? 0 : curMatch.HOME_FULL_SCORE;
                    int iGuestFullScore = curMatch.IsGUEST_FULL_SCORENull() ? 0 : curMatch.GUEST_FULL_SCORE;

                    if(curMatch.IS_ZOUDI)
                    {
                         lblScore.Text = string.Format(strZouDiTemplate,
                                bIsMatchBeginPlay?strGreenColorStyle:strBlankColorStyle,
                                bIsMatchBeginPlay?iHomeFullScore.ToString() + " - " + iGuestFullScore.ToString()  : "V",
                                bIsMatchBeginPlay ? LangManager.GetString("InPlay") : LangManager.GetString("HasZouDi"));
                    }
                    else
                    {
                         lblScore.Text = string.Format(strIsNotZouDiTemplate,"V");                                
                    }

                    if (Language == LanguageEnum.Chinese)
                    {
                        lblTeamA.Text = curMatch.EVENT_HOME_TEAM_NAME;
                        lblTeamB.Text = curMatch.EVENT_GUEST_TEAM_NAME;
                    }
                    else
                    {
                        lblTeamA.Text = curMatch.EVENT_HOME_TEAM_NAME_EN;
                        lblTeamB.Text = curMatch.EVENT_GUEST_TEAM_NAME_EN;
                    }
                }
            }
            updScore.Update();
        }

        #endregion
    }
}