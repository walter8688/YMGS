using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;
using YMGS.Trade.Web.Football.BusinessLogic;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for FootballMatchListService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FootballMatchListService : ServiceBase
    {

        [WebMethod]
        public ServiceOperationResult<IEnumerable<FootballObject>> LoadFootballGameList(int cid, int language)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                FootballGameListBL fbBL = new FootballGameListBL((LanguageEnum)language);
                IEnumerable<FootballObject> tempList = fbBL.GetFootballGameList();
                IEnumerable<FootballObject> fbList = null;
                if (tempList != null)
                {
                    if (DateTime.Now.Hour >= 11)
                    {
                        fbList = tempList.Where(e => ((e.EventStartDate >= DateTime.Now.Date.AddDays(cid - 1).AddHours(11) && e.EventStartDate < DateTime.Now.Date.AddDays(cid).AddHours(11))));
                    }
                    else
                    {
                        fbList = tempList.Where(e => ((e.EventStartDate >= DateTime.Now.Date.AddDays(cid - 2).AddHours(11) && e.EventStartDate < DateTime.Now.Date.AddDays(cid - 1).AddHours(11))));
                    }
                }
                return ServiceOperationResult<IEnumerable<FootballObject>>.Success(fbList);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};LoadFootballMatchList failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<IEnumerable<FootballObject>>.Fail("LoadFootballMatchList failed:" + ex.Message);
            }
        }
    }
}
