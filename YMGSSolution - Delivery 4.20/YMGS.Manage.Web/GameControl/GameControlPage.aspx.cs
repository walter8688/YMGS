using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.EventManage;
using YMGS.Data.DataBase;
using YMGS.Business.GameMarket;
using YMGS.Business.GameControl;
using YMGS.Data.Presentation;
using YMGS.Framework;
using YMGS.Data.Entity;

namespace YMGS.Manage.Web.GameControl
{
    [TopMenuId(FunctionIdList.GameControl.GameControlModule)]
    [LeftMenuId(FunctionIdList.GameControl.GameControlPage)]
    public partial class GameControlPage : BasePage
    {
        private const int _CommandNumber = 9;
        private const string _CommandActivated = "CommandActivated";
        private const string _CommandEditStartDate = "CommandEditStartDate";
        private const string _CommandMatchHalfEnd = "CommandMatchHalfEnd";
        private const string _CommandMatchFullEnd = "CommandMatchFullEnd";
        private const string _CommandFrezz = "CommandFrezz";
        private const string _CommandClearMarket = "CommandClearMarket";
        private const string _CommandRecordScore = "CommandRecordScore";
        private const string _CommandStartMatch = "CommandStartMatch";
        private const string _CommandMatchSecHalfStart = "CommandMatchSecHalfStart";
        private const string _CommandScript = "if(window.confirm('确定要{0}吗?')) return true;else return false;";
        private const string _DataKey = "DataKey";
        private const string _CancleFreezed = "取消封盘";
        private const string _CommandHandicap = "CommandHandicap";

        public LinkButton[] OperatorButtons
        {
            get;
            set;
        }

        public string[] OperatorButtonCmds
        {
            get
            {
                return new string[_CommandNumber] { _CommandActivated, _CommandEditStartDate, _CommandMatchHalfEnd, _CommandMatchSecHalfStart, _CommandMatchFullEnd, _CommandFrezz, _CommandClearMarket, _CommandRecordScore, _CommandStartMatch };
            }
        }

        public string[] OperatorCmdScriptParams
        {
            get
            {
                return new string[_CommandNumber] { "激活", "修改开始时间", "半场结束", "下半场开始", "全场结束", "封盘", "清理市场", "录入比分", "开始比赛" };
            }
        }

        public bool[] PageAuthorityArr
        {
            get
            {
                return PageAuthority();
            }
        }

        public int CurrentUserId
        {
            get
            {
                return MySession.CurrentUser.ACCOUNT[0].USER_ID;
            }
        }

        public int? CurrentDataKey
        {
            get
            {
                if (ViewState[_DataKey] == null)
                    return null;
                return (Convert.ToInt32(ViewState[_DataKey]));
            }
            set
            {
                ViewState[_DataKey] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            //pageNavigatorHandicap.PageIndexChanged += new EventHandler(pageNavigatorHandicap_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitPage();
        }

        #region 页面权限
        private bool[] PageAuthority()
        {
            return new bool[] { MySession.Accessable(FunctionIdList.GameControl.ActivatedGame),
                MySession.Accessable(FunctionIdList.GameControl.EditGameStartTime),
                MySession.Accessable(FunctionIdList.GameControl.HalfEndGame),
                MySession.Accessable(FunctionIdList.GameControl.SecHalfStart),
                MySession.Accessable(FunctionIdList.GameControl.EndGame),
                MySession.Accessable(FunctionIdList.GameControl.EntertainGame),
                MySession.Accessable(FunctionIdList.GameControl.ClearGameMarket),
                MySession.Accessable(FunctionIdList.GameControl.ScoreGame),
                MySession.Accessable(FunctionIdList.GameControl.StartGame)};
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameControl.GameControlPage;
            }
        }
        #endregion

        #region 加载页面初始化数据
        private void InitPage()
        {
            //设置定时时间
            this.tmGameControl.Interval = 5000;
            //加载赛事相关数据
            LoadEventItem();
            LoadEventZone(null);
            //初始化时间
            var curDate = PageHelper.QueryCurSysDateTime();
            startDate.Value = curDate.AddDays(-30);
            endDate.Value = curDate.AddDays(30);
            //初始化控件状态
            txtHomeFullScore.Enabled = false;
            txtGuestFullScore.Enabled = false;
            //加载Grid数据
            LoadGridData();
        }

