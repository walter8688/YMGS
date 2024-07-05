using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    public class PersistBroker
    {
        public static IPersistBroker GetPersistBroker()
        {
            return PersistBrokerFactory.Instance().NewPersistBroker();
        }
    }
}
