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
    public partial class AsianMatchMarketTop1 : System.Web.UI.UserControl, IScriptControl
    {
        public int Language { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public CenterTop3MarketObject Top3ObjectData { get; set; }
        /// <summary>
        /// 控件的名称
        /// </summary>
        public string MarketNameTitle { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public MatchTop3Parameter Param { get; set; }
        /// <summary>
        /// 是否显示show all
        /// </summary>
        public bool IsDisplayShowAll { get; set; }

        public int Match_ID { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.AsianMatchMarketTop1";

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
            descriptor.AddElementProperty("divAsianTop1", divAsianTop1.ClientID);
            descriptor.AddProperty("dataSource", Top3ObjectData);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("buttonLst", new ButtonTitle());
            descriptor.AddProperty("matchID", Match_ID);
            descriptor.AddProperty("marketNameTitle", MarketNameTitle);
            descriptor.AddProperty("isDisplayShowAll", IsDisplayShowAll);
            descriptor.AddProperty("param", Param);
            descriptor.AddProperty("showCash", true);
            descriptor.AddProperty("showGoIn", true);
            descriptor.AddProperty("showRule", true);
            descriptor.AddProperty("showRefresh", true);
            descriptor.AddProperty("autoRefresh", Param.IsAutoRefresh);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/AsianMatchMarketTop1.js");
        }
        #endregion

    }
}