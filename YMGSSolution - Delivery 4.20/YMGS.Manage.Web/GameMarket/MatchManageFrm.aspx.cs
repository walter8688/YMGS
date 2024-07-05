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

namespace YMGS.Manage.Web.GameMarket
{
    [TopMenuId(FunctionIdList.GameMarketManagement.GameMarketManageModule)]
    [LeftMenuId(FunctionIdList.GameMarketManagement.GameManagePage)]
    public partial class MatchManageFrm : BasePage
    {
        private const string _EditDataKey = "EditDataKey";
        private const string _EditCommandName = "Update";
        private const string _DeleteCommandName = "Del";
        private const string _SuspendCommandName = "Suspend";
        private const string _ActivateCommandName = "Activate";
        private const string _AbortCommandName = "Abort";
        private const string _EditMatchTimeCommandName = "EditTime";
        private const string _SaveAsCommandName = "SaveAs";
        private const string _RecommendMatchCommandName = "Recommend";
        private const string _NotRecommendMatchCommandName = "NotRecommend";

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameMarketManagement.GameManagePage;
            }
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(MatchManageFrm), "GameMarket").AbsoluteUri;
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
                btnNew.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.AddGame);
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
                blankRow.EVENTZONE_NAME_EN = string.Empty;
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
            pageNavigator.databinds(queryDS.Match_List, gridData);
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
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛前暂停";
                        else
                            strStatus = "赛前封盘";
                        //strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.InMatching:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        if (matchAddiStatus == MatchAdditionalStatusEnum.Suspended)
                            strStatus = "赛中暂停";
                        else
                            strStatus = "赛中封盘";
                        //strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                    }
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    if(matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        //strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
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

            lblStatus.Text = GetMatchStatus((MatchStatusEnum)bindRow.STATUS, (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS);

            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            LinkButton btnActivate = (LinkButton)e.Row.FindControl("btnActivate");
            LinkButton btnSuspend = (LinkButton)e.Row.FindControl("btnSuspend");
            LinkButton btnEditTime = (LinkButton)e.Row.FindControl("btnEditTime");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            LinkButton btnStop = (LinkButton)e.Row.FindControl("btnStop");
            LinkButton btnSaveAs = (LinkButton)e.Row.FindControl("btnSaveAs");
            LinkButton btnRecommend = (LinkButton)e.Row.FindControl("btnRecommend");
            LinkButton btnNotRecommend = (LinkButton)e.Row.FindControl("btnNotRecommend");

            btnEdit.CommandName = _EditCommandName;
            btnEdit.CommandArgument = bindRow.MATCH_ID.ToString();
            btnActivate.CommandName = _ActivateCommandName;
            btnActivate.CommandArgument = bindRow.MATCH_ID.ToString();
            btnSuspend.CommandName = _SuspendCommandName;
            btnSuspend.CommandArgument = bindRow.MATCH_ID.ToString();
            btnEditTime.CommandName = _EditMatchTimeCommandName;
            btnEditTime.CommandArgument = bindRow.MATCH_ID.ToString();
            btnDelete.CommandName = _DeleteCommandName;
            btnDelete.CommandArgument = bindRow.MATCH_ID.ToString();
            btnStop.CommandName = _AbortCommandName;
            btnStop.CommandArgument = bindRow.MATCH_ID.ToString();
            btnSaveAs.CommandName = _SaveAsCommandName;
            btnSaveAs.CommandArgument = bindRow.MATCH_ID.ToString();
            btnRecommend.CommandName = _RecommendMatchCommandName;
            btnRecommend.CommandArgument = bindRow.MATCH_ID.ToString();
            btnNotRecommend.CommandName = _NotRecommendMatchCommandName;
            btnNotRecommend.CommandArgument = bindRow.MATCH_ID.ToString();

            //btnActivate.OnClientClick = "if(window.confirm('确定要激活比赛吗?')) return true;else return false;";
            //btnSuspend.OnClientClick = "if(window.confirm('确定要暂停比赛吗?')) return true;else return false;";
            btnDelete.OnClientClick = "if(window.confirm('确定要删除比赛吗?')) return true;else return false;";
            btnStop.OnClientClick = "if(window.confirm('确定要终止比赛吗?')) return true;else return false;";
            //btnRecommend.OnClientClick = "if(window.confirm('确定要推荐比赛吗?')) return true;else return false;";
            //btnNotRecommend.OnClientClick = "if(window.confirm('确定要取消推荐比赛吗?')) return true;else return false;";

            btnEdit.Visible = true;
            btnSaveAs.Visible = true;
            btnActivate.Visible = false;
            btnSuspend.Visible = false;
            btnEditTime.Visible = false;
            btnDelete.Visible = false;
            btnStop.Visible = false;
            btnNotRecommend.Visible = false;
            btnRecommend.Visible = false;

            MatchStatusEnum matchStatusEnum = (MatchStatusEnum)bindRow.STATUS;
            MatchAdditionalStatusEnum matchAddiStatusEnum = (MatchAdditionalStatusEnum)bindRow.ADDITIONALSTATUS;
            switch(matchStatusEnum)
            {
                case MatchStatusEnum.NotActivated:
                    btnActivate.Visible = true;
                    btnDelete.Visible = true;
                    btnEditTime.Visible = true;
                    btnActivate.Text = "激活";
                    break;
                case MatchStatusEnum.Activated:
                    if (matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal)
                    {
                        btnSuspend.Visible = true;
                    }
                    else
                    {
                        btnActivate.Visible = true;
                        if (matchAddiStatusEnum == MatchAdditionalStatusEnum.Suspended)
                        {
                            btnActivate.Text = "取消暂停";
                            //btnActivate.OnClientClick = "if(window.confirm('确定要取消暂停比赛吗?')) return true;else return false;";
                        }
                        else if (matchAddiStatusEnum == MatchAdditionalStatusEnum.FreezingMatch)
                        {
                            btnActivate.Text = "取消封盘";
                            //btnActivate.OnClientClick = "if(window.confirm('确定要取消封盘比赛吗?')) return true;else return false;";
                        }
                    }
                    btnEditTime.Visible = true;
                    btnStop.Visible = true;
                    if (bindRow.RECOMMENDMATCH)
                        btnNotRecommend.Visible = true;
                    else
                        btnRecommend.Visible = true;
                    break;
                case MatchStatusEnum.Abort:
                    break;
                case MatchStatusEnum.FinishedCalculation:
                    break;
                case MatchStatusEnum.InMatching:
                    if (matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal)
                    {
                        btnSuspend.Visible = true;
                    }
                    else
                    {
                        btnActivate.Visible = true;
                        if (matchAddiStatusEnum == MatchAdditionalStatusEnum.Suspended)
                        {
                            btnActivate.Text = "取消暂停";
                            //btnActivate.OnClientClick = "if(window.confirm('确定要取消暂停比赛吗?')) return true;else return false;";
                        }
                        else if (matchAddiStatusEnum == MatchAdditionalStatusEnum.FreezingMatch)
                        {
                            btnActivate.Text = "取消封盘";
                            //btnActivate.OnClientClick = "if(window.confirm('确定要取消封盘比赛吗?')) return true;else return false;";
                        }
                    }
                    btnStop.Visible = true;
                    if (bindRow.RECOMMENDMATCH)
                        btnNotRecommend.Visible = true;
                    else
                        btnRecommend.Visible = true;
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    if (matchAddiStatusEnum == MatchAdditionalStatusEnum.Normal)
                    {
                        btnSuspend.Visible = true;
                    }
                    else
                    {
                        btnActivate.Visible = true;
                    }
                    if (bindRow.RECOMMENDMATCH)
                        btnNotRecommend.Visible = true;
                    else
                        btnRecommend.Visible = true;
                    break;
                case MatchStatusEnum.FullTimeFinished:
                    break;
                default:
                    break;
            }    

            //增加各按钮的权限验证
            btnSaveAs.Visible = btnSaveAs.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.SaveAsChampionGame);
            btnActivate.Visible = btnActivate.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.SuspendChampionGame);
            btnSuspend.Visible = btnSuspend.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.SuspendChampionGame);
            btnEditTime.Visible = btnEditTime.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.EditChampionGame);
            btnDelete.Visible = btnDelete.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.DeleteChampionGame);
            btnStop.Visible = btnStop.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.StopChampionGame);
            btnNotRecommend.Visible = btnNotRecommend.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.RecommendGame);
            btnRecommend.Visible = btnRecommend.Visible && MySession.Accessable(FunctionIdList.GameMarketManagement.RecommendGame);
        }

        protected void ddlEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventItem.SelectedIndex == -1)
                LoadEventZone(null);
            else
                LoadEventZone(Convert.ToInt32(ddlEventItem.SelectedValue));
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            EditDetailInfo(null);
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //详细信息
            if (e.CommandName == _EditCommandName)
            {
                EditDetailInfo(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //另存
            if (e.CommandName == _SaveAsCommandName)
            {
                SaveAs(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //激活
            if (e.CommandName == _ActivateCommandName)
            {
                Activate(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //暂停
            if (e.CommandName == _SuspendCommandName)
            {
                Suspend(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //编辑时间
            if (e.CommandName == _EditMatchTimeCommandName)
            {
                EditTime(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //删除
            if (e.CommandName == _DeleteCommandName)
            {
                Delete(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //终止
            if (e.CommandName == _AbortCommandName)
            {
                Abort(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //推荐比赛
            if (e.CommandName == _RecommendMatchCommandName)
            {
                RecommendMatch(Convert.ToInt32(e.CommandArgument),true);
                return;
            }

            //取消推荐比赛
            if (e.CommandName == _NotRecommendMatchCommandName)
            {
                RecommendMatch(Convert.ToInt32(e.CommandArgument), false);
                return;
            }
        }

        private void EditDetailInfo(int? editKeyId)
        {
            if (!editKeyId.HasValue)
                Response.Redirect(GameDetailFrm.Url(-1, UserOperateTypeEnum.AddData));
            else
                Response.Redirect(GameDetailFrm.Url(editKeyId.Value, UserOperateTypeEnum.EditData));
        }


        private void SaveAs(int editKeyId)
        {
            Response.Redirect(GameDetailFrm.Url(editKeyId, UserOperateTypeEnum.SaveAs));
        }

        private void Activate(int editKeyId)
        {
            try
            {
                MatchManager.ActivateMatch(editKeyId, CurrentUserId);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        private void Suspend(int editKeyId)
        {
            try
            {
                MatchManager.SuspendMatch(editKeyId, CurrentUserId);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        private void EditTime(int editKeyId)
        {
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

            CurEditDataId = editKeyId;
            mdlPopup.Show();
        }

        private void Abort(int editKeyId)
        {
            try
            {
                MatchManager.AbortMatch(editKeyId, CurrentUserId);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        private void Delete(int editKeyId)
        {
            try
            {
                MatchManager.DeleteMatch(editKeyId);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 推荐比赛
        /// </summary>
        /// <param name="editKeyId"></param>
        private void RecommendMatch(int editKeyId,bool bIsRecommend)
        {
            try
            {
                MatchManager.RecommendOrCancelMatch(editKeyId, CurrentUserId, bIsRecommend);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
        
        protected void btSave_Click(object sender, EventArgs e)
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
                if (CurEditDataId.HasValue)
                {
                    MatchManager.ModifyMatchTime(CurEditDataId.Value, startDates, endDates, freezeDates, CurrentUserId);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
    }
}