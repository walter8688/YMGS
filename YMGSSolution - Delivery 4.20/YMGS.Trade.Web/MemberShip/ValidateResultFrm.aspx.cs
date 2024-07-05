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
    public partial class ValidateResultFrm : MemberShipBasePage
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

            return UrlHelper.BuildUrl(typeof(ValidateResultFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string v = Request.QueryString["v"];
                string id = Request.QueryString["id"];
                string vr = Request.QueryString["vr"];
                if (vr == "s")
                {
                    lblEditEmailSuccess.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(v))
                {
                    Response.Redirect(Default.Url());
                }
                string vid = id + "vd";
                string enid = EncryptManager.GetEncryString(vid);
                if (enid == v)
                {
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
                    string activeUrl = EditEmailFrm.Url() + "?id=" + id + "&v=" + v;
                    var mailFrom = ConfigurationManager.AppSettings[CommConstant.SmtpFromUserKey];
                    NotificationMail mailInfo = new NotificationMail();
                    mailInfo.Body = string.Format(LangManager.GetString("editemailbody"), row.USER_NAME, activeUrl);
                    mailInfo.To = row.EMAIL_ADDRESS;
                    mailInfo.Subject = LangManager.GetString("editemailsubject");

                    MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
                    lblSendEditEmail.Visible = true;
                }
                else
                {
                    Response.Redirect(Default.Url());
                }



            }
        }
    }
}