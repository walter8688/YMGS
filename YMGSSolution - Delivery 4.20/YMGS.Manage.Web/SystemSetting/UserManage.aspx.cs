using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using System.Data;
using YMGS.Business.MemberShip;

namespace YMGS.Manage.Web.SystemSetting
{
    [LeftMenuId(FunctionIdList.SystemManagement.UserAccountPage)]
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    public partial class UserManage : QueryBasePage
    {
        public static ListItem removedItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(RoleManageFrm), "SystemSetting").AbsoluteUri;
        }
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.UserAccountPage;
            }
        }
        protected override DataTable GetData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value.AddDays(1);
            else
                eDate = DateTime.MaxValue;
            return SystemSettingManager.QueryAccount(txtUserName.Text, sDate, eDate).TB_Account;
        }

        public int CurUserStatus
        {
            get
            {
                if (ViewState["Status"] == null)
                    return 0;
                return (int)ViewState["Status"];
            }
            set
            {
                ViewState["Status"] = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (ViewState["IsAdmin"] == null)
                    return false;
                return (bool)ViewState["IsAdmin"];
            }
            set
            {
                ViewState["IsAdmin"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                bindRole();
                BindGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton)
            {
                ddlstatus.Visible = MySession.Accessable(FunctionIdList.SystemManagement.LockUserAccount);
                ddlRole.Visible = MySession.Accessable(FunctionIdList.SystemManagement.SetUserRole);
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                txtuserID.Text = commandString;
                DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = SystemSettingManager.QueryData("", int.Parse(commandString), "").TB_SYSTEM_ACCOUNT[0];
                //管理员的角色/状态不能修改
                if (obj.USER_ID == 0 || obj.USER_NAME.ToUpper() == "ADMIN" || obj.LOGIN_NAME == "ADMIN")
                    IsAdmin = true;
                else
                    IsAdmin = false;
                User_NAME.Text = obj.LOGIN_NAME;
                int status=obj.ACCOUNT_STATUS;
                CurUserStatus = status;
                if (status == 0)
                {
                    ddlstatus.Enabled = false;
                }
                else
                {
                    ddlstatus.Enabled = true && !IsAdmin;
                    ddlstatus.SelectedValue = status.ToString();
                }

                ddlstatus.Enabled = !IsAdmin;
                ddlRole.Enabled = !IsAdmin;

                if (ddlRole != null)
                {
                    if (obj.ROLE_ID == 3 && !ddlRole.Items.Contains(removedItem))
                        ddlRole.Items.Insert(3, removedItem);
                    else
                        ddlRole.Items.Remove(removedItem);
                }

                ddlRole.SelectedValue = obj.ROLE_ID.ToString();
                this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                mdlPopup.Show();
            }
        }

        protected void btnEditFund_Click(object sender, EventArgs e)
        {
            var userName = (sender as LinkButton).CommandName;
            var userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            var userFund = UserFundManager.QueryUserFund(userId).TB_USER_FUND[0];
            txtCurUserFund.Text = userFund.CUR_FUND.ToString();
            txtCurUserName.Text = userName;
            txtuserID.Text = userId.ToString();
            mdlFundPopup.Show();
        }

        protected void BtnFundSave_Click(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(txtuserID.Text);
            var modifiedUser = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            var userFund = Convert.ToDecimal( txtCurUserFund.Text);
            SystemSettingManager.SetUserFund(userId, modifiedUser, userFund);
            BindGrid();
        }

        private string GetName(string id, int cellIndex)
        {
            foreach (GridViewRow item in gdvMain.Rows)
            {
                if (item.Cells[0].Text == id)
                    return item.Cells[cellIndex].Text.Replace("&nbsp;", "");
            }
            return "";
        }

        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[6].Text;
                if (status == "0")
                    status = "未激活";
                if (status == "1")
                    status = "活动";
                if (status == "2")
                    status = "锁定";
                e.Row.Cells[6].Text = status;
                var btnEditFund = e.Row.FindControl("btnEditFund") as LinkButton;
                btnEditFund.Visible = MySession.Accessable(FunctionIdList.SystemManagement.SetUserFund);
            }
        }

        private void bindRole()
        {
            ddlRole.DataTextField = "ROLE_NAME";
            ddlRole.DataValueField = "ROLE_ID";
            DSSystemRole ds = new DSSystemRole();
            DSSystemRole.TB_SYSTEM_ROLERow obj = ds.TB_SYSTEM_ROLE.NewRow() as DSSystemRole.TB_SYSTEM_ROLERow;
            obj.ROLE_ID = 0;
            obj.ROLE_NAME = "";
            ddlRole.DataSource = SystemSettingManager.QueryRole(obj).TB_SYSTEM_ROLE;
            ddlRole.DataBind();
            ListItem item = new ListItem("请选择", "0");
            ddlRole.Items.Insert(0, item);

            foreach (ListItem lt in ddlRole.Items)
            {
                if (lt.Value == "3")
                    removedItem = lt;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = SystemSettingManager.QueryData("", int.Parse(txtuserID.Text), "").TB_SYSTEM_ACCOUNT[0];

                obj.ROLE_ID = int.Parse(ddlRole.SelectedValue);
                if (CurUserStatus != 0)
                    obj.ACCOUNT_STATUS = int.Parse(ddlstatus.SelectedValue);
                else
                    obj.ACCOUNT_STATUS = CurUserStatus;
                SystemSettingManager.UpdateSystemAccount(obj);
                BindGrid();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
                return;
            }
        }


    }
}