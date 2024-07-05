using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    [Serializable]
    public class UserApplyProxyStatusInfo
    {
        public int ApplyProxyStatusID
        {
            get;
            set;
        }

        public string ApplyProxyStatusName
        {
            get;
            set;
        }
    }
}
