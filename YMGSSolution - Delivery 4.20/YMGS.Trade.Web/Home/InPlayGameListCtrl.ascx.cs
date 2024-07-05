using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.Game;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;
using System.Data;

namespace YMGS.Trade.Web.Home
{
    public partial class InPlayGameListCtrl : System.Web.UI.UserControl
    {
        public delegate void Refresh();
        public delegate void BetBack(IEnumerable<MatchMarcketInfo> data);
        public delegate void BetLay(IEnumerable<MatchMarcketInfo> data);
        public Refresh refresh;
        public BetBack betBack;
        public BetLay betLay;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public bool showViewAll
        {
            get
            {
                return btnViewAll.Visible;
            }
            set
            {
                btnViewAll.Visible = value;
            }
        }


        public string AccordionTitle
        {
            set
            {
                lblTitle.Text = value;
            }
        }

        public DetailUserInfo CurrentUser
        {
            get
            {
                return PageHelper.GetCurrentUser();
            }
        }

        public DSYourInPlay YourInPlayDS
        {
            get
            {
                if (CurrentUser == null)
                    return null;
                return YourInPlayManager.QueryYourInPlay(CurrentUser.UserId);
            }
        }

        public LanguageEnum Language
        {
            get
            {
                if (ViewState["Language"] == null)
                    return LanguageEnum.Chinese;
                return (LanguageEnum)ViewState["Language"];
            }
            set
            {
                ViewState["Language"] = value;
            }
        }

        public bool IsViewAll
        {
            get
            {
                if (ViewState["IsViewAll"] == null)
                    return false;
                return (bool)ViewState["IsViewAll"];
            }
            set
            {
                ViewState["IsViewAll"] = value;
            }
        }

        public IList<MatchObject> MatchDataSource {
            get
            {
                if (ViewState["MatchDataSource"] == null)
                    return null;
                return AddYourInPlayToMatchList(ViewState["MatchDataSource"] as IList<MatchObject>);
            }
            set
            {
                ViewState["MatchDataSource"] = value;
            }
        }

        public IList<MarketObject> MarketDataSource 
        {
            get
            {
                return GetMarketList();
            }
        }

        public IList<MarketObject> GetMarketList()
        {
            if (MatchDataSource == null)
                return null;
            if (MatchDataSource.FirstOrDefault() == null)
                return null;
            var marketList = MarketFactory.GetMarketObject(MatchType.Football).GetMarketList(MatchDataSource.FirstOrDefault(m => m.DefaultMarketTmpId != 0).DefaultMarketTmpId);
            return marketList;
        }

        public override void DataBind()
        {
            base.DataBind();
            if (MatchDataSource == null || MarketDataSource == null)
                return;
            //绑定具体玩法
            var marketFlags = (from m in MarketDataSource
                               orderby m.MarketFlag
                               select new 
                               { 
                                  MarketFlag = LangManager.GetString(( (MatchMarketFlagEnum)m.MarketFlag ).ToString())
                               }).Distinct();
            rptMLFlag.DataSource = marketFlags;
            rptMLFlag.DataBind();
            //绑定赛事
            var eventsList = from e in MatchDataSource
                             orderby e.MatchStartDate
                             group e by new
                             {
                                 e.MatchStartDate,
                                 e.MatchEventId
                             }
                             into g
                             select new
                             {
                                EventId = g.Key.MatchEventId,
                                MatchStartDate = g.Key.MatchStartDate,
                                EventName = g.Max(p => p.MatchEventName),
                                EventNameEN = g.Max(p => p.MatchEventNameEN)
                             };
            ELRpt.DataSource = eventsList;
            ELRpt.DataBind();
        }

