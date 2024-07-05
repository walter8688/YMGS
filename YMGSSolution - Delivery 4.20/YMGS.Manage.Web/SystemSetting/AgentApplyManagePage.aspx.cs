using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.SystemSetting;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Data.Entity;

namespace YMGS.Manage.Web.SystemSetting
{
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    [LeftMenuId(FunctionIdList.SystemManagement.AgentApplyManagePage)]
    public partial class AgentApplyManagePage : BasePage
    {
        private readonly string _ApproveProcess = "ApproveProcess";
        private readonly string _Confirm = "Confirm";
        private readonly string _Reject = "Reject";

        public int ApplyProxyID
        {
            get
            {
                if (ViewState["ApplyProxyID"] == null)
                    return -1;
                return (int)ViewState["ApplyProxyID"];
            }
            set
            {
                ViewState["ApplyProxyID"] = value;
            }
        }

        public bool IsApproveProcess
        {
            get
            {
                ViewState["ApproveProcess"] = MySession.Accessable(FunctionIdList.SystemManagement.AgentApplyApproval);
                return (bool)ViewState["ApproveProcess"];
            }
        }

        public bool IsConfirmApply
        {
            get
            {
                ViewState["ConfirmApply"] = MySession.Accessable(FunctionIdList.SystemManagement.AgentApplyConfirm);
                return (bool)ViewState["ConfirmApply"];
            }
        }

