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
using YMGS.Data.Entity;
using YMGS.Business.AssistManage;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class MemberShipHomeFrm : MemberShipBasePage
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

            return UrlHelper.BuildUrl(typeof(MemberShipHomeFrm), "MemberShip").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
               BindCountry();
               BindYear();
               BindCurrency();
               BindTimeZone();
               this.lbtEditEmail.Visible= this.btnSave.Enabled = IsAllow(FunctionIdList.MemberCenter.UserInfoManage);
               DetailUserInfo userInfo = PageHelper.GetCurrentUser();
               DataTable dt = SystemSettingManager.QueryData("", userInfo.UserId, "").TB_SYSTEM_ACCOUNT;
               Session["UserEdit"] = dt;
               PageHelper.GetValue(dt, base.Master.FindControl("mainContent"));
            }
        }

        protected void BindCurrency()
        {
            var currencyDS = CurrencyManager.QueryAllCurrency();
            string dataTextFiled = Language == LanguageEnum.Chinese ? "CURRENCY_NAME" : "CURRENCY_EN";
            PageHelper.BindListControlData(CURRENCY_ID, currencyDS, dataTextFiled, "CURRENCY_ID", false);
        }

        protected void BindTimeZone()
        {
            var timezoneDS = TimeZoneManager.QueryAllTimeZone();
            PageHelper.BindListControlData(TIMEZONE_ID, timezoneDS, "TIMEZONE_NAME", "TIMEZONE_ID", false);
        }

        protected void lbtEditEmail_Click(object sender, EventArgs e)
        {
            string id = PageHelper.GetCurrentUser().UserId.ToString();
            string vid = id + "vd";
            string url = ValidateEmailFrm.Url() + "?id=" + id + "&v=" + EncryptManager.GetEncryString(vid);
            Response.Redirect(url);
        }

        protected void btnRegiester_Click(object sender, EventArgs e)
        { 
            DataTable dt = Session["UserEdit"] as DataTable;
            DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable sysAcount = dt as DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable;

            PageHelper.SetValue(sysAcount, base.Master.FindControl("mainContent"));
            if (sysAcount.Count == 1)
            {
                int result = SystemSettingManager.UpdateSystemAccount(sysAcount[0]);
                if (result >= 0)
                    PageHelper.ShowMessage(this.Page, LangManager.GetString("SaveSucceed"));
                else
                    PageHelper.ShowMessage(this.Page, LangManager.GetString("SaveFailed"));
            }
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

        protected void lbtEditPassword_Click(object sender, EventArgs e)
        {
            string id= PageHelper.GetCurrentUser().UserId.ToString();
             string vid = id + "editPsw";
              string editpswMark =  id+ "sendEmail";
              string url = EditPasswordFrm.Url() + "?id=" + id + "&v=" + EncryptManager.GetEncryString(vid) + "&mark=" + EncryptManager.GetEncryString(editpswMark);
              Response.Redirect(url);
        }

        protected void lbtEditSecuretyQuestion_Click(object sender, EventArgs e)
        {
            string id = PageHelper.GetCurrentUser().UserId.ToString();
            string vid = id + "ESQ";
            string vm = id + "SendEmail";
            string url = EditSecuretyQuestionFrm.Url() + "?id=" + id + "&v=" + EncryptManager.GetEncryString(vid) + "&vm=" + EncryptManager.GetEncryString(vm);
            Response.Redirect(url);
        }


    }
}