using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class MatchTop3Parameter
    {
        private string _marketTmpID = string.Empty;
        public string MarketTmpID
        {
            get { return _marketTmpID; }
            set { _marketTmpID = value; }
        }

        private string _orderNO = string.Empty;
        public string OrderNO
        {
            get { return _orderNO; }
            set { _orderNO = value; }
        }

        private string _betTypeID = string.Empty;
        public string BetTypeID
        {
            get { return _betTypeID; }
            set { _betTypeID = value; }
        }

        private string _marketTmpType = string.Empty;
        public string MarketTmpType
        {
            get { return _marketTmpType; }
            set { _marketTmpType = value; }
        }

        private bool _isAutoRefresh = false;
        public bool IsAutoRefresh
        {
            get { return _isAutoRefresh; }
            set { _isAutoRefresh = value; }
        }
    }
}