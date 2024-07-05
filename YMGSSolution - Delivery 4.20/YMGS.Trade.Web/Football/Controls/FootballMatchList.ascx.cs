using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Football.Model;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.Football.Controls
{
    public partial class FootballMatchList : System.Web.UI.UserControl,IScriptControl
    {
        public int Language { get; set; }
        public string NoDataMessageStr 
        { 
            get { return LangManager.GetString("FbCalanderNotice"); } 
        }
        public int FootballCalanderId { get; set; }
        public bool IsAutoRefresh { get; set; }
        public IEnumerable<MarketFlagObject> MarketFlagList { get; set; }
        public IList<FootballCalendar> FootballCalendarList { get; set; }
        public IList<FootballObject> FootballList { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.FootballMatchList";
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
            descriptor.AddElementProperty("footballMatchList", footballMatchList.ClientID);
            descriptor.AddElementProperty("footballContent", footballContent.ClientID);
            descriptor.AddProperty("calanderDataSource", FootballCalendarList);
            descriptor.AddProperty("dataSource", FootballList);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("noDataMessageStr", NoDataMessageStr);
            descriptor.AddProperty("footballCalanderId", FootballCalanderId);
            descriptor.AddProperty("isAutoRefresh", IsAutoRefresh);
            descriptor.AddProperty("marketFlagList", MarketFlagList);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/FootballMatchList.js");
        }
        #endregion
    }
}