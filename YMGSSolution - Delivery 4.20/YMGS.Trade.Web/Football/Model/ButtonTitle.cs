using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.Football.Model
{
    /// <summary>
    /// 按钮的名字——存储多语言时的名称
    /// </summary>
    public class ButtonTitle
    {
        /// <summary>
        /// Lines
        /// </summary>
        public string LinesTitleName { get { return LangManager.GetString("LinesTitle"); } }
        /// <summary>
        /// Summary
        /// </summary>
        public string SummaryTitleName { get { return LangManager.GetString("SummaryTitle"); } }
        /// <summary>
        /// Show ALL
        /// </summary>
        public string ShowAllTitleName { get { return LangManager.GetString("ShowAllTitle"); } }
        /// <summary>
        /// Back
        /// </summary>
        public string BackTitleName { get { return LangManager.GetString("BackTitle"); } }
        /// <summary>
        /// Lay
        /// </summary>
        public string LayTitleName { get { return LangManager.GetString("LayTitle"); } }
        /// <summary>
        /// Cash Out
        /// </summary>
        public string CashOutTitleName { get { return LangManager.GetString("CashOutTitle"); } }
        /// <summary>
        /// Go In-Play
        /// </summary>
        public string GoInPlayTitleName { get { return LangManager.GetString("GoInPlayTitle"); } }
        /// <summary>
        /// Rules
        /// </summary>
        public string RulesTitleName { get { return LangManager.GetString("RulesTitle"); } }
        /// <summary>
        /// Refresh
        /// </summary>
        public string RefreshTitleName { get { return LangManager.GetString("RefreshTitle"); } }
        /// <summary>
        /// View Full Market
        /// </summary>
        public string ViewFullMarketTitleName { get { return LangManager.GetString("ViewFullMarketTitle"); } }
        /// <summary>
        /// Matched：
        /// </summary>
        public string MatchedTitleName { get { return LangManager.GetString("MatchedTitle"); } }
    }
}