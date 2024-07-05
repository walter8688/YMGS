using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.Home
{
    public partial class HomeLoginClientCtrl : System.Web.UI.UserControl,IScriptControl
    {
        public DetailUserInfo CurUser
        {
            get
            {
                if (PageHelper.GetCurrentUser() != null)
                {
                    return PageHelper.GetCurrentUser();
                }
                return null;
            }
        }

        public bool isLogin
        {
            get
            {
                if (CurUser == null)
                {
                    return false;
                }
                return true;
            }
        }

        public string UserId { 
            get
            {
                if (CurUser != null)
                    return CurUser.UserId.ToString();
                return null;
            } 
        }

        public string UserName 
        {
            get 
            {
                if (CurUser != null)
                    return CurUser.UserName;
                return null;
            }
            set
            {
                UserName = value;
            }
        }

        public string UserFund { get; set; }

        public int Language { get; set; }

        public HomeLoginClientCtrlUIText homeLoginClientCtrlUIText = new HomeLoginClientCtrlUIText();

        internal const string ClientControlType = "YMGS.Trade.Web.Home.HomeLoginClientCtrl";
        internal const string DefaultUserNameCookie = "DefaultUserNameCookie";
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
            descriptor.AddElementProperty("loginPanel", loginPanel.ClientID);
            descriptor.AddElementProperty("userInfoPanel", userInfoPanel.ClientID);
            descriptor.AddProperty("DefaultUserNameCookie", DefaultUserNameCookie);
            descriptor.AddProperty("isLogin", isLogin);
            descriptor.AddProperty("language", Language);
            descriptor.AddProperty("userId", UserId);
            descriptor.AddProperty("userName", UserName);
            descriptor.AddProperty("userFund", UserFund);
            descriptor.AddProperty("homeLoginClientCtrlUIText", homeLoginClientCtrlUIText);
            return new[] { descriptor };
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(@"~/Home/HomeLoginClientCtrl.js");
        }
        #endregion
    }
}