using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Business;
using YMGS.Manage.Web.Common;
using YMGS.Data.Presentation;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;
using YMGS.Business.EventManage;
using System.Data;

namespace YMGS.Manage.Web.EventManage
{
    [TopMenuId(FunctionIdList.EventManagement.EventManageModule)]
    [LeftMenuId(FunctionIdList.EventManagement.TeamManagePage)]
    public partial class TeamManagePage :BasePage
    {
        private static bool canEdit;
        private static bool canDelete;
        private static bool canDisable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPageAccess();
                LoadDrpData();
                LoadGridData();
                BindTreeView();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.EventManagement.TeamManagePage;
            }
        }

        private void InitPageAccess()
        {
            this.btnAddEventTeam.Enabled = MySession.Accessable(FunctionIdList.EventManagement.AddTeam);
            canEdit = MySession.Accessable(FunctionIdList.EventManagement.EditTeam);
            canDelete = MySession.Accessable(FunctionIdList.EventManagement.DeleteTeam);
            canDisable = MySession.Accessable(FunctionIdList.EventManagement.DisableTeam);
        }

        private DSEventTeam.TB_EVENT_TEAMRow GetQueryCurrentEventTeam()
        {
            DSEventTeam.TB_EVENT_TEAMRow eventTeam = new DSEventTeam.TB_EVENT_TEAMDataTable().NewTB_EVENT_TEAMRow();
            eventTeam.EVENT_ITEM_ID = Convert.ToInt32(drpEventItem.SelectedValue);
            eventTeam.EVENT_TEAM_NAME = txtEventTeamName.Text.Trim();
            eventTeam.EVENT_TEAM_NAME_EN = txtEventTeamNameEn.Text.Trim();
            eventTeam.EVENT_TEAM_TYPE1 = Convert.ToInt32(drpParamType1.SelectedValue);
            eventTeam.EVENT_TEAM_TYPE2 = Convert.ToInt32(drpParamType2.SelectedValue);
            eventTeam.STATUS = Convert.ToInt32(drpParamStatus.SelectedValue);
            eventTeam.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            eventTeam.LAST_UPDATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            eventTeam.PARAM_ZONE_ID = string.IsNullOrEmpty(ZoneID.Text) ? -1 : Convert.ToInt32(ZoneID.Text);
            if (string.IsNullOrEmpty(ZoneName.Text.Trim()))
                eventTeam.PARAM_ZONE_ID = -1;
            return eventTeam;
        }

        private DSEventTeam.TB_EVENT_TEAMRow GetQueryNonCurrentEventTeam()
        {
            DSEventTeam.TB_EVENT_TEAMRow eventTeam = new DSEventTeam.TB_EVENT_TEAMDataTable().NewTB_EVENT_TEAMRow();
            eventTeam.EVENT_ITEM_ID = Convert.ToInt32(popDrpEventItem.SelectedValue);
            eventTeam.EVENT_TEAM_NAME = popTxtEventTeamName.Text.Trim();
            eventTeam.EVENT_TEAM_NAME_EN = popTxtEventTeamNameEn.Text.Trim();
            eventTeam.EVENT_TEAM_TYPE1 = Convert.ToInt32(popDrpParamType1.SelectedValue);
            eventTeam.EVENT_TEAM_TYPE2 = Convert.ToInt32(popDrpParamType2.SelectedValue);
            eventTeam.STATUS = Convert.ToInt32(popDrpParamStatus.SelectedValue);
            eventTeam.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            eventTeam.LAST_UPDATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            eventTeam.PARAM_ZONE_ID = Convert.ToInt32(txtZoneID.Text);
            return eventTeam;
        }

        #region 加载页面数据
        private void LoadDrpData()
        {
            var eventItemDS = EventZoneManager.QueryEventItem();
            var paramType1DS = ParamParamManager.QueryParamByType(ParamTypeEnum.ParamEventTeamType);
            var paramType2DS = ParamParamManager.QueryParamByType(ParamTypeEnum.ParamEventTeamTypeSex);
            var paramStatusDS = CommonFunction.QueryAllEventTeamStatus();
            //赛事项目
            BindDropDownList(drpEventItem, eventItemDS, "EventItem_Name", "EventItem_ID", true);

            //类型(国家/职业)
            BindDropDownList(drpParamType1, paramType1DS, "PARAM_NAME", "PARAM_ID", true);

            //类型(男子/女子)
            BindDropDownList(drpParamType2, paramType2DS, "PARAM_NAME", "PARAM_ID", true);

            //参赛成员状态
            BindDropDownList(drpParamStatus, paramStatusDS, "EventTeamStatusName", "EventTeamStatusId", true);

            //PopUp赛事项目
            BindDropDownList(popDrpEventItem, eventItemDS, "EventItem_Name", "EventItem_ID", false);

            //PopUp类型(国家/职业)
            BindDropDownList(popDrpParamType1, paramType1DS, "PARAM_NAME", "PARAM_ID", false);

            //PopUp类型(男子/女子)
            BindDropDownList(popDrpParamType2, paramType2DS, "PARAM_NAME", "PARAM_ID", false);

            //参赛成员状态
            BindDropDownList(popDrpParamStatus, paramStatusDS, "EventTeamStatusName", "EventTeamStatusId", false);
        }

        private void BindDropDownList(DropDownList obj,object datasource, string dataTextField, string dataValueField,bool needNullItem)
        {
            obj.Items.Clear();
            obj.DataSource = datasource;
            obj.DataTextField = dataTextField;
            obj.DataValueField = dataValueField;
            obj.DataBind();
            if (needNullItem)
                obj.Items.Insert(0, GetNullValueListItem());
        }

        private ListItem GetNullValueListItem()
        {
            ListItem item = new ListItem(CommonConstant.DropDownListNullKey, CommonConstant.DropDownListNullValue);
            return item;
        }

        private void LoadGridData()
        {
            var eventTeamRow = GetQueryCurrentEventTeam();
            var eventTeamDS = EventTeamManager.QueryEventTeam(eventTeamRow);
            this.pageNavigator.databinds(eventTeamDS.Tables[0], this.gdvEventTeam);
        }
        #endregion

        #region 页面事件
        #region deleted
        /*protected void drpEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as DropDownList).SelectedIndex == 0)
            {
                this.drpEventZone.Items.Clear();
                return;
            }
            DSEventZone.TB_EVENT_ZONERow eventZone = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            eventZone.EVENTITEM_ID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            eventZone.EVENTZONE_NAME = eventZone.EVENTZONE_DESC = "";
            BindDropDownList(this.drpEventZone, EventZoneManager.QueryEventZone(eventZone), "EVENTZONE_NAME", "EVENTZONE_ID", false);
        }
        protected void popDrpEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSEventZone.TB_EVENT_ZONERow eventZone = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            eventZone.EVENTITEM_ID = Convert.ToInt32((sender as DropDownList).SelectedValue);
            eventZone.EVENTZONE_NAME = eventZone.EVENTZONE_DESC = "";
            BindDropDownList(this.popDrpEventZone, EventZoneManager.QueryEventZone(eventZone), "EVENTZONE_NAME", "EVENTZONE_ID", false);
        }*/
        #endregion
        protected void btnQueryEventTeam_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvEventTeam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSTeamList.DSTeamListRow)((DataRowView)e.Row.DataItem).Row;
                var eventTeamID = bindRow.EVENT_TEAM_ID.ToString();
                var eventItemID = (e.Row.FindControl("lblEventItemID") as Label).Text;
                var param1ID = (e.Row.FindControl("lblParamID1") as Label).Text;
                var param2ID = (e.Row.FindControl("lblParamID2") as Label).Text;
                var statusID = (e.Row.FindControl("lblStatus") as Label).Text;
                var eventTeamName = e.Row.Cells[1].Text;
                var eventTeamNameEn = e.Row.Cells[2].Text;
                var paramZoneId = bindRow.IsPARAM_ZONE_IDNull() ? -1 : bindRow.PARAM_ZONE_ID;
                var zoneName = bindRow.IsZONE_NAMENull() ? string.Empty : bindRow.ZONE_NAME;

                (e.Row.FindControl("hlEdit") as LinkButton).CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", eventTeamID, eventItemID, param1ID, param2ID, statusID, eventTeamName, eventTeamNameEn, paramZoneId, zoneName);
                (e.Row.FindControl("hlDelete") as LinkButton).CommandArgument = eventTeamID;//删除前需要判断此参数成员是否和赛事关联？？
                (e.Row.FindControl("hlActivity") as LinkButton).CommandArgument = string.Format("{0},{1}", eventTeamID, EventTeamStatusEnum.Activity.ToString());
                (e.Row.FindControl("hlInActivity") as LinkButton).CommandArgument = string.Format("{0},{1}", eventTeamID, EventTeamStatusEnum.InActivity.ToString());

                (e.Row.FindControl("hlDelete") as LinkButton).Visible = canDelete;
                (e.Row.FindControl("hlEdit") as LinkButton).Visible = canEdit;
                (e.Row.FindControl("hlActivity") as LinkButton).Visible = canDisable;
                (e.Row.FindControl("hlInActivity") as LinkButton).Visible = canDisable;

                var eventTeamStatus = (EventTeamStatusEnum)(Convert.ToInt32(statusID));
                switch (eventTeamStatus)
                {
                    case EventTeamStatusEnum.Activity:
                        (e.Row.FindControl("hlActivity") as LinkButton).Visible = false;
                        break;
                    case EventTeamStatusEnum.InActivity:
                        (e.Row.FindControl("hlInActivity") as LinkButton).Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        /// <summary>
        /// 新增&编辑赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddEventTeam_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            if (sender is Button)//新增
            {
                //初始化DropDownList SelectedIndex
                DropDownList[] drpArr = new DropDownList[] { popDrpEventItem, popDrpParamType1, popDrpParamType2, popDrpParamStatus };
                foreach (DropDownList drp in drpArr)
                {
                    if (drp.Items.Count > 0)
                        drp.SelectedIndex = 0;
                }
                popTxtEventTeamName.Text = popTxtEventTeamNameEn.Text = txtZone.Text = "";
                txtZoneID.Text = "";
                btnSave.CommandName = ButtonCommandType.Add.ToString();
            }
            else if (sender is LinkButton)//编辑
            {
                var argArr = (sender as LinkButton).CommandArgument.Split(',');
                popDrpEventItem.SelectedValue = argArr[1];
                popDrpParamType1.SelectedValue = argArr[2];
                popDrpParamType2.SelectedValue = argArr[3];
                popDrpParamStatus.SelectedValue = argArr[4];
                popTxtEventTeamName.Text = argArr[5];
                popTxtEventTeamNameEn.Text = argArr[6];
                txtZoneID.Text = argArr[7];
                txtZone.Text = argArr[8];
                btnSave.CommandArgument = argArr[0];
                btnSave.CommandName = ButtonCommandType.Edit.ToString();
            }
            TxtParamZoneStatus.Text = "Edit";
        }

        /// <summary>
        /// 启用/禁用赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateEventTeamStatus_Click(object sender, EventArgs e)
        {
            var argArr = (sender as LinkButton).CommandArgument.Split(',');
            var eventTeamID = Convert.ToInt32(argArr[0]);
            if (argArr[1] == EventTeamStatusEnum.Activity.ToString())//启用
                EventTeamManager.UpdateEventTeamStatus(eventTeamID, EventTeamStatusEnum.Activity);
            else//禁用
                EventTeamManager.UpdateEventTeamStatus(eventTeamID, EventTeamStatusEnum.InActivity);
            LoadGridData();
        }

        /// <summary>
        /// 删除赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelEventTeam_Click(object sender, EventArgs e)
        {
            try
            {
                var eventTeamID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                EventTeamManager.DeleteEventTeam(eventTeamID);
                LoadGridData();
            }
            catch(Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        /// <summary>
        /// 保存赛事成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEventTeam_Click(object sender, EventArgs e)
        {
            try
            {
                DSEventTeam.TB_EVENT_TEAMRow eventTeam = GetQueryNonCurrentEventTeam();
                var buttonCommandType = (sender as Button).CommandName;
                if (buttonCommandType == ButtonCommandType.Add.ToString())
                {
                    EventTeamManager.AddEventTeam(eventTeam);
                }
                else if (buttonCommandType == ButtonCommandType.Edit.ToString())
                {
                    eventTeam.EVENT_TEAM_ID = Convert.ToInt32((sender as Button).CommandArgument);
                    EventTeamManager.UpdateEventTeam(eventTeam);
                }
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
            var flag = TxtParamZoneStatus.Text;
            if (flag == "Edit")
                mdlPopup.Hide();
            mdlPopupZone.Show();
           
        }

        protected void tvParamZone_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;
            var flag = TxtParamZoneStatus.Text;
            if (flag == "Edit")
            {
                txtZone.Text = node.Text;
                txtZoneID.Text = node.Value;
                mdlPopup.Show();
            }
            else
            {
                ZoneName.Text = node.Text;
                ZoneID.Text = node.Value;
            }
            mdlPopupZone.Hide();
            tvParamZone.Nodes[0].Selected = true;
        }
        #endregion
    }
}