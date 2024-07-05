using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Framework;

namespace YMGS.Manage.Web.Controls {
    [ValidationPropertyAttribute("Value")]
    [ControlValuePropertyAttribute("Value")]
    public partial class AjaxCalendar : UserControl, IScriptControl
    {
        internal const string ClientControlType = "YMGS.Manage.Web.Controls.AjaxCalendar";
        internal const string ScriptPath = "~/Controls/AjaxCalendar.js";

        private bool renderDefaultStyles = true;

        public bool RenderDefaultStyles {
            get { return renderDefaultStyles; }
            set { renderDefaultStyles = value; }
        }

        public TextBox TextField {
            get { return txtDateHolder; }
        }

        public DateTime? Value {
            get {
                try {
                    return  UtilityHelper.StrToDate(txtDateHolder.Text);
                } catch {
                    return null;
                }
            }
            set {
                txtDateHolder.Text = value.HasValue
                    ? UtilityHelper.DateToStr(value)
                    : string.Empty;
            }
        }

        private string width = "100%";

        [DefaultValue("100%")]
        public string Width {
            get { return width; }
            set { width = value; }
        }

        private bool readOnly;

        [DefaultValue(false)]
        public virtual bool ReadOnly {
            get { return readOnly; }
            set {
                readOnly = value;
                TextField.ReadOnly = value;
                NeedCalendarButton = !value;
            }
        }

        [Category("Appearance")]
        public bool NeedCalendarButton {
            set {
                if (value) {
                    imgDatePopup.Style.Remove(HtmlTextWriterStyle.Display);
                } else {
                    imgDatePopup.Style[HtmlTextWriterStyle.Display] = "none"; 
                }
            }
        }

        protected override void OnPreRender(EventArgs e) {
            if (RenderDefaultStyles) {
                txtDateHolder.Style.Add(HtmlTextWriterStyle.Width, "12ex");
            }
            calendarExtender.Format = CommConstant.DateFormatString;

            base.OnPreRender(e);

            var man = ScriptManager.GetCurrent(Page);
            if (!DesignMode && man != null) {
                man.RegisterScriptControl(this);
            }

            calendarExtender.OnClientDateSelectionChanged = string.Format("function() {{ OnClientDateSelectionChangedHandler($find(\"{0}\")) }}", ClientID);
        }

        protected override void Render(HtmlTextWriter writer) {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            if (RenderDefaultStyles) {
                writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width);
                writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "left");
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            base.Render(writer);

            writer.RenderEndTag();

            var man = ScriptManager.GetCurrent(Page);
            if (!DesignMode && man != null) {
                man.RegisterScriptDescriptors(this);
            }
        }

        #region IScriptControl Members

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors() {
            var descriptor = new ScriptControlDescriptor(ClientControlType, ClientID);

            descriptor.AddElementProperty("txtDateHolder", txtDateHolder.ClientID);
            descriptor.AddElementProperty("imgDatePopup", imgDatePopup.ClientID);

            yield return descriptor;
        }

        public IEnumerable<ScriptReference> GetScriptReferences() {
            yield return new ScriptReference(ScriptPath);
        }

        #endregion
    }
}