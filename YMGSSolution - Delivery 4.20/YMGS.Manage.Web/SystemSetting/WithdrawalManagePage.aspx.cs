using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.MemberShip;


namespace YMGS.Manage.Web.SystemSetting
{
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    [LeftMenuId(FunctionIdList.SystemManagement.WithdrawalManagePage)]
    public partial class WithdrawalManagePage : BasePage
    {
        private const string _CommandConfirm = "CommandConfirm";
        private const string _CommandReject = "CommandReject";
        private const string _CommandTransfer = "CommandTransfer";
        private const string _Supply = "申请中";
        private const string _Comfirm = "已确认";
        private const string _Reject= "已拒绝";
        private const string _Transfer = "已转账";
        private const string _Cancle = "已取消";

        public bool[] ButtonAuthority
        {
            get
            {
                return new bool[]{MySession.Accessable(FunctionIdList.SystemManagement.ApproveWithdrawal),
                MySession.Accessable(FunctionIdList.SystemManagement.CompleteWithdrawal),
                MySession.Accessable(FunctionIdList.SystemManagement.RejectWithdrawl)};
            }
        }

        public int CurUserWDId
        {
            get
            {
                return (int)ViewState["UserWD"];
            }
            set
            {
                ViewState["UserWD"] = value;
            }
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.WithdrawalManagePage;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageInitData();
                LoadGridData();
            }
        }

        private void LoadPageInitData()
        {
            PageHelper.BindListControlData(drpWDStatus, CommonFunction.QuertAllUserWithDrawStatus(), "WDStatusName", "WDStatusID", true);
        }

        private void LoadGridData()
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
            decimal fromWDAmt = -1, toWDAmt = -1;
            var curWDUserRow = GetCurWDUserRow();
            var dsAccountWD = UserWithDrawManager.QueryAllUserWDInfo(sDate, eDate, fromWDAmt, toWDAmt, curWDUserRow);
            pageNavigator.databinds(dsAccountWD.TB_USER_WITHDRAW, gdvUserWithDraw);
        }

        private DSAccountWithDraw.TB_USER_WITHDRAWRow GetCurWDUserRow()
        {
            var curWDUserRow = new DSAccountWithDraw.TB_USER_WITHDRAWDataTable().NewTB_USER_WITHDRAWRow();
            curWDUserRow.LOGIN_NAME = txtUserName.Text.Trim();
            curWDUserRow.WD_STATUS = Convert.ToInt32(drpWDStatus.SelectedValue);
            curWDUserRow.WD_BANK_NAME = string.Empty;
            curWDUserRow.WD_CARD_NO = string.Empty;
            curWDUserRow.WD_ACCOUNT_HOLDER = string.Empty;
            curWDUserRow.TRANS_ID = string.Empty;
            return curWDUserRow;
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvWithDraw_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSAccountWithDraw.TB_USER_WITHDRAWRow)((DataRowView)e.Row.DataItem).Row;
                var lblWDStatus = e.Row.FindControl("lblWDStatus") as Label;
                var lblWDDate = e.Row.FindControl("lblWDDate") as Label;
                var btnConfirmWD = e.Row.FindControl("btnConfirmWD") as LinkButton;
                var btnRejectWD = e.Row.FindControl("btnRejectWD") as LinkButton;
                var btnTransferWD = e.Row.FindControl("btnTransferWD") as LinkButton;

                btnConfirmWD.Visible = false;
                btnTransferWD.Visible = false;
                btnRejectWD.Visible = false;
                switch ((UserWithDrawStatus)bindRow.WD_STATUS)
                {
                    case UserWithDrawStatus.Supplying:
                        lblWDStatus.Text = _Supply;
                        btnConfirmWD.Visible = true && ButtonAuthority[0];
                        btnTransferWD.Visible = true && ButtonAuthority[1];
                        btnRejectWD.Visible = true && ButtonAuthority[2];
                        break;
                    case UserWithDrawStatus.Confirmed:
                        lblWDStatus.Text = _Comfirm;
                        btnTransferWD.Visible = true && ButtonAuthority[1];
                        break;
                    case UserWithDrawStatus.Rejected:
                        lblWDStatus.Text = _Reject;
                        break;
                    case UserWithDrawStatus.Transfered:
                        lblWDStatus.Text = _Transfer;
                        break;
                    case UserWithDrawStatus.Cancled:
                        lblWDStatus.Text = _Cancle;
                        break;
                }

                lblWDDate.Text = UtilityHelper.DateToStr(bindRow.WD_DATE);

                var userWDId = bindRow.USER_WD_ID.ToString();
                btnConfirmWD.CommandArgument = btnRejectWD.CommandArgument = btnTransferWD.CommandArgument = userWDId;
                btnConfirmWD.CommandName = _CommandConfirm;
                btnRejectWD.CommandName = _CommandReject;
                btnTransferWD.CommandName = _CommandTransfer;
            }
        }

        protected void gdvWithDraw_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //确认提现
            if (e.CommandName == _CommandConfirm)
            {
                var userWDId = Convert.ToInt32(e.CommandArgument);
                ComfirmWithdraw(userWDId);
                return;
            }
            //拒绝提现
            if (e.CommandName == _CommandReject)
            {
                var userWDId = Convert.ToInt32(e.CommandArgument);
                RejectWithdraw(userWDId);
                return;
            }
            //转账提现
            if (e.CommandName == _CommandTransfer)
            {
                CurUserWDId = Convert.ToInt32(e.CommandArgument);
                txtTransId.Text = string.Empty;
                mdlPopup.Show();
                return;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            TransferWithdraw(CurUserWDId,txtTransId.Text);
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        /// <param name="userWDId"></param>
        private void ComfirmWithdraw(int userWDId)
        {
            try
            {
                UserWithDrawManager.ConfirmUserWithDraw(userWDId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page, ex.Message);
            }
        }

        /// <summary>
        /// 拒绝提现
        /// </summary>
        /// <param name="userWDId"></param>
        private void RejectWithdraw(int userWDId)
        {
            try
            {
                UserWithDrawManager.RejectUserWithDraw(userWDId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page, ex.Message);
            }
        }

        /// <summary>
        /// 转账提现
        /// </summary>
        /// <param name="userWDId"></param>
        private void TransferWithdraw(int userWDId,string transId)
        {
            try
            {
                UserWithDrawManager.TransferUserWithDraw(userWDId, transId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page, ex.Message);
            }
        }
    }
}