        public bool IsRejectApply
        {
            get
            {
                ViewState["RejectApply"] = MySession.Accessable(FunctionIdList.SystemManagement.AgentApplyReject);
                return (bool)ViewState["RejectApply"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.AgentApplyManagePage;
            }
        }

        private IList<UserApplyProxyStatusInfo> GetAllUserApplyProxyStatusLst()
        {
            var userApplyProxyStatusList = new List<UserApplyProxyStatusInfo>()
            {
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = "申请中", ApplyProxyStatusID = (int)ApplyAgentStatus.Appyling },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = "审批中", ApplyProxyStatusID = (int)ApplyAgentStatus.ApprovalProcess },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = "已批准", ApplyProxyStatusID = (int)ApplyAgentStatus.Confirmed },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = "已拒绝", ApplyProxyStatusID = (int)ApplyAgentStatus.Rejected },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = "已取消", ApplyProxyStatusID = (int)ApplyAgentStatus.Canceled }
            };
            return userApplyProxyStatusList;
        }

        private void LoadPageData()
        {
            var agentTypeDS = AgentManager.GetAllAgentType();
            PageHelper.BindListControlData(drpRoleType, agentTypeDS, "ROLE_NAME", "ROLE_ID", true);
            PageHelper.BindListControlData(drpApplyStatus, GetAllUserApplyProxyStatusLst(), "ApplyProxyStatusName", "ApplyProxyStatusID", true);
            if(drpRoleType.Items.Count == 7)
                drpRoleType.Items.RemoveAt(6);

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
            int? roleId = Convert.ToInt32(drpRoleType.SelectedValue);
            var userName = txtUserName.Text.Trim();
            int applyStatus = Convert.ToInt32(drpApplyStatus.SelectedValue);
            var applyAgentDS = AgentManager.QueryApplyAgentDeatail(roleId, sDate, eDate, userName, applyStatus);
            pageNavigator.databinds(applyAgentDS.TB_APPLY_AGENT, gdv);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSApplyAgent.TB_APPLY_AGENTRow)((DataRowView)e.Row.DataItem).Row;
                var btnApproveProcess = e.Row.FindControl("btnApproveProcess") as LinkButton;
                var btnConfirm = e.Row.FindControl("btnConfirm") as LinkButton;
                var btnReject = e.Row.FindControl("btnReject") as LinkButton;
                btnApproveProcess.CommandArgument = btnConfirm.CommandArgument = btnReject.CommandArgument = bindRow.Apply_Proxy_ID.ToString();
                btnApproveProcess.CommandName = _ApproveProcess;
                btnConfirm.CommandName = _Confirm;
                btnReject.CommandName = _Reject;

                //权限
                btnApproveProcess.Visible = IsApproveProcess;
                btnReject.Visible = IsRejectApply;

                var lblApplyDate = e.Row.FindControl("lblApplyDate") as Label;
                lblApplyDate.Text = UtilityHelper.DateToStr(bindRow.Apply_Date);
                var lblApplyStatus = e.Row.FindControl("lblApplyStatus") as Label;
                switch ((ApplyAgentStatus)bindRow.Apply_Status)
                {
                    case ApplyAgentStatus.Appyling:
                        lblApplyStatus.Text = "申请中";
                        btnConfirm.Visible = false;
                        break;
                    case ApplyAgentStatus.ApprovalProcess:
                        lblApplyStatus.Text = "审批中";
                        btnApproveProcess.Visible = false;
                        btnConfirm.Visible = IsConfirmApply && true;
                        break;
                    case ApplyAgentStatus.Confirmed:
                        lblApplyStatus.Text = "已批准";
                        btnReject.Visible = btnConfirm.Visible = btnApproveProcess.Visible = false;
                        break;
                    case ApplyAgentStatus.Rejected:
                        lblApplyStatus.Text = "已拒绝";
                        btnReject.Visible = btnConfirm.Visible = btnApproveProcess.Visible = false;
                        break;
                    case ApplyAgentStatus.Canceled:
                        lblApplyStatus.Text = "已取消";
                        btnReject.Visible = btnConfirm.Visible = btnApproveProcess.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void gdv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ApplyProxyID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == _ApproveProcess)
            {
                ApproveProcess(ApplyProxyID);
            }
            if (e.CommandName == _Confirm)
            {
                Confirm(ApplyProxyID);
            }
            if (e.CommandName == _Reject)
            {
                Reject(ApplyProxyID);
            }
        }

        private void ApproveProcess(int EditKey)
        {
            try
            {
                AgentManager.ApproveProcessApplyProxy(EditKey);
                SendSystemMessageAboutApplyProxyStatusToUser(EditKey);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        private void Confirm(int EditKey)
        {
            try
            {
                AgentManager.ConfirmApplyProxy(EditKey);
                SendSystemMessageAboutApplyProxyStatusToUser(EditKey);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        private void Reject(int EditKey)
        {
            try
            {
                AgentManager.RejectApplyProxy(EditKey);
                SendSystemMessageAboutApplyProxyStatusToUser(EditKey);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
        /// <summary>
        /// 将状态更改后的消息发送给用户
        /// </summary>
        /// <param name="Apply_Proxy_ID"></param>
        private void SendSystemMessageAboutApplyProxyStatusToUser(int Apply_Proxy_ID)
        {
            DataTable dtDetails = AgentManager.GetApplyProxyDetailsByAPID(Apply_Proxy_ID).Tables[0];
            if (dtDetails.Rows.Count > 0)
            {
                string CnMessgae = string.Empty;
                string EnMessge = string.Empty;
                CnMessgae = string.Format("您在 {0}申请的{1},状态变为{2}", dtDetails.Rows[0]["Apply_DateCon"].ToString(),
                    dtDetails.Rows[0]["CnRoleName"].ToString(), dtDetails.Rows[0]["CnStatus"].ToString());
                EnMessge = string.Format("Your application status about apply for {0} has changed to {1}, which you applyed at {2}.", dtDetails.Rows[0]["EnRoleName"].ToString(),
                    dtDetails.Rows[0]["EnStatus"].ToString(), dtDetails.Rows[0]["Apply_DateCon"].ToString());
                
                DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow msgRow = new DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGEDataTable().NewTB_SYSTEM_AUTOMESSAGERow();
                msgRow.MESSAGE_CONTENT = CnMessgae;
                msgRow.MESSAGE_CONTENT_EN = EnMessge;
                msgRow.SENDTO_USERID = Convert.ToInt32(dtDetails.Rows[0]["User_ID"]);
                msgRow.SENDBY_SYSTEMID = 1;

                PageHelper.SendSystemMessageToUser(msgRow);
            }
        }
    }
}