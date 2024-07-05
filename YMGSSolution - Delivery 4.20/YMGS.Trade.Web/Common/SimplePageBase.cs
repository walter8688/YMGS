using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.MasterPage;

namespace YMGS.Trade.Web.Common
{
    public class SimplePageBase:BasePage
    {
        public SimpleMaster Master
        {
            get
            {
                return (base.Master as SimpleMaster);
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (Master != null)
            {
                Master.PageTitle = PageTitle;
            }
        }
    }
}