        /// <summary>
        /// 加载赛事项目
        /// </summary>
        private void LoadEventItem()
        {
            var eventItemList = EventZoneManager.QueryEventItem();
            var blankRow = eventItemList.TB_EVENT_ITEM.NewTB_EVENT_ITEMRow();
            blankRow.EventItem_ID = -1;
            blankRow.EventItem_Name = string.Empty;
            eventItemList.TB_EVENT_ITEM.Rows.InsertAt(blankRow, 0);
            ddlEventItem.DataValueField = "EVENTITEM_ID";
            ddlEventItem.DataTextField = "EVENTITEM_NAME";
            ddlEventItem.DataSource = eventItemList.TB_EVENT_ITEM;
            ddlEventItem.DataBind();
            ddlEventItem.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载赛事区域
        /// </summary>
        /// <param name="eventItemId"></param>
        private void LoadEventZone(int? eventItemId)
        {
            ddlEventZone.Items.Clear();
            ddlEventZone.SelectedIndex = -1;
            if (eventItemId.HasValue)
            {
                DSEventZone dsEventZone = new DSEventZone();
                DSEventZone.TB_EVENT_ZONERow queryRow = dsEventZone.TB_EVENT_ZONE.NewTB_EVENT_ZONERow();
                queryRow.EVENTITEM_ID = eventItemId.Value;
                queryRow.EVENTZONE_NAME = string.Empty;
                queryRow.EVENTZONE_NAME_EN = string.Empty;
                queryRow.EVENTZONE_DESC = string.Empty;
                queryRow.PARAM_ZONE_ID = -1;
                var eventZoneList = EventZoneManager.QueryEventZone(queryRow);
                var blankRow = eventZoneList.TB_EVENT_ZONE.NewTB_EVENT_ZONERow();
                blankRow.EVENTZONE_ID = -1;
                blankRow.EVENTZONE_NAME = string.Empty;
                eventZoneList.TB_EVENT_ZONE.Rows.InsertAt(blankRow, 0);
                ddlEventZone.DataValueField = "EVENTZONE_ID";
                ddlEventZone.DataTextField = "EVENTZONE_NAME";
                ddlEventZone.DataSource = eventZoneList.TB_EVENT_ZONE;
                ddlEventZone.DataBind();
            }
            else
            {
                ddlEventZone.Items.Add(new ListItem(string.Empty, "-1"));
            }

            ddlEventZone.SelectedIndex = 0;
        }

        private void LoadEvent(int? eventZoneId)
        {
            if (!eventZoneId.HasValue)
                eventZoneId = -1;
            var eventData = EventManager.QueryEvent(eventZoneId, string.Empty, string.Empty, string.Empty, null, null, 0, -1, string.Empty);
            var blankRow = eventData._DSEventTeamList.NewDSEventTeamListRow();
            blankRow.EVENT_ID = -1;
            blankRow.EVENT_NAME = string.Empty;
            blankRow.EVENT_NAME_EN = string.Empty;
            blankRow.EventItem_ID = -1;
            eventData._DSEventTeamList.Rows.InsertAt(blankRow, 0);
            ddlEvent.DataTextField = "EVENT_NAME";
            ddlEvent.DataValueField = "EVENT_ID";
            ddlEvent.DataSource = eventData;
            ddlEvent.DataBind();
            ddlEvent.SelectedIndex = 0;
        }

        protected void ddlEventZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventZone.SelectedIndex > 0)
            {
                LoadEvent(Convert.ToInt32(ddlEventZone.SelectedValue));
            }
            else
            {
                LoadEvent(null);
            }
        }

        private IList<MatchStatusInfo> _MatchStatusList = null;
        private IList<MatchStatusInfo> QueryMatchStatus()
        {
            if (_MatchStatusList == null)
            {
                _MatchStatusList = CommonFunction.QueryAllMatchStatus();
            }
            return _MatchStatusList;
        }

        private IList<MatchStatusInfo> _MatchAddiStatusList = null;
        private IList<MatchStatusInfo> QueryMatchAddiStatus()
        {
            if (_MatchAddiStatusList == null)
            {
                _MatchAddiStatusList = CommonFunction.QueryAllMatchAdditionalStatus();
            }
            return _MatchAddiStatusList;
        }

