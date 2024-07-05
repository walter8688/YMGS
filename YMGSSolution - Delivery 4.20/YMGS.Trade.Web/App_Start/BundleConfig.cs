using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace YMGS.Trade.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-1.8.3.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery.ui.core.js",
            //            "~/Scripts/jquery.ui.autocomplete.js",
            //            "~/Scripts/jquery.ui.menu.js",
            //            "~/Scripts/jquery.ui.position.js",
            //            "~/Scripts/jquery.ui.widget.js"
            //            ));
            //bundles.Add(new ScriptBundle("~/bundles/common").Include(
            //        "~/Scripts/CommonJS.js"
            //    ));


            bundles.Add(new StyleBundle("~/Css/defaultcss").Include(
                        "~/Css/DefaultCss.css"));
            bundles.Add(new StyleBundle("~/Css/homedefault").Include(
                        "~/Css/HomeDefault.css"));
            bundles.Add(new StyleBundle("~/Css/JqueryUI/all").Include(
            "~/Css/JqueryUI/jquery.ui.all.css"));

        }
    }
}