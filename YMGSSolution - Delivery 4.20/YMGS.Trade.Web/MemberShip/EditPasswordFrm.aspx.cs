using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.SystemSetting;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Configuration;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class EditPasswordFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("MemberHomePage");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.UserInfoManagePage);
        }

        public static string Url()
        {

            return UrlHelper.BuildUrl(typeof(EditPasswordFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mark = Request.QueryString["mark"];


                string id = Request.QueryString["id"];
                hfdid.Value = id;
                string v = Request.QueryString["v"];
                string vid = id + "editPsw";
                if (EncryptManager.GetEncryString(vid) == v)
                {
                    string editpswMark = id + "sendEmail";
                    if (EncryptManager.GetEncryString(editpswMark) == mark)
                    {

                        string activeUrl = Url() + "?id=" + id + "&v=" + EncryptManager.GetEncryString(vid) + "&mark=" + EncryptManager.GetEncryString(id + "editpassword");
                        DSSystemAccount sa = SystemSettingManager.QueryData("", int.Parse(id), "");
                        DSSystemAccount.TB_SYSTEM_ACCOUNTRow row = null;
                        if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
                        {
                            row = sa.TB_SYSTEM_ACCOUNT[0];
                        }
                        else
                        {
                            Response.Redirect(Default.Url());
                        }

                        var mailFrom = ConfigurationManager.AppSettings[CommConstant.SmtpFromUserKey];
                        NotificationMail mailInfo = new NotificationMail();
                        mailInfo.Body = string.Format(LangManager.GetString("editpasswordbody"), row.USER_NAME, activeUrl);
                        mailInfo.To = row.EMAIL_ADDRESS;
                        mailInfo.Subject = LangManager.GetString("editpasswordsubject");

                        MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
                        this.pnlresult.Visible = true;
                        this.lblEditEmailSuccess.Visible = true;
                        return;
                    }
                    editpswMark = id + "result";
                    if (EncryptManager.GetEncryString(editpswMark) == mark)
                    {
                        this.pnlresult.Visible = true;
                        this.lbltips.Visible = true;
                        return;
                    }
                    editpswMark = id + "editpassword";
                    if (EncryptManager.GetEncryString(editpswMark) == mark)
                    {
                        this.pnleditpassword.Visible = true;
                        return;
                    }


                }

            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            DSSystemAccount sa = SystemSettingManager.QueryData("", int.Parse(hfdid.Value), "");
            DSSystemAccount.TB_SYSTEM_ACCOUNTRow row = null;
            if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
            {
                row = sa.TB_SYSTEM_ACCOUNT[0];
            }
            else
            {
                Response.Redirect(Default.Url());
            }
            if (txtcpassword.Text != PASSWORD.Text)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("passwordnotsame"));
                txtcpassword.Focus();
                return;
            }

            if (row.PASSWORD != EncryptManager.GetEncryString(txtoldpsw.Text))
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("oldpassworderror"));
                txtcpassword.Focus();
                return;
            }
            row.PASSWORD = EncryptManager.GetEncryString(PASSWORD.Text);
            int result = SystemSettingManager.UpdateSystemAccount(row);
            string vid = hfdid.Value + "editPsw";
            if (result >= 0)
                Response.Redirect(Url() + "?id=" + hfdid.Value + "&v=" + EncryptManager.GetEncryString(vid) + "&mark=" + EncryptManager.GetEncryString(hfdid.Value + "result"));
        }
    }
}