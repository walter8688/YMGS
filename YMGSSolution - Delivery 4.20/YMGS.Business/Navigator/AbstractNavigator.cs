using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.Business.Navigator
{
    public abstract class AbstractNavigator
    {
        public abstract IList<NavigatorObject> GetNavigatots(IList<NavigatorSearchObject> navSeachObjList,LanguageEnum language);
    }
}