        private string GetMatchStatus(MatchStatusEnum matchStatus, MatchAdditionalStatusEnum matchAddiStatus)
        {
            string strStatus = string.Empty;
            IList<MatchStatusInfo> matchStatusList = QueryMatchStatus();
            var status = matchStatusList.Where(r => r.MatchStatus == (int)matchStatus).FirstOrDefault();
            var matchAddiStatusList = QueryMatchAddiStatus();
            var addiStatus = matchAddiStatusList.Where(r => r.MatchStatus == (int)matchAddiStatus).FirstOrDefault();

            if (status != null)
                strStatus = status.MatchStatusName;

            switch (matchStatus)
            {
                case MatchStatusEnum.NotActivated:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.Activated:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        //strStatus = addiStatus.MatchStatusName;
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛前暂停";
                        else
                            strStatus = "赛前封盘";
                    }
                    break;
                case MatchStatusEnum.InMatching:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        //strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛中暂停";
                        else
                            strStatus = "赛中封盘";
                    }
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        //strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛中暂停";
                        else
                            strStatus = "赛中封盘";
                    }
                    break;
                case MatchStatusEnum.SecHalfStarted:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛中暂停";
                        else
                            strStatus = "赛中封盘";
                    }
                    break;
                default:
                    break;
            }

            return strStatus;
        }

        /// <summary>
        /// 加载Grid数据
        /// </summary>
        private void LoadGridData()
        {
            DateTime? beginDates, endDates;
            if (startDate.Value.HasValue)
                beginDates = startDate.Value.Value;
            else
                beginDates = null;
            if (endDate.Value.HasValue)
                endDates = endDate.Value.Value.AddDays(1);
            else
                endDates = null;

            int? eventItem = null;
            int? eventZone = null;
            if (ddlEventItem.SelectedIndex >= 0)
                eventItem = Convert.ToInt32(ddlEventItem.SelectedValue);
            if (ddlEventZone.SelectedIndex >= 0)
                eventZone = Convert.ToInt32(ddlEventZone.SelectedValue);

            var eventName = ddlEvent.SelectedIndex > 0 ? ddlEvent.SelectedItem.Text : string.Empty;
            var queryDS = MatchManager.QueryMatchByParam(txtMatchName.Text,
                eventItem, eventZone, eventName, beginDates, endDates);
            var matchList = from m in queryDS.Match_List
                            where (m.STATUS == (int)MatchStatusEnum.Activated || m.STATUS == (int)MatchStatusEnum.InMatching
                            || m.STATUS == (int)MatchStatusEnum.HalfTimeFinished || m.STATUS == (int)MatchStatusEnum.FullTimeFinished
                            || m.STATUS == (int)MatchStatusEnum.SecHalfStarted
                            || m.STATUS == (int)MatchStatusEnum.FinishedCalculation)
                            select m;
            var dt = new DsMatchList.Match_ListDataTable();
            foreach (var row in matchList)
                dt.ImportRow(row);
            pageNavigator.databinds(dt, gridData);
        }
        #endregion

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void ddlEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventItem.SelectedIndex == -1)
                LoadEventZone(null);
            else
                LoadEventZone(Convert.ToInt32(ddlEventItem.SelectedValue));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            var bindRow = (DsMatchList.Match_ListRow)((DataRowView)e.Row.DataItem).Row;
            if (bindRow == null)
                return;

            var lblStartDate = e.Row.FindControl("lblStartDate") as Label;
            var lblFreezeDate = e.Row.FindControl("lblFreezeDate") as Label;
            var lblStatus = e.Row.FindControl("lblStatus") as Label;
            var lblMatchDate = e.Row.FindControl("lblMatchDate") as Label;

            var timeSpanToBegin = (new TimeSpan(DateTime.Now.Ticks)).Subtract(new TimeSpan(bindRow.STARTDATE.Ticks)).Duration();
            var matchToBeginTimeFormat = string.Format("距离比赛开始还有:{0}天{1}小时{2}分{3}秒", timeSpanToBegin.Days, timeSpanToBegin.Hours, timeSpanToBegin.Minutes, timeSpanToBegin.Seconds);
            var timeSpanBeging = (new TimeSpan(bindRow.STARTDATE.Ticks)).Subtract(new TimeSpan(DateTime.Now.Ticks)).Duration();
            var timeSpanBegingFormat = string.Format("比赛已开始:{0}天{1}小时{2}分{3}秒", timeSpanBeging.Days, timeSpanBeging.Hours, timeSpanBeging.Minutes, timeSpanBeging.Seconds);
            lblMatchDate.Text = (MatchStatusEnum)bindRow.STATUS == MatchStatusEnum.Activated ? matchToBeginTimeFormat : timeSpanBegingFormat;

            lblStartDate.Text = UtilityHelper.DateToDateAndTimeStr(bindRow.STARTDATE);
            lblFreezeDate.Text = UtilityHelper.DateToDateAndTimeStr(bindRow.AUTO_FREEZE_DATE);
            lblStatus.Text = GetMatchStatus((MatchStatusEnum)bindRow.STATUS, (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS);

            var btnActivated = e.Row.FindControl("btnActivated") as LinkButton;
            var btnEditStartDate = e.Row.FindControl("btnEditStartDate") as LinkButton;
            var btnMatchHalfEnd = e.Row.FindControl("btnMatchHalfEnd") as LinkButton;
            var btnMatchFullEnd = e.Row.FindControl("btnMatchFullEnd") as LinkButton;
            var btnFrezz = e.Row.FindControl("btnFrezz") as LinkButton;
            var btnClearMarket = e.Row.FindControl("btnClearMarket") as LinkButton;
            var btnRecordScore = e.Row.FindControl("btnRecordScore") as LinkButton;
            var btnStartMatch = e.Row.FindControl("btnStartMatch") as LinkButton;
            var btnHandicapManange = e.Row.FindControl("btnHandicapManange") as LinkButton;
            var btnMatchSecHalfStart = e.Row.FindControl("btnMatchSecHalfStart") as LinkButton;
            btnHandicapManange.CommandName = _CommandHandicap;
            btnHandicapManange.CommandArgument = bindRow.MATCH_ID.ToString();

            OperatorButtons = new LinkButton[_CommandNumber] { btnActivated, btnEditStartDate, btnMatchHalfEnd, btnMatchSecHalfStart, btnMatchFullEnd, btnFrezz, btnClearMarket, btnRecordScore, btnStartMatch };
            foreach (var b in OperatorButtons)
                b.Visible = false;
            //根据当前比赛状态，显示对应控制功能
            var addtionalStatus = (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS;
            switch ((MatchStatusEnum)bindRow.STATUS)
            {
                case MatchStatusEnum.Activated:
                    if (addtionalStatus == MatchAdditionalStatusEnum.Normal)
                    {
                        btnEditStartDate.Visible = true;
                        btnStartMatch.Visible = true;
                        btnFrezz.Visible = true;
                        btnActivated.Text = "激活";
                    }
                    else if (addtionalStatus == MatchAdditionalStatusEnum.FreezingMatch)
                    {
                        btnActivated.Visible = true;
                        btnClearMarket.Visible = true;
                        btnActivated.Text = _CancleFreezed;
                    }
                    break;
                case MatchStatusEnum.InMatching:
                    btnRecordScore.Visible = true;
                    if (addtionalStatus == MatchAdditionalStatusEnum.Normal)
                    {
                        btnFrezz.Visible = true;
                        btnMatchHalfEnd.Visible = true;
                    }
                    else if (addtionalStatus == MatchAdditionalStatusEnum.FreezingMatch)
                    {
                        btnActivated.Visible = true;
                        btnClearMarket.Visible = true;
                        btnActivated.Text = _CancleFreezed;
                    }
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    btnRecordScore.Visible = true;
                    if (addtionalStatus == MatchAdditionalStatusEnum.Normal)
                    {
                        btnMatchSecHalfStart.Visible = true;
                        btnFrezz.Visible = true;
                    }
                    else if (addtionalStatus == MatchAdditionalStatusEnum.FreezingMatch)
                    {
                        btnActivated.Visible = true;
                        btnClearMarket.Visible = true;
                        btnActivated.Text = _CancleFreezed;
                    }
                    break;
                case MatchStatusEnum.SecHalfStarted:
                    btnRecordScore.Visible = true;
                    if (addtionalStatus == MatchAdditionalStatusEnum.Normal)
                    {
                        btnMatchFullEnd.Visible = true;
                        btnFrezz.Visible = true;
                    }
                    else if (addtionalStatus == MatchAdditionalStatusEnum.FreezingMatch)
                    {
                        btnActivated.Visible = true;
                        btnClearMarket.Visible = true;
                        btnActivated.Text = _CancleFreezed;
                    }
                    break;
                case MatchStatusEnum.FullTimeFinished:
                    if (addtionalStatus == MatchAdditionalStatusEnum.Normal)
                    {
                        btnRecordScore.Visible = true;
                    }
                    break;
                case MatchStatusEnum.FinishedCalculation:
                    btnRecordScore.Visible = true;
                    btnHandicapManange.Visible = false;
                    break;
            }

            bool falg;
            for (int i = 0; i < _CommandNumber; i++)
            {
                OperatorButtons[i].CommandName = OperatorButtonCmds[i];
                OperatorButtons[i].CommandArgument = bindRow.MATCH_ID.ToString();
                if (OperatorButtonCmds[i] == _CommandEditStartDate || OperatorButtonCmds[i] == _CommandRecordScore)
                    continue;
                falg = OperatorButtons[i].Visible;
                OperatorButtons[i].Visible = PageAuthorityArr[i] && falg;
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == _CommandActivated)
            {
                Activate(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandEditStartDate)
            {
                EditTime(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandStartMatch)
            {
                StartMatch(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandMatchSecHalfStart)
            {
                MatchSecHalfStart(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandMatchHalfEnd)
            {
                HalfEndMatch(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandMatchFullEnd)
            {
                FullEndMatch(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandFrezz)
            {
                FreezingMatch(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandClearMarket)
            {
                ClearMarket(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandRecordScore)
            {
                RecordScore(Convert.ToInt32(e.CommandArgument));
                return;
            }
            if (e.CommandName == _CommandHandicap)
            {
                HandicapManage(Convert.ToInt32(e.CommandArgument));
                return;
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSMarketTmp.MarketTmpRow)((DataRowView)e.Row.DataItem).Row;
                var ckcHandicap = e.Row.FindControl("ckcHandicap") as CheckBox;
                ckcHandicap.Checked = bindRow.MARKET_STATUS == 0 ? true : false;
                ckcHandicap.CssClass = string.Format("{0}", bindRow.MARKET_TMP_ID);
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string martketTmpIdList = "(";
            foreach (ListItem item in ckbCorrectScore.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }
            foreach (ListItem item in ckbOverUnder.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }
            foreach (ListItem item in ckbAsianHandicap.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }

            foreach (ListItem item in ckbHalfCorrectScore.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }
            foreach (ListItem item in ckbHalfOverUnder.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }
            foreach (ListItem item in ckbHalfAsianHandicap.Items)
            {
                if (item.Selected)
                    martketTmpIdList = martketTmpIdList + item.Value + ",";
            }

            if (martketTmpIdList != "(")
                martketTmpIdList = martketTmpIdList.Substring(0, martketTmpIdList.Length - 1) + ")";
            else
                martketTmpIdList = "(-1)";
            GameControlManager.UpdateMatchMarketStatus(CurrentDataKey, martketTmpIdList);
            GameControlManager.ClearMatchOverUnderMarketByMarketIds(CurrentDataKey, 1, GetOverUnderCancledMarketIds());
            StartTimer();
        }

        private string GetOverUnderCancledMarketIds()
        {
            string marketIds = string.Empty;
            foreach (ListItem item in ckbOverUnder.Items)
            {
                if (item.Selected)
                    marketIds = marketIds + item.Value + ",";
            }
            foreach (ListItem item in ckbHalfOverUnder.Items)
            {
                if (item.Selected)
                    marketIds = marketIds + item.Value + ",";
            }
            return marketIds;
        }

        private void HandicapManage(int editKeyId)
        {
            StopTimer();
            CurrentDataKey = editKeyId;
            loadMatchMarkerData();
            mdlHandicap.Show();
        }

        protected void loadMatchMarkerData()
        {
            if (!CurrentDataKey.HasValue)
                return;
            var matchMarket = GameControlManager.GetMarketByMatchId(CurrentDataKey);
            LoadSingleMarketTemplate(ckbCorrectScore, matchMarket, BetTypeEnum.CorrectScore, ckcCorrectAll,false);
            LoadSingleMarketTemplate(ckbOverUnder, matchMarket, BetTypeEnum.OverUnderGoal, ckcOverUnderAll,false);
            LoadSingleMarketTemplate(ckbAsianHandicap, matchMarket, BetTypeEnum.AsianHandicap, ckcAsianHandicapAll,false);
            LoadSingleMarketTemplate(ckbHalfCorrectScore, matchMarket, BetTypeEnum.CorrectScore, ckcCorrectAll, true);
            LoadSingleMarketTemplate(ckbHalfOverUnder, matchMarket, BetTypeEnum.OverUnderGoal, ckcOverUnderAll, true);
            LoadSingleMarketTemplate(ckbHalfAsianHandicap, matchMarket, BetTypeEnum.AsianHandicap, ckcAsianHandicapAll, true);
            mdlHandicap.Show();
        }

        private void LoadSingleMarketTemplate(CheckBoxList ckbTemp, DSMarketTmp dsTemplate, BetTypeEnum betType,System.Web.UI.HtmlControls.HtmlInputCheckBox ckc,bool bIsHalfMatch)
        {
            ckbTemp.DataValueField = "MARKET_TMP_ID";
            ckbTemp.DataTextField = "MARKET_TMP_NAME";
            int iMarketTmpType;
            if (bIsHalfMatch)
                iMarketTmpType = 0;
            else
                iMarketTmpType = 1;
            var curData = dsTemplate.MarketTmp.Where(r => r.BET_TYPE_ID == (int)betType
                                                          && r.MARKET_TMP_TYPE == iMarketTmpType);
            
            ckbTemp.DataSource = curData;
            ckbTemp.DataBind();
            int checkCount = 0, unCheckCount = 0;
            
            foreach (ListItem item in ckbTemp.Items)
            {
                if (string.IsNullOrEmpty(item.Value))
                    continue;
                var tempMarkets = dsTemplate.MarketTmp.Where(r => r.MARKET_TMP_ID == Convert.ToInt32(item.Value) && r.MARKET_STATUS == 0).ToArray();
                item.Selected = tempMarkets.Length > 0;
                if (item.Selected)
                    checkCount++;
                else
                    unCheckCount++;
            }
            ckc.Checked = checkCount == ckbTemp.Items.Count ? true : false;
            
        }

        private void MatchSecHalfStart(int editKeyId)
        {
            try
            {
                MatchManager.SecHalfStartMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 激活比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void Activate(int editKeyId)
        {
            try
            {
                MatchManager.ActivateMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 开始比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void StartMatch(int editKeyId)
        {
            try
            {
                MatchManager.StartMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 暂停比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        [Obsolete]
        private void Suspend(int editKeyId)
        {
            try
            {
                MatchManager.SuspendMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 终止比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        [Obsolete]
        private void Abort(int editKeyId)
        {
            try
            {
                MatchManager.AbortMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 编辑时间
        /// </summary>
        /// <param name="editKeyId"></param>
        private void EditTime(int editKeyId)
        {
            StopTimer();
            var dsMatchInfo = MatchManager.QueryMatchIncMarketById(editKeyId);
            if (dsMatchInfo.TB_MATCH.Rows.Count == 0)
                return;
            var curMatch = dsMatchInfo.TB_MATCH[0];
            txtBeginDate.Value = curMatch.STARTDATE;
            txtBeginTime.Text = UtilityHelper.TimeToStr(curMatch.STARTDATE);
            if (!curMatch.IsENDDATENull())
            {
                txtEndDate.Value = curMatch.ENDDATE;
                txtEndTime.Text = UtilityHelper.TimeToStr(curMatch.ENDDATE);
            }
            else
            {
                txtEndDate.Value = null;
                txtEndTime.Text = string.Empty;
            }
            txtFreezeDate.Value = curMatch.AUTO_FREEZE_DATE;
            txtFreezeTime.Text = UtilityHelper.TimeToStr(curMatch.AUTO_FREEZE_DATE);
            
            CurrentDataKey = editKeyId;
            mdlPopup.Show();
        }

        /// <summary>
        /// 保存比赛时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveMatchDate_Click(object sender, EventArgs e)
        {
            try
            {
                string beginDate = txtBeginDate.TextField.Text + txtBeginTime.Text;
                string endDate = txtEndDate.TextField.Text + txtEndTime.Text;
                string freezeDate = txtFreezeDate.TextField.Text + txtFreezeTime.Text;
                if (((beginDate.CompareTo(endDate) >= 0 || freezeDate.CompareTo(endDate) >= 0) && !string.IsNullOrEmpty(endDate))
                    || freezeDate.CompareTo(beginDate) >= 0)
                {
                    PageHelper.ShowMessage(this, "请检查日期区间，确认开始时间小于结束时间!");
                    return;
                }

                var startDates = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtBeginDate.Value), txtBeginTime.Text);
                DateTime endDates;
                if (!string.IsNullOrEmpty(endDate))
                    endDates = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtEndDate.Value), txtEndTime.Text);
                else
                    endDates = DateTime.MinValue;
                var freezeDates = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtFreezeDate.Value), txtFreezeTime.Text);
                if (CurrentDataKey.HasValue)
                {
                    MatchManager.ModifyMatchTime(CurrentDataKey.Value, startDates, endDates, freezeDates, CurrentUserId);
                    LoadGridData();
                }
                StartTimer();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 半场结束比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void HalfEndMatch(int editKeyId)
        {
            try
            {
                MatchManager.HalfEndMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page,ex.Message);
            }
        }

        /// <summary>
        /// 全场结束比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void FullEndMatch(int editKeyId)
        {
            try
            {
                MatchManager.FullEndMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        /// <summary>
        /// 封盘比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void FreezingMatch(int editKeyId)
        {
            try
            {
                MatchManager.FreezingMatch(editKeyId, CurrentUserId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        private void SetRecordScoreStatus(MatchStatusEnum matchStatus)
        {
            TextBox[] txtArr = new TextBox[] { txtHomeSecHalfScore, txtGuestSecHalfScore, txtHomeOverTimeScore, txtGuestOverTimeScore, txtHomePointScore, txtGuestPointScore };
            if (matchStatus == MatchStatusEnum.HalfTimeFinished)
            {
                foreach (var txt in txtArr)
                    txt.Enabled = false;
            }
            else
            {
                foreach (var txt in txtArr)
                    txt.Enabled = true;
            }

        }

        /// <summary>
        /// 录入比分
        /// </summary>
        /// <param name="editKeyId"></param>
        private void RecordScore(int editKeyId)
        {
            StopTimer();
            var matchList = MatchManager.QueryMatchIncMarketById(editKeyId);
            if (matchList == null)
                return;
            
            var curMatch = matchList.TB_MATCH[0];
            //SetRecordScoreStatus((MatchStatusEnum)curMatch.STATUS);
            txtHomeHalfScore.Text = curMatch.IsHOME_FIR_HALF_SCORENull() || curMatch.HOME_FIR_HALF_SCORE == -1 ? string.Empty : curMatch.HOME_FIR_HALF_SCORE.ToString();
            txtGuestHalfScore.Text = curMatch.IsGUEST_FIR_HALF_SCORENull() || curMatch.GUEST_FIR_HALF_SCORE == -1 ? string.Empty : curMatch.GUEST_FIR_HALF_SCORE.ToString();
            txtHomeSecHalfScore.Text = curMatch.IsHOME_SEC_HALF_SCORENull() || curMatch.HOME_SEC_HALF_SCORE == -1 ? string.Empty : curMatch.HOME_SEC_HALF_SCORE.ToString();
            txtGuestSecHalfScore.Text = curMatch.IsGUEST_SEC_HALF_SCORENull() || curMatch.GUEST_SEC_HALF_SCORE == -1 ? string.Empty : curMatch.GUEST_SEC_HALF_SCORE.ToString();
            txtHomeOverTimeScore.Text = curMatch.IsHOME_OVERTIME_SCORENull() || curMatch.HOME_OVERTIME_SCORE == -1 ? string.Empty : curMatch.HOME_OVERTIME_SCORE.ToString();
            txtGuestOverTimeScore.Text = curMatch.IsGUEST_OVERTIME_SCORENull() || curMatch.GUEST_OVERTIME_SCORE == -1 ? string.Empty : curMatch.GUEST_OVERTIME_SCORE.ToString();
            txtHomePointScore.Text = curMatch.IsHOME_POINT_SCORENull() || curMatch.HOME_POINT_SCORE == -1 ? string.Empty : curMatch.HOME_POINT_SCORE.ToString();
            txtGuestPointScore.Text = curMatch.IsGUEST_POINT_SCORENull() || curMatch.GUEST_POINT_SCORE == -1 ? string.Empty : curMatch.GUEST_POINT_SCORE.ToString();
            txtHomeFullScore.Text = curMatch.IsHOME_FULL_SCORENull() || curMatch.HOME_FULL_SCORE == -1 ? string.Empty : curMatch.HOME_FULL_SCORE.ToString();
            txtGuestFullScore.Text = curMatch.IsGUEST_FULL_SCORENull() || curMatch.GUEST_FULL_SCORE == -1 ? string.Empty : curMatch.GUEST_FULL_SCORE.ToString();
            CurrentDataKey = editKeyId;
            mdlScore.Show();
        }

        /// <summary>
        /// 保存录入比分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveRecordScore_Click(object sender, EventArgs e)
        {
            if (!CurrentDataKey.HasValue)
                return;
            FootBallMatchScore curScore = GetCurrentMatchScore();
            MatchManager.RecordMatchScore((int)CurrentDataKey, curScore);
            LoadGridData();
            StartTimer();
        }

        /// <summary>
        /// 清理市场
        /// </summary>
        /// <param name="editKeyId"></param>
        private void ClearMarket(int editKeyId)
        {
            try
            {
                GameControlManager.ClearMarket(editKeyId,1);
                PageHelper.ShowMessage(this.Page, "清理市场成功!");
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void btnCancleScore_Click(object sender, EventArgs e)
        {
            StartTimer();
            mdlScore.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            StartTimer();
            mdlPopup.Hide();
        }

        private FootBallMatchScore GetCurrentMatchScore()
        {
            FootBallMatchScore score = new FootBallMatchScore();
            if (string.IsNullOrEmpty(txtHomeHalfScore.Text.Trim()))
                score.HomeFirHalfScore = null;
            else
                score.HomeFirHalfScore = Convert.ToInt32(txtHomeHalfScore.Text.Trim());
            if (string.IsNullOrEmpty(txtGuestHalfScore.Text.Trim()))
                score.GuestFirHalfScore = null;
            else
                score.GuestFirHalfScore = Convert.ToInt32(txtGuestHalfScore.Text.Trim());
            if (string.IsNullOrEmpty(txtHomeSecHalfScore.Text.Trim()))
                score.HomeSecHalfScore = null;
            else
                score.HomeSecHalfScore = Convert.ToInt32(txtHomeSecHalfScore.Text.Trim());
            if (string.IsNullOrEmpty(txtGuestSecHalfScore.Text.Trim()))
                score.GuestSecHalfScore = null;
            else
                score.GuestSecHalfScore = Convert.ToInt32(txtGuestSecHalfScore.Text.Trim());
            if (string.IsNullOrEmpty(txtHomeOverTimeScore.Text.Trim()))
                score.HomeOverTimeScore = null;
            else
                score.HomeOverTimeScore = Convert.ToInt32(txtHomeOverTimeScore.Text.Trim());
            if (string.IsNullOrEmpty(txtGuestOverTimeScore.Text.Trim()))
                score.GuestOverTimeScore = null;
            else
                score.GuestOverTimeScore = Convert.ToInt32(txtGuestOverTimeScore.Text.Trim());
            if (string.IsNullOrEmpty(txtHomePointScore.Text.Trim()))
                score.HomePointScore = null;
            else
                score.HomePointScore = Convert.ToInt32(txtHomePointScore.Text.Trim());
            if (string.IsNullOrEmpty(txtGuestPointScore.Text.Trim()))
                score.GuestPointScore = null;
            else
                score.GuestPointScore = Convert.ToInt32(txtGuestPointScore.Text.Trim());
            if (string.IsNullOrEmpty(txtHomeFullScore.Text.Trim()))
                score.HomeFullScore = null;
            else
                score.HomeFullScore = Convert.ToInt32(txtHomeFullScore.Text.Trim());
            if (string.IsNullOrEmpty(txtGuestFullScore.Text.Trim()))
                score.GuestFullScore = null;
            else
                score.GuestFullScore = Convert.ToInt32(txtGuestFullScore.Text.Trim());
            return score;
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            LoadGridData();
            return;
        }

        private void StopTimer()
        {
            foreach (Control timer in this.Master.FindControl("ListPlace").Controls)
            {
                if (timer is Timer)
                    (timer as Timer).Interval = int.MaxValue;
            }
        }

        private void StartTimer()
        {
            tmGameControl.Interval = 5000;
        }
    }
}