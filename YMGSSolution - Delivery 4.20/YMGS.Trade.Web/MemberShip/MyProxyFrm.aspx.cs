using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Trade.Web.Common;
using YMGS.Business.MemberShip;
using YMGS.Business.GameSettle;
using YMGS.Business.SystemSetting;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class MyProxyFrm : MemberShipBasePage
    {
        private const string _CommandCancle = "CommandCancle";

        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("MyApplyProxyManage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.MyProxyPage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(MyProxyFrm), "MemberShip").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDLL();
                LoadGridData();
            }
        }

        private void LoadGridData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value;
            else
                eDate = DateTime.MaxValue;
            int status = Convert.ToInt32(ddlApplyStatus.SelectedValue);
            pageNavigator.databinds(AccountApplyProxyManager.QueryAccountApplyProxyByUserID(sDate, eDate, status, CurrentUser.UserId).TB_APPLY_PROXY, gdvApplyProxy);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvApplyProxy_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSApplyProxy.TB_APPLY_PROXYRow)((DataRowView)e.Row.DataItem).Row;

                var lblRoleLevel = e.Row.FindControl("lblRoleLevel") as Label;
                var lblStatus = e.Row.FindControl("lblStatus") as Label;
                var lblApplyDate = e.Row.FindControl("lblApplyDate") as Label;
                var btnCancleApply = e.Row.FindControl("btnCancleApply") as LinkButton;

                lblRoleLevel.Text = GetAgentNameByID(bindRow.Role_ID);

                switch ((ApplyAgentStatus)bindRow.Apply_Status)
                {
                    case ApplyAgentStatus.Appyling:
                        lblStatus.Text = LangManager.GetString("Appyling");
                        break;
                    case ApplyAgentStatus.ApprovalProcess:
                        lblStatus.Text = LangManager.GetString("ApprovalProcess");
                        break;
                    case ApplyAgentStatus.Confirmed:
                        lblStatus.Text = LangManager.GetString("Confirmed");
                        break;
                    case ApplyAgentStatus.Rejected:
                        lblStatus.Text = LangManager.GetString("Rejected");
                        break;
                    case ApplyAgentStatus.Canceled:
                        lblStatus.Text = LangManager.GetString("Canceled");
                        break;
                }

                lblApplyDate.Text = UtilityHelper.DateToDateAndTimeStr(bindRow.Apply_Date);

                btnCancleApply.Visible = bindRow.Apply_Status == (int)ApplyAgentStatus.Appyling ? true : false;
                btnCancleApply.CommandName = _CommandCancle;
                btnCancleApply.CommandArgument = bindRow.Apply_Proxy_ID.ToString();
            }
        }

        protected void gdvApplyProxy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == _CommandCancle)
            {
                var userApplyProxyID = Convert.ToInt32(e.CommandArgument);
                CancleUserApplyProxy(userApplyProxyID);
                return;
            }
        }

        /// <summary>
        /// 用户取消代理申请
        /// </summary>
        /// <param name="userWDId"></param>
        private void CancleUserApplyProxy(int userApplyProxyID)
        {
            try
            {
                AccountApplyProxyManager.CancelUserApplyProxy(userApplyProxyID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnAddApplyProxy_Click(object sender, EventArgs e)
        {
            ClearData();
            mdlApplyProxyPopup.Show();
        }

        protected void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            mdlApplyProxyPopup.Show();
            if (chkAgree.Checked)
            {
                btnToApplyProxy.Enabled = true;
            }
            else
            {
                btnToApplyProxy.Enabled = false;
            }
        }

        protected void btnToApplyProxy_Click(object sender, EventArgs e)
        {
            try
            {
                var newDataRow = GetUserAPRow();
                AccountApplyProxyManager.AddAccountApplyProxy(newDataRow);
                LoadGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        private DSApplyProxy.TB_APPLY_PROXYRow GetUserAPRow()
        {
            DSApplyProxy.TB_APPLY_PROXYRow newRow = new DSApplyProxy.TB_APPLY_PROXYDataTable().NewTB_APPLY_PROXYRow();
            newRow.User_ID = CurrentUser.UserId;
            newRow.User_Telephone = txtUser_Telephone.Text;
            newRow.User_Country = txtUser_Country.Text;
            newRow.User_Province = txtUser_Province.Text;
            newRow.User_City = txtUser_City.Text;
            newRow.User_BankAddress = txtUser_BankAddress.Text;
            newRow.User_BankNO = txtUser_BankNO.Text;

            if (ddlProxyData.SelectedValue != "-1")
            {
                newRow.Role_ID = Convert.ToInt32(ddlProxyData.SelectedValue);
            }
            else
            {
                newRow.Role_ID = 2;
            }
            newRow.Apply_Status = (int)ApplyAgentStatus.Appyling;
            newRow.Apply_Date = GlobalBrManager.QueryCurrentDateTime();
            return newRow;
        }

        private DateTime GetBirth()
        {
            //string monthStr = "1";
            //string dayStr = "1";
            //string yearStr = ddlyear.SelectedValue;
            //if (ddlmonth.SelectedValue != "")
            //{
            //    monthStr = ddlmonth.SelectedValue;
            //}
            //if (ddlday.SelectedValue != "")
            //{
            //    dayStr = ddlday.SelectedValue;
            //}
            //string birthStr = string.Format("{0}-{1}-{2}", yearStr, monthStr, dayStr);
            //return Convert.ToDateTime(birthStr);
            return DateTime.Now;
        }

        private void BindDLL()
        {
            BindApplyStatus();
            //BindYear();
            BindLargeProxyData();
        }

        private void BindApplyStatus()
        {
            PageHelper.BindListControlData(ddlApplyStatus, GetAllUserApplyProxyStatusLst(), "ApplyProxyStatusName", "ApplyProxyStatusID", true);
        }

        /// <summary>
        /// 获取用户提现状态
        /// </summary>
        /// <returns></returns>
        private IList<UserApplyProxyStatusInfo> GetAllUserApplyProxyStatusLst()
        {
            var userApplyProxyStatusList = new List<UserApplyProxyStatusInfo>()
            {
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = LangManager.GetString("Appyling"), ApplyProxyStatusID = (int)ApplyAgentStatus.Appyling },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = LangManager.GetString("ApprovalProcess"), ApplyProxyStatusID = (int)ApplyAgentStatus.ApprovalProcess },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = LangManager.GetString("Confirmed"), ApplyProxyStatusID = (int)ApplyAgentStatus.Confirmed },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = LangManager.GetString("Rejected"), ApplyProxyStatusID = (int)ApplyAgentStatus.Rejected },
                new UserApplyProxyStatusInfo(){ ApplyProxyStatusName = LangManager.GetString("Canceled"), ApplyProxyStatusID = (int)ApplyAgentStatus.Canceled }
            };
            return userApplyProxyStatusList;
        }

        private void BindYear()
        {
            //for (int year = 1900; year <= GlobalBrManager.QueryCurrentDateTime().Year; year++)
            //{
            //    ddlyear.Items.Add(year.ToString());
            //}
        }

        private void BindLargeProxyData()
        {
            var agentTypeList = GetAgentData();
            PageHelper.BindListControlData(this.ddlProxyData, agentTypeList, "ROLE_NAME", "ROLE_ID", true);
        }

        private DataTable GetAgentData()
        {
            DataTable agentTypeList = AgentManager.GetAllAgentType().Tables[0];
            int totalCount = agentTypeList.Rows.Count;
            agentTypeList.Rows.RemoveAt(totalCount - 1);
            for (int i = 0; i < agentTypeList.Rows.Count; i++)
            {
                agentTypeList.Rows[i]["ROLE_NAME"] = LangManager.GetString("Agent" + (i + 1).ToString());
            }

            return agentTypeList;
        }

        private string GetAgentNameByID(int roleID)
        {
            DataTable agentTypeList = GetAgentData();
            DataRow[] agentRow = agentTypeList.Select("ROLE_ID =" + roleID);
            if (agentRow.Length > 0)
            {
                return agentRow[0]["ROLE_NAME"].ToString();
            }
            else
            {
                return "";
            }
        }

        private void ClearData()
        {
            txtUser_Telephone.Text = txtUser_Country.Text = txtUser_Province.Text =
                txtUser_City.Text = txtUser_BankAddress.Text = txtUser_BankNO.Text = "";
            ddlProxyData.SelectedIndex = 0;
            chkAgree.Checked = false;
            btnToApplyProxy.Enabled = false;
        }

    }
}