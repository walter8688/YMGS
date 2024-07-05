using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    public class ChinaPayObject : BasePayObject
    {
        /// <summary>
        /// 提交交易数据的URL地址
        /// </summary>
        public string ChinaPayURL
        {
            get;
            set;
        }

        /// <summary>
        /// ChinaPay统一分配给商户的商户号，15位长度，必填
        /// </summary>
        public string MerId
        {
            get;
            set;
        }

        /// <summary>
        /// 商户提交给ChinaPay的交易订单号，16位长度，必填
        /// </summary>
        public string OrdId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单交易金额，单位为分，12位长度，左补0，必填
        /// </summary>
        public string TransAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 订单交易币种，3位长度，固定为人民币156,必填
        /// </summary>
        public string CuryId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单交易日期，8位长度，必填
        /// </summary>
        public string TransDate
        {
            get;
            set;
        }

        /// <summary>
        /// 交易类型，4位长度，必填
        /// </summary>
        public string TransType
        {
            get;
            set;
        }

        /// <summary>
        /// 支付接入版本号，必填
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// 后台交易接收URL，必填，长度不要超过80个字节
        /// </summary>
        public string BgRetUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 页面交易接收URL，长度不要超过80个字节，必填
        /// </summary>
        public string PageRetUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 支付网关号，可选
        /// </summary>
        public string GateId
        {
            get;
            set;
        }

        /// <summary>
        /// 商户私有域，长度不要超过60个字节，可选
        /// 商户通过此字段向Chinapay发送的信息，Chinapay依原样填充返回给商户
        /// </summary>
        public string Priv1
        {
            get;
            set;
        }

        /// <summary>
        /// 256字节长的ASCII码,为此次交易提交关键数据的数字签名，必填
        /// </summary>
        public string ChkValue
        {
            get;
            set;
        }
    }
}
