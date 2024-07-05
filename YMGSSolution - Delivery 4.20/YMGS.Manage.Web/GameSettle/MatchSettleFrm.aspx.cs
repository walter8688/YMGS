using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.GameMarket;
using YMGS.Data.Entity;
using YMGS.Business.EventManage;
using YMGS.Business.SystemSetting;
using YMGS.Data.Presentation;
using YMGS.Framework;
using YMGS.Business.GameSettle;
using YMGS.Manage.Web.Controls;

namespace YMGS.Manage.Web.GameSettle
{
    [TopMenuId(FunctionIdList.GameSettle.GameSettleManageModule)]
    [LeftMenuId(FunctionIdList.GameSettle.GameSettlePage)]
    public partial class MatchSettleFrm : BasePage
    {
        private const string _EditDataKey = "EditDataKey";
        private const string _FirstHalfCalcCommandName = "FirstHalfCalc";
        private const string _FullCalcCommandName = "FullCalc";
        private const string _ReFullCalcCommandName = "ReFullCalc";
        private const string _ReHalfCalcCommandName = "ReHalfCalc";
        private ASPProgressBar _AspProgressBar = null;


        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameSettle.GameSettlePage;
            }
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(MatchSettleFrm), "GameSettle").AbsoluteUri;
        }

        private int? CurEditDataId
        {
            get
            {
                if (ViewState[_EditDataKey] == null)
                    return null;
                else
                    return Convert.ToInt32(ViewState[_EditDataKey]);
            }
            set
            {
                ViewState[_EditDataKey] = value;
            }
        }

        private int CurrentUserId
        {
            get
            {
                return MySession.CurrentUser.ACCOUNT[0].USER_ID;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventItem();
                LoadEventZone(null);
                var curDate = PageHelper.QueryCurSysDateTime();
                startDate.Value = curDate.AddDays(-30);
                LoadData();
            }
        }

        #region 加载页面初始化数据

        /// <summary>
        /// 加载赛事项目
        /// </summary>
        private void LoadEventItem()
        {
            var eventItemList = EventZoneManager.QueryEventItem();
            var blankRow = eventItemList.TB_EVENT_ITEM.NewTB_EVENT_ITEMRow();
            blankRow.EventItem_ID = -1;
            blankRow.EventItem_Name = string.Empty;
            blankRow.EventItem_Name_En = string.Empty;
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

        /// <summary>
        /// 加载比赛数据
        /// </summary>
        private void LoadData()
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
            //queryDS.Match_List.DefaultView.RowFilter = "STATUS IN(3,4,5)";
            var dt = queryDS.Match_List.Clone();
            foreach (var row in queryDS.Match_List)
            {
                if (row.STATUS == (int)MatchStatusEnum.HalfTimeFinished ||
                    row.STATUS == (int)MatchStatusEnum.SecHalfStarted ||
                    row.STATUS == (int)MatchStatusEnum.FullTimeFinished ||
                     row.STATUS == (int)MatchStatusEnum.FinishedCalculation)
                {
                    dt.ImportRow(row);
                }
            }
            pageNavigator.databinds(dt, gridData);
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

            switch(matchStatus)
            {
                case MatchStatusEnum.NotActivated:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.Activated:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.InMatching:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                    }
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                    }
                    break;
                default:
                    break;
            }

            return strStatus;
        }

        #endregion

        void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            var bindRow = (DsMatchList.Match_ListRow)((DataRowView)e.Row.DataItem).Row;
            if (bindRow == null)
                return;

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblStartDate = (Label)e.Row.FindControl("lblStartDate");
            Label lblFreezeDate = (Label)e.Row.FindControl("lblFreezeDate");

            lblStartDate.Text = UtilityHelper.DateToDateAndTimeStr(bindRow.STARTDATE);
            lblFreezeDate.Text = UtilityHelper.DateToDateAndTimeStr(bindRow.AUTO_FREEZE_DATE);

            //lblStatus.Text = GetMatchStatus((MatchStatusEnum)bindRow.STATUS, (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS);
            if (bindRow.IsSETTLE_STATUSNull())
                lblStatus.Text = "未结算";
            else
                lblStatus.Text = (MatchSettleStatus)bindRow.SETTLE_STATUS == MatchSettleStatus.UnSettle ? "未结算" : ((MatchSettleStatus)bindRow.SETTLE_STATUS == MatchSettleStatus.HalfSettled ? "半场已结算" : "全场已结算");

            LinkButton btnFirstHalfCalc = (LinkButton)e.Row.FindControl("btnFirstHalfCalc");
            LinkButton btnFullCalc = (LinkButton)e.Row.FindControl("btnFullCalc");
            LinkButton btnReFullCalc = (LinkButton)e.Row.FindControl("btnReFullCalc");
            LinkButton btnReHalfCalc = (LinkButton)e.Row.FindControl("btnReHalfCalc");

            btnFirstHalfCalc.CommandName = _FirstHalfCalcCommandName;
            btnFirstHalfCalc.CommandArgument = bindRow.MATCH_ID.ToString();
            btnFullCalc.CommandName = _FullCalcCommandName;
            btnFullCalc.CommandArgument = bindRow.MATCH_ID.ToString();
            btnReFullCalc.CommandName = _ReFullCalcCommandName;
            btnReFullCalc.CommandArgument = bindRow.MATCH_ID.ToString();
            btnReHalfCalc.CommandName = _ReHalfCalcCommandName;
            btnReHalfCalc.CommandArgument = bindRow.MATCH_ID.ToString();

            //btnFirstHalfCalc.OnClientClick = "if(window.confirm('确定要进行半场比赛结算吗?')) return true;else return false;";
            //btnFullCalc.OnClientClick = "if(window.confirm('确定要进行全场比赛结算吗?')) return true;else return false;";
            btnReFullCalc.OnClientClick = "if(window.confirm('确定要重新结算吗?')) return true;else return false;";
            btnReHalfCalc.OnClientClick = "if(window.confirm('确定要半场重新结算吗?')) return true;else return false;";

            btnFirstHalfCalc.Visible = false;
            btnFullCalc.Visible = false;
            btnReFullCalc.Visible = false;
            btnReHalfCalc.Visible = false;

            MatchStatusEnum matchStatusEnum = (MatchStatusEnum)bindRow.STATUS;
            MatchAdditionalStatusEnum matchAddiStatusEnum = (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS;

            MatchSettleStatus settleStatus = MatchSettleStatus.UnSettle;
            if(!bindRow.IsSETTLE_STATUSNull())
                settleStatus = (MatchSettleStatus)bindRow.SETTLE_STATUS;
            switch(matchStatusEnum)
            {
                case MatchStatusEnum.HalfTimeFinished:
                    btnFirstHalfCalc.Visible = true && (matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal);
                    btnReHalfCalc.Visible = settleStatus == MatchSettleStatus.HalfSettled;
                    break;
                case MatchStatusEnum.SecHalfStarted:
                    btnFirstHalfCalc.Visible = true && (matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal);
                    btnReHalfCalc.Visible = settleStatus == MatchSettleStatus.HalfSettled;
                    break;
                case MatchStatusEnum.FullTimeFinished:
                    btnFullCalc.Visible = true && matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal;
                    btnReHalfCalc.Visible = settleStatus == MatchSettleStatus.HalfSettled;
                    break;
                case MatchStatusEnum.FinishedCalculation:
                    btnReFullCalc.Visible = true;
                    break;
                default:
                    break;
            }
            if (!bindRow.IsSETTLE_STATUSNull())
            {
                if ((MatchSettleStatus)bindRow.SETTLE_STATUS == MatchSettleStatus.HalfSettled)
                    btnFirstHalfCalc.Visible = false;
            }

            btnFirstHalfCalc.Visible = btnFirstHalfCalc.Visible && MySession.Accessable(FunctionIdList.GameSettle.SettleGame);
            btnFullCalc.Visible = btnFullCalc.Visible && MySession.Accessable(FunctionIdList.GameSettle.SettleGame);
            btnReFullCalc.Visible = btnReFullCalc.Visible && MySession.Accessable(FunctionIdList.GameSettle.ReSettleGame);
            btnReHalfCalc.Visible = btnReHalfCalc.Visible && MySession.Accessable(FunctionIdList.GameSettle.ReSettleGame);
        }

        protected void ddlEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventItem.SelectedIndex == -1)
                LoadEventZone(null);
            else
                LoadEventZone(Convert.ToInt32(ddlEventItem.SelectedValue));
        }


        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == _FirstHalfCalcCommandName)
            {
                FirstHalfCalc(Convert.ToInt32(e.CommandArgument));
                return;
            }

            if (e.CommandName == _FullCalcCommandName)
            {
                FullCalc(Convert.ToInt32(e.CommandArgument));
                return;
            }

            if (e.CommandName == _ReFullCalcCommandName)
            {
                ReFullCalc(Convert.ToInt32(e.CommandArgument));
                return;
            }

            if (e.CommandName == _ReHalfCalcCommandName)
            {
                ReHalfCalc(Convert.ToInt32(e.CommandArgument));
                return;
            }
        }

        private void InitProgressBar()
        {
            _AspProgressBar = new ASPProgressBar(this, "GameSettle");
            _AspProgressBar.Init();
            _AspProgressBar.Show();
        }

        private void UpdateProgress(string strProgressText, int value)
        {
            if (_AspProgressBar != null)
            {
                _AspProgressBar.UpdateProgress(value, strProgressText);
            }
        }        

        private void FirstHalfCalc(int editKeyId)
        {
            try
            {
                string strMessage = GameSettleManager.CalcSportMatchGame(editKeyId, CurrentUserId,
                                    true, InitProgressBar, UpdateProgress,true);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(MatchSettleFrm.Url());
                }
            }
        }

        private void FullCalc(int editKeyId)
        {
            try
            {
                string strMessage = GameSettleManager.CalcSportMatchGame(editKeyId, CurrentUserId,
                                    false, InitProgressBar, UpdateProgress,true);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);                
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(MatchSettleFrm.Url());
                }
            }
        }

        /// <summary>
        /// 重新全场结算
        /// </summary>
        /// <param name="editKeyId"></param>
        private void ReFullCalc(int editKeyId)
        {
            try
            {
                string strMessage = RollbackSettlementManager.RollbackSettlement(
                            editKeyId, 1, 1, InitProgressBar, UpdateProgress);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                    return;
                }

                strMessage = GameSettleManager.CalcSportMatchGame(editKeyId, CurrentUserId,
                                    false, InitProgressBar, UpdateProgress, false);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(MatchSettleFrm.Url());
                }
            }
        }


        /// <summary>
        /// 半场重新结算
        /// </summary>
        /// <param name="editKeyId"></param>
        private void ReHalfCalc(int editKeyId)
        {
            try
            {
                string strMessage = RollbackSettlementManager.RollbackSettlement(
                            editKeyId, 1, 0, InitProgressBar, UpdateProgress);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                    return;
                }

                strMessage = GameSettleManager.CalcSportMatchGame(editKeyId, CurrentUserId,
                                    true, InitProgressBar, UpdateProgress, false);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(MatchSettleFrm.Url());
                }
            }
        }
    }
}