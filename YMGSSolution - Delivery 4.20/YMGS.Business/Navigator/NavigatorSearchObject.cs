using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Business.Navigator
{
    [Serializable]
    public class NavigatorSearchObject
    {
        public int NavigatorTypeId { get; set; }
        public string NavigatorId { get; set; }
    }
}
