using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;

namespace YMGS.Trade.Web.Football.Controls
{
    public partial class DefaultInplayList : System.Web.UI.UserControl, IScriptControl
    {
        public int Language { get; set; }
        public string NoDataMessageStr
        {
            get { return LangManager.GetString("FbCalanderNotice"); }
        }
        public string ShowAllStr
        {
            get { return LangManager.GetString("ViewAll"); }
        }
        public string FootballStr
        {
            get { return LangManager.GetString("football"); }
        }
        public bool IsAutoRefresh { get; set; }
        public bool IsShowAll { get; set; }
        public IEnumerable<MarketFlagObject> MarketFlagList { get; set; }
        public IEnumerable<FootballObject> FootballList { get; set; }


        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.DefaultInplayList";

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
            descriptor.AddElementProperty("inPlayContent", inPlayContent.ClientID);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("noDataMessageStr", NoDataMessageStr);
            descriptor.AddProperty("showAllStr", ShowAllStr);
            descriptor.AddProperty("footballStr", FootballStr);
            descriptor.AddProperty("isAutoRefresh", IsAutoRefresh);
            descriptor.AddProperty("isShowAll", IsShowAll);
            descriptor.AddProperty("marketFlagList", MarketFlagList);
            descriptor.AddProperty("footballList", FootballList);

            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/DefaultInplayList.js");
        }
        #endregion
    }
}