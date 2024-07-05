using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text.RegularExpressions;

namespace YMGS.Trade.Web.Common
{
    public class PageAccessProvider
    {
        private static readonly IEnumerable<Type> PageObjects;
        private static readonly Regex PageObjectsNameFinder = new Regex(@"(?<name>\w*).aspx",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        static PageAccessProvider()
        {
            var basePageClass = typeof(IPageObject);

            PageObjects = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(basePageClass.IsAssignableFrom)
                .Where(t => t.Namespace.StartsWith("YMGS.Trade.Web"))
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null);
        }

        public static bool IsAccessible(UserAccess userAccess, string virtualUrl)
        {
            if (!PageObjectsNameFinder.IsMatch(virtualUrl))
            {
                return true;
            }

            var pageClassName = PageObjectsNameFinder
                .Match(virtualUrl).Groups["name"].Value;

            var pageObjectType = PageObjects
                .FirstOrDefault(t => t.Name.Equals(pageClassName, StringComparison.InvariantCultureIgnoreCase));

            if (pageObjectType == null)
            {
                return true;
            }

            IPageObject iPage;
            try
            {
                iPage = (IPageObject)Activator.CreateInstance(pageObjectType);
                return iPage.IsAccessible(userAccess);
            }
            catch
            {
                return true;
            }
        }
    }
}
