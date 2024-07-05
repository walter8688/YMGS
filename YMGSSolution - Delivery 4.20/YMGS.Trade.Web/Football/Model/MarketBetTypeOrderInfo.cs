using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class MarketBetTypeOrderInfo
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string BetTypeName_CN { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string BetTypeName_EN { get; set; }
        /// <summary>
        /// 下注类型：标准盘、波胆、大小球、让球盘
        /// </summary>
        public int BetTypeId { get; set; }
        /// <summary>
        /// 市场模板类别:全场标准盘、半场标准盘、半场4.5球 etc
        /// </summary>
        public int MarketTmpType { get; set; }
        /// <summary>
        /// 大小球时的第一个
        /// </summary>
        public double GoalsEqualsOne { get; set; }
        /// <summary>
        /// 大小球时的第二个
        /// </summary>
        public double GoalsEqualsTwo { get; set; }
        /// <summary>
        /// 排列序号
        /// </summary>
        public int OrdNo { get; set; }
        /// <summary>
        /// 是否打开 true：打开 false：折叠
        /// </summary>
        public bool isOpen { get; set; }
        /// <summary>
        /// 是否显示show all 和 summary
        /// </summary>
        public bool isShowAllOrSummary { get; set; }

    }
}