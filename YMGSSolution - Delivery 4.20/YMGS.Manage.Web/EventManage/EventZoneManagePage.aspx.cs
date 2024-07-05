using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.DataBase;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Business.EventManage;
using YMGS.Manage.Web.Common;
using System.Data;
using YMGS.Business.AssistManage;

namespace YMGS.Manage.Web.EventManage
{
    [TopMenuId(FunctionIdList.EventManagement.EventManageModule)]
    [LeftMenuId(FunctionIdList.EventManagement.EventZoneManagePage)]
    public partial class EventZoneManagePage : BasePage
    {
        private const string _CommandScript = "if(window.confirm('确定要{0}吗?')) return true;else return false;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventItemData();
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
                return FunctionIdList.EventManagement.EventZoneManagePage;
            }
        }

        private void PageActionAccess()
        {
            this.btnAddEventZone.Visible = MySession.Accessable(FunctionIdList.EventManagement.AddEventZone);
        }

        private void LoadEventItemData()
        {
            drpEventItem.Items.Clear();
            popUpDrpEventItem.Items.Clear();
            popUpDrpEventItem.DataSource = drpEventItem.DataSource = EventZoneManager.QueryEventItem();
            popUpDrpEventItem.DataTextField = drpEventItem.DataTextField = CommonConstant.EventZoneManagePage_EventItem_Name;
            popUpDrpEventItem.DataValueField = drpEventItem.DataValueField = CommonConstant.EventZoneManagePage_EventItem_ID;
            drpEventItem.DataBind();
            drpEventItem.Items.Insert(0,new ListItem("",CommonConstant.PageErrorCode.ToString()));
            popUpDrpEventItem.DataBind();
        }

        private DSEventZone.TB_EVENT_ZONERow GetQueryEventZoneRow()
        {
            DSEventZone.TB_EVENT_ZONERow row = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            row.EVENTITEM_ID = Convert.ToInt32(drpEventItem.SelectedValue);
            row.EVENTZONE_NAME = txtEventZoneName.Text;
            row.EVENTZONE_NAME_EN = txtEventZoneNameEn.Text;
            row.EVENTZONE_DESC = txtEventZoneDesc.Text;
            row.PARAM_ZONE_ID = string.IsNullOrEmpty(ZoneName.Text) ? -1 : Convert.ToInt32(ZoneID.Text);
            return row;
        }

        private DSEventZone.TB_EVENT_ZONERow GetNonQueryEventZoneRow()
        {
            DSEventZone.TB_EVENT_ZONERow row = new DSEventZone.TB_EVENT_ZONEDataTable().NewTB_EVENT_ZONERow();
            row.EVENTITEM_ID = Convert.ToInt32(popUpDrpEventItem.SelectedValue);
            row.EVENTZONE_NAME = popUpTxtEventZoneName.Text.Trim();
            row.EVENTZONE_NAME_EN = popUpTxtEventZoneNameEn.Text.Trim();
            row.EVENTZONE_DESC = popUpTxtEventZoneDesc.Text;
            row.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            row.LAST_UPDATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            row.PARAM_ZONE_ID = string.IsNullOrEmpty(txtZone.Text) ? -1 : Convert.ToInt32(txtZoneID.Text);
            return row;
        }

        private void LoadGridData()
        {
            var eventZoneDS = EventZoneManager.QueryEventZone(GetQueryEventZoneRow());
            pageNavigator.databinds(eventZoneDS.Tables[0], gdvEventZone);
        }

        protected void btnQueryEventZone_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void btnAddEventZone_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            if (sender is Button)//新增
            {
                popUpDrpEventItem.Enabled = true;
                popUpTxtEventZoneName.Text = popUpTxtEventZoneDesc.Text = popUpTxtEventZoneNameEn.Text = txtHiddenEventZoneID.Text = "";
                txtZone.Text = txtZoneID.Text = "";
            }
            else if (sender is LinkButton)//编辑
            {
                var commandArgArr = (sender as LinkButton).CommandArgument.Split(',');
                popUpDrpEventItem.SelectedValue = commandArgArr[0];
                popUpDrpEventItem.Enabled = false;
                txtHiddenEventZoneID.Text = commandArgArr[1];
                popUpTxtEventZoneName.Text = commandArgArr[2].Trim();
                popUpTxtEventZoneNameEn.Text = commandArgArr[3].Trim();
                popUpTxtEventZoneDesc.Text = commandArgArr[4];
                txtZoneID.Text = commandArgArr[5];
                txtZone.Text = commandArgArr[6];
            }
        }

        protected void btnSaveEventZone_Click(object sender, EventArgs e)
        {
            try
            {
                DSEventZone.TB_EVENT_ZONERow row = GetNonQueryEventZoneRow();
                if (popUpDrpEventItem.Enabled)//新增
                {
                    EventZoneManager.AddEventZone(row);
                }
                else
                {
                    row.EVENTZONE_ID = Convert.ToInt32(txtHiddenEventZoneID.Text);
                    EventZoneManager.UpdateEventZone(row);
                }
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        protected void btnDelEventZone_Click(object sender, EventArgs e)
        {
            try
            {
                var eventZoneID = (sender as LinkButton).CommandArgument;
                EventZoneManager.DeleteEventZone(Convert.ToInt32(eventZoneID));
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page,ex.Message);
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvEventZone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool editAccess = MySession.Accessable(FunctionIdList.EventManagement.EditEventZone);
            bool delAccess = MySession.Accessable(FunctionIdList.EventManagement.DeleteEventZone);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSEventZone.TB_EVENT_ZONERow)((DataRowView)e.Row.DataItem).Row;
                //数据绑定
                var eventItemID = this.gdvEventZone.DataKeys[e.Row.RowIndex].Value.ToString();
                var hlEdit = (e.Row.FindControl("hlEdit") as LinkButton);
                var hlDelete = (e.Row.FindControl("hlDelete") as LinkButton);
                var param_Zone_Id = bindRow.IsPARAM_ZONE_IDNull() ? -1 : bindRow.PARAM_ZONE_ID;
                var param_Zone_Name = bindRow.IsZONE_NAMENull() ? string.Empty : bindRow.ZONE_NAME;
                hlEdit.CommandArgument = string.Format("{0},{1},{2},{3},{4},{5},{6}", bindRow.EVENTITEM_ID, bindRow.EVENTZONE_ID, bindRow.EVENTZONE_NAME, bindRow.EVENTZONE_NAME_EN, bindRow.EVENTZONE_DESC, param_Zone_Id, param_Zone_Name);
                hlDelete.CommandArgument = eventItemID;
                hlDelete.OnClientClick = string.Format(_CommandScript, "删除");
                //权限处理
                hlEdit.Visible = editAccess;
                hlDelete.Visible = delAccess;
            }
        }

        private const int RootID = 0;
        DSParamZone paramZoneDS = null;
        private void BindTreeView()
        {
            paramZoneDS = ParamZoneManager.QueryParamZone();
            tvParamZone.Nodes.Clear();
            IntiTreeNode(tvParamZone.Nodes, RootID);
            tvParamZone.ExpandDepth = 1;
            tvParamZone.ExpandAll();
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
                node = new TreeNode();
                node.Value = rv["ZONE_ID"].ToString();
                node.Text = rv["ZONE_NAME"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                nodes.Add(node);
                if (rv["PARENT_ZONE_ID"].ToString() == "0")
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
    }
}