        protected void ELRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var eventDataItem = e.Item.DataItem;
                var lblEventId = DataBinder.Eval(e.Item.DataItem, "EventId").ToString();
                var MatchStartDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "MatchStartDate"));
                Label lblEventName = e.Item.FindControl("EName") as Label;
                lblEventName.Text = Language == LanguageEnum.Chinese ? DataBinder.Eval(e.Item.DataItem, "EventName").ToString() : DataBinder.Eval(e.Item.DataItem, "EventNameEN").ToString();
                Repeater MatchListRpt = e.Item.FindControl("MLRpt") as Repeater;
                var matchList = MatchDataSource.Where(m => m.MatchEventId == lblEventId && m.MatchStartDate == MatchStartDate);
                if (matchList.Count() == 0)
                    e.Item.Visible = false;
                MatchListRpt.DataSource = matchList;
                MatchListRpt.DataBind();
            }
        }

        protected void MLRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var matchObj = e.Item.DataItem as MatchObject;
                Label lblMatchId = e.Item.FindControl("MID") as Label;
                Label lblHomeTeamName = e.Item.FindControl("HTName") as Label;
                Label lblGuestTeamName = e.Item.FindControl("GTName") as Label;
                Label lblBetStatus = e.Item.FindControl("lblBetStatus") as Label;
                Label lblCusParam = e.Item.FindControl("lblCusParam") as Label;
                lblHomeTeamName.Text = Language == LanguageEnum.Chinese ? matchObj.HomeTeamName : matchObj.HomeTeamNameEN;
                lblGuestTeamName.Text = Language == LanguageEnum.Chinese ? matchObj.GuestTeamName : matchObj.GuestTeamNameEN;
                lblBetStatus.Text = LangManager.GetString(matchObj.MatchLimitBetStatus);
                lblCusParam.Text = matchObj.CustomParam_1 == "HT" ? LangManager.GetString("HT") : matchObj.CustomParam_1;
                //绑定投注市场
                var matchId = Convert.ToInt32(lblMatchId.Text);
                var marketList = MarketDataSource.Where(m => m.MatchId == matchId).OrderBy(m => m.MarketFlag);
                if (marketList.Count() == 0)
                    e.Item.Visible = false;
                if (!string.IsNullOrEmpty(matchObj.MatchStatusClass))
                {
                    foreach (var item in marketList)
                        item.MarketEnabled = false;
                }
                Repeater MarketListRpt = e.Item.FindControl("MKRpt") as Repeater;
                MarketListRpt.DataSource = marketList;
                MarketListRpt.DataBind();
            }
        }

        protected void MKRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var marketObj = e.Item.DataItem as MarketObject;
                if (!marketObj.MarketEnabled)
                {
                    foreach (var ctl in e.Item.Controls)
                    {
                        if (ctl is Button)
                            (ctl as Button).OnClientClick = "return false;";
                    }
                }
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            if (betBack == null)
                return;
            var marketId = Convert.ToInt32((sender as Button).CommandArgument);
            betBack(GetMatchMarcketInfo(true, marketId));
        }

        protected void btnLay_OnClick(object sender, EventArgs e)
        {
            if (betLay == null)
                return;
            var marketId = Convert.ToInt32((sender as Button).CommandArgument);
            betLay(GetMatchMarcketInfo(false, marketId));
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            if (refresh == null)
                return;
            refresh();
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            IsViewAll = true;
            btnViewAll.Visible = false;
            refresh();
        }

        private IEnumerable<MatchMarcketInfo> GetMatchMarcketInfo(bool IsBack, int marketId)
        {
            if (MarketDataSource == null)
                return null;
            IEnumerable<MatchMarcketInfo> data = from s in MarketDataSource
                                                 where s.MarketId == marketId
                                                 select new MatchMarcketInfo
                                                 {
                                                     MATCHTYPE = 1,
                                                     MATCH_ID = s.MatchId,
                                                     MATCH_NAME = Language == LanguageEnum.Chinese ? s.MatchName : s.MatchNameEN,
                                                     MARKET_ID = s.MarketId,
                                                     MARKET_NAME = Language == LanguageEnum.Chinese ?
                                                         s.MarketName : s.MarketNameEN,
                                                     MARKET_TMP_ID = s.MarketTmpId,
                                                     MARKET_TMP_NAME = Language == LanguageEnum.Chinese ? s.MarketTmpName : s.MarketTmpNameEN,
                                                     AMOUNTS = !IsBack ? s.BackMatchAmouts : s.LayMatchAmouts,
                                                     odds = !IsBack ? s.BackOdds : s.LayOdds
                                                 };
            return data;
        }

        private IList<MatchObject> AddYourInPlayToMatchList(IList<MatchObject> data)
        {
            if (CurrentUser == null)
                return data;
            if (YourInPlayDS == null)
                return data;
            foreach (var match in data)
            {
                var inPlayMatch = YourInPlayDS.TBYourInPlay.FirstOrDefault(m => m.MATCH_ID == match.MatchId);
                if (inPlayMatch == null)
                    continue;
                match.IsMatchFaved = inPlayMatch.IS_FAV;
                match.MatchFavedCalss = match.IsMatchFaved == 1 ? "starchoosed" : "stars";
            }
            return data;
        }
    }
}