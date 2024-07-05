using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    [Serializable]
    public class UserWithDrawStatusInfo
    {
        public int WDStatusID
        {
            get;
            set;
        }

        public string WDStatusName
        {
            get;
            set;
        }
    }
}
