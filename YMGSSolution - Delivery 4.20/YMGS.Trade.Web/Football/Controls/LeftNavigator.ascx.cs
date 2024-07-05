using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.Navigator;
using YMGS.Data.Common;

namespace YMGS.Trade.Web.Football.Controls
{
    public partial class LeftNavigator : System.Web.UI.UserControl, IScriptControl
    {
        public int Language { get; set; }
        public IList<NavigatorObject> NavigatorList { get; set; }

        internal const string ClientControlType = "YMGS.Trade.Web.Football.Controls.LeftNavigator";

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
            descriptor.AddElementProperty("leftNavigator", leftNavigator.ClientID);
            descriptor.AddProperty("dataSource", NavigatorList);
            descriptor.AddProperty("clientId", ClientID);
            descriptor.AddProperty("language", Language);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Football/Controls/LeftNavigator.js");
        }
        #endregion
    }
}