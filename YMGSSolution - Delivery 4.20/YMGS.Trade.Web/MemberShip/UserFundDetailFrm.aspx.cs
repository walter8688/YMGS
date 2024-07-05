using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.MemberShip;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class UserFundDetailFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("UserFundDetailPage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.FundAccountManagePage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(UserFundDetailFrm), "MemberShip").AbsoluteUri;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            btnOnlineCharge.Visible = base.IsAllow(FunctionIdList.MemberCenter.OnlinePay);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();

                if (CurrentUser.RoleId == (int)AgentTypeEnum.NoramlAgent || CurrentUser.RoleId == (int)AgentTypeEnum.RootAgent)
                {
                    lblTypeTitle.Visible = true;
                    ddlType.Visible = true;
                }
                else
                {
                    lblTypeTitle.Visible = false;
                    ddlType.Visible = false;
                }
            }
        }

        private IList<FundHistoryTradeType> GetFundHistoryTradeTypeList()
        {
            IList<FundHistoryTradeType> fundHistoryTradeTypeList = new List<FundHistoryTradeType>()
            {
                new FundHistoryTradeType(){ TradeTypeText = LangManager.GetString("Commission"), TradeTypeValue = (int)UserFundTradeType.Commission},
            };

            if (CurrentUser.RoleId == (int)AgentTypeEnum.NoramlAgent)
                fundHistoryTradeTypeList.Add(new FundHistoryTradeType() { TradeTypeText = LangManager.GetString("AgentReimbursement"), TradeTypeValue = (int)UserFundTradeType.AgentReimbursement });
            else if (CurrentUser.RoleId == (int)AgentTypeEnum.RootAgent)
                fundHistoryTradeTypeList.Add(new FundHistoryTradeType() { TradeTypeText = LangManager.GetString("MainAgentReimbursement"), TradeTypeValue = (int)UserFundTradeType.MainAgentReimbursement });
            else
                fundHistoryTradeTypeList.Clear();
            return fundHistoryTradeTypeList;
        }

        private void LoadPageData()
        {
            PageHelper.BindListControlData(ddlType, GetFundHistoryTradeTypeList(), "TradeTypeText", "TradeTypeValue", true);
        }

        private void LoadGridData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value.AddDays(1);
            else
                eDate = DateTime.MaxValue;
            int fundTradeType;
            if (ddlType.Visible)
                fundTradeType = Convert.ToInt16(ddlType.SelectedValue);
            else
                fundTradeType = -1;
            var fundHistoryDS = UserFundManager.QueryFundHistory(CurrentUser.UserId, sDate, eDate, fundTradeType);
            decimal temp = 0;
            decimal dblRewards = 0;
            foreach (var row in fundHistoryDS.TB_FUND_HISTORY)
            {
                if (!row.IsTRADE_FUNDNull() && ((UserFundTradeType)row.TRADE_TYPE == UserFundTradeType.Calculating 
                    || (UserFundTradeType)row.TRADE_TYPE == UserFundTradeType.Commission))
                    temp += row.TRADE_FUND;

                if (!row.IsTRADE_FUNDNull() && (row.TRADE_TYPE == (int)UserFundTradeType.AgentReimbursement
                                || row.TRADE_TYPE == (int)UserFundTradeType.MainAgentReimbursement))
                    dblRewards += row.TRADE_FUND;
            }
            lblFundTotal.Text = temp.ToString();

            if (CurrentUser.RoleId == (int)AgentTypeEnum.RootAgent || CurrentUser.RoleId == (int)AgentTypeEnum.NoramlAgent)
            {
                lblReimbursementTitle.Visible = true;
                lblReimbursement.Visible = true;
                lblReimbursement.Text = dblRewards.ToString();
            }
            else
            {
                lblReimbursementTitle.Visible = false;
                lblReimbursement.Visible = false;
            }
            pageNavigator.databinds(fundHistoryDS.TB_FUND_HISTORY, gdvFundDetail);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvFundDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblTradeType = e.Row.FindControl("lblTradeType") as Label;
                var bindRow = (DSFundHistory.TB_FUND_HISTORYRow)((DataRowView)e.Row.DataItem).Row;
                switch ((UserFundTradeType)bindRow.TRADE_TYPE)
                {
                    case UserFundTradeType.ChargeCash:
                        lblTradeType.Text = LangManager.GetString("ChargeCash");
                        break;
                    case UserFundTradeType.WithDrawCash:
                        lblTradeType.Text = LangManager.GetString("WithDrawCash");
                        break;
                    case UserFundTradeType.Buy:
                        lblTradeType.Text = LangManager.GetString("Back");
                        e.Row.Cells[2].Text = e.Row.Cells[2].Text;
                        break;
                    case UserFundTradeType.Sell:
                        lblTradeType.Text = LangManager.GetString("Sell");
                        e.Row.Cells[2].Text = e.Row.Cells[2].Text;
                        break;
                    case UserFundTradeType.CancelFreezeCash:
                        lblTradeType.Text = LangManager.GetString("CancelFreezeCash");
                        break;
                    case UserFundTradeType.Calculating:
                        lblTradeType.Text = LangManager.GetString("Calculating");
                        break;
                    case UserFundTradeType.Commission:
                        lblTradeType.Text = LangManager.GetString("Commission");
                        break;
                    case UserFundTradeType.AgentReimbursement:
                        lblTradeType.Text = LangManager.GetString("AgentReimbursement");
                        break;
                    case UserFundTradeType.MainAgentReimbursement:
                        lblTradeType.Text = LangManager.GetString("MainAgentReimbursement");
                        break;
                    case UserFundTradeType.OfflineTransfer:
                        lblTradeType.Text = LangManager.GetString("OfflineTransfer");
                        break;
                    case UserFundTradeType.RealTimeHedge:
                        lblTradeType.Text = LangManager.GetString("DeducteHhedgeFunds");
                        break;
                    case UserFundTradeType.ReleaseRealTimeHedge:
                        lblTradeType.Text = LangManager.GetString("ReleasedHedgeFunds");
                        break;
                    default:
                        break;
                }
            }
        }

        protected void BtnSetBankInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(FinancialAccountFrm.Url());
        }

        protected void BtnOnlineCharge_Click(object sender, EventArgs e)
        {
            Response.Redirect(OnlineChargeFrm.Url());
        }

        protected void BtnSupplyWithdraw_Click(object sender, EventArgs e)
        {
            Response.Redirect(SupplyWithdrawFrm.Url());
        }
    }
}