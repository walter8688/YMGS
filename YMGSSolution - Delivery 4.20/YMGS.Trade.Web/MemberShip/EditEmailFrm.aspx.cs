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
    public partial class EditEmailFrm : MemberShipBasePage
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

            return UrlHelper.BuildUrl(typeof(EditEmailFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string v = Request.QueryString["v"];
                string id = Request.QueryString["id"]; hfdid.Value = id;
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
                       Session["sa"]= row = sa.TB_SYSTEM_ACCOUNT[0];
                    }
                    else
                    {
                        Response.Redirect(Default.Url());
                    }
                    lbloldemail.Text = row.EMAIL_ADDRESS;
                    btnsave.Visible = true;
                }
                else
                {
                    Response.Redirect(Default.Url());
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = Session["sa"] as DSSystemAccount.TB_SYSTEM_ACCOUNTRow;
          obj.EMAIL_ADDRESS = txtEmail.Text;
          int resetEmailResult = SystemSettingManager.CheckResetEmail(obj.USER_ID, obj.EMAIL_ADDRESS);
          if (resetEmailResult > 0)
          {
              txtEmail.Focus();
              PageHelper.ShowMessage(this.Page, LangManager.GetString("emailaddressexist"));
              return;
          }
         int result= SystemSettingManager.UpdateSystemAccount(obj);
         if (result >= 0)
         {
             Response.Redirect(ValidateResultFrm.Url() + "?vr=s");
         }
         else
         {
             PageHelper.ShowMessage(this.Page, LangManager.GetString("eidtemailfail"));
         }
        }
    }
}