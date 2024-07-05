using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Trade.Web.Common;
using YMGS.Business.GameSettle;
using System.Data;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class IntegralBrokerageRuleFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("IntegralBrokerageRule");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.MyIntegralPage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(IntegralBrokerageRuleFrm), "MemberShip").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGridData();
        }

        private void LoadGridData()
        {
            var brokerageIntegralDS = CommissionManager.QueryBrokerageIntegral();
            gdvBrokerage.DataSource = brokerageIntegralDS;
            gdvBrokerage.DataBind();
        }

        protected void gdvBrokerage_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow)((DataRowView)e.Row.DataItem).Row;
                var lblBrokerageRate = (e.Row.FindControl("lblBrokerageRate") as Label);
                var lblIntegral = (e.Row.FindControl("lblIntegral") as Label);
                lblBrokerageRate.Text = string.Format("{0}", bindRow.Brokerage_Rate * 100);
                lblBrokerageRate.Text = lblBrokerageRate.Text.Substring(0, lblBrokerageRate.Text.Length - 2) + "%";
                lblIntegral.Text = string.Format("{0}-{1}", bindRow.Min_Integral, bindRow.Max_Integral);
            }
        }

        protected void BtnMyIntegral_Click(object sender, EventArgs e)
        {
            Response.Redirect(MyIntegralFrm.Url());
        }
    }
}