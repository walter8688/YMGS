using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Game.GameHelper;
using YMGS.Data.Common;
using YMGS.Trade.Web.Common;
using YMGS.Business.Game;
using YMGS.Data.Entity;
using YMGS.Framework;

namespace YMGS.Trade.Web.Home
{
    public partial class FootballGameListCtrl : System.Web.UI.UserControl
    {
        public delegate void BetBack(IEnumerable<MatchMarcketInfo> data);
        public delegate void BetLay(IEnumerable<MatchMarcketInfo> data);
        public BetBack betBack;
        public BetLay betLay;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public bool IsAutoRefresh
        {
            set
            {
                FbCalanderTimer.Enabled = value;
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

        public IList<MatchObject> MatchDataSource
        {
            get
            {
                if (ViewState["MatchDataSource"] == null)
                    return null;
                var fbCalander = Convert.ToInt32(hdfFbCal.Value);
                var matchList = from m in (ViewState["MatchDataSource"] as IList<MatchObject>)
                                //where UtilityHelper.DateToStr(m.MatchStartDate.Date).Equals(UtilityHelper.DateToStr(DateTime.Now.AddDays(fbCalander)))
                                where m.MatchStartDate >= DateTime.Now.Date.AddDays(fbCalander - 1).AddHours(11) && m.MatchStartDate < DateTime.Now.Date.AddDays(fbCalander).AddHours(11)
                                select m;
                return matchList.ToList<MatchObject>();
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

        public IEnumerable<Object> MarketFlagDataSource
        {
            get
            {
                if (MatchDataSource == null || MarketDataSource == null)
                    return null;
                //绑定具体玩法
                var marketFlags = (from m in MarketDataSource
                                   orderby m.MarketFlag
                                   select new
                                   {
                                       MarketFlag = LangManager.GetString(((MatchMarketFlagEnum)m.MarketFlag).ToString())
                                   }).Distinct();
                return marketFlags;
            }
        }

        public IList<MarketObject> GetMarketList()
        {
            if (MatchDataSource.Count == 0)
                return null;
            var marketList = MarketFactory.GetMarketObject(MatchType.Football).GetMarketList(MatchDataSource.FirstOrDefault(m => m.DefaultMarketTmpId != 0).DefaultMarketTmpId);
            return marketList;
        }

        public override void DataBind()
        {
            base.DataBind();
            var fbCalList = FootballCalendarManager.GetFootballCalendar();
            foreach (var item in fbCalList)
            {
                item.WeekCalendarName = LangManager.GetString(item.WeekCalendarName);
            }
            rptCalendar.DataSource = fbCalList;
            rptCalendar.DataBind();
            BindMatch();
        }

        private void BindMatch()
        {
            //绑定赛事
            if (MatchDataSource == null || MarketDataSource == null)
            {
                divGameList.Visible = false;
                divNotice.Visible = true;
                lblNotice.Text = LangManager.GetString("FbCalanderNotice");
                return;
            }
            divNotice.Visible = false;
            divGameList.Visible = true;
            var eventsList = from ev in MarketDataSource
                             join m in MatchDataSource
                             on ev.MatchId equals m.MatchId
                             orderby m.MatchStartDate
                             group m by new
                             {
                                 m.MatchStartDate,
                                 m.MatchEventId
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

        protected void rptFt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //绑定赛事
                if (MatchDataSource == null)
                    return;
                if (MatchDataSource.FirstOrDefault() == null)
                    return;
                var fbCal = e.Item.DataItem as FootballCalendar;
                var ELRpt = e.Item.FindControl("ELRpt") as Repeater;
                if (fbCal.CalendarDateId == Convert.ToInt32(hdfFbCal.Value))
                {
                    var eventsList = from ev in MarketDataSource
                                     join m in MatchDataSource
                                     on ev.MatchId equals m.MatchId
                                     orderby m.MatchStartDate
                                     group m by new
                                     {
                                         m.MatchStartDate,
                                         m.MatchEventId
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
                else
                {
                    ELRpt.DataSource = null;
                    ELRpt.DataBind();
                }
           
            }
        }

        protected void ELRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var rptMLFlag = e.Item.FindControl("rptMLFlag") as Repeater;
                rptMLFlag.DataSource = MarketFlagDataSource;
                rptMLFlag.DataBind();

                var eventDataItem = e.Item.DataItem;
                var lblEventId = DataBinder.Eval(e.Item.DataItem, "EventId").ToString();
                var MatchStartDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "MatchStartDate"));
                Label lblEventName = e.Item.FindControl("EName") as Label;
                lblEventName.Text = Language == LanguageEnum.Chinese ? DataBinder.Eval(e.Item.DataItem, "EventName").ToString() : DataBinder.Eval(e.Item.DataItem, "EventNameEN").ToString();
                Repeater MatchListRpt = e.Item.FindControl("MLRpt") as Repeater;
                var matchList = MatchDataSource.Where(m => m.MatchEventId == lblEventId && m.MatchStartDate == MatchStartDate).OrderBy(m => m.MatchStartDate);
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

        protected void btnReBind_Click(object sender, EventArgs e)
        {
            RefreshMatchData();
            BindMatch();
            uplFbCal.Update();
        }

        private void RefreshMatchData()
        {
            MatchDataSource = MatchFactory.GetMatchObjecj(MatchType.Football).GetMatchList().ToList<MatchObject>();
        }

        protected void FbCalanderTimer_OnClick(object sender, EventArgs e)
        {
            RefreshMatchData();
            BindMatch();
            uplFbCal.Update();
        }
    }
}