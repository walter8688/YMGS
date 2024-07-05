using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.EventManage;
using YMGS.Data.DataBase;
using YMGS.Data.Entity;
using YMGS.Business.GameMarket;
using YMGS.Manage.Web.Controls;
using System.Data;
using YMGS.Framework;
using System.Collections;

namespace YMGS.Manage.Web.GameMarket
{
    [TopMenuId(FunctionIdList.GameMarketManagement.GameMarketManageModule)]
    [LeftMenuId(FunctionIdList.GameMarketManagement.ChampionGameManagePage)]
    public partial class ChampionGameManagePage : BasePage
    {
        private const string BtnCommandAdd = "CommandAdd";
        private const string BtnCommandEdit = "CommandEdit";
        private const string BtnCommandSaveAs = "CommandSaveAs";
        private const string ChampSportsEvent = "体育";
        private const string ChampEntertainmentEvent = "娱乐";
        private const string ChampEventStatusUnActivated = "未激活";
        private const string ChampEventStatusActivated = "激活";
        private const string ChampEventStatusPause = "暂停";
        private const string ChampEventStatusAbort = "终止";
        private const string _ChampEventStatusCalculated = "已结算";
        private const string _ChampEventStatusFinished = "已结束";
        private const string DateFomater = "yyyy-MM-dd";
        private static bool canEdit, canDel, canSuspend, canAbort, canSaveAs, canFinish, canRecord = false;
        private const string _CommandScript = "if(window.confirm('确定要{0}吗?')) return true;else return false;";
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameMarketManagement.ChampionGameManagePage;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageNavigator.PageIndexChanged+=new EventHandler(pageNavigator_PageIndexChanged);
            if (!IsPostBack)
            {
                InitPageCommandAccess();
                InitPageContralStatus();
                LoadPageContralData();
                this.btnNew.CommandName = BtnCommandAdd;
                LoadGridData();
            }
        }
        #region 加载页面状态
        private void InitPageCommandAccess()
        {
            this.btnNew.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.AddChampionGame);
            canEdit = MySession.Accessable(FunctionIdList.GameMarketManagement.EditChampionGame);
            canDel = MySession.Accessable(FunctionIdList.GameMarketManagement.DeleteChampionGame);
            canSuspend = MySession.Accessable(FunctionIdList.GameMarketManagement.SuspendChampionGame);
            canAbort = MySession.Accessable(FunctionIdList.GameMarketManagement.StopChampionGame);
            canSaveAs = MySession.Accessable(FunctionIdList.GameMarketManagement.SaveAsChampionGame);
            canFinish = MySession.Accessable(FunctionIdList.GameMarketManagement.FinishChampionGame);
            canRecord = MySession.Accessable(FunctionIdList.GameMarketManagement.RecordChampionGameResult);
        }

        private void InitPageContralStatus()
        {
            DropDownList[] drpArr = new DropDownList[] { popDrpEventItem, popDrpEvent, popDrpEventZone };
            TextBox[] txtArr = new TextBox[] { popTxtEventName,popTxtEventDesc, popTxtEventDesc, popTxtEntChampName, popTxtEntChampDesc, txtAddChampEventName,popTxtEntChampNameEn,popTxtEventNameEn };
            ListBox[] listArr = new ListBox[] { lstEventTeam, lsbEntEventTeam, lstEventSelectedTeam, lsbEventWinMemList, lsbEntWimMemList };
            AjaxCalendar[] calArr = new AjaxCalendar[] {popCalStartDate,popCalEndDate,popEntCalStartDate,popEntCalEndDate };
            foreach (DropDownList drp in drpArr)
            {
                drp.Enabled = true;
                if (drp.Items.Count > 0)
                    drp.SelectedIndex = 0;
            }
            foreach (TextBox txt in txtArr)
            {
                txt.Text = string.Empty;
            }
            foreach (ListBox lsb in listArr)
            {
                lsb.Items.Clear();
            }
            foreach (AjaxCalendar cal in calArr)
            {
                cal.Value = null;
            }
            txtBeginTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            txtEntBeignTime.Text = string.Empty;
            txtEntEndTime.Text = string.Empty;
            this.btnSave.Visible = true;
        }
        #endregion

        #region 加载页面数据
        private void LoadPageContralData()
        {
            var eventItemDS = EventZoneManager.QueryEventItem();
            PageHelper.BindListControlData(this.popDrpEventItem, eventItemDS, "EventItem_Name", "EventItem_ID", true);

            var champEventTypeList = CommonFunction.QueryAllChampEventType();
            PageHelper.BindListControlData(this.drpChampEventType, champEventTypeList, "ChampEventTypeName", "ChampEventTypeID", true);
        }

        private void BindEventZone(int eventItemId)
        {
            DSEventZone.TB_EVENT_ZONERow row = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            row.EVENTITEM_ID = eventItemId;
            row.EVENTZONE_NAME = row.EVENTZONE_NAME_EN = row.EVENTZONE_DESC = string.Empty;
            row.PARAM_ZONE_ID = -1;
            var eventZoneDS = EventZoneManager.QueryEventZone(row);
            PageHelper.BindListControlData(this.popDrpEventZone, eventZoneDS, "EVENTZONE_NAME", "EVENTZONE_ID", true);
        }

        private DSChampEvent.TB_Champ_EventRow GetCurrentChampEventRow(ChampEventTypeEnum champEventType, bool isAdd)
        {
            DSChampEvent.TB_Champ_EventRow chamEventRow = new DSChampEvent.TB_Champ_EventDataTable().NewTB_Champ_EventRow();
            chamEventRow.Champ_Event_Type = (int)champEventType;
            chamEventRow.Last_Update_User = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            if (isAdd)
                chamEventRow.Create_User = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            if (champEventType == ChampEventTypeEnum.Sports)
            {
                chamEventRow.Event_ID = Convert.ToInt32(popDrpEvent.SelectedValue);
                chamEventRow.Champ_Event_Name = popTxtEventName.Text;
                chamEventRow.Champ_Event_Name_En = popTxtEventNameEn.Text;
                chamEventRow.Champ_Event_Desc = popTxtEventDesc.Text;
                if(string.IsNullOrEmpty(txtBeginTime.Text))
                    chamEventRow.Champ_Event_StartDate = popCalStartDate.Value.Value;
                else
                    chamEventRow.Champ_Event_StartDate = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(popCalStartDate.Value), txtBeginTime.Text);
                if(string.IsNullOrEmpty(txtEndTime.Text))
                    chamEventRow.Champ_Event_EndDate = popCalEndDate.Value.Value;
                else
                    chamEventRow.Champ_Event_EndDate = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(popCalEndDate.Value), txtEndTime.Text);
                chamEventRow.Champ_Event_Status = (int)ChampEventStatusEnum.UnActivated;
            }
            else if (champEventType == ChampEventTypeEnum.Entertainment)
            {
                chamEventRow.Event_ID = -1;
                chamEventRow.Champ_Event_Name = popTxtEntChampName.Text;
                chamEventRow.Champ_Event_Name_En = popTxtEntChampNameEn.Text;
                chamEventRow.Champ_Event_Desc = popTxtEntChampDesc.Text;
                if (string.IsNullOrEmpty(txtEntBeignTime.Text))
                    //chamEventRow.Champ_Event_StartDate = popEntCalStartDate.Value.Value;
                    chamEventRow.Champ_Event_StartDate = DateTime.Now;
                else
                    chamEventRow.Champ_Event_StartDate = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(popEntCalStartDate.Value), txtEntBeignTime.Text);
                if(string.IsNullOrEmpty(txtEntEndTime.Text))
                    chamEventRow.Champ_Event_EndDate = popEntCalEndDate.Value.Value;
                else
                    chamEventRow.Champ_Event_EndDate = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(popEntCalEndDate.Value), txtEntEndTime.Text);
                chamEventRow.Champ_Event_Status = (int)ChampEventStatusEnum.UnActivated;
            }
            return chamEventRow;
        }

        private string GetCuurentMembers(string champEventMembers,ListBox lsb)
        {
            foreach (ListItem item in lsb.Items)
            {
                champEventMembers += item.Text + ",";
            }
            if (!string.IsNullOrEmpty(champEventMembers))
                champEventMembers = champEventMembers.Substring(0, champEventMembers.Length - 1);
            return champEventMembers;
        }
        #endregion

        #region 下拉框事件
        protected void popDrpEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
            BindEventZone(Convert.ToInt32((sender as DropDownList).SelectedValue));
        }

        protected void popDrpEventZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
            var eventZoneID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            var eventStatus = (int)EventStatusEnum.Activated;
            var eventDS = EventManager.QueryEvent(eventZoneID, string.Empty, string.Empty, string.Empty, null, null, eventStatus, -1, string.Empty);
            PageHelper.BindListControlData(this.popDrpEvent, eventDS, "EVENT_NAME", "EVENT_ID", true);
        }

        protected void popDrpEvent_SelectedIndexChanged(object sender,EventArgs e)
        {
            mdlPopup.Show();
            var eventID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            if (eventID == -1)
                return;
            var eventRow = EventManager.QueryEventByEventID(eventID);
            //赛事成员
            var eventTeamDS = EventTeamManager.QueryEventTeamByEventID(eventID);
            PageHelper.BindListControlData(this.lstEventTeam, eventTeamDS, "EVENT_TEAM_NAME_ALL", "EVENT_TEAM_ID", false);
            FilterSelectedListItem();
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            InitPageContralStatus();
            this.btnSave.CommandName = (sender as Button).CommandName;
        }

        #region 编辑冠军赛事
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            InitPageContralStatus();
            var btnEdit = (sender as LinkButton);
            this.btnSave.CommandName = btnEdit.CommandName;
            var argArr = btnEdit.CommandArgument.Split(',');
            this.txtChampEventId.Text = argArr[0];
            var champEventType = (ChampEventTypeEnum)Convert.ToInt32(argArr[1]);
            var memberList = ChampEventManager.QueryChampEventMemberById(Convert.ToInt32(argArr[0]));
            var wimMemList = ChampEventManager.QueryChampWinMemList(Convert.ToInt32(argArr[0]));
            var status = (ChampEventStatusEnum)(Convert.ToInt32(argArr[7]));
            if (status != ChampEventStatusEnum.UnActivated && (sender as LinkButton).CommandName != BtnCommandSaveAs)
            {
                this.btnSave.Visible = false;
            }

            switch (champEventType)
            {
                case ChampEventTypeEnum.Sports:
                    txtChampEventType.Text = ((int)ChampEventTypeEnum.Sports).ToString();
                    popTxtEventName.Text = argArr[2];
                    popTxtEventDesc.Text = argArr[3];
                    popCalStartDate.Value = UtilityHelper.StrToDate(DateTime.Parse(argArr[4]).ToString(DateFomater));
                    popCalEndDate.Value = UtilityHelper.StrToDate(DateTime.Parse(argArr[5]).ToString(DateFomater));
                    txtBeginTime.Text = UtilityHelper.TimeToStr(DateTime.Parse(argArr[4]));
                    txtEndTime.Text = UtilityHelper.TimeToStr(DateTime.Parse(argArr[5]));
                    popTxtEventNameEn.Text = argArr[8];
                    PageHelper.BindListControlData(lstEventSelectedTeam, memberList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);

                    //赛事信息
                    var eventId = Convert.ToInt32(argArr[6]);
                    var eventRow = EventManager.QueryEventByEventID(eventId);
                    popDrpEvent.Items.Clear();
                    popDrpEvent.Items.Add(new ListItem(eventRow.EVENT_NAME, eventRow.EVENT_ID.ToString()));
                    BindEventZone(Convert.ToInt32(popDrpEventItem.SelectedValue));
                    popDrpEventZone.SelectedValue = eventRow.EVENTZONE_ID.ToString();
                    popDrpEventItem.SelectedIndex = 1;
                    popDrpEvent.Enabled = false;
                    popDrpEventZone.Enabled = false;
                    popDrpEventItem.Enabled = false;
                    //赛事成员
                    var eventTeamDS = EventTeamManager.QueryEventTeamByEventID(eventId);
                    PageHelper.BindListControlData(lstEventTeam, eventTeamDS, "EVENT_TEAM_NAME_ALL", "EVENT_TEAM_ID", false);
                    FilterSelectedListItem();
                    //绑定获胜成员
                    PageHelper.BindListControlData(lsbEventWinMemList, wimMemList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);
                    break;
                case ChampEventTypeEnum.Entertainment:
                    //cpeid,cpetype,cpename,cpedesc,cpesdate,cpeendate
                    this.popTxtEntChampName.Text = argArr[2];
                    this.popTxtEntChampDesc.Text = argArr[3];
                    popTxtEntChampNameEn.Text = argArr[8];
                    this.popEntCalStartDate.Value = UtilityHelper.StrToDate(DateTime.Parse(argArr[4]).ToString(DateFomater));
                    this.popEntCalEndDate.Value = UtilityHelper.StrToDate(DateTime.Parse(argArr[5]).ToString(DateFomater));
                    txtEntBeignTime.Text = UtilityHelper.TimeToStr(DateTime.Parse(argArr[4]));
                    txtEntEndTime.Text = UtilityHelper.TimeToStr(DateTime.Parse(argArr[5]));
                    this.txtChampEventType.Text = ((int)ChampEventTypeEnum.Entertainment).ToString();
                    PageHelper.BindListControlData(this.lsbEntEventTeam, memberList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);
                    PageHelper.BindListControlData(this.lsbEntWimMemList, wimMemList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);
                    break;
            }
            mdlPopup.Show();
        }
        #endregion

        #region 过滤已选的ListItem
        private void FilterSelectedListItem()
        {
            Hashtable ht = new Hashtable();
            foreach (ListItem item in this.lstEventSelectedTeam.Items)
            {
                ht.Add(item.Text, item.Value);
            }
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in this.lstEventTeam.Items)
            {
                if (ht.ContainsKey(item.Text))
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                this.lstEventTeam.Items.Remove(item);
            }
        }
        #endregion


        #region 控制比赛
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                ChampEventManager.DeleteChampEvent(champEventId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActiveEvent_Click(object sender, EventArgs e)
        {
            var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            ChampEventManager.ActiveChampEvent(champEventId);
            LoadGridData();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PaurseEvent_Click(object sender, EventArgs e)
        {
            var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            ChampEventManager.ParseChampEvent(champEventId);
            LoadGridData();
        }

        /// <summary>
        /// 终止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AbortEvent_Click(object sender, EventArgs e)
        {
            try
            {
                var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                ChampEventManager.AbortChampEvent(champEventId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 结束比赛
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishEvent_Click(object sender, EventArgs e)
        {
            var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            ChampEventManager.FinishChampEvent(champEventId);
            LoadGridData();
        }
        #endregion

        #region 赛事成员操作
        protected void btnRecordWinMems_Click(object sender, EventArgs e)
        {
            mdlRecordWinMembers.Show();
            txtChampEventId.Text = (sender as LinkButton).CommandArgument;
            var champEventId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            var memberList = ChampEventManager.QueryChampEventMemberById(champEventId);
            var wimMemList = ChampEventManager.QueryChampWinMemList(champEventId);
            PageHelper.BindListControlData(lstEventMembers, memberList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);
            PageHelper.BindListControlData(lstWinMembers, wimMemList, "Champ_Event_Member_Name", "Champ_Event_Member_ID", false);
            List<ListItem> winMems = new List<ListItem>();
            foreach (ListItem item in lstWinMembers.Items)
                winMems.Add(item);
            foreach (ListItem item in winMems)
                lstEventMembers.Items.Remove(item);

        }

        /// <summary>
        /// 维护获胜成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRecordWinMembers_Click(object sender, EventArgs e)
        {
            var champEventId = Convert.ToInt32(txtChampEventId.Text);
            var winMemIds = string.Empty;
            foreach (ListItem item in lstWinMembers.Items)
                winMemIds = winMemIds + item.Value + ",";
            if (!string.IsNullOrEmpty(winMemIds))
                winMemIds = winMemIds.Substring(0, winMemIds.Length - 1);
            ChampEventManager.RecordWinMembers(champEventId, winMemIds);
        }

        /// <summary>
        /// 新增赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            if (string.IsNullOrEmpty(txtAddChampEventName.Text.Trim()) || string.IsNullOrEmpty(txtAddChampEventNameEn.Text))
                return;
            foreach (ListItem item in lsbEntEventTeam.Items)
            {
                if (item.Text == txtAddChampEventName.Text)
                    return;
            }
            ListItem newItem = new ListItem(txtAddChampEventName.Text + "|" + txtAddChampEventNameEn.Text, txtAddChampEventName.Text);
            lsbEntEventTeam.Items.Insert(lsbEntEventTeam.Items.Count, newItem);
            txtAddChampEventName.Text = txtAddChampEventNameEn.Text = string.Empty;
        }

        /// <summary>
        /// 移除赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveMember_Click(object sender, EventArgs e)
        {
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in lsbEntEventTeam.Items)
            {
                if (item.Selected)
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                lsbEntEventTeam.Items.Remove(item);
            }
            mdlPopup.Show();
        }

        /// <summary>
        /// 选择体育类赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectedEventMember(object sender, EventArgs e)
        {
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in this.lstEventTeam.Items)
            {
                if (item.Selected)
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                this.lstEventSelectedTeam.Items.Insert(this.lstEventSelectedTeam.Items.Count, item);
                this.lstEventTeam.Items.Remove(item);
            }
            mdlPopup.Show();
        }

        /// <summary>
        /// 移除体育类赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveEventMember(object sender, EventArgs e)
        {
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in this.lstEventSelectedTeam.Items)
            {
                if (item.Selected)
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                this.lstEventTeam.Items.Insert(this.lstEventTeam.Items.Count, item);
                this.lstEventSelectedTeam.Items.Remove(item);
            }
            mdlPopup.Show();
        }

        /// <summary>
        /// 新增获胜成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddWinMember_Click(object sender, EventArgs e)
        {
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in lstEventMembers.Items)
            {
                if (item.Selected)
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                this.lstWinMembers.Items.Insert(lstWinMembers.Items.Count, item);
                this.lstEventMembers.Items.Remove(item);
            }
            mdlRecordWinMembers.Show();
        }

        /// <summary>
        /// 移除获胜成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveWinMember_Click(object sender, EventArgs e)
        {
            IList<ListItem> selItemList = new List<ListItem>();
            foreach (ListItem item in lstWinMembers.Items)
            {
                if (item.Selected)
                    selItemList.Add(item);
            }
            foreach (ListItem item in selItemList)
            {
                this.lstEventMembers.Items.Insert(lstEventMembers.Items.Count, item);
                this.lstWinMembers.Items.Remove(item);
            }
            mdlRecordWinMembers.Show();
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var champEventType = (ChampEventTypeEnum)(Convert.ToInt32(this.txtChampEventType.Text));
                var commandName = (sender as Button).CommandName;
                string champEventMembers = string.Empty;
                switch (commandName)
                {
                    case BtnCommandAdd://新增
                        if (champEventType == ChampEventTypeEnum.Sports)
                        {
                            if (this.popDrpEventItem.SelectedIndex <= 0)
                                return;
                        }
                        //获取冠军赛事对象
                        var chamEventRow = GetCurrentChampEventRow(champEventType, true);
                        if (champEventType == ChampEventTypeEnum.Sports)//获取冠军赛事参赛成员
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lstEventSelectedTeam); 
                        else if (champEventType == ChampEventTypeEnum.Entertainment)
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lsbEntEventTeam);
                        ChampEventManager.AddChampEvent(chamEventRow, champEventMembers);
                        break;
                    case BtnCommandEdit://编辑数据
                        if (champEventType == ChampEventTypeEnum.Sports)
                        {
                            if (this.popDrpEventItem.SelectedIndex <= 0)
                                return;
                        }
                        var champEvent = GetCurrentChampEventRow(champEventType, false);
                        var champEventId = Convert.ToInt32(this.txtChampEventId.Text);
                        champEvent.Champ_Event_ID = champEventId;
                        if (champEventType == ChampEventTypeEnum.Sports)//获取冠军赛事参赛成员
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lstEventSelectedTeam); 
                        else if (champEventType == ChampEventTypeEnum.Entertainment)
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lsbEntEventTeam);
                        ChampEventManager.UpdateChampEvent(champEvent, champEventMembers);//更新
                        break;
                    case BtnCommandSaveAs:
                        if (champEventType == ChampEventTypeEnum.Sports)
                        {
                            if (this.popDrpEventItem.SelectedIndex <= 0)
                                return;
                        }
                        //获取冠军赛事对象
                        chamEventRow = GetCurrentChampEventRow(champEventType, true);
                        if (champEventType == ChampEventTypeEnum.Sports)//获取冠军赛事参赛成员
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lstEventSelectedTeam); 
                        else if (champEventType == ChampEventTypeEnum.Entertainment)
                            champEventMembers = GetCuurentMembers(champEventMembers, this.lsbEntEventTeam);
                        ChampEventManager.AddChampEvent(chamEventRow, champEventMembers);
                        break;
                    default:
                        break;
                }
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
                mdlPopup.Show();
            }
        }

        private void LoadGridData()
        {
            int? champEventType = Convert.ToInt32(drpChampEventType.SelectedValue);
            string champEventName = txtChampEventName.Text.Trim();
            string champEventNameEn = txtChampEventNameEn.Text.Trim();
            string champEventDesc = txtChampEventDesc.Text.Trim();
            DateTime? startDate, endDate;
            if (calStartDate.Value.HasValue)
                startDate = calStartDate.Value.Value;
            else
                startDate = null;
            if (this.calEndDate.Value.HasValue)
                endDate = calEndDate.Value.Value;
            else
                endDate = null;
            var champEventList = ChampEventManager.QueryChampEvent(champEventType, champEventName,champEventNameEn, champEventDesc, startDate, endDate);
            this.pageNavigator.databinds(champEventList.Tables[0], this.gdvChampEvent);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvChampEvent_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSChampEvent.TB_Champ_EventRow)((DataRowView)e.Row.DataItem).Row;//获取当前数据行
                var typeEnum = (ChampEventTypeEnum)bindRow.Champ_Event_Type;
                Label lblChampEventTypeName = (e.Row.FindControl("lblChampEventTypeName") as Label);
                lblChampEventTypeName.Text = typeEnum == ChampEventTypeEnum.Sports ? ChampSportsEvent : ChampEntertainmentEvent;
                var statusEnum = (ChampEventStatusEnum)bindRow.Champ_Event_Status;
                Label lblChampEventStatusName = (e.Row.FindControl("lblChampEventStatusName") as Label);

                var hlEdit = (e.Row.FindControl("hlEdit") as LinkButton);
                var hlDelete = (e.Row.FindControl("hlDelete") as LinkButton);
                var hlActivity = (e.Row.FindControl("hlActivity") as LinkButton);
                var hlPause = (e.Row.FindControl("hlPause") as LinkButton);
                var hlAbort = (e.Row.FindControl("hlAbort") as LinkButton);
                var hlFinish = (e.Row.FindControl("hlFinish") as LinkButton);
                var hlSaveAs = (e.Row.FindControl("hlSaveAs") as LinkButton);
                var hlRecordWinMems = (e.Row.FindControl("hlRecordWinMems") as LinkButton);

                hlAbort.OnClientClick = string.Format(_CommandScript, "终止比赛");
                hlDelete.OnClientClick = string.Format(_CommandScript, "删除比赛");

                hlEdit.Visible = canEdit;
                hlDelete.Visible = false & canDel;
                hlActivity.Visible = false & canSuspend;
                hlPause.Visible = true & canSuspend;
                hlAbort.Visible = true & canAbort;
                hlRecordWinMems.Visible = false;
                hlFinish.Visible = false;
                hlSaveAs.Visible = true & canSaveAs;
                switch (statusEnum)
                {
                    case ChampEventStatusEnum.UnActivated:
                        lblChampEventStatusName.Text = ChampEventStatusUnActivated;
                        hlEdit.Visible = true & canEdit;
                        hlDelete.Visible = true & canDel;
                        hlActivity.Visible = true & canSuspend;
                        hlPause.Visible = false;
                        hlAbort.Visible = false;
                        break;
                    case ChampEventStatusEnum.Activated:
                        lblChampEventStatusName.Text = ChampEventStatusActivated;
                        hlFinish.Visible = true & canFinish;
                        break;
                    case ChampEventStatusEnum.Pause:
                        lblChampEventStatusName.Text = ChampEventStatusPause;
                        hlActivity.Visible = true & canSuspend;
                        hlPause.Visible = false;
                        break;
                    case ChampEventStatusEnum.Abort:
                        lblChampEventStatusName.Text = ChampEventStatusAbort;
                        hlPause.Visible = false;
                        hlAbort.Visible = false;
                        break;
                    case ChampEventStatusEnum.Calculated:
                        lblChampEventStatusName.Text = _ChampEventStatusCalculated;
                        hlDelete.Visible = false;
                        hlActivity.Visible = false;
                        hlPause.Visible = false;
                        hlAbort.Visible = false;
                        hlRecordWinMems.Visible = true & canRecord;
                        break;
                    case ChampEventStatusEnum.Finished:
                        lblChampEventStatusName.Text = _ChampEventStatusFinished;
                        hlDelete.Visible = false;
                        hlActivity.Visible = false;
                        hlPause.Visible = false;
                        hlAbort.Visible = false;
                        hlRecordWinMems.Visible = true & canRecord;
                        break;
                }

                var champEventId = bindRow.Champ_Event_ID;
                var champTypeId = bindRow.Champ_Event_Type;
                var eventID = bindRow.Event_ID;
                var champEventName = bindRow.Champ_Event_Name;
                var champEventDesc = bindRow.Champ_Event_Desc;
                var startDate = bindRow.Champ_Event_StartDate;
                var endDate = bindRow.Champ_Event_EndDate;
                var status = bindRow.Champ_Event_Status;
                //cpeid,cpetype,cpename,cpedesc,cpesdate,cpeendate,status
                hlEdit.CommandName = BtnCommandEdit;
                hlEdit.CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", champEventId, champTypeId, champEventName, champEventDesc, startDate, endDate, eventID,status,bindRow.Champ_Event_Name_En);
                hlDelete.CommandArgument = champEventId.ToString();
                hlActivity.CommandArgument = champEventId.ToString();
                hlPause.CommandArgument = champEventId.ToString();
                hlAbort.CommandArgument = champEventId.ToString();
                hlRecordWinMems.CommandArgument = champEventId.ToString();
                hlFinish.CommandArgument = champEventId.ToString();
                hlSaveAs.CommandName = BtnCommandSaveAs;
                hlSaveAs.CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", champEventId, champTypeId, champEventName, champEventDesc, startDate, endDate, eventID, status, bindRow.Champ_Event_Name_En);
            }
        }
    }
}