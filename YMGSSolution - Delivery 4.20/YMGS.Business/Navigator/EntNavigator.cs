using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;

namespace YMGS.Business.Navigator
{
    public class EntNavigator : AbstractNavigator
    {
        public override IList<NavigatorObject> GetNavigatots(IList<NavigatorSearchObject> navSeachObjList, LanguageEnum language)
        {
            EntNavigatorManager entNav = new EntNavigatorManager(language);
            return entNav.GetEntNavigators();
        }
    }
}
