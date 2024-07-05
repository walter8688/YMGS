using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;

namespace YMGS.Trade.Web.Common
{
    public class UrlHelper
    {
        public static string GetRawUrl(Type type)
        {
            return type.Name + ".aspx";
        }
        public static string GetRawUrlAshx(Type type)
        {
            return type.Name + ".aspx";
        }

        private static Uri BuildUrl(string rawUrl, bool isFormatStrings, string relativePagePath, params object[] parameters)
        {
            const char URL_SEPARATOR = '/';
            var urlParameters = new StringBuilder();
            if (isFormatStrings)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (i != 0) urlParameters.Append("&");
                    urlParameters.AppendFormat("{0}={{{1}}}", parameters[i], i);
                }
            }
            else
            {
                // Prepare parameters
                if (parameters.Length % 2 != 0)
                {
                    throw new ArgumentException("Url helper expects pairs of arguments");
                }

                for (var i = 0; i < parameters.Length - 1; i += 2)
                {
                    if (i != 0) urlParameters.Append("&");
                    urlParameters.AppendFormat("{0}={1}", parameters[i], HttpUtility.UrlEncode(parameters[i + 1].ToString()));
                }
            }
            // Build url

            var relativePath = HttpContext.Current.Request.ApplicationPath.TrimEnd(URL_SEPARATOR);
            if (string.IsNullOrEmpty(relativePagePath))
                relativePath += URL_SEPARATOR;
            else
                relativePath += URL_SEPARATOR + relativePagePath + URL_SEPARATOR;
            relativePath += rawUrl.TrimStart(URL_SEPARATOR);

            //Configurator.UseHTTPS
            

            var result = new UriBuilder(HttpContext.Current.Request.Url)
            {
                //Host = SystemConfigManager.WebServerName,
                Host = HttpContext.Current.Request.Url.Host,
                Path = relativePath,
                Query = urlParameters.ToString(),
                Scheme = SystemConfigManager.URIScheme,
                //Port = -1
            };


            return result.Uri;
        }

        /// <summary>
        /// Build page Uri by the relative page url and parameters
        /// </summary>
        /// <param name="rawUrl">relative page url</param>
        /// <param name="parameters">Parameters for query string</param>
        /// <returns></returns>
        private static Uri BuildUrl(string rawUrl, string relativePagePath, params object[] parameters)
        {
            return BuildUrl(rawUrl, false, relativePagePath, parameters);
        }

        /// <summary>
        /// Build page Uri by the relative page url and parameters
        /// </summary>
        /// <param name="pageClassType">Type of Page Url to = typeof(pageClass)</param>
        /// <param name="parameters">Parameters for query string</param>
        /// <returns></returns>
        public static Uri BuildUrl(Type pageClassType, string relativePagePath, params object[] parameters)
        {
            return BuildUrl(GetRawUrl(pageClassType), relativePagePath, parameters);
        }

        public static Uri BuildUrlAshx(Type pageClassType, string relativePagePath, params object[] parameters)
        {
            return BuildUrl(GetRawUrlAshx(pageClassType), relativePagePath, parameters);
        }

        /// <summary>
        /// Build page Uri by the relative page url and parameters
        /// </summary>
        /// <param name="rawUrl">relative page url</param>
        /// <param name="parameters">Parameters for query string</param>
        /// <returns></returns>
        public static string BuildUrlFormat(string rawUrl, string relativePagePath, params object[] parameters)
        {
            return HttpUtility.UrlDecode(BuildUrl(rawUrl, true, relativePagePath, parameters).AbsoluteUri);
        }

        /// <summary>
        /// Build page Uri by the relative page url and parameters
        /// </summary>
        /// <param name="pageClassType">Type of Page Url to = typeof(pageClass)</param>
        /// <param name="parameters">Parameters for query string</param>
        /// <returns></returns>
        public static string BuildUrlFormat(Type pageClassType, string relativePagePath, params object[] parameters)
        {
            return BuildUrlFormat(GetRawUrl(pageClassType), relativePagePath, parameters);
        }
    }
}