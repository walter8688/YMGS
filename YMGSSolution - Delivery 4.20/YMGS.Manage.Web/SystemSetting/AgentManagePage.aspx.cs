using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Business.SystemSetting;
using YMGS.Manage.Web.Common;

namespace YMGS.Manage.Web.SystemSetting
{
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    [LeftMenuId(FunctionIdList.SystemManagement.AgentManagePage)]
    public partial class AgentManagePage : BasePage
    {
        private const string NotSet = "未设";
        private const string _CommandScript = "if(window.confirm('确定要{0}吗?{1}')) return true;else return false;";
        private static bool canSetAgent = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
            this.pageNavigatorMember.PageIndexChanged +=new EventHandler(pageNavigatorMember_PageIndexChanged);
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.AgentManagePage;
            }
        }

        private void LoadPageData()
        {
            //var agentTypeList = CommonFunction.QueryAllAgentType();
            var agentTypeList = AgentManager.GetAllAgentType();
            PageHelper.BindListControlData(this.drpAgentType, agentTypeList, "ROLE_NAME", "ROLE_ID", true);
            canSetAgent = MySession.Accessable(FunctionIdList.SystemManagement.ManageAgent);
        }

        private void LoadGridData()
        {
            var roleID = Convert.ToInt32(this.drpAgentType.SelectedValue);
            var userName = this.txtUserName.Text;
            var agentName = this.txtAgentName.Text;
            var agentList = AgentManager.QueryAgent(roleID, userName, agentName);
            this.pageNavigator.databinds(agentList.Tables[0], this.gdvAgent);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void pageNavigatorMember_PageIndexChanged(object sender, EventArgs e)
        {
            this.pageNavigatorMember.databinds(ViewState["agentMemList"] as DataTable, gdvMember);
            mdlPopupMember.Show();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void EditAgent_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            var args = (sender as LinkButton).CommandArgument.Split(',');
            this.hidTxtUserID.Text = args[0];
            this.txtAgentRole.Text = args[1];
            this.txtAgentUserName.Text = args[2];
            this.txtAgentBrokerage.Text = args[3];
            this.txtAgentMemberCount.Text = args[4];
        }

        protected void ViewMember_Click(object sender, EventArgs e)
        {
            mdlPopupMember.Show();
            var userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            var agentMemList = AgentManager.QueryAgentMember(userId);
            ViewState["agentMemList"] = agentMemList.Tables[0];
            this.pageNavigatorMember.databinds(agentMemList.Tables[0], gdvMember);
        }

        protected void CancleAgent_Click(object sender, EventArgs e)
        {
            try
            {
                var userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                AgentManager.CancleAgent(userId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DSAgentDetail.TB_AGENT_DETAILRow agentDetail = new DSAgentDetail().TB_AGENT_DETAIL.NewTB_AGENT_DETAILRow();
                agentDetail.Agent_User_ID = Convert.ToInt32(this.hidTxtUserID.Text);
                agentDetail.Brokerage = Convert.ToDecimal(this.txtAgentBrokerage.Text) / 100;
                agentDetail.Member_Count = Convert.ToInt32(this.txtAgentMemberCount.Text);
                AgentManager.SetAgentDetail(agentDetail);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void gdvAgent_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSAgentList.DSAgentListRow)((DataRowView)e.Row.DataItem).Row;
                var hlBelongMember = e.Row.FindControl("hlBelongMember") as LinkButton;
                var hlCancleAgent = e.Row.FindControl("hlCancleAgent") as LinkButton;
                hlBelongMember.CommandArgument = bindRow.USER_ID.ToString();
                hlCancleAgent.CommandArgument = bindRow.USER_ID.ToString();
                hlCancleAgent.OnClientClick = string.Format(_CommandScript, "取消代理","取消后会影响下属所有会员和代理!");
                var lblBrokerage = e.Row.FindControl("lblBrokerage") as Label;
                var tempBrokerage = "00.00";
                if (bindRow.Brokerage > 0)
                {
                    tempBrokerage = (bindRow.Brokerage * 100).ToString();
                    tempBrokerage = tempBrokerage.Substring(0, tempBrokerage.Length - 2);
                    lblBrokerage.Text = tempBrokerage + "%";
                }
                else
                {
                    lblBrokerage.Text = NotSet;
                }
                var lbEdit = e.Row.FindControl("hlEdit") as LinkButton;
                //id,type,name,brokerage,numbers
                lbEdit.Visible = canSetAgent;
                lbEdit.CommandArgument = string.Format("{0},{1},{2},{3},{4}", bindRow.USER_ID, bindRow.ROLE_NAME, bindRow.USER_NAME, tempBrokerage, bindRow.Member_Count);
            }
        }
    }
}