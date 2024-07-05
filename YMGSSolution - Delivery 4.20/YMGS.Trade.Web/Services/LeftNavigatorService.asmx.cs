using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Business.Navigator;
using YMGS.Trade.Web.Common;


namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for LeftNavigatorService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LeftNavigatorService : ServiceBase
    {
        [WebMethod]
        public ServiceOperationResult<IEnumerable<NavigatorObject>> LoadLeftNavigator(IList<NavigatorSearchObject> searchConditonList,int Language)
        {
            try
            {
                NavigatorManager navigator = new NavigatorManager();
                var tempList = navigator.GetLeftNavigators(searchConditonList, (LanguageEnum)Language);
                return ServiceOperationResult<IEnumerable<NavigatorObject>>.Success(tempList);
            }
            catch(Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};LoadLeftNavigator failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<IEnumerable<NavigatorObject>>.Fail("LoadLeftNavigator failed:" + ex.Message);
            }
        }
    }
}
