using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.Business.Navigator
{
    public class LeftNavigator : AbstractNavigator
    {
        public override IList<NavigatorObject> GetNavigatots(IList<NavigatorSearchObject> navSeachObjList, LanguageEnum language)
        {
            NavigatorManager navigator = new NavigatorManager();
            return navigator.GetLeftNavigators(navSeachObjList, language);
        }
    }
}
