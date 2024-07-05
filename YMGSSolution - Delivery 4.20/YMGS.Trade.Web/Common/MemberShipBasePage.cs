using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.MasterPage;
using System.Web.Security;

namespace YMGS.Trade.Web.Common
{
    public class MemberShipBasePage :BasePage
    {
        public MemberShipMaster Master
        {
            get
            {
                return (base.Master as MemberShipMaster);
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (Master != null)
            {
                Master.TopMenuId = TopMenuId;
                Master.PageTitle = PageTitle;
            }
        }
    }
}