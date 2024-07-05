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
    public partial class ChampMatchMarketTop3 : System.Web.UI.UserControl, IScriptControl
    {
        public int Language { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public FTMarketsObj ChampData { get; set; }

        public int Champ_EventID { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.ChampMatchMarketTop3";

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
            descriptor.AddElementProperty("divMatchMarketTop3", divChampMatchMarketTop3.ClientID);
            descriptor.AddProperty("dataSource", ChampData);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("buttonLst", new ButtonTitle());
            descriptor.AddProperty("champEventID", Champ_EventID);
            descriptor.AddProperty("showCash", true);
            descriptor.AddProperty("showGoIn", false);
            descriptor.AddProperty("showRule", true);
            descriptor.AddProperty("showRefresh", true);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/ChampMatchMarketTop3.js");
        }
        #endregion
    }
}