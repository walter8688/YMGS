using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using YMGS.Trade.Web.MasterPage;

namespace YMGS.Trade.Web.Common
{
    public class HomeBasePage : BasePage
    {
        string SessionKey = "SessionKey";
        //protected override void SavePageStateToPersistenceMedium(object state)
        //{
        //    LosFormatter formatter = new LosFormatter();
        //    StringWriter writer = new StringWriter();
        //    formatter.Serialize(writer, state);
        //    string viewState = writer.ToString();
        //    byte[] data = Convert.FromBase64String(viewState);
        //    byte[] compressedData = ViewStateHelper.Compress(data);
        //    string str = Convert.ToBase64String(compressedData);
            
        //    ScriptManager.RegisterHiddenField(this, "__ZIPSTATE", @str);
        //}

        //protected override object LoadPageStateFromPersistenceMedium()
        //{
        //    string custState = Request.Form["__ZIPSTATE"];
        //    byte[] b = Convert.FromBase64String(custState);
        //    b = ViewStateHelper.Decompress(b);
        //    LosFormatter losformatter = new LosFormatter();
        //    return losformatter.Deserialize(Convert.ToBase64String(b));
        //}

        public HomeMaster CurMasterPage
        {
            get
            {
                return Master as HomeMaster;
            }
        }
    }
}