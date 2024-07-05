using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Business.Pay;
using YMGS.Data.Entity;
using YMGS.Trade.Web.Common;
using YMGS.Business.SystemSetting;
using YMGS.Trade.Web.MemberShip;

namespace YMGS.Trade.Web.OnlinePay
{
    public class ChinaPayStrategy : IPayStrategy
    {
        public string OrdId
        {
            get;
            set;
        }

        public string CurDate
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMdd");
            }
        }

        public string TransAmt
        {
            get;set;
        }

        public HttpContext Context { get; set; }

        public int CurUserId
        {
            get;
            set;
        }

        public string RequestStatus { get; set; }

        public void SendPayData()
        {
            var payObj = (ChinaPayObject)GetPayObject();
            var sendObj = ChinaPayManager.GetPayHTML(payObj);
            HttpContext.Current.Response.Write(sendObj);
        }

        public void NotifyPayData()
        {
            if (VerifyRequestData() && RequestStatus == Common.CommonConstant.ChinaPayResponseStatusSuccess)
            {
                var VCardID = VCardManager.GenerateVCard(Convert.ToInt32(TransAmt), CurUserId);
                new ChinaPayManager().UserPaySuccessed(OrdId, VCardID);
                HttpContext.Current.Response.Redirect(OnlineChargeFrm.Url(OrdId,true));
            }
        }

        public BasePayObject GetPayObject()
        {
            ChinaPayObject payObj = new ChinaPayObject();
            payObj.ChinaPayURL = System.Configuration.ConfigurationManager.AppSettings["CHINA_PAY_URL"].ToString();
            payObj.MerId = System.Configuration.ConfigurationManager.AppSettings["ChinaPayMerId"].ToString();
            bool isTest = payObj.MerId == "808080040192810" ? true : false;
            payObj.OrdId = string.IsNullOrEmpty(OrdId) ? new ChinaPayManager().GetOrdId(CurDate, isTest) : OrdId;
            payObj.TransAmt = new ChinaPayManager().GetTransAmt(TransAmt);
            payObj.CuryId = CommonConstant.ChinaPayCuryId;
            payObj.TransDate = CurDate;
            payObj.TransType = CommonConstant.ChinaPayTransType_Pay;
            payObj.Version = CommonConstant.ChinaPayVersion20070129;
            payObj.BgRetUrl = System.Configuration.ConfigurationManager.AppSettings["BgRetUrl"].ToString();
            payObj.PageRetUrl = System.Configuration.ConfigurationManager.AppSettings["PageRetUrl"].ToString();
            payObj.GateId = "";
            payObj.Priv1 = string.Format("{0}|{1}", CurUserId, payObj.OrdId);
            string plainData = payObj.MerId + payObj.OrdId + payObj.TransAmt + payObj.CuryId + payObj.TransDate + payObj.TransType + payObj.Priv1;
            payObj.ChkValue = GetSignDate(plainData);
            return payObj;
        }

        public string GetSignDate(string plainData)
        {
            NetPay pay = new NetPay();
            var merId = System.Configuration.ConfigurationManager.AppSettings["ChinaPayMerId"].ToString();
            var chinaPayPrivateKeyPath = System.Configuration.ConfigurationManager.AppSettings["ChinaPayPrivateKeyPath"].ToString();
            pay.buildKey(merId, 0, HttpContext.Current.Request.PhysicalApplicationPath + chinaPayPrivateKeyPath);
            if (pay.PrivateKeyFlag)
                return pay.Sign(plainData);
            return string.Empty;
        }

        public BasePayRequestObject GetPayRequestObject()
        {
            ChinaPayRequestObject payObj = new ChinaPayRequestObject();
            payObj.Merid = Context.Request["merid"].ToString();
            payObj.Orderno = Context.Request["orderno"].ToString();
            payObj.Transdate = Context.Request["transdate"].ToString();
            payObj.Amount = Context.Request["amount"].ToString();
            payObj.Currencycode = Context.Request["currencycode"].ToString();
            payObj.Transtype = Context.Request["transtype"].ToString();
            payObj.Status = Context.Request["status"].ToString();
            payObj.Checkvalue = Context.Request["checkvalue"].ToString();
            payObj.GateId = Context.Request["GateId"].ToString();
            payObj.Priv1 = Context.Request["Priv1"].ToString();
            return payObj;
        }

        public bool VerifyRequestData()
        {
            NetPay pay = new NetPay();
            var merId = System.Configuration.ConfigurationManager.AppSettings["ChinaPayMerId"].ToString();//999999999999999
            pay.buildKey("999999999999999", 0, HttpContext.Current.Request.PhysicalApplicationPath + CommonConstant.ChinaPayPublicKeyPath);
            ChinaPayRequestObject payObj = (ChinaPayRequestObject)GetPayRequestObject();
            if (!string.IsNullOrEmpty(payObj.Priv1))
                CurUserId = Convert.ToInt32(payObj.Priv1.Split('|')[0]);
            OrdId = payObj.Orderno;
            TransAmt = (int.Parse(payObj.Amount) / 100).ToString();
            RequestStatus = payObj.Status;
            bool flag = false;
            flag = pay.verifyTransResponse(payObj.Merid, payObj.Orderno, payObj.Amount, payObj.Currencycode, payObj.Transdate, payObj.Transtype, payObj.Status, payObj.Checkvalue);
            return flag;
        }
    }
}