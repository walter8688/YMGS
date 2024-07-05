using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    public class ChinaPayRequestObject : BasePayRequestObject
    {
        public string Merid
        {
            get;
            set;
        }

        public string Orderno
        {
            get;
            set;
        }

        public string Transdate
        {
            get;
            set;
        }

        public string Amount
        {
            get;
            set;
        }

        public string Currencycode
        {
            get;
            set;
        }

        public string Transtype
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Checkvalue
        {
            get;
            set;
        }

        public string GateId
        {
            get;
            set;
        }

        public string Priv1
        {
            get;
            set;
        }
    }
}
