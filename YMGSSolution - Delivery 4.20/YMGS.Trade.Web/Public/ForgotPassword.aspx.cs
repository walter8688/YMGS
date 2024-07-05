using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;
using YMGS.Framework;
using System.Configuration;

namespace YMGS.Trade.Web.Public
{
    public partial class ForgotPassword : SimplePageBase
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("forgetpswTitle");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ForgotPassword), "Public").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    SetVisiable(true);
                    return;
                }

                int userid = 0;
                try
                {
                    userid = int.Parse(id);
                }
                catch { }


                DSSystemAccount sa = SystemSettingManager.QueryData("", userid,"");
                if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
                {
                    string vc = Request.QueryString["vc"];
                    if (string.IsNullOrEmpty(vc))
                    {
                        SetVisiable(true);
                        return;
                    }

                    DSSystemAccount.TB_SYSTEM_ACCOUNTRow row = sa.TB_SYSTEM_ACCOUNT[0];
                    string str = EncryptManager.GetEncryString((row.USER_ID + 173).ToString());
                    if (vc == str)
                    {
                        SetVisiable(false);
                        LOGIN_NAME.Enabled = false;
                        LOGIN_NAME.Text = row.LOGIN_NAME;
                    }
                    else
                    { SetVisiable(true); }
                }
                else
                {
                    SetVisiable(true);
                }
            }
        }

        public void SetVisiable(bool flag)
        {
          lblanswer.Visible=lblquestion.Visible=lblastar.Visible=  SANSWER1.Visible = SQUESTION1.Visible = btnok.Visible = flag;
          lblpsw.Visible=lblpstar.Visible=lblcpsw.Visible=lblscpstar.Visible=  txtcpassword.Visible = PASSWORD.Visible = btnreset.Visible = !btnok.Visible;
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            DSSystemAccount sa = SystemSettingManager.QueryData(LOGIN_NAME.Text, 0,"");

            if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
            {
                if (sa.TB_SYSTEM_ACCOUNT[0].SANSWER1 != SANSWER1.Text || sa.TB_SYSTEM_ACCOUNT[0].SQUESTION1 != SQUESTION1.SelectedIndex + 1)
                {
                    PageHelper.ShowMessage(this.Page, LangManager.GetString("wronganswer"));
                    SANSWER1.Focus();
                    return;
                }
                int userid = sa.TB_SYSTEM_ACCOUNT[0].USER_ID + 173;
                string str = EncryptManager.GetEncryString(userid.ToString());
                string url = string.Format("{0}?id={1}&vc={2}", HttpContext.Current.Request.Url.ToString(), sa.TB_SYSTEM_ACCOUNT[0].USER_ID, str);
                var mailFrom = ConfigurationManager.AppSettings[CommConstant.SmtpFromUserKey];
                NotificationMail mailInfo = new NotificationMail();
                mailInfo.Body = string.Format(LangManager.GetString("resetpswbody"), sa.TB_SYSTEM_ACCOUNT[0].USER_NAME, url);
                mailInfo.To = sa.TB_SYSTEM_ACCOUNT[0].EMAIL_ADDRESS;
                mailInfo.Subject =LangManager.GetString("resetpswsubject");

                MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
                PageHelper.ShowMessage(this.Page,  LangManager.GetString("resetyourpassword"));
                return;
            }
            else
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("notexistuser"));
                return;
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            if (txtcpassword.Text != PASSWORD.Text)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("passwordnotsame"));
                txtcpassword.Focus();
                return;
            }
            string psw = EncryptManager.GetEncryString(PASSWORD.Text);
            string id = Request.QueryString["id"];
            DSSystemAccount sa = SystemSettingManager.QueryData(LOGIN_NAME.Text, 0,"");
            if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
            {

                sa.TB_SYSTEM_ACCOUNT[0].PASSWORD = psw;
                int result = SystemSettingManager.UpdateSystemAccount(sa.TB_SYSTEM_ACCOUNT[0]);
                if (result < 0)
                {
                    PageHelper.ShowMessage(this.Page, LangManager.GetString("resetpswerror"));
                    return;
                }
                PageHelper.ShowMessage(this.Page, LangManager.GetString("resetseccuss"));
                Response.Redirect("ResetPasswordResult.aspx");
            }
        }
    }
}