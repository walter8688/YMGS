using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.OnlinePay
{
    public interface IPayStrategy
    {
        string OrdId { get; set; }
        string TransAmt { get; set; }
        int CurUserId { get; set; }
        HttpContext Context { get; set; }
        string RequestStatus { get;set; }
        void SendPayData();
        void NotifyPayData();
        bool VerifyRequestData();
        BasePayObject GetPayObject();
        BasePayRequestObject GetPayRequestObject();
    }
}