using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.MemberShip;
using YMGS.Data.Presentation;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class SetAgentDeatailFrm : MemberShipBasePage
    {
        private const string NotSet = "未设";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
            if (!IsPostBack)
            {
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
            return base.IsAllow(FunctionIdList.MemberCenter.AgentManagePage) && falg;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(SetAgentDeatailFrm), "MemberShip").AbsoluteUri;
        }

        private void LoadGridData()
        {
            int userID = CurrentUser.UserId;
            var agentDetail = AgentAccountManager.QueryAgentDetailByGeneralAgentId(userID);
            this.pageNavigator.databinds(agentDetail.Tables[0], this.gdvAgent);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void GrowMember_Click(object sender, EventArgs e)
        {
            Response.Redirect(AgentAccountFrm.Url());
        }

        protected void HandleAgent_Click(object sender, EventArgs e)
        {
            Response.Redirect(HandleAgentFrm.Url());
        }

        protected void gdvAgent_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSAgentList.DSAgentListRow)((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = LangManager.GetString("Agent");
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
                lbEdit.CommandArgument = string.Format("{0},{1},{2},{3},{4}", bindRow.USER_ID,  LangManager.GetString("Agent"), bindRow.USER_NAME, tempBrokerage, bindRow.Member_Count);
            }
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
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "')", true);
            }
        }
    }
}