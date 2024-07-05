using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Public;
using YMGS.Trade.Web.Common;
using System.Web.Security;
using YMGS.Trade.Web.MemberShip;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Data.Entity;
using YMGS.Business.MemberShip;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeLoginCtrl : System.Web.UI.UserControl
    {
        private const string _DefaultUserNameCookie = "DefaultUserNameCookie";
        public string GrowMemberLoginName
        {
            get
            {
                if (Request.QueryString["L"] != null)
                    return EncryptManager.DESDeCrypt(Request.QueryString["L"].ToString());
                return null;
            }
        }

        public string GrowMemberPassword
        {
            get
            {
                if (Request.QueryString["W"] != null)
                    return EncryptManager.DESDeCrypt(Request.QueryString["W"].ToString());
                return null;
            }
        }

        private string DefaultUserName
        {
            get
            {
                if (Request.Cookies.AllKeys.Contains(_DefaultUserNameCookie))
                {
                    return Request.Cookies[_DefaultUserNameCookie].Value;
                }
                else
                    return string.Empty;
            }
            set
            {
                HttpCookie userNameCookie = new HttpCookie(_DefaultUserNameCookie);
                userNameCookie.Value = value;
                userNameCookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(userNameCookie);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hyRegister.NavigateUrl = UserRegisterFrm.Url();
                hlMyAccount.NavigateUrl = MemberShipHomeFrm.Url();
                hlmytrade.NavigateUrl = MyTradeFrm.Url();
                hlonlinecharge.NavigateUrl = OnlineChargeFrm.Url();
                hyForgetPwd.NavigateUrl = ForgotPassword.Url();
                DisplayUserInfoPanel();
                if (GrowMemberLoginName != null && GrowMemberPassword != null)
                    LoginSystem(GrowMemberLoginName, GrowMemberPassword);

                if (Session[CommonConstant.CurrentLoginUserSessionKey] != null)
                {
                    var curUser = (DetailUserInfo)Session[CommonConstant.CurrentLoginUserSessionKey];
                    var userId = curUser.UserId;
                    lblLoginName.Text = curUser.UserName;
                    var userFund = UserFundManager.QueryUserFund(userId).TB_USER_FUND[0];
                    lblCurFund.Text = userFund.CUR_FUND.ToString();
                }

                if (pnlLogin.Visible)
                {
                    txtUserName.Value = DefaultUserName;
                }
            }
        }

        private void DisplayUserInfoPanel()
        {
            if (PageHelper.GetCurrentUser() == null)
            {
                pnlLogin.Visible = true;
                pnlUserInfo.Visible = false;
            }
            else
            {
                //加载用户账号信息
                pnlLogin.Visible = false;
                pnlUserInfo.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginSystem(txtUserName.Value, txtPassword.Value);
        }

        private void LoginSystem(string loginName, string pwd)
        {
            DSSystemAccount dssa = SystemSettingManager.QueryData(loginName, 0, "");
            if (dssa.TB_SYSTEM_ACCOUNT.Count > 1)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("usernameexist"));
                return;
            }
            if (dssa.TB_SYSTEM_ACCOUNT.Count < 1)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("notexistuser"));
                return;
            }
            DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = dssa.TB_SYSTEM_ACCOUNT[0];
            if (string.IsNullOrEmpty(obj.PASSWORD))
            {
                string activeUrl = UserRegisterFrm.Url(EncryptManager.DESEnCrypt(obj.USER_ID.ToString()));
                Response.Redirect(activeUrl);
            }
            if (obj.ACCOUNT_STATUS == 2)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("AccountLockError"));
                return;
            }
            if (obj.ACCOUNT_STATUS == 0)
            {
                string activeUrl = ResendEmail.Url(obj.USER_ID);
                Response.Redirect(activeUrl);
            }
            if (obj.PASSWORD != EncryptManager.GetEncryString(pwd))
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("passworderror"));
                return;
            }

            DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable dsUserFuncMap = RoleFuncMapManager.QueryData(obj.ROLE_ID).TB_ROLE_FUNC_MAP;

            DetailUserInfo userInfo = new DetailUserInfo();
            userInfo.UserId = obj.USER_ID;
            userInfo.RoleId = obj.ROLE_ID;
            userInfo.UserName = txtUserName.Value;
            userInfo.UserFunctionList = dsUserFuncMap;

            Session[CommonConstant.CurrentLoginUserSessionKey] = userInfo;
            FormsAuthentication.SetAuthCookie(txtUserName.Value, false);

            var userFund = UserFundManager.QueryUserFund(obj.USER_ID).TB_USER_FUND[0];
            lblCurFund.Text = userFund.CUR_FUND.ToString();

            lblLoginName.Text = userInfo.UserName;
            DefaultUserName = userInfo.UserName;
            DisplayUserInfoPanel();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session[CommonConstant.CurrentLoginUserSessionKey] = null;
            FormsAuthentication.SignOut();
            Response.Redirect(Default.Url());
        }

        protected void lbtresentemail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Value))
                PageHelper.ShowMessage(this.Page, LangManager.GetString("LoginNamenotnull"));
            DSSystemAccount dssa = SystemSettingManager.QueryData(txtUserName.Value, 0, "");
            if (dssa.TB_SYSTEM_ACCOUNT.Count == 1)
            {
                string activeUrl = RegisterResult.Url(dssa.TB_SYSTEM_ACCOUNT[0].USER_ID.ToString());
                Response.Redirect(activeUrl);
            }
        }

        //public void Refresh()
        //{
        //    if (Session[CommonConstant.CurrentLoginUserSessionKey] != null)
        //    {
        //        var userId = ((DetailUserInfo)Session[CommonConstant.CurrentLoginUserSessionKey]).UserId;
        //        var userFund = UserFundManager.QueryUserFund(userId).TB_USER_FUND[0];
        //        lblCurFund.Text = userFund.CUR_FUND.ToString();
        //        updLoginForm.Update();
        //    }
        //}

        protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SystemAutoMessageFrm.Url());
        }
    }
}