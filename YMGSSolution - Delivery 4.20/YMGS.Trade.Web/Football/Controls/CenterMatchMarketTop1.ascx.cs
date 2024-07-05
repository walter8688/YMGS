using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Football.BusinessLogic;
using YMGS.Trade.Web.Football.Model;

namespace YMGS.Trade.Web.Football.Controls
{
    public partial class CenterMatchMarketTop1 : System.Web.UI.UserControl, IScriptControl
    {
        public int Language { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public FTMarketsObj top1Obj { get; set; }

        /// <summary>
        /// 控件的名称
        /// </summary>
        public string ControlTitle { get; set; }
        /// <summary>
        /// 条件筛选
        /// </summary>
        public MarketBetTypeOrderInfo BetBO { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public int MatchID { get; set; }
        /// <summary>
        /// 是否自动刷新
        /// </summary>
        public bool IsAutoRefresh { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            base.DataBind();
        }

        #region 注册UserControl为ClientControl
        protected override void OnPreRender(EventArgs e)
        {
            if (!DesignMode)
            {
                base.OnPreRender(e);
                var sm = ScriptManager.GetCurrent(Page);
                if (sm != null)
                {
                    sm.RegisterScriptControl(this);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!DesignMode)
            {
                base.Render(writer);
                var sm = ScriptManager.GetCurrent(Page);
                if (sm != null)
                {
                    sm.RegisterScriptDescriptors(this);
                }
            }
        }
        #endregion

        #region 必须实现的接口
        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptor = new ScriptControlDescriptor(ClientControlType, ClientID);
            descriptor.AddElementProperty("divMatchMarketTop1", divMatchMarketTop1.ClientID);
            descriptor.AddProperty("dataSource", top1Obj);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("buttonLst", new ButtonTitle());
            descriptor.AddProperty("matchID", MatchID);
            descriptor.AddProperty("controlTitle", ControlTitle);
            descriptor.AddProperty("betBO", BetBO);
            descriptor.AddProperty("showCash", true);
            descriptor.AddProperty("showGoIn", true);
            descriptor.AddProperty("showRule", true);
            descriptor.AddProperty("showRefresh", true);
            descriptor.AddProperty("autoRefresh", BetBO.isOpen);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/CenterMatchMarketTop1.js");
        }
        #endregion
    }
}