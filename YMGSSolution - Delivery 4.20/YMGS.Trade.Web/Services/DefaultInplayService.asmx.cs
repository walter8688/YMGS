using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;
using YMGS.Data.Common;
using YMGS.Trade.Web.Football.BusinessLogic;
using YMGS.Framework;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for DefaultInplayService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DefaultInplayService : ServiceBase
    {

        [WebMethod]
        public ServiceOperationResult<IEnumerable<FootballObject>> LoadDefaultInplayData(bool isShowAll, int language)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                InPlayFootballGameListBL fbBL = new InPlayFootballGameListBL((LanguageEnum)language);
                IEnumerable<FootballObject> fbList = fbBL.GetDefaultFootballGameList();
                if (!isShowAll && fbList != null)
                    fbList = fbList.Take(10).ToList();
                return ServiceOperationResult<IEnumerable<FootballObject>>.Success(fbList);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};LoadDefaultInplayData failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<IEnumerable<FootballObject>>.Fail("LoadDefaultInplayData failed:" + ex.Message);
            }
        }
    }
}
