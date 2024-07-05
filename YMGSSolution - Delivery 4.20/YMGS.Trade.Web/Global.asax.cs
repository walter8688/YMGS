using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using System.Reflection;
using System.Web.Optimization;
using YMGS.Framework;
using System.Data.SqlClient;
using YMGS.Business.Cache;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public static string CompanyName
        {
            get;
            private set;
        }

        /// <summary>
        /// 产品版本
        /// </summary>
        public static string ProductVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public static string ProductName
        {
            get;
            private set;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            SqlDependency.Start(DomainManager.GetLFrameworkDbConfigInfo().DBConnectionString);
            SqlCacheManager.InitSqlCacheManager();
     
            var thisAssemblyName = Assembly.GetExecutingAssembly().GetName(false);
            ProductVersion = thisAssemblyName.Version.ToString();
            var companyAtributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            //CompanyName = companyAtributes.Length > 0 ? ((AssemblyCompanyAttribute)companyAtributes[0]).Company : string.Empty;
            CompanyName = LangManager.GetString("CompanyName");
            //var productAttrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            //ProductName = productAttrs.Length > 0 ? ((AssemblyProductAttribute)productAttrs[0]).Product : String.Empty;

            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
            System.Data.SqlClient.SqlDependency.Stop(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
      }
    }
}