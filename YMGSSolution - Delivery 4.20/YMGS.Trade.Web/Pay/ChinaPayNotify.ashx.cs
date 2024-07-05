using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Common;
using YMGS.Data.Entity;
using YMGS.Business.SystemSetting;
using YMGS.Business.Pay;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.MemberShip;
using YMGS.Trade.Web.OnlinePay;

namespace YMGS.Trade.Web.Pay
{
    /// <summary>
    /// Summary description for ChinaPayNotify
    /// </summary>
    public class ChinaPayNotify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            IPayStrategy payStrategy = null;
            payStrategy = new ChinaPayStrategy();
            payStrategy.Context = context;
            payStrategy.NotifyPayData();


            //ChinaPayRequestObject payObj = GetRespnsePayObj(context);
            //NetPay pay = new NetPay();
            ////pay.buildKey(CommonConstant.MerId, 0, context.Request.PhysicalApplicationPath + CommonConstant.PublicKeyPath);
            //pay.buildKey("999999999999999", 0, context.Request.PhysicalApplicationPath + CommonConstant.ChinaPayPublicKeyPath);
            //bool flag = false;
            ////验证是否是ChinaPay回传的数据
            //flag = pay.verifyTransResponse(payObj.Merid, payObj.Orderno, payObj.Amount, payObj.Currencycode, payObj.Transdate, payObj.Transtype, payObj.Status, payObj.Checkvalue);
            //if (!flag)
            //    return;
            ////验证支付交易是否成功
            //if (payObj.Status == Common.CommonConstant.ChinaPayResponseStatusSuccess)
            //{
            //    var userId = Convert.ToInt32(payObj.Priv1.Split('|')[0]);
            //    var VCardID = VCardManager.GenerateVCard(Convert.ToInt32(payObj.Amount), userId);
            //    new ChinaPayManager().UserPaySuccessed(payObj.Orderno, VCardID);
            //    context.Response.Redirect(OnlineChargeFrm.Url());
            //}
        }


        //public ChinaPayRequestObject GetRespnsePayObj(HttpContext context)
        //{
        //    ChinaPayRequestObject payObj = new ChinaPayRequestObject();
        //    payObj.Merid = context.Request["merid"].ToString();
        //    payObj.Orderno = context.Request["orderno"].ToString();
        //    payObj.Transdate = context.Request["transdate"].ToString();
        //    payObj.Amount = context.Request["amount"].ToString();
        //    payObj.Currencycode = context.Request["currencycode"].ToString();
        //    payObj.Transtype = context.Request["transtype"].ToString();
        //    payObj.Status = context.Request["status"].ToString();
        //    payObj.Checkvalue = context.Request["checkvalue"].ToString();
        //    payObj.GateId = context.Request["GateId"].ToString();
        //    payObj.Priv1 = context.Request["Priv1"].ToString();
        //    return payObj;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}