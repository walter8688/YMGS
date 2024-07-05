using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using System.Configuration;
using YMGS.Framework;

namespace YMGS.Trade.Web.Public
{
    public partial class RegisterResult : SimplePageBase
    {
        private const string _IDKey = "id";
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("UserRegister");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(RegisterResult), "Public").AbsoluteUri;
        }

        public static string Url(string id)
        {
            return UrlHelper.BuildUrl(typeof(RegisterResult), "Public", _IDKey, id).AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string resend = Request.QueryString["resend"];
                string id = Request.QueryString["id"];
                string vc = Request.QueryString["vc"];
                if (!string.IsNullOrEmpty(id))
                {
                   

                    DSSystemAccount sa = SystemSettingManager.QueryData("", int.Parse(id),"");
                    if (sa.TB_SYSTEM_ACCOUNT.Count == 1)
                    {
                        DSSystemAccount.TB_SYSTEM_ACCOUNTRow row = sa.TB_SYSTEM_ACCOUNT[0];
                        if (row.ACCOUNT_STATUS == 0)
                        {
                            if (string.IsNullOrEmpty(vc))
                            {
                                string activeUrl = HttpContext.Current.Request.Url.ToString()+"&vc=8e83Uc822U883eyrUoD";
                                var mailFrom = ConfigurationManager.AppSettings[CommConstant.SmtpFromUserKey];
                                NotificationMail mailInfo = new NotificationMail();
                                mailInfo.Body = string.Format(LangManager.GetString("activebody"), row.USER_NAME, activeUrl);
                                mailInfo.To = row.EMAIL_ADDRESS;
                                mailInfo.Subject = LangManager.GetString("activesubject");

                                MailHelper.SendMultiMailInThread(mailFrom, mailInfo);
                                if (string.IsNullOrEmpty(resend))
                                {
                                    lblnote.Visible = true;
                                    lblresult.Visible = true;
                                }
                                else
                                {
                                    lblresendemail.Visible = true;
                                }
                            }
                            else
                            {
                                row.ACCOUNT_STATUS = 1;//激活
                                int result = SystemSettingManager.UpdateSystemAccount(row);
                                if (result >= 0)
                                {
                                    lblactive.Visible = true;
                                    lblrelogin.Visible = true;
                                }
                                else
                                {
                                    lblfail.Visible = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblfail.Visible = true;
                    }
                }
            }
        }
    }
}