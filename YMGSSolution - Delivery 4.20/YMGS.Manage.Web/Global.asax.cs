using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using YMGS.Framework;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace YMGS.Manage.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exc = Server.GetLastError();
            var type = TraceEventType.Error;

            if (exc is HttpException && (exc.Message.IndexOf("The file") == 0 || exc.Message.IndexOf("文件") == 0))
                type = TraceEventType.Information;


            ExceptionHelper.Publish(exc, type);
            ExceptionPolicy.HandleException(exc, HandlingPolicy.Publish);
            Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}