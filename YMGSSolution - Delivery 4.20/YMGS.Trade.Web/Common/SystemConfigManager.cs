using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace YMGS.Trade.Web.Common
{
    /// <summary>
    /// 应用程序配置信息读取类
    /// </summary>
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

        public static int DefaultGridViewPageSize
        {
            get
            {
                var size = ConfigurationManager.AppSettings["DefaultGridViewPageSize"];
                int iPageSize;
                if (!int.TryParse(size, out iPageSize))
                {
                    iPageSize = 10;
                }
                return iPageSize;
            }
        }
    }
}