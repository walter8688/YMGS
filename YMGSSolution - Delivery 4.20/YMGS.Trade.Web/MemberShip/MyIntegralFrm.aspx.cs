using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Trade.Web.Common;
using YMGS.Business.MemberShip;
using YMGS.Business.GameSettle;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class MyIntegralFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("MyIntegralPage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.MyIntegralPage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(MyIntegralFrm), "MemberShip").AbsoluteUri;
        }

        protected override void OnInit(EventArgs e)
        {
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        private void LoadPageData()
        {
            var userFund = UserFundManager.QueryUserFund(CurrentUser.UserId).TB_USER_FUND[0];
            lblCurIntegral.Text = userFund.CUR_INTEGRAL.ToString();
            var curBrokerage = CommissionManager.QueryBrokerageByIntegral(userFund.CUR_INTEGRAL) * 100;
            lblCurBrokerage.Text = curBrokerage.ToString();
            lblCurBrokerage.Text = lblCurBrokerage.Text.Substring(0, lblCurBrokerage.Text.Length - 2) + "%";
        }

        private void LoadGridData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value;
            else
                eDate = DateTime.MaxValue;
            var integralDS = IntegralManager.QueryIntegralHistory(CurrentUser.UserId, sDate, eDate);
            pageNavigator.databinds(integralDS.TB_INTEGRAL_HISTORY, gdvIntegral);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvIntegral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSIntegralHistory.TB_INTEGRAL_HISTORYRow)((DataRowView)e.Row.DataItem).Row;
                var lblTradeDate = e.Row.FindControl("lblTradeDate") as Label;
                lblTradeDate.Text = UtilityHelper.DateToStr(bindRow.TRADE_DATE);
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnIntegralRule_Click(object sender, EventArgs e)
        {
            Response.Redirect(IntegralBrokerageRuleFrm.Url());
        }
    }
}