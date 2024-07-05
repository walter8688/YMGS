using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace YMGS.Trade.Web.Common
{
    public class SiteMapHelper
    {
        public static IEnumerable<SiteMapNode> GetSubMenus(string topName)
        {
            XmlSiteMapProvider xmlSiteProvider = GetMap();
            if (xmlSiteProvider.RootNode.Title.CompareTo(topName) == 0)
            {
                return xmlSiteProvider.RootNode.ChildNodes.Cast<SiteMapNode>();
            }
            else
            {
                var siteNodes = xmlSiteProvider.RootNode.GetAllNodes().Cast<SiteMapNode>().FirstOrDefault(x => x.Title == topName);
                return siteNodes.ChildNodes.Cast<SiteMapNode>();
            }
        }

        public static XmlSiteMapProvider GetMap()
        {
            XmlSiteMapProvider xmlSiteProvider = new XmlSiteMapProvider();
            NameValueCollection providerAttributes = new NameValueCollection();
            providerAttributes.Add("siteMapFile", "~/Web.sitemap");
            xmlSiteProvider.Initialize("xmlSiteProvider", providerAttributes);
            xmlSiteProvider.BuildSiteMap();

            return xmlSiteProvider;
        }
    }
}