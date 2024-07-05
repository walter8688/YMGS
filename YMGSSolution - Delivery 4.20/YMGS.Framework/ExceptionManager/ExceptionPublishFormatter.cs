using System;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace YMGS.Framework
{
    public class ExceptionPublishFormatter : TextExceptionFormatter
    {
        public ExceptionPublishFormatter(TextWriter writer, Exception exception)
            : base(writer, exception) { }

        protected override void WriteDescription()
        {
            base.WriteDescription();
            WriteException(this.Exception);
            WriteWebInfo();
        }

        private void WriteWebInfo()
        {
            if (null == HttpContext.Current) return;

            HttpRequest lastRequest = HttpContext.Current.Request;

            if (null != lastRequest)
            {
                this.Writer.WriteLine("Web Info:\n");
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(lastRequest.ServerVariables["HTTP_HOST"]);
                    this.Writer.WriteLine("Http host = {0}", host.HostName);
                    this.Writer.WriteLine("UserHostAddress = {0}", lastRequest.UserHostAddress);
                    this.Writer.WriteLine("Remote host = {0}", lastRequest.ServerVariables["REMOTE_ADDR"]);
                    this.Writer.WriteLine("Browser = {0}", lastRequest.UserAgent);
                    this.Writer.WriteLine("Page name = {0}", lastRequest.FilePath);
                    this.Writer.WriteLine("Physical path = {0}", lastRequest.PhysicalPath);
                    this.Writer.WriteLine("Referer = {0}", lastRequest.UrlReferrer);
                    this.Writer.WriteLine("PhysicalApplicationPath = {0}", lastRequest.PhysicalApplicationPath);
                }
                catch(Exception ex) {
                    string s = ex.Message;
                    WriteException(ex);
                }
            }
            string separator = new string('-', 80);
            this.Writer.WriteLine(separator);
        }

        private void WriteException(Exception ex)
        {
            try
            {
                this.Writer.WriteLine("Error message: " + @ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    this.Writer.WriteLine("StackTrace: " + ex.StackTrace);
                }
            }
            catch
            {
            }
        }
    }
}
