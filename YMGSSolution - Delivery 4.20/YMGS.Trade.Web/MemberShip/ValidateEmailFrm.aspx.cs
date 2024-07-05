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

namespace YMGS.Trade.Web.MemberShip
{
      [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class ValidateEmailFrm : MemberShipBasePage
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

            return UrlHelper.BuildUrl(typeof(ValidateEmailFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserEdit"] == null)
                {
                    Response.Redirect(Default.Url());
                }
               
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Session["UserEdit"] == null)
            {
                Response.Redirect(Default.Url());
            }

 DataTable dt = Session["UserEdit"] as DataTable;
               
                DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable sysAcount = dt as DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable;
              
                if (sysAcount.Count == 1)
                {
                    string psw = PASSWORD.Text;
                    string enpasw=EncryptManager.GetEncryString(psw);
                    if (sysAcount[0].PASSWORD == enpasw && sysAcount[0].SQUESTION1.ToString() == SQUESTION1.SelectedValue && sysAcount[0].SANSWER1 == SANSWER1.Text)
                    {
                        string id = sysAcount[0].USER_ID.ToString();
                        string vid = id + "vd";
                        Response.Redirect("ValidateResultFrm.aspx?v=" + EncryptManager.GetEncryString(vid) + "&id=" + id);
                    }
                    else
                    {
                        PageHelper.ShowMessage(this.Page,LangManager.GetString("validateError"));
                    }
                }
                else
                {
                    btnsave.Visible = false;
                }
        }
    }
}