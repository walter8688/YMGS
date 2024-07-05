using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Business.SystemSetting;
using System.Drawing;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Configuration;
using YMGS.Data.Common;
using YMGS.Business.AssistManage;

namespace YMGS.Trade.Web.Public
{
    public partial class UserRegisterFrm : SimplePageBase
    {
        private const string USER_ID = "UserId";
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("UserRegister");
            }
        }

        public bool IsGrowMember
        {
            get
            {
                if (Request.QueryString[USER_ID] == null || string.IsNullOrEmpty(Request.QueryString[USER_ID].ToString()))
                    return false;
                return true;
            }
        }

        public int GrowMemberUserID
        {
            get
            {
                if (IsGrowMember)
                    return Convert.ToInt32(EncryptManager.DESDeCrypt(Request.QueryString[USER_ID].ToString()));
                return 0;
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(UserRegisterFrm), "Public").AbsoluteUri;
        }

        public static string Url(string userId)
        {
            return UrlHelper.BuildUrl(typeof(UserRegisterFrm), "Public", USER_ID, userId).AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hlkhelper.NavigateUrl = HelperCenter.Url();// +"?node=3";
                imgValidate.ImageUrl = ValidateCode.Url();
                BindCountry();
                BindYear();
                BindCurrency();
                BindTimeZone();
                // btnRegiester.Enabled = false;
                if (IsGrowMember)
                {
                    lblCompleteYourDetail.Visible = true;
                    BindGrowMemberInfo();
                }
                else
                {
                    lblCompleteYourDetail.Visible = false;
                }
            }
        }

        protected void BindCurrency()
        {
            var currencyDS = CurrencyManager.QueryAllCurrency();
            string dataTextFiled = Language == LanguageEnum.Chinese ? "CURRENCY_NAME" : "CURRENCY_EN";
            PageHelper.BindListControlData(drpCurrency, currencyDS, dataTextFiled, "CURRENCY_ID", false);
        }

        protected void BindTimeZone()
        {
            var timezoneDS = TimeZoneManager.QueryAllTimeZone();
            PageHelper.BindListControlData(drpTimeZone, timezoneDS, "TIMEZONE_NAME", "TIMEZONE_ID", false);
        }

        protected void BindCountry()
        {
            DSCountry countryList = CountryManager.QueryAllCountry();
            countryList.TB_COUNTRY.DefaultView.Sort = Language == LanguageEnum.Chinese ? "COUNTRY_NAME_CN" : "COUNTRY_NAME_EN";
            COUNTRY.Items.Clear();
            COUNTRY.DataTextField = Language == LanguageEnum.Chinese ? "COUNTRY_NAME_CN" : "COUNTRY_NAME_EN";
            COUNTRY.DataValueField = "COUNTRY_ID";
            COUNTRY.DataSource = countryList.TB_COUNTRY;
            COUNTRY.DataBind();
            ListItem li = new ListItem("", "0");
            COUNTRY.Items.Insert(0, li);
        }

        private void BindGrowMemberInfo()
        {
            DSSystemAccount sa = SystemSettingManager.QueryData("", GrowMemberUserID, "");
            var userRow = sa.TB_SYSTEM_ACCOUNT[0];
            if (userRow.ACCOUNT_STATUS == (int)SysAccountStatusEnum.Activated)
            {
                string scripts = @"<script>alert('" + LangManager.GetString("GrowMemberRegistedMsg") + "');location.href='" + Default.Url() + "';</script>";
                Page.ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), scripts);
                return;
            }
            LOGIN_NAME.Text = userRow.LOGIN_NAME;
            EMAIL_ADDRESS.Text = userRow.EMAIL_ADDRESS;
            LOGIN_NAME.Enabled = EMAIL_ADDRESS.Enabled = false;
        }

        protected void BindYear()
        {
            BORN_YEAR.Items.Clear();
            ListItem li = new ListItem("Year", "0");
            BORN_YEAR.Items.Add(li);
            for (int year = 1940; year <= GlobalBrManager.QueryCurrentDateTime().Year; year++)
            {
                BORN_YEAR.Items.Add(year.ToString());
            }
        }

        protected void btnRegiester_Click(object sender, EventArgs e)
        {
            DSSystemAccount saemail = SystemSettingManager.QueryData("", 0, EMAIL_ADDRESS.Text);
            if (saemail.TB_SYSTEM_ACCOUNT.Count > 0 && !IsGrowMember)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("emailaddressexist"));
                EMAIL_ADDRESS.Focus();
                return;
            }
            DSSystemAccount sa = SystemSettingManager.QueryData(LOGIN_NAME.Text.Trim(), 0,"");
            if (sa.TB_SYSTEM_ACCOUNT.Count > 0 && !IsGrowMember)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("usernameexist"));
                LOGIN_NAME.Focus();
                return;
            }
            if (txtcpassword.Text!=PASSWORD.Text)
            {
                PageHelper.ShowMessage(this.Page,  LangManager.GetString("passwordnotsame"));
                txtcpassword.Focus();
                return;
            }


            string vcode = Session["vcode"].ToString();
            if (vcode != txtvalidatecode.Text)
            {
                PageHelper.ShowMessage(this.Page, LangManager.GetString("vcodeerror"));
                return;
            }
            DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable sysAcount = new DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable();
            PageHelper.SetValue(sysAcount, base.Master.FindControl("mainContent"));
            sysAcount[0].CURRENCY_ID = Convert.ToInt32(drpCurrency.SelectedValue);
            sysAcount[0].TIMEZONE_ID = Convert.ToInt32(drpTimeZone.SelectedValue);
            if (sysAcount.Count == 1)
            {
                sysAcount[0].PASSWORD=EncryptManager.GetEncryString(PASSWORD.Text);
                sysAcount[0].ROLE_ID = 4;//默认会员
                if (IsGrowMember)
                {
                    sysAcount[0].ACCOUNT_STATUS = (int)SysAccountStatusEnum.Activated;
                    SystemSettingManager.RegisterGrowMemberAccount(sysAcount[0]);
                    string redirectUrl = Default.Url() + "?L=" + EncryptManager.DESEnCrypt(LOGIN_NAME.Text) + "&W=" + EncryptManager.DESEnCrypt(PASSWORD.Text);
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    sysAcount[0].ACCOUNT_STATUS = (int)SysAccountStatusEnum.UnActivated;
                    int result = SystemSettingManager.SaveSystemAccount(sysAcount[0]);
                    if (result > 0)
                    {
                        string activeUrl = "RegisterResult.aspx?id=" + result.ToString();

                        Response.Redirect(activeUrl);
                    }
                }
            }

        }

    }
}