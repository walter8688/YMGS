using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using YMGS.Business.Navigator;
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
using YMGS.Trade.Web.Football.Common;
using YMGS.Trade.Web.Football.BusinessLogic;
using YMGS.Trade.Web.Football.Model;

namespace YMGS.Trade.Web
{
    public partial class Default : HomeBasePage
    {
        #region 常量
        private const string PageIdParam = "PageId";
        private const string EventItemIdParam = "EventItemId";
        private const string EventZoneIdParam = "EventZoneId";
        private const string EventIdParam = "EventId";
        private const string MatchStartDateParam = "MatchStartDate";
        private const string ChampionEventIdParam = "ChampionEventId";
        private const string MatchIdParam = "MatchId";
        private const string MarketTmpIdParam = "MarketTmpId";
        private const string BetTypeIdParam = "BetTypeId";
        private const string MarketTmpTypeParam = "MarketTmpType";
        private const string OrderNOParam = "OrderNO";

        #endregion

        #region 属性
        public int CurPageId
        {
            get
            {
                if (Request.QueryString[PageIdParam] == null)
                    return (int)PageIdEnum.Sports;
                var pageId = Convert.ToInt32(Request.QueryString[PageIdParam]);
                hdfPageId.Value = pageId.ToString();
                return pageId;
            }
        }
        #endregion

