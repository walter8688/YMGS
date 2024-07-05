using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;

namespace YMGS.Business.Pay
{
    public class PayManagerAbstract
    {
        public virtual string TransAmt { get; set; }

        public virtual string OrdId { get; set; }

        public virtual int CurUserId { get; set; }

        public virtual string MerId { get; set; }

        public virtual string CurDate { get { return DateTime.Now.ToString("yyyyMMdd");} }

        public virtual string GetOrdId(string ordKey,bool isTest)
        {
            throw new NotImplementedException();
        }

        public virtual string GetTransAmt(string amtKey)
        {
            throw new NotImplementedException();
        }

        public virtual string AddUserPay()
        {
            throw new NotImplementedException();
        }

        public virtual void UserPaySuccessed(string ordId, int VCardId)
        {
        }
    }
}
