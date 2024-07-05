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
    public partial class EditSecuretyQuestionFrm : MemberShipBasePage
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
            return UrlHelper.BuildUrl(typeof(EditSecuretyQuestionFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mark = Request.QueryString["vm"];
                string id = Request.QueryString["id"];
                hfdid.Value = id;
                string v = Request.QueryString["v"];
                string vid = id + "ESQ";
                if (EncryptManager.GetEncryString(vid) == v)
                {
                    if (EncryptManager.GetEncryString(id + "S") == mark)
                    {
                        this.pnlresult.Visible = true;
                        this.lblEditSeccuss.Visible = true;
                        return;
                    }
                    if (EncryptManager.GetEncryString(id + "SendEmail") == mark)
                    {
                        string activeUrl = Url() + "?id=" + id + "&v=" + EncryptManager.GetEncryString(vid) + "&vm=" + EncryptManager.GetEncryString(id + "EditSQ");
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
                        mailInfo.Body = string.Format(LangManager.GetString("editSQbody"), row.USER_NAME, activeUrl);
                        mailInfo.To = row.EMAIL_ADDRESS;
                        mailInfo.Subject = LangManager.GetString("editSQsubject");

                        MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
  this.pnlresult.Visible = true;
                        lblEmailSended.Visible = true;

                        return;
                    }
                    if (EncryptManager.GetEncryString(id + "EditSQ") == mark)
                    {
                        pnlESQ.Visible = true;

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
            row.SQUESTION1 = int.Parse(SQUESTION1.SelectedValue);
            row.SANSWER1 = SANSWER1.Text;
            int result = SystemSettingManager.UpdateSystemAccount(row);
            string vid = hfdid.Value + "ESQ";
            if (result >= 0)
                Response.Redirect(Url() + "?id=" + hfdid.Value + "&v=" + EncryptManager.GetEncryString(vid) + "&vm=" + EncryptManager.GetEncryString(hfdid.Value + "S"));
       
        }
    }
}