        #region 首页Url
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(Default), string.Empty).AbsoluteUri;
        }

        public static string Url(int pageId)
        {
            return UrlHelper.BuildUrl(typeof(Default), string.Empty, PageIdParam, pageId).AbsoluteUri;
        }
        #endregion

        #region 页面加载

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            TR.LanguageMark = oc.LanguageMark = adwords.LanguageMark = RR.LanguageMark = (int)Language;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPageByPageIdParam();
            if (!IsPostBack)
            {
                languagemark.Value = ((int)Language).ToString();
            }
        }

        public void LoadPageByPageIdParam()
        {
            var curPageParam = (PageIdEnum)CurPageId;
            var homeLogin = Master.FindControl("homeLogin") as HomeLoginClientCtrl;
            homeLogin.Language = (int)Language;
            bet.Language = (int)Language;
            //足球不加载LeftNavigator
            if (curPageParam == PageIdEnum.Sports)
            {
                leftNav.Visible = true;
                entNav.Visible = false;
                inPlayCtrl.Visible = false;
                fbCtrl.Visible = false;

                LoadLeftNavigatorData();
                LoadCenterData();
            }
            else if (curPageParam == PageIdEnum.InPlay)
            {
                leftNav.Visible = false;
                entNav.Visible = false;
                TR.Visible = false;
                fbCtrl.Visible = false;
                inPlayCtrl.Visible = true;
                SetAllTop1Visabled(false);
                MMtop3.Visible = false;
                SetChampTop3Visabled(false);
                LoadInplayData();
            }
            else if (curPageParam == PageIdEnum.Football)
            {
                TR.Visible = false;
                leftNav.Visible = true;
                entNav.Visible = false;
                fbCtrl.Visible = true;
                inPlayCtrl.Visible = false;
                SetAllTop1Visabled(false);
                LoadLeftNavigatorData();
                LoadFootballMatchListData();
            }
            else if (curPageParam == PageIdEnum.Entertaiment)
            {
                SetAllTop1Visabled(false);
                entNav.Visible = true;
                MMtop3.Visible = false;
                SetChampTop3Visabled(true);
                LoadEntNavigatorData();
                LoadChampTop3Data();
            }
        }
        #endregion

        #region 加载页面数据

        /// <summary>
        /// 构造DeafultPage页面参数对象
        /// </summary>
        /// <returns></returns>
        private DefaultPageQueryStringObject InitDefaultPageQueryStringObj()
        {
            DefaultPageQueryStringObject defaultPageQueryStringObj = new DefaultPageQueryStringObject();
            var pageIdTmp = Request.QueryString[PageIdParam];
            var eventItemIdTmp = Request.QueryString[EventItemIdParam];
            var eventZoneIdTmp = Request.QueryString[EventZoneIdParam];
            var eventIdTmp = Request.QueryString[EventIdParam];
            var matchStartDateTmp = Request.QueryString[MatchStartDateParam];
            var championEventIdTmp = Request.QueryString[ChampionEventIdParam];
            var matchIdTmp = Request.QueryString[MatchIdParam];
            var marketTmpTypeIdTmp = Request.QueryString[MarketTmpIdParam];
            var betTypeIdTmp = Request.QueryString[BetTypeIdParam];
            var marketTmpTypeTmp = Request.QueryString[MarketTmpTypeParam];
            var OrderNoTmp = Request.QueryString[OrderNOParam];
            if (!string.IsNullOrEmpty(pageIdTmp))
            {
                defaultPageQueryStringObj.PageId = pageIdTmp;
            }
            if (!string.IsNullOrEmpty(eventItemIdTmp))
            {
                defaultPageQueryStringObj.EventItemId = eventItemIdTmp;
            }
            if (!string.IsNullOrEmpty(eventZoneIdTmp))
            {
                defaultPageQueryStringObj.EventZoneId = eventZoneIdTmp;
            }
            if (!string.IsNullOrEmpty(eventIdTmp))
            {
                defaultPageQueryStringObj.EventId = eventIdTmp;
            }
            if (!string.IsNullOrEmpty(matchStartDateTmp))
            {
                defaultPageQueryStringObj.MatchStartDate = matchStartDateTmp;
            }
            if (!string.IsNullOrEmpty(championEventIdTmp))
            {
                defaultPageQueryStringObj.ChampionEventId = championEventIdTmp;
                return defaultPageQueryStringObj;
            }
            if (!string.IsNullOrEmpty(matchIdTmp))
            {
                defaultPageQueryStringObj.MatchId = matchIdTmp;
            }
            if (!string.IsNullOrEmpty(marketTmpTypeIdTmp))
            {
                defaultPageQueryStringObj.MarketTmpId = marketTmpTypeIdTmp;
            }
            if (!string.IsNullOrEmpty(betTypeIdTmp))
            {
                defaultPageQueryStringObj.BetTypeId = betTypeIdTmp;
            }
            if (!string.IsNullOrEmpty(marketTmpTypeTmp))
            {
                defaultPageQueryStringObj.MarketTmpType = marketTmpTypeTmp;
            }
            if (!string.IsNullOrEmpty(OrderNoTmp))
            {
                defaultPageQueryStringObj.OrderNO = OrderNoTmp;
            }
            return defaultPageQueryStringObj;
        }

        /// <summary>
        /// 加载LeftNavigator
        /// </summary>
        private void LoadLeftNavigatorData()
        {
            var navSearchObjList = DefaultPageHelper.InitNavigatorSearchList(InitDefaultPageQueryStringObj());
            AbstractNavigator leftNavigator = new LeftNavigator();
            leftNav.Language = (int)Language;
            leftNav.NavigatorList = leftNavigator.GetNavigatots(navSearchObjList, Language);
            leftNav.DataBind();
        }

        /// <summary>
        /// 加载娱乐Navigator
        /// </summary>
        private void LoadEntNavigatorData()
        {
            int selectedNavigatorId = -1;
            if (Request.QueryString[ChampionEventIdParam] != null)
                selectedNavigatorId = Convert.ToInt32(Request.QueryString[ChampionEventIdParam]);
            AbstractNavigator entNavigator = new EntNavigator();
            entNav.Language = (int)Language;
            entNav.SelectedNavigatorId = selectedNavigatorId;
            entNav.NavigatorList = entNavigator.GetNavigatots(null, Language);
            entNav.DataBind();
        }
        /// <summary>
        /// 加载足球
        /// </summary>
        private void LoadFootballMatchListData()
        {
            FootballGameListBL fbBL = new FootballGameListBL(Language);
            var fbObjectList = fbBL.GetFootballGameList();
            IList<FootballObject> footballList = null;
            if (fbObjectList != null)
            {
                if (DateTime.Now.Hour >= 11)
                {
                    footballList = fbBL.GetFootballGameList().Where(e => ((e.EventStartDate >= DateTime.Now.Date.AddHours(11) && e.EventStartDate < DateTime.Now.Date.AddDays(1).AddHours(11)))).ToList();
                }
                else
                {
                    footballList = fbBL.GetFootballGameList().Where(e => ((e.EventStartDate >= DateTime.Now.Date.AddDays(-1).AddHours(11) && e.EventStartDate < DateTime.Now.Date.AddHours(11)))).ToList();
                }
            }
            var fbCalanderList = FootballCommonFunction.GetFootballCalendar();
            fbCtrl.Language = (int)Language;
            fbCtrl.FootballCalanderId = 1;
            fbCtrl.IsAutoRefresh = true;
            fbCtrl.FootballCalendarList = fbCalanderList;
            fbCtrl.MarketFlagList = FootballCommonFunction.GenerateMarketFlagList();
            fbCtrl.FootballList = footballList;
            fbCtrl.DataBind();
        }

        /// <summary>
        /// 加载Inplay
        /// </summary>
        private void LoadInplayData()
        {
            InPlayFootballGameListBL bl = new InPlayFootballGameListBL(Language);
            int? curUserId = CurrentUser == null ? -1 : CurrentUser.UserId;
            var inplayTabsList = FootballCommonFunction.GenerateInplayTabList();
            var fbOjectsList = bl.GetFootballGameList(curUserId);
            inPlayCtrl.Language = (int)Language;
            inPlayCtrl.InPlayTabId = 1;
            inPlayCtrl.IsAutoRefresh = true;
            inPlayCtrl.IsShowAll = GetInPlayShowAll(fbOjectsList);
            inPlayCtrl.FootballList = fbOjectsList == null ? null : fbOjectsList.Take(10);
            inPlayCtrl.MarketFlagList = FootballCommonFunction.GenerateMarketFlagList();
            inPlayCtrl.InplayTabList = inplayTabsList;
            inPlayCtrl.DataBind();
        }

        /// <summary>
        /// 加载top1或者top3
        /// </summary>
        private void LoadCenterData()
        {
            DefaultPageQueryStringObject defaultPageQueryStringObj = InitDefaultPageQueryStringObj();
            if (string.IsNullOrEmpty(defaultPageQueryStringObj.PageId) ||
                (defaultPageQueryStringObj.PageId == "0" && string.IsNullOrEmpty(defaultPageQueryStringObj.EventItemId)))
            {//为空时，表示第一次加载数据，默认为置顶比赛
                TR.Visible = true;
                ////取默认置顶比赛的比赛ID
                //int matchID = TR.Marchid;
                //SetAllTop1Visabled(true);
                //SetAllTop3Visabled(false);
                //SetChampTop3Visabled(false);
                //////比赛的标题
                //LoadMatchRealTitleInfo(matchID);
                ////load top1的数据
                //LoadCenterTop1Data(matchID);

                SetDefaultInplayListVisabled(true);
                SetAllTop1Visabled(false);
                SetAllTop3Visabled(false);
                SetChampTop3Visabled(false);
                LoadDefaultInplayListData();
            }
            else
            {
                TR.Visible = false;
                if (!string.IsNullOrEmpty(defaultPageQueryStringObj.MatchId))
                {//不为空 即为其他比赛 为空即为冠军比赛
                    int matchID = Convert.ToInt32(defaultPageQueryStringObj.MatchId);
                    //比赛的标题
                    LoadMatchRealTitleInfo(matchID);

                    if (string.IsNullOrEmpty(defaultPageQueryStringObj.MarketTmpId)
                        && string.IsNullOrEmpty(defaultPageQueryStringObj.OrderNO)
                        && string.IsNullOrEmpty(defaultPageQueryStringObj.BetTypeId)
                        )
                    {//全为空的话 即为top1
                        SetDefaultInplayListVisabled(false);
                        SetAllTop1Visabled(true);
                        SetAllTop3Visabled(false);
                        SetChampTop3Visabled(false);
                        LoadCenterTop1Data(matchID);
                    }
                    else//不为空的话 即为top3
                    {
                        SetDefaultInplayListVisabled(false);
                        SetAllTop1Visabled(false);
                        SetAllTop3Visabled(true);
                        SetChampTop3Visabled(false);
                        MatchTop3Parameter param = new MatchTop3Parameter();
                        param.MarketTmpID = defaultPageQueryStringObj.MarketTmpId;
                        param.OrderNO = defaultPageQueryStringObj.OrderNO;
                        param.BetTypeID = defaultPageQueryStringObj.BetTypeId;
                        param.MarketTmpType = defaultPageQueryStringObj.MarketTmpType;

                        LoadCenterTop3DataByParam(matchID, param);
                    }
                }
                else
                {
                    //int champEventID = Convert.ToInt32(defaultPageQueryStringObj.ChampionEventId);
                    SetDefaultInplayListVisabled(false);
                    SetAllTop1Visabled(false);
                    SetAllTop3Visabled(false);
                    SetChampTop3Visabled(true);
                    LoadChampTop3Data();
                }
            }
        }

        /// <summary>
        /// 首页显示今天的即将开始的所有比赛信息
        /// </summary>
        private void LoadDefaultInplayListData()
        {
            InPlayFootballGameListBL bl = new InPlayFootballGameListBL(Language);
            int? curUserId = CurrentUser == null ? -1 : CurrentUser.UserId;
            var fbOjectsList = bl.GetDefaultFootballGameList();
            DefaultIn.Language = (int)Language;
            DefaultIn.IsAutoRefresh = true;
            DefaultIn.IsShowAll = GetInPlayShowAll(fbOjectsList);
            DefaultIn.FootballList = fbOjectsList == null ? null : fbOjectsList.Take(10);
            DefaultIn.MarketFlagList = FootballCommonFunction.GenerateMarketFlagList();
            DefaultIn.DataBind();
        }

        /// <summary>
        /// 冠军比赛的Top3
        /// </summary>
        private void LoadChampTop3Data()
        {
            DefaultPageQueryStringObject defaultPageQueryStringObj = InitDefaultPageQueryStringObj();
            if (!string.IsNullOrEmpty(defaultPageQueryStringObj.ChampionEventId))
            {
                int champEventID = Convert.ToInt32(defaultPageQueryStringObj.ChampionEventId);
                ChampMatchBL champBL = new ChampMatchBL(Language);
                var champLst = champBL.GetChampMatchMarketInfoByEventID(champEventID);

                ChampTop3.Language = (int)Language;
                ChampTop3.Champ_EventID = champEventID;
                ChampTop3.ChampData = champLst;
                ChampTop3.DataBind();
            }
        }

        private void LoadCenterTop1Data(int matchID)
        {
            LoadAsianMatchMarketTop1(matchID);
            LoadOtherTop1Data(matchID);
        }

        private void LoadAsianMatchMarketTop1(int matchID)
        {
            IList<Control> top1AsianLst = GetTop1AsianUC();
            //IList<MarketBetTypeOrderInfo> marketBetTypeLst = FootballCommonFunction.QueryAllMarketBetTypeOrderInfo();
            IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(matchID);
            FTCenterTopBL matchBL = new FTCenterTopBL(matchID, Language);
            int i = 0;
            foreach (Control top1AsianUC in top1AsianLst)
            {
                if (top1AsianUC is YMGS.Trade.Web.Football.Controls.AsianMatchMarketTop1)
                {
                    var AsianTop1 = top1AsianUC as YMGS.Trade.Web.Football.Controls.AsianMatchMarketTop1;
                    MatchTop3Parameter param = new MatchTop3Parameter();
                    param.OrderNO = marketBetTypeLst[i].OrdNo.ToString();
                    param.IsAutoRefresh = marketBetTypeLst[i].isOpen;

                    string controlTitleName = matchBL.GetTop3ControlTitle(matchID, param);
                    bool isDisplayShowAllLink = matchBL.IsDisplayShowAllButton(param);
                    CenterTop3MarketObject marketObject = matchBL.GetAllAndSummaryTop3Data(matchID, param);

                    AsianTop1.Language = (int)Language;
                    AsianTop1.Match_ID = matchID;
                    AsianTop1.MarketNameTitle = controlTitleName;
                    AsianTop1.IsDisplayShowAll = isDisplayShowAllLink;
                    AsianTop1.Param = param;
                    AsianTop1.Top3ObjectData = marketObject;
                    AsianTop1.DataBind();
                    i++;
                }
            }
        }

        private IList<Control> GetTop1AsianUC()
        {
            IList<Control> top1AsianLst = new List<Control>();
            ContentPlaceHolder ContentID = (ContentPlaceHolder)Master.FindControl("mph");
            foreach (Control ctr in ContentID.Controls)
            {
                if (ctr is YMGS.Trade.Web.Football.Controls.AsianMatchMarketTop1)
                {
                    top1AsianLst.Add(ctr);
                }
            }
            return top1AsianLst;
        }

        private void LoadOtherTop1Data(int matchID)
        {
            IList<Control> top1UCLst = GetTop1UC();

            //IList<MarketBetTypeOrderInfo> marketBetTypeLst = FootballCommonFunction.QueryAllMarketBetTypeOrderInfo();
            IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(matchID);
            FTCenterTopBL matchBL = new FTCenterTopBL(matchID, Language);
            int i = 2;
            foreach (Control top1UC in top1UCLst)
            {
                if (top1UC is YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1)
                {
                    var top1 = top1UC as YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1;
                    MarketBetTypeOrderInfo betOrder = marketBetTypeLst[i];
                    string titleName = Language == LanguageEnum.Chinese ? betOrder.BetTypeName_CN : betOrder.BetTypeName_EN;

                    var top1BO = matchBL.GetWebServiceTop1Data(matchID, betOrder);
                    top1.Language = (int)Language;
                    top1.MatchID = matchID;
                    top1.ControlTitle = titleName;
                    top1.BetBO = betOrder;
                    top1.top1Obj = top1BO;
                    top1.DataBind();
                    i++;
                }
            }
        }

        private IList<Control> GetTop1UC()
        {
            IList<Control> top1UCLst = new List<Control>();
            ContentPlaceHolder ContentID = (ContentPlaceHolder)Master.FindControl("mph");
            foreach (Control ctr in ContentID.Controls)
            {
                if (ctr is YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1)
                {
                    top1UCLst.Add(ctr);
                }
            }
            return top1UCLst;
        }

        private void LoadCenterTop3DataByParam(int matchID, MatchTop3Parameter param)
        {
            FTCenterTopBL matchBL = new FTCenterTopBL(matchID, Language);

            string controlTitleName = matchBL.GetTop3ControlTitle(matchID, param);
            bool isDisplayShowAllLink = matchBL.IsDisplayShowAllButton(param);
            CenterTop3MarketObject marketObject = matchBL.GetAllAndSummaryTop3Data(matchID, param);

            MMtop3.Language = (int)Language;
            MMtop3.Match_ID = matchID;
            MMtop3.MarketNameTitle = controlTitleName;
            MMtop3.IsDisplayShowAll = isDisplayShowAllLink;
            MMtop3.Param = param;
            MMtop3.Top3ObjectData = marketObject;
            MMtop3.DataBind();
        }

        private void SetAllTop1Visabled(bool isDisplay)
        {
            AsianTop1FullAll.Visible = AsianTop1HalfAll.Visible =
                //MMtop1HandicapFullAll.Visible = MMtop1HandicapHalfAll.Visible = 
            MMtop1SoccerHalfSpecial.Visible =
            MMtop1SoccerFullSpecial.Visible = MMtop1CorrectFull.Visible = MMtop1CorrectHalf.Visible =
            MMtop1StandardFull.Visible = MMtop1StandardHalf.Visible = MMtop1StandardHalfAndFull.Visible =
            MMtop1SoccerHalfOthers.Visible = MMtop1SoccerFullOthers.Visible = isDisplay;
        }

        private void SetAllTop3Visabled(bool isDisplay)
        {
            MMtop3.Visible = isDisplay;
        }

        private void SetChampTop3Visabled(bool isDisplay)
        {
            ChampTop3.Visible = isDisplay;
        }

        private void SetDefaultInplayListVisabled(bool isDisplay)
        {
            DefaultIn.Visible = isDisplay;
        }

        private bool GetInPlayShowAll(IEnumerable<FootballObject> fbOjectsList)
        {
            if (fbOjectsList != null)
            {
                int count = 0;
                foreach (var fbObj in fbOjectsList)
                {
                    foreach (var matchObj in fbObj.FootballMatchs)
                    {
                        count++;
                        if (count > 10)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 加载比赛标题信息
        /// </summary>
        /// <param name="MatchID"></param>
        private void LoadMatchRealTitleInfo(int MatchID)
        {
            InPlayFootballGameListBL bl = new InPlayFootballGameListBL(Language);
            YMGS.Trade.Web.Football.Model.FootballMatch matchRealBO = bl.GetSingleMatchRealInfo(MatchID);
            MatchRealInfo.Language = (int)Language;
            MatchRealInfo.MatchID = MatchID;
            MatchRealInfo.MatchRealBO = matchRealBO;

            MatchRealInfo.DataBind();
        }
        #endregion
    }
}