using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Business;
using YMGS.Manage.Web.Common;
using YMGS.Business.EventManage;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;
using YMGS.Data.Presentation;
using YMGS.Business.AssistManage;

namespace YMGS.Manage.Web.EventManage
{
    [TopMenuId(FunctionIdList.EventManagement.EventManageModule)]
    [LeftMenuId(FunctionIdList.EventManagement.EventManagePage)]
    public partial class EventManagePage : BasePage
    {
        private static bool canEdit, canDelete, canSuspend, canStop, canSaveAs;
        private const string _CommandScript = "if(window.confirm('确定要{0}吗?{1}')) return true;else return false;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDrpData();
                LoadGridData();
                BindTreeView();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitPageAccess();
            this.pageNavigator.PageIndexChanged+=new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.EventManagement.EventManagePage;
            }
        }

        private void InitPageAccess()
        {
            this.btnAddEvent.Visible = MySession.Accessable(FunctionIdList.EventManagement.AddEvent);
            canEdit = MySession.Accessable(FunctionIdList.EventManagement.EditEvent);
            canDelete = MySession.Accessable(FunctionIdList.EventManagement.DeleteEvent);
            canSuspend = MySession.Accessable(FunctionIdList.EventManagement.SuspendEvent);
            canStop = MySession.Accessable(FunctionIdList.EventManagement.StopEvent);
            canSaveAs = MySession.Accessable(FunctionIdList.EventManagement.SaveAsEvent);
        }

        #region 页面数据
        private void BindControlData(Control obj, object datasource, string dataTextField, string dataValueField, bool needNullItem)
        {
            if (obj is DropDownList)
            {
                DropDownList drp = obj as DropDownList;
                drp.Items.Clear();
                drp.DataSource = datasource;
                drp.DataTextField = dataTextField;
                drp.DataValueField = dataValueField;
                drp.DataBind();
                if (needNullItem)
                    drp.Items.Insert(0, GetNullValueListItem());
            }
            if (obj is ListBox)
            {
                ListBox ckcList = obj as ListBox;
                ckcList.Items.Clear();
                ckcList.DataSource = datasource;
                ckcList.DataTextField = dataTextField;
                ckcList.DataValueField = dataValueField;
                ckcList.DataBind();
                if (needNullItem)
                    ckcList.Items.Insert(0, GetNullValueListItem());
            }
        }

        private ListItem GetNullValueListItem()
        {
            ListItem item = new ListItem(CommonConstant.DropDownListNullKey, CommonConstant.DropDownListNullValue);
            return item;
        }

        private void LoadDrpData()
        {
            var eventItemDS = EventZoneManager.QueryEventItem();
            var eventStatusDS = CommonFunction.QueryAllEventStatus();
            var paramType1DS = ParamParamManager.QueryParamByType(ParamTypeEnum.ParamEventTeamType);
            var paramType2DS = ParamParamManager.QueryParamByType(ParamTypeEnum.ParamEventTeamTypeSex);
            var paramStatusDS = CommonFunction.QueryAllEventTeamStatus();

            BindControlData(this.drpParamItem, eventItemDS, "EventItem_Name", "EventItem_ID", true);//赛事项目
            BindControlData(this.popDrpEventItem, eventItemDS, "EventItem_Name", "EventItem_ID", false);//PopUp赛事项目
            BindControlData(this.popDrpEventItemTeam, eventItemDS, "EventItem_Name", "EventItem_ID", false);//PopUp赛事项目
            BindControlData(this.drpEventStatus, eventStatusDS, "EventStatusName", "EventStatusID", true);//赛事状态
            BindControlData(this.popDrpEventStatus, eventStatusDS, "EventStatusName", "EventStatusID", false);//PopUp赛事状态
            //PopUp赛事区域
            DSEventZone.TB_EVENT_ZONERow eventZone = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            eventZone.EVENTITEM_ID = Convert.ToInt32(this.popDrpEventItem.SelectedValue);
            eventZone.EVENTZONE_NAME = eventZone.EVENTZONE_NAME_EN = eventZone.EVENTZONE_DESC = "";
            eventZone.PARAM_ZONE_ID = -1;
            BindControlData(this.popDrpEventZone, EventZoneManager.QueryEventZone(eventZone), "EVENTZONE_NAME", "EVENTZONE_ID", false);

            //PopUp类型(国家/职业)
            BindControlData(this.popDrpParamType1, paramType1DS, "PARAM_NAME", "PARAM_ID", false);

            //PopUp类型(男子/女子)
            BindControlData(this.popDrpParamType2, paramType2DS, "PARAM_NAME", "PARAM_ID", false);
        }

        private void LoadGridData()
        {
            DateTime? startDate, endDate;
            int? eventZoneID = drpEventZone.Items.Count == 0 ? CommonConstant.PageErrorCode : Convert.ToInt32(this.drpEventZone.SelectedValue);
            string eventName = txtEventName.Text.Trim();
            string eventNameEn = txtEventNameEn.Text.Trim();
            string eventDesc = txtEventDesc.Text.Trim();
            if (this.calStartDate.Value.HasValue)
                startDate = calStartDate.Value.Value;
            else
                startDate = null;
            if (this.calEndDate.Value.HasValue)
                endDate = calEndDate.Value.Value;
            else
                endDate = null;
            int? status = Convert.ToInt32(drpEventStatus.SelectedValue);
            int eventTeamID = -1;
            string eventTeamName = "";
            var eventTeamList = EventManager.QueryEvent(eventZoneID, eventName, eventNameEn, eventDesc, startDate, endDate, status, eventTeamID, eventTeamName);
            this.pageNavigator.databinds(eventTeamList.Tables[0], gdvEvent);
        }

        private void LoadEventTeamData()
        {
            var eventTeamRow = GetQueryCurrentEventTeam();
            if (eventTeamRow == null)
                return;
            var eventTeamDS = EventTeamManager.QueryEventTeam(eventTeamRow);
            BindControlData(popCkcEventTeamList, eventTeamDS, "EVENT_TEAM_NAME", "EVENT_TEAM_ID", false);
        }

        private DSEventTeam.TB_EVENT_TEAMRow GetQueryCurrentEventTeam()
        {
            if (popDrpEventItem.Items.Count == 0 || popDrpParamType1.Items.Count == 0 || popDrpParamType2.Items.Count == 0)
                return null;
            DSEventTeam.TB_EVENT_TEAMRow eventTeam = new DSEventTeam.TB_EVENT_TEAMDataTable().NewTB_EVENT_TEAMRow();
            eventTeam.EVENT_ITEM_ID = Convert.ToInt32(popDrpEventItem.SelectedValue);
            eventTeam.EVENT_TEAM_NAME = popTxtEventTeamName.Text.Trim();
            eventTeam.EVENT_TEAM_NAME_EN = "";
            eventTeam.EVENT_TEAM_TYPE1 = Convert.ToInt32(popDrpParamType1.SelectedValue);
            eventTeam.EVENT_TEAM_TYPE2 = Convert.ToInt32(popDrpParamType2.SelectedValue);
            eventTeam.STATUS = (int)EventTeamStatusEnum.Activity;
            eventTeam.PARAM_ZONE_ID = string.IsNullOrEmpty(txtZoneID.Text) ? -1 : Convert.ToInt32(txtZoneID.Text);
            return eventTeam;
        }

        private DSEvent.TB_EVENTRow GetCurrentNoQueryEvent()
        {
            DSEvent.TB_EVENTRow row = new DSEvent.TB_EVENTDataTable().NewTB_EVENTRow();
            row.EVENTZONE_ID = Convert.ToInt32(popDrpEventZone.SelectedValue);
            row.EVENT_NAME = popTxtEventName.Text;
            row.EVENT_NAME_EN = popTxtEventNameEn.Text;
            row.EVENT_DESC = popTxtEventDesc.Text;
            if (popCalStartDate.Value.HasValue)
                row.START_DATE = popCalStartDate.Value.Value;
            else
                row.START_DATE = DateTime.Now;
            row.END_DATE = DateTime.MaxValue;
            row.STATUS = (int)EventStatusEnum.UnActivated;
            row.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            row.LAST_UPDATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            return row;
        }

        #endregion

        #region 页面事件
        protected void popBtnQuery_Click(object sender, EventArgs e)
        {
            LoadEventTeamData();
            IList<ListItem> itemList = new List<ListItem>();
            foreach (ListItem item in this.popCkcEventTeamList.Items)
            {
                itemList.Add(item);
            }
            foreach (ListItem item in this.popCkcSelectedEventTeamList.Items)
            {
                if (itemList.Contains<ListItem>(item))
                {
                    this.popCkcEventTeamList.Items.Remove(item);
                }
            }
            mdlPopup.Show();
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var eventRow = GetCurrentNoQueryEvent();
                if (eventRow.END_DATE.CompareTo(DateTime.Now.AddDays(-1)) < 0)
                {
                    PageHelper.ShowMessage(Page, "赛事结束时间不能早于当前时间!");
                    mdlPopup.Show();
                    return;
                }
                //构造赛事关联参赛成员集合
                var eventTeamIDS = string.Empty;
                foreach (ListItem item in this.popCkcSelectedEventTeamList.Items)
                {
                    eventTeamIDS += item.Value + ",";
                }
                if (!string.IsNullOrEmpty(eventTeamIDS))
                    eventTeamIDS = eventTeamIDS.Substring(0, eventTeamIDS.Length - 1);
                //Save的类型
                var buttonCommandType = (sender as Button).CommandName;
                //新增
                if (buttonCommandType == ButtonCommandType.Add.ToString())
                {
                    EventManager.AddEvent(eventRow, eventTeamIDS);
                }
                //编辑
                else if (buttonCommandType == ButtonCommandType.Edit.ToString())
                {
                    eventRow.EVENT_ID = Convert.ToInt32((sender as Button).CommandArgument);
                    EventManager.UpdateEvent(eventRow, eventTeamIDS);
                }
                //赛事另存
                else if (buttonCommandType == ButtonCommandType.SaveAs.ToString())
                {
                    EventManager.AddEvent(eventRow, eventTeamIDS);
                }
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
            
        }
        
        protected void drpParamItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as DropDownList).SelectedIndex == 0)
            {
                drpEventZone.Items.Clear();
                return;
            }
            DSEventZone.TB_EVENT_ZONERow eventZone = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            eventZone.EVENTITEM_ID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            eventZone.EVENTZONE_NAME = eventZone.EVENTZONE_NAME_EN = eventZone.EVENTZONE_DESC = "";
            eventZone.PARAM_ZONE_ID = -1;
            BindControlData(drpEventZone, EventZoneManager.QueryEventZone(eventZone), "EVENTZONE_NAME", "EVENTZONE_ID", false);
        }

        protected void popDrpEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSEventZone.TB_EVENT_ZONERow eventZone = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            eventZone.EVENTITEM_ID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            eventZone.EVENTZONE_NAME = eventZone.EVENTZONE_NAME_EN = eventZone.EVENTZONE_DESC = "";
            BindControlData(popDrpEventZone, EventZoneManager.QueryEventZone(eventZone), "EVENTZONE_NAME", "EVENTZONE_ID", false);
        }

        private void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void btnQueryEvent_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var eventID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                EventManager.DeleteEvent(eventID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page,ex.Message);
            }
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            popDrpEventStatus.Visible = false;
            popCkcSelectedEventTeamList.Items.Clear();
            //新增
            if (sender is Button)
            {
                //初始化PopUp新增区域
                DropDownList[] drpArr = new DropDownList[] { popDrpEventItem, popDrpEventZone, popDrpEventStatus };
                foreach (DropDownList drp in drpArr)
                {
                    if (drp.Items.Count > 0)
                        drp.SelectedIndex = 0;
                }
                popCalStartDate.Value = null;
                popTxtEventName.Text = popTxtEventDesc.Text = popTxtEventNameEn.Text = "";
                btnSave.CommandName = ButtonCommandType.Add.ToString();
            }
            //编辑 || 赛事另存为
            else if (sender is LinkButton)
            {
                
                var argArr = (sender as LinkButton).CommandArgument.Split(',');
                popTxtEventName.Text = argArr[1];
                popTxtEventDesc.Text = argArr[2];
                if (!string.IsNullOrEmpty(argArr[3]))
                    popCalStartDate.Value = UtilityHelper.StrToDate(argArr[3]);
                popDrpEventItem.SelectedValue = argArr[4];
                popDrpEventZone.SelectedValue = argArr[5];
                popDrpEventStatus.SelectedValue = argArr[6];
                popTxtEventNameEn.Text = argArr[7];
                this.btnSave.CommandArgument = argArr[0];
                //赛事另存OR编辑
                this.btnSave.CommandName = (sender as LinkButton).CommandName == ButtonCommandType.SaveAs.ToString() ? ButtonCommandType.SaveAs.ToString() : btnSave.CommandName = ButtonCommandType.Edit.ToString();
                //已关联赛事成员列表
                var eventTeamDS = EventTeamManager.QueryEventTeamByEventID(Convert.ToInt32(argArr[0]));
                BindControlData(popCkcSelectedEventTeamList, eventTeamDS, "EVENT_TEAM_NAME", "EVENT_TEAM_ID", false);
            }
            //参赛成员列表
            popBtnQuery_Click(this, null);
        }

        protected void gdvEvent_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSEventTeamList.DSEventTeamListRow)((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[4].Text = UtilityHelper.DateToStr(DateTime.Parse(bindRow.START_DATE.ToString("yyyy-MM-dd")));

                var eventID = this.gdvEvent.DataKeys[e.Row.RowIndex].Value.ToString();
                var eventName = bindRow.EVENT_NAME;
                var eventNameEn = bindRow.EVENT_NAME_EN;
                var eventDesc = bindRow.EVENT_DESC;
                var startDate = e.Row.Cells[4].Text;
                var eventItemID = (e.Row.FindControl("lblEventItemID") as Label).Text;
                var eventZoneID = (e.Row.FindControl("lblEventZoneID") as Label).Text;
                var eventStatusID = (e.Row.FindControl("lblEventStatus") as Label).Text;

                var hlActivity = (e.Row.FindControl("hlActivity") as LinkButton);
                var hlPause = (e.Row.FindControl("hlPause") as LinkButton);
                var hlAbort = (e.Row.FindControl("hlAbort") as LinkButton);
                var hlDelete = (e.Row.FindControl("hlDelete") as LinkButton);
                var hlEdit = (e.Row.FindControl("hlEdit") as LinkButton);
                var hlEventSaveAs = (e.Row.FindControl("hlEventSaveAs") as LinkButton);
                //绑定CommandArgument
                hlActivity.CommandArgument = eventID;
                hlPause.CommandArgument = eventID;
                hlAbort.CommandArgument = eventID;
                hlDelete.CommandArgument = eventID;
                hlEdit.CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", eventID, eventName, eventDesc, startDate, eventItemID, eventZoneID, eventStatusID, eventNameEn);
                hlEventSaveAs.CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", eventID, eventName, eventDesc, startDate, eventItemID, eventZoneID, eventStatusID,eventNameEn);
                //按钮状态
                hlActivity.Visible = false;
                hlPause.Visible = false;
                hlAbort.Visible = false;
                hlDelete.Visible = false;
                hlEdit.Visible = true && canEdit;
                hlEventSaveAs.Visible = true && canSaveAs;
                //提示信息
                hlAbort.OnClientClick = string.Format(_CommandScript,"终止赛事","终止后下属比赛都会终止!");
                hlDelete.OnClientClick = string.Format(_CommandScript, "删除赛事", "");

                var eventStatus = (EventStatusEnum)bindRow.STATUS;
                switch (eventStatus)
                {
                    case EventStatusEnum.UnActivated:
                        hlActivity.Visible = true && canSuspend;
                        hlDelete.Visible = true && canDelete;
                        break;
                    case EventStatusEnum.Activated:
                        hlPause.Visible = true && canSuspend;
                        hlAbort.Visible = true && canStop;
                        break;
                    case EventStatusEnum.Pause:
                        hlActivity.Visible = true && canSuspend;
                        hlAbort.Visible = true && canStop;
                        break;
                    case EventStatusEnum.Abort:
                        hlEdit.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void selectEventTeam_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            if (this.popCkcEventTeamList.Items.Count == 0)
                return;
            bool flag = false;
            IList<ListItem> selectedItems = new List<ListItem>();
            for (int i = 0; i < this.popCkcEventTeamList.Items.Count; i++)
            {
                if (this.popCkcEventTeamList.Items[i].Selected)
                {
                    //如果选择列表中已包含则不做操作
                    foreach (ListItem item in this.popCkcSelectedEventTeamList.Items)
                    {
                        if (item.Value == this.popCkcEventTeamList.Items[i].Value)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        selectedItems.Add(this.popCkcEventTeamList.Items[i]);
                    }
                }
            }
            foreach (ListItem item in selectedItems)
            {
                this.popCkcSelectedEventTeamList.Items.Insert(this.popCkcSelectedEventTeamList.Items.Count, item);
                this.popCkcEventTeamList.Items.Remove(item);
            }
        }

        protected void removeEventTeam_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            if (this.popCkcSelectedEventTeamList.Items.Count == 0)
                return;
            IList<ListItem> selectedItems = new List<ListItem>();
            for (int i = 0; i < this.popCkcSelectedEventTeamList.Items.Count; i++)
            {
                if (this.popCkcSelectedEventTeamList.Items[i].Selected)
                {
                    selectedItems.Add(this.popCkcSelectedEventTeamList.Items[i]);
                }
            }
            foreach (ListItem item in selectedItems)
            {
                this.popCkcEventTeamList.Items.Insert(this.popCkcEventTeamList.Items.Count, item);
                this.popCkcSelectedEventTeamList.Items.Remove(item);
            }
        }

        protected void ActiveEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var eventID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                var lastUpdateUserID = MySession.CurrentUser.ACCOUNT[0].USER_ID;
                EventManager.AvtivatedEvent(eventID, lastUpdateUserID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void PaurseEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var eventID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                var lastUpdateUserID = MySession.CurrentUser.ACCOUNT[0].USER_ID;
                EventManager.PauseEvent(eventID, lastUpdateUserID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void AbortEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var eventID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                var lastUpdateUserID = MySession.CurrentUser.ACCOUNT[0].USER_ID;
                EventManager.AbortEvent(eventID, lastUpdateUserID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }
        private const int RootID = 0;
        private static int rootZoneID;
        DSParamZone paramZoneDS = null;
        private void BindTreeView()
        {
            paramZoneDS = ParamZoneManager.QueryParamZone();
            this.tvParamZone.Nodes.Clear();
            IntiTreeNode(this.tvParamZone.Nodes, RootID);
            this.tvParamZone.ExpandDepth = 1;
            this.tvParamZone.ExpandAll();
        }
        /// <summary>
        /// 初始化TreeView
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentID"></param>
        private void IntiTreeNode(TreeNodeCollection nodes, int parentID)
        {
            DataView paramZoneTemp = ParamZoneManager.QueryParamZoneByParentZoneID(paramZoneDS, parentID);
            if (paramZoneTemp == null)
                return;
            TreeNode node = null;
            foreach (DataRowView rv in paramZoneTemp)
            {
                if (parentID == RootID)
                {
                    rootZoneID = Convert.ToInt32(rv["ZONE_ID"].ToString());
                }
                node = new TreeNode();
                node.Value = rv["ZONE_ID"].ToString();
                node.Text = rv["ZONE_NAME"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                nodes.Add(node);
                IntiTreeNode(nodes[nodes.Count - 1].ChildNodes, Convert.ToInt32(node.Value));
            }
        }

        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            mdlPopup.Hide();
            mdlPopupZone.Show();
        }

        protected void tvParamZone_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;
            txtZone.Text = node.Text;
            txtZoneID.Text = node.Value;
            mdlPopupZone.Hide();
            mdlPopup.Show();
            tvParamZone.Nodes[0].Selected = true;
        }
        #endregion
    }
}