using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace YMGS.Framework
{

    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class ApplicationExceptionHandler : IExceptionHandler
    {
        public ApplicationExceptionHandler(NameValueCollection ignore)
        {
        }

        public ApplicationExceptionHandler()
        {
        }

        Exception IExceptionHandler.HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (exception is System.Threading.ThreadAbortException) return null;

            var httpContext = HttpContext.Current;

            var page = @"~/Error.aspx";
            if (page.StartsWith("~/") && httpContext.Request != null)
            {
                var appPath = httpContext.Request.ApplicationPath;
                if (!appPath.EndsWith("/"))
                {
                    appPath += "/";
                }

                page = page.Remove(0, 2);
                page = page.Insert(0, appPath);
            }

            // Redirect
            if (httpContext.Session == null)
            {
                string message = HttpUtility.UrlEncode(exception.Message);
                if (message.Length > 100)
                    message = message.Substring(0, 100);
                var url = string.Format("{0}?level={1}&message={2}",
                    page,
                    HttpUtility.UrlEncode("Error"),
                    message);
                httpContext.Response.Redirect(url);
            }
            else
            {
                httpContext.Session[ExceptionSessionInfo.SessionKey] = new ExceptionSessionInfo(exception,ExceptionSessionInfo.ExceptionLevel.Error, exception.Message);
                httpContext.Response.Redirect(page);
            }
            return null;
        }
    }
}
