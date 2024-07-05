using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using YMGS.Framework;

namespace YMGS.Trade.Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayErrorMessage();
            }
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        private void DisplayErrorMessage()
        {
            ExceptionSessionInfo excInfo;
            if (Page.Session != null && Page.Session[ExceptionSessionInfo.SessionKey] != null)
            {
                excInfo = Page.Session[ExceptionSessionInfo.SessionKey] as ExceptionSessionInfo;
                Page.Session.Remove(ExceptionSessionInfo.SessionKey);
            }
            else
            {
                string strLevel = Page.Request.QueryString["level"];
                ExceptionSessionInfo.ExceptionLevel level;
                try
                {
                    level = (ExceptionSessionInfo.ExceptionLevel)Enum.Parse(typeof(ExceptionSessionInfo.ExceptionLevel), strLevel, true);
                }
                catch (Exception)
                {
                    level = ExceptionSessionInfo.ExceptionLevel.Error;
                }

                excInfo = new ExceptionSessionInfo(
                    null,
                    level,
                    Page.Request.QueryString["message"]);
            }

            ViewState[ExceptionSessionInfo.SessionKey] = excInfo;
            lblErrorMessage.Text = excInfo.Message;
            DisplayDetailErrorMessage(excInfo);
        }

        private void DisplayDetailErrorMessage(ExceptionSessionInfo excInfo)
        {
#if DEBUG

            const string EXCEPTION_MESSAGE_FORMAT = "<br /><br /><pre>{0}</pre>";
            StringBuilder sb = new StringBuilder();
            Exception ex = excInfo.Exception;
            while (ex != null)
            {
                if (!(ex is System.Web.HttpUnhandledException) || ex.InnerException == null)
                {
                    sb.AppendFormat(EXCEPTION_MESSAGE_FORMAT, ex);
                }
                ex = ex.InnerException;
            }

            lblErrorDetail.Text = sb.ToString();
            lblErrorDetail.Visible = true;
#else
            lblErrorDetail.Visible = false;
#endif
        }
    }
}