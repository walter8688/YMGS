using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;
using System.Reflection;
using Resources;

namespace YMGS.Trade.Web.Common
{
    public class LangManager
    {
        public static string GetString(string strResourceKey)
        {
            return GlobalLanguage.ResourceManager.GetString(strResourceKey);
        }

    }
}