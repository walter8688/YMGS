using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.MemberShip;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class HandleAgentFrm : MemberShipBasePage
    {
        private static string handleType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("AgentManagePageTitle");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            int roleID = CurrentUser.RoleId;
            bool falg = roleID == (int)SysAccountTypeEnum.GeneralAgent ? true : false;
            return base.IsAllow(FunctionIdList.MemberCenter.AgentManagePage) & falg;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(HandleAgentFrm), "MemberShip").AbsoluteUri;
        }

        public void GrowMember_Click(object sender, EventArgs e)
        {
            Response.Redirect(AgentAccountFrm.Url());
        }

        public void SetAgentDeatail_Click(object sender, EventArgs e)
        {
            Response.Redirect(SetAgentDeatailFrm.Url());
        }

        private void LoadPageData()
        {
            this.drpHandleType.Items.Clear();
            this.drpHandleType.Items.Add(new ListItem(LangManager.GetString("Member"), SysAccountTypeEnum.Member.ToString()));
            this.drpHandleType.Items.Add(new ListItem(LangManager.GetString("Agent"), SysAccountTypeEnum.Agent.ToString()));
        }

        private void LoadGridData()
        {
            DSAccount accountList = null;
            var userId = CurrentUser.UserId;
            handleType = this.drpHandleType.SelectedValue;
            if (handleType == SysAccountTypeEnum.Member.ToString())
                accountList = AgentAccountManager.QueryMembersByGeneralAgentId(userId);
            else
                accountList = AgentAccountManager.QueryAgentByGeneralId(userId);
            this.pageNavigator.databinds(accountList.Tables[0], this.gdvAgent);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }
        protected void gdv_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblRoleName = e.Row.FindControl("lblRoleName") as Label;
                lblRoleName.Text = handleType == SysAccountTypeEnum.Member.ToString() ? LangManager.GetString("Member") : LangManager.GetString("Agent");
                var hlEdit = e.Row.FindControl("hlEdit") as LinkButton;
                hlEdit.Text = handleType == SysAccountTypeEnum.Member.ToString() ? LangManager.GetString("SetAgent") : LangManager.GetString("CancleAgent");
                if (handleType == SysAccountTypeEnum.Agent.ToString())
                    hlEdit.Visible = false;
                var userId = this.gdvAgent.DataKeys[e.Row.RowIndex].Value.ToString();
                hlEdit.CommandArgument = userId;
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                var agentId = CurrentUser.UserId;
                var userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                if (handleType == SysAccountTypeEnum.Member.ToString())
                    AgentAccountManager.SetMemberAsAgent(agentId, userId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }
    }
}