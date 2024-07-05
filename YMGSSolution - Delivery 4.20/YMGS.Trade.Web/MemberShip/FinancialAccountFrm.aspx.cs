using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.MemberShip;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class FinancialAccountFrm : MemberShipBasePage
    {
        private const string BtnSetBankInfoCommand = "SetBankInfoCommand";
        private const string BtnOnlineChargeCommand = "OnlineChargeCommand";
        private const string BtnSupplyWithdrawCommand = "SupplyWithdrawCommand";

        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("FinancialAccountPage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.FundAccountManagePage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(FinancialAccountFrm), "MemberShip").AbsoluteUri;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnOnlineCharge.Visible = base.IsAllow(FunctionIdList.MemberCenter.OnlinePay);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitButtonCommand();
                LoadUserFundInfo();
            }
        }

        public int CurrentUserID
        {
            get
            {
                var user = PageHelper.GetCurrentUser();
                if (user == null)
                    return 0;
                return user.UserId;
            }
        }

        private void LoadUserFundInfo()
        {
            var userFundInfo = UserFundManager.QueryUserFund(CurrentUserID);
            DSUserFund.TB_USER_FUNDRow userFundRow = (DSUserFund.TB_USER_FUNDRow)userFundInfo.TB_USER_FUND.Rows[0];
            this.txtBankName.Text = userFundRow.BANK_NAME;
            this.popTxtBankName.Text = userFundRow.BANK_NAME;
            this.txtOpenBankName.Text = userFundRow.OPEN_BANK_NAME;
            this.popTxtOpenBankName.Text = userFundRow.OPEN_BANK_NAME;
            this.txtBankCardNo.Text = userFundRow.CARD_NO;
            this.popTxtBankCardNo.Text = userFundRow.CARD_NO;
            this.txtAccountHolder.Text = userFundRow.ACCOUNT_HOLDER;
            this.popTxtAccountHolder.Text = userFundRow.ACCOUNT_HOLDER;
            this.txtCurrentFund.Text = userFundRow.CUR_FUND.ToString();
            this.txtFreezedFund.Text = userFundRow.FREEZED_FUND.ToString();
            this.txtCurrentIntegral.Text = userFundRow.CUR_INTEGRAL.ToString();
            this.txtFundAccountStatus.Text = (UserFundStatusEnum)userFundRow.STATUS == UserFundStatusEnum.Activated ? LangManager.GetString("UserFundStatusNomal") : LangManager.GetString("UserFundStatusFreezed");
        }

        private void InitButtonCommand()
        {
            this.btnSetBankInfo.CommandName = BtnSetBankInfoCommand;
            this.btnOnlineCharge.CommandName = BtnOnlineChargeCommand;
            this.btnSupplyWithdraw.CommandName = BtnSupplyWithdrawCommand;
        }

        protected void showPopUp_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var bankName = this.popTxtBankName.Text;
            var openBankName = this.popTxtOpenBankName.Text;
            var cardNo = this.popTxtBankCardNo.Text;
            var accountHolder = this.popTxtAccountHolder.Text;
            UserFundManager.SetUserFundBankInfo(CurrentUserID, bankName, openBankName, cardNo, accountHolder);
            LoadUserFundInfo();
        }

        protected void btnOnlineCharge_Click(object sender, EventArgs e)
        {
            Response.Redirect(OnlineChargeFrm.Url());
        }

        protected void BtnSupplyWithdraw_Click(object sender, EventArgs e)
        {
            Response.Redirect(SupplyWithdrawFrm.Url());
        }

        protected void btnFundDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect(UserFundDetailFrm.Url());
        }
    }
}