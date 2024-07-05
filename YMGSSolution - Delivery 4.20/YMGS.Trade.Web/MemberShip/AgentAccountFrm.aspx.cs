using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Trade.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Business.SystemSetting;
using System.Configuration;
using YMGS.Trade.Web.Public;
using YMGS.Business.MemberShip;
using System.Data;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class AgentAccountFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("AgentManagePageTitle");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            var bIsAllow = base.IsAllow(FunctionIdList.MemberCenter.AgentManagePage);
            if (bIsAllow)
            {
                int roleID = CurrentUser.RoleId;
                var generaAgentDS = AgentManager.GetAllAgentType();
                var flag = false;
                foreach (DataRow row in generaAgentDS.Tables[0].Rows)
                {
                    if (Convert.ToInt32(row["ROLE_ID"]) == roleID)
                    {
                        flag = true;
                        break;
                    }
                }
                this.btnHandleAgent.Visible = this.btnSetAgentDeatail.Visible = flag;
            }
            return bIsAllow;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(AgentAccountFrm), "MemberShip").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            if (!IsPostBack)
            {
                LoadGridData();
            }
        }

        private void LoadGridData()
        {
            var memberList = AgentAccountManager.QueryMembersByAgentId(CurrentUser.UserId);
            this.pageNavigator.databinds(memberList.Tables[0], this.gdvGrowMember);
        }

        private void SendGrowMail(int userId)
        {
            try
            {
                DSSystemAccount sa = SystemSettingManager.QueryData("", userId, "");
                if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
                {
                    DSSystemAccount.TB_SYSTEM_ACCOUNTRow row = sa.TB_SYSTEM_ACCOUNT[0];
                    if (row.ACCOUNT_STATUS == 0)//若已激活则不再发送此邮件
                    {
                        string activeUrl = UserRegisterFrm.Url(EncryptManager.DESEnCrypt(userId.ToString()));
                        var mailFrom = ConfigurationManager.AppSettings[CommConstant.SmtpFromUserKey];
                        NotificationMail mailInfo = new NotificationMail();
                        mailInfo.Body = string.Format(LangManager.GetString("GrowMemberRegisterMailTemp"), row.USER_NAME, activeUrl);
                        mailInfo.To = row.EMAIL_ADDRESS;
                        mailInfo.Subject = LangManager.GetString("GrowMemberRegisterSubject");
                        MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
                        lblGrowSuccess.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblGrowFail.Visible = true;
            }
        }

        public void GrowMember_Click(object sender, EventArgs e)
        {
            try
            {
                //1.检查发展会员数是否超过限制
                AgentAccountManager.CheckGrowMemberCount(CurrentUser.UserId);
                //2.新增账户
                var sysAcount = new DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable().NewTB_SYSTEM_ACCOUNTRow();
                sysAcount.USER_NAME = this.txtGrowMemberAccout.Text;
                sysAcount.LOGIN_NAME = this.txtGrowMemberAccout.Text;
                sysAcount.PASSWORD = string.Empty;
                sysAcount.EMAIL_ADDRESS = this.txtGrowMemberEmail.Text;
                sysAcount.ACCOUNT_STATUS = (int)SysAccountStatusEnum.UnActivated;
                sysAcount.ROLE_ID = (int)SysAccountTypeEnum.Member;
                sysAcount.AGENT_ID = CurrentUser.UserId;
                int result = SystemSettingManager.SaveSystemAccount(sysAcount);
                if (result > 0)
                {
                    SendGrowMail(result);
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessageByScriptManager(Page, ex.Message);
            }
            //var url = AgentAccountFrm.Url(result.ToString());
            //Response.Redirect(url);
        }

        protected void gdvGrowMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSAccount.TB_AccountRow)((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[2].Text = bindRow.ACCOUNT_STATUS == (int)SysAccountStatusEnum.Activated ? LangManager.GetString("Activated") : LangManager.GetString("UnActivated");
                var lblReSend = e.Row.FindControl("hlResendMail") as LinkButton;
                lblReSend.CommandArgument = string.Format("{0}", bindRow.USER_ID);
                if (bindRow.ACCOUNT_STATUS == (int)SysAccountStatusEnum.Activated)
                    lblReSend.Visible = false;
            }
        }

        protected void ResendMail_Click(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SendGrowMail(userId);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        public void HandleAgent_Click(object sender, EventArgs e)
        {
            Response.Redirect(HandleAgentFrm.Url());
        }

        public void SetAgentDeatail_Click(object sender, EventArgs e)
        {
            Response.Redirect(SetAgentDeatailFrm.Url());
        }
    }
}