using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace YMGS.Manage.Web.Common
{
    public class SystemConfigManager
    {
        /// <summary>
        /// Web服务器机器名
        /// </summary>
        public static string WebServerName
        {
            get
            {
                return ConfigurationManager.AppSettings["WebServerName"];
            }
        }

        /// <summary>
        /// http or https
        /// </summary>
        public static string URIScheme
        {
            get
            {
                return ConfigurationManager.AppSettings["URIScheme"];
            }
        }

        public static string HelperCenterDeep
        {
            get { return ConfigurationManager.AppSettings["HelperCenterDeep"]; }
        }

    }
}