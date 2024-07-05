using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Trade.Web.Pay;
using YMGS.Business.Pay;
using YMGS.Business.SystemSetting;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Business.MemberShip;
using YMGS.Business.AssistManage;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class OnlineChargeFrm : MemberShipBasePage
    {
        private const string _CommandRePay = "CommandRePay";
        private const string _OrdNo = "ordno";
        private const string _IsPayNotify = "isPayNotify";

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
            return UrlHelper.BuildUrl(typeof(OnlineChargeFrm), "MemberShip").AbsoluteUri;
        }

        public static string Url(string ordno)
        {
            return UrlHelper.BuildUrl(typeof(OnlineChargeFrm), "MemberShip", _OrdNo, ordno).AbsoluteUri;
        }

        public static string Url(string ordno, bool isPayNotify)
        {
            return UrlHelper.BuildUrl(typeof(OnlineChargeFrm), "MemberShip", _OrdNo, ordno, _IsPayNotify, isPayNotify).AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var onlinePaySucceedMsg = ShowOnlinePaySucceedMsg();
                if (!string.IsNullOrEmpty(onlinePaySucceedMsg))
                {
                    divPayMsg.InnerHtml = onlinePaySucceedMsg;
                    mdlPayMsg.Show();
                }
                LoadPageData();
                LoadGridData();

                //btnPay.Visible = false;
            }
        }

        private string ShowOnlinePaySucceedMsg()
        {
            if (Request.QueryString[_IsPayNotify] == null)
                return string.Empty;
            if (Request.QueryString[_IsPayNotify].ToString().ToUpper() != "TRUE")
                return string.Empty;
            if (Request.QueryString[_OrdNo] == null)
                return string.Empty;
            var ordno = Request.QueryString[_OrdNo].ToString();
            var userPayVCardDS = UserFundManager.QueryUserPayVCardDetail(ordno);
            if (userPayVCardDS == null)
                return string.Empty;
            if (userPayVCardDS.Tables.Count == 0)
                return string.Empty;
            var info = userPayVCardDS.TBUserPayVCard[0];
            var alertMsg = string.Format(LangManager.GetString("SuccessActivateVCardPayMsg"), info.TRAN_AMOUNT, EncryptManager.DESDeCrypt(info.VCARD_NO), EncryptManager.DESDeCrypt(info.VCARD_ACTIVATE_NO),info.ORDER_ID);
            return alertMsg;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        private void LoadPageData()
        {
            var param = new DSParamParam.TB_PARAM_PARAMDataTable().NewTB_PARAM_PARAMRow();
            param.PARAM_TYPE = 7;
            param.PARAM_NAME = string.Empty;
            var VCardFaceValueList = ParamParamManager.QueryParam(param);
            //var VCardFaceValueList = CommonFunction.QueryAllVCardFaceValueInfo();
            PageHelper.BindListControlData(DrpVCardFaceValue, VCardFaceValueList, "PARAM_NAME", "PARAM_NAME", false);
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
            pageNavigator.databinds(ChinaPayManager.QueryUserPay(sDate, eDate,CurrentUser.UserId).TB_USER_PAY, gdvUserPay);
        }

        protected void BtnSetBankInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(FinancialAccountFrm.Url());
        }

        protected void BtnSupplyWithdraw_Click(object sender, EventArgs e)
        {
            Response.Redirect(SupplyWithdrawFrm.Url());
        }

        protected void btnFundDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect(UserFundDetailFrm.Url());
        }

        protected void BtnPay_Cikck(object sender, EventArgs e)
        {
            DrpVCardFaceValue.SelectedIndex = 0;
            mdlPopup.Show();
            LoadGridData();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSUserPay.TB_USER_PAYRow)((DataRowView)e.Row.DataItem).Row;
                var btnRePay = e.Row.FindControl("btnRePay") as LinkButton;
                var lblPayType = e.Row.FindControl("lblPayType") as Label;
                var lblTransDate = e.Row.FindControl("lblTransDate") as Label;
                var lblTransStatus = e.Row.FindControl("lblTransStatus") as Label;
                btnRePay.Visible = bindRow.TRAN_STATUS == (int)UserPayStatus.WaitingPay ? true : false;
                btnRePay.CommandName = _CommandRePay;
                btnRePay.CommandArgument = string.Format("{0},{1}", bindRow.TRAN_AMOUNT, bindRow.ORDER_ID);
                //lblTransDate.Text = UtilityHelper.DateToStr(bindRow.TRAN_DATE);
                lblTransDate.Text = bindRow.TRAN_DATE.ToString();
                switch ((UserPayStatus)bindRow.TRAN_STATUS)
                { 
                    case UserPayStatus.WaitingPay:
                        lblTransStatus.Text = LangManager.GetString("WaitingPay");
                        break;
                    case UserPayStatus.PaySuccessed:
                        lblTransStatus.Text = LangManager.GetString("PaySuccessed");
                        break;
                    case UserPayStatus.ChargeSuccessed:
                        lblTransStatus.Text = LangManager.GetString("ChargeSuccessed");
                        break;
                    case UserPayStatus.ChargeFailed:
                        lblTransStatus.Text = LangManager.GetString("ChargeFailed");
                        break;
                }

                switch ((UserPayTypeEnum)bindRow.TRAN_TYPE)
                {
                    case UserPayTypeEnum.VCard:
                        lblPayType.Text = LangManager.GetString("VCardPay");
                        break;
                    case UserPayTypeEnum.ChinaPay:
                        lblPayType.Text = LangManager.GetString("OnlinePay");
                        break;
                }
            }
        }

        protected void gdvPay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (e.CommandName == _CommandRePay)
            {
                var args = e.CommandArgument.ToString().Split(',');
                GotoPay(args[0], args[1]);
                return;
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnPayMoney_Click(object sender, EventArgs e)
        {
            GotoPay(DrpVCardFaceValue.SelectedValue, string.Empty);
            DrpVCardFaceValue.SelectedIndex = 0;
        }

        protected void BtnVCardActivat_Click(object sender, EventArgs e)
        {
            txtVCardNo.Text = txtVCardActivateNo.Text = string.Empty;
            mdlActivateVCard.Show();
        }

        protected void BtnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                var VCardNo = EncryptManager.DESEnCrypt(txtVCardNo.Text.Trim());
                var VCardActivateNo = EncryptManager.DESEnCrypt(txtVCardActivateNo.Text.Trim());
                var returnObj = VCardManager.ActivatedVCard(VCardNo, VCardActivateNo, CurrentUser.UserId);
                if (returnObj != 0)
                {
                    //添加充值记录
                    var userPay = GetUserPay();
                    userPay.ORDER_ID = string.Empty;
                    userPay.TRAN_AMOUNT = returnObj;
                    userPay.VCARD_ID = VCardManager.QueryVCardID(VCardNo,VCardActivateNo);
                    userPay.TRAN_STATUS = (int)UserPayStatus.ChargeSuccessed;
                    userPay.TRAN_TYPE = (int)UserPayTypeEnum.VCard;
                    new ChinaPayManager().AddUserPay(userPay);
                    LoadGridData();
                    //提示充值记录
                    var alertMsg = string.Format(LangManager.GetString("SuccessActivateVCardMsg"), returnObj, txtVCardNo.Text, txtVCardActivateNo.Text, returnObj);
                    divMsg.InnerHtml = alertMsg;
                    mdlMsg.Show();
                }
                //ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + alertMsg + "');", true);
            }
            catch (Exception ex)
            {
                mdlActivateVCard.Show();
                var msg = Language == LanguageEnum.English ? ex.Message.Split('|')[1] : ex.Message.Split('|')[0];
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + msg + "');", true);
            }
        }

        private void GotoPay(string transAmt,string ordId)
        {
            
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^[0-9]{1,18}(.[0-9]{1,2})?$");
            if (reg.IsMatch(transAmt))
                Response.Redirect(Pay.ChinaPay.Url(transAmt,ordId,(int)PayTypeEnum.ChinaPay));
            else
                PageHelper.ShowMessage(Page, LangManager.GetString("PayMoneyErrMsg"));
        }

        private DSUserPay.TB_USER_PAYRow GetUserPay()
        {
            var merId = System.Configuration.ConfigurationManager.AppSettings["ChinaPayMerId"].ToString();
            bool isTest = merId == "808080040192810" ? true : false;
            var userPay = new DSUserPay.TB_USER_PAYDataTable().NewTB_USER_PAYRow();
            userPay.USER_ID = CurrentUser.UserId;
            userPay.MER_ID = string.Empty;
            userPay.ORDER_ID = new ChinaPayManager().GetOrdId(UtilityHelper.DateToStr(DateTime.Now), isTest);
            return userPay;
        }
    }
}