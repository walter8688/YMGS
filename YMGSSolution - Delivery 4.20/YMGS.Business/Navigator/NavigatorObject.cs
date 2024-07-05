using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Business.Navigator
{
    [Serializable]
    public class NavigatorObject
    {
        public string NavigatorId { get; set; }
        public int NavigatorTypeId { get; set; }
        public string NavigatorName { get; set; }
        public string NavigatorLinkAddress { get; set; }
        public bool isZouDi { get; set; }
        public IList<NavigatorSearchObject> SearchCondition { get; set; }
    }
}
