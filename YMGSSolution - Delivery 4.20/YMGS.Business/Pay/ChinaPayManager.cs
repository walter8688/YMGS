using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using YMGS.DataAccess.Pay;
using YMGS.Data.DataBase;
using YMGS.Data.Common;
using System.Configuration;

namespace YMGS.Business.Pay
{
    public class ChinaPayManager : PayManagerAbstract
    {
        #region 构造函数
        public ChinaPayManager()
        {
        }

        public ChinaPayManager(string transAmt, string ordId, int curUserId, string merId)
        {
            base.TransAmt = transAmt;
            base.OrdId = ordId;
            base.CurUserId = curUserId;
            base.MerId = merId;
        }
        #endregion

        #region 获取订单号
        /// <summary>
        /// 获取新的OrdId
        /// </summary>
        /// <returns></returns>
        public override string GetOrdId(string transDate,bool isTest)
        {
            var dsOrderID = PayDA.QueryMaxOrderId(transDate);
            if (!isTest)
            {
                if (dsOrderID == null)
                    return DateTime.Now.ToString("yyyyMMdd") + "00000001";
                if (dsOrderID.Tables[0].Rows.Count == 0 || string.IsNullOrEmpty(dsOrderID.Tables[0].Rows[0][0].ToString().Trim()))
                    return DateTime.Now.ToString("yyyyMMdd") + "00000001";
                var curMaxOrderId = dsOrderID.Tables[0].Rows[0][0].ToString();
                var orderNum = curMaxOrderId.Replace(transDate, "");
                orderNum = (Convert.ToInt32(orderNum) + 1).ToString().PadLeft(8, '0');
                return transDate + orderNum;
            }
            else
            {
                if(dsOrderID == null)
                    return DateTime.Now.ToString("yyyyMMdd") + "001" + "92810";
                if (dsOrderID.Tables[0].Rows.Count == 0 || string.IsNullOrEmpty(dsOrderID.Tables[0].Rows[0][0].ToString().Trim()))
                    return DateTime.Now.ToString("yyyyMMdd") + "001" + "92810";
                var curMaxOrderId = dsOrderID.Tables[0].Rows[0][0].ToString();
                var orderNum = curMaxOrderId.Replace(transDate, "").Replace("92810","");
                orderNum = (Convert.ToInt32(orderNum) + 1).ToString().PadLeft(3, '0');
                return transDate + orderNum + "92810";
            }
        }
        #endregion

        #region 格式化交易金额
        /// <summary>
        /// 生成交易金额
        /// </summary>
        /// <param name="transAmt"></param>
        /// <returns></returns>
        public override string GetTransAmt(string transAmt)
        {
            string strTransAmt = transAmt.IndexOf('.') > 0 ? ((int)(decimal.Parse(transAmt) * 100)).ToString() : (int.Parse(transAmt) * 100).ToString();
            strTransAmt = strTransAmt.PadLeft(12, '0');
            return strTransAmt;
        }
        #endregion

        #region 新增充值记录
        /// <summary>
        /// 新增用户充值记录
        /// </summary>
        /// <param name="userPay"></param>
        public override string AddUserPay()
        {
            var UserPay = GetChinaPayCurUserPay();
            PayDA.AddUserPay(UserPay);
            return UserPay.ORDER_ID;
        }
        #endregion

        #region 获取银联充值对象
        private DSUserPay.TB_USER_PAYRow GetChinaPayCurUserPay()
        {
            DSUserPay.TB_USER_PAYRow userPay = new DSUserPay.TB_USER_PAYDataTable().NewTB_USER_PAYRow();
            userPay.USER_ID = base.CurUserId;
            userPay.MER_ID = base.MerId;
            bool isTest = userPay.MER_ID == "808080040192810" ? true : false;
            userPay.ORDER_ID = string.IsNullOrEmpty(base.OrdId) ? new ChinaPayManager().GetOrdId(base.CurDate, isTest) : base.OrdId;
            userPay.TRAN_AMOUNT = decimal.Parse(TransAmt);
            userPay.TRAN_STATUS = (int)UserPayStatus.WaitingPay;
            userPay.VCARD_ID = -1;
            userPay.TRAN_TYPE = (int)UserPayTypeEnum.ChinaPay;
            return userPay;
        }
        #endregion

        #region 带参数新增充值记录
        public void AddUserPay(DSUserPay.TB_USER_PAYRow UserPay)
        {
            PayDA.AddUserPay(UserPay);
        }
        #endregion

        #region 充值成功
        /// <summary>
        /// 用户充值成功
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="VCardId"></param>
        public override void UserPaySuccessed(string ordId, int VCardId)
        {
            PayDA.UserPaySuccessed(ordId, VCardId);
        }
        #endregion 

        #region 获取用户充值记录
        /// <summary>
        /// 获取用户充值记录
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DSUserPay QueryUserPay(DateTime startDate, DateTime endDate, int userId)
        {
            return PayDA.QueryUserPay(startDate, endDate, userId);
        }
        #endregion

        #region 产生银联交互数据
        /// <summary>
        /// 获取传给ChinaPay的HTML内容
        /// </summary>
        /// <param name="payObj"></param>
        /// <param name="PrivateKeyFlag"></param>
        /// <returns></returns>
        public static string GetPayHTML(ChinaPayObject payObj)
        {
            if (payObj == null)
                return null;

            StringBuilder payHtml = new StringBuilder();
            payHtml.Append("<form name='chinapayForm' action='" + payObj.ChinaPayURL + "' method='post'>");//支付地址
            payHtml.Append("<input type='hidden' name='MerId' value='" + payObj.MerId + "' />");//商户号
            payHtml.Append("<input type='hidden' name='OrdId' value='" + payObj.OrdId + "' />");//订单号
            payHtml.Append("<input type='hidden' name='TransAmt' value='" + payObj.TransAmt + "' />");//支付金额
            payHtml.Append("<input type='hidden' name='CuryId' value='" + payObj.CuryId + "' />");//交易币种
            payHtml.Append("<input type='hidden' name='TransDate' value='" + payObj.TransDate + "' />");//交易日期
            payHtml.Append("<input type='hidden' name='TransType' value='" + payObj.TransType + "' />");//交易类型
            payHtml.Append("<input type='hidden' name='Version' value='" + payObj.Version + "' />");//支付接入版本号
            payHtml.Append("<input type='hidden' name='BgRetUrl' value='" + payObj.BgRetUrl + "' />");//后台接收应答地址
            payHtml.Append("<input type='hidden' name='PageRetUrl' value='" + payObj.PageRetUrl + "' />");//为页面接收应答地址
            payHtml.Append("<input type='hidden' name='GateId' value='" + payObj.GateId + "' />");//支付网关号
            payHtml.Append("<input type='hidden' name='Priv1' value='" + payObj.Priv1 + "' />");//商户私有域
            payHtml.Append("<input type='hidden' name='ChkValue' value='" + payObj.ChkValue + "' />");//此次交易所提交的关键数据的数字签名
            payHtml.Append("<script>");
            payHtml.Append("document.chinapayForm.submit();");
            payHtml.Append("</script></form>");
            return payHtml.ToString();
        }
        #endregion
    }
}
