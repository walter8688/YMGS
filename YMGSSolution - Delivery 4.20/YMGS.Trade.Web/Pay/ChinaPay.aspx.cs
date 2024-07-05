using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Entity;
using YMGS.Business.Pay;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Trade.Web.MemberShip;
using YMGS.Trade.Web.OnlinePay;

namespace YMGS.Trade.Web.Pay
{
    public partial class ChinaPay : MemberShipBasePage
    {
        private const string _TransAmt = "TransAmt";
        private const string _OrdId = "OrdId";
        private const string _DateFormator = "yyyyMMdd";
        private const string _PayType = "PayType";

        public int CurPayType
        {
            get
            {
                if (Request.QueryString[_PayType] == null)
                    return -1;
                return Convert.ToInt32(Request.QueryString[_PayType].ToString());
            }
        }

        public string TransAmt
        {
            get
            {
                if (HttpContext.Current.Request[_TransAmt] != null)
                {
                    return HttpContext.Current.Request[_TransAmt].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string CurDate
        {
            get
            {
                return DateTime.Now.ToString(_DateFormator);
            }
        }

        public string ordId
        {
            get
            {
                if (HttpContext.Current.Request[_OrdId] != null)
                {
                    return HttpContext.Current.Request[_OrdId].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string Url(string transAmt,string ordId,int payType)
        {
            return UrlHelper.BuildUrl(typeof(ChinaPay), "Pay", _TransAmt, transAmt, _OrdId, ordId, _PayType, payType).AbsoluteUri;
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(CurPayType == -1)
                Response.Redirect(OnlineChargeFrm.Url());
            
            PayManagerAbstract payManager = null;
            IPayStrategy payStrategy = null;
            var CurOrdId = string.Empty;
            switch ((PayTypeEnum)CurPayType)
            {
                case PayTypeEnum.ChinaPay:
                    //生成在线支付记录
                    if (string.IsNullOrEmpty(ordId))
                    {
                        var merId = System.Configuration.ConfigurationManager.AppSettings["ChinaPayMerId"].ToString();
                        payManager = new ChinaPayManager(TransAmt, ordId, CurrentUser.UserId, merId);
                        CurOrdId = payManager.AddUserPay();
                    }
                    //ChinaPay在线支付
                    payStrategy = new ChinaPayStrategy();
                    payStrategy.OrdId = string.IsNullOrEmpty(ordId) ? CurOrdId : ordId;
                    payStrategy.TransAmt = TransAmt;
                    payStrategy.CurUserId = CurrentUser.UserId;
                    payStrategy.SendPayData();
                    break;
            }
        }
        #region
        
        //public ChinaPayObject GetPayObject()
        //{
        //    ChinaPayObject payObj = new ChinaPayObject();
        //    payObj.ChinaPayURL = System.Configuration.ConfigurationManager.AppSettings["CHINA_PAY_URL"].ToString();
        //    payObj.MerId = CommonConstant.ChinaPayMerId;
        //    payObj.OrdId = string.IsNullOrEmpty(ordId) ? new ChinaPayManager().GetOrdId(CurDate) : ordId;
        //    payObj.TransAmt =new ChinaPayManager().GetTransAmt(TransAmt);
        //    payObj.CuryId = CommonConstant.ChinaPayCuryId;
        //    payObj.TransDate = CurDate;
        //    payObj.TransType = CommonConstant.ChinaPayTransType_Pay;
        //    payObj.Version = CommonConstant.ChinaPayVersion20070129;
        //    payObj.BgRetUrl = "";
        //    payObj.PageRetUrl = "";
        //    payObj.GateId = "";
        //    payObj.Priv1 = string.Format("{0}|{1}",CurrentUser.UserId,payObj.OrdId);
        //    return payObj;
        //}

        //private DSUserPay.TB_USER_PAYRow GetCurUserPay()
        //{
        //    DSUserPay.TB_USER_PAYRow userPay = new DSUserPay.TB_USER_PAYDataTable().NewTB_USER_PAYRow();
        //    userPay.USER_ID = CurrentUser.UserId;
        //    userPay.MER_ID = CommonConstant.ChinaPayMerId;
        //    userPay.ORDER_ID = string.IsNullOrEmpty(ordId) ?new ChinaPayManager().GetOrdId(CurDate) : ordId;
        //    userPay.TRAN_AMOUNT = decimal.Parse(TransAmt);
        //    userPay.TRAN_STATUS = (int)UserPayStatus.WaitingPay;
        //    userPay.VCARD_ID = -1;
        //    return userPay;
        //}
        #endregion
    }
}