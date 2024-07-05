using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Navigator;

namespace YMGS.Trade.Web.Football.Controls
{
    public partial class EntNavigator : System.Web.UI.UserControl,IScriptControl
    {
        public int Language { get; set; }
        public int SelectedNavigatorId { get; set; }
        public IList<NavigatorObject> NavigatorList { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.EntNavigator";
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
            descriptor.AddElementProperty("entNavigator", entNavigator.ClientID);
            descriptor.AddProperty("dataSource", NavigatorList);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("selectedNavigatorId", SelectedNavigatorId);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/EntNavigator.js");
        }
        #endregion
    }
}