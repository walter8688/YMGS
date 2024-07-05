using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;
using YMGS.Framework;
using YMGS.Trade.Web.Football.BusinessLogic;
using YMGS.Data.Common;
using System.Threading;
using System.Globalization;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for InPlayMatchListService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InPlayMatchListService : ServiceBase
    {
        [WebMethod(EnableSession = true)]
        public ServiceOperationResult<IEnumerable<FootballObject>> LoadInPlayFootballGameList(bool isShowAll, int language)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                var curUserId = base.CurrentUser == null ? -1 : base.CurrentUser.UserId;
                InPlayFootballGameListBL bl = new InPlayFootballGameListBL((LanguageEnum)language);
                var tempList = bl.GetFootballGameList(curUserId);
                if (!isShowAll && tempList != null)
                    tempList = tempList.Take(10).ToList();
                return ServiceOperationResult<IEnumerable<FootballObject>>.Success(tempList);
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};LoadInPlayFootballGameList Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<FootballObject>>.Fail(errMessage);
            }
        }

        [WebMethod(EnableSession = true)]
        public ServiceOperationResult<IEnumerable<FootballMatch>> LoadInPlayFootballMatchList(int cid, int language)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                var curUserId = base.CurrentUser == null ? -1 : base.CurrentUser.UserId;
                InPlayFootballGameListBL bl = new InPlayFootballGameListBL((LanguageEnum)language);
                var tempList = bl.GetFootballMatchList(curUserId);
                if(tempList == null)
                    return ServiceOperationResult<IEnumerable<FootballMatch>>.Success(null);
                if (cid == 2 || cid == 3)
                {
                    if (DateTime.Now.Hour >= 11)
                    {
                        tempList = tempList.Where(m => ((m.MatchStartDate >= DateTime.Now.Date.AddDays(cid - 2).AddHours(11) && m.MatchStartDate < DateTime.Now.Date.AddDays(cid - 1).AddHours(11))));
                    }
                    else
                    {
                        tempList = tempList.Where(m => ((m.MatchStartDate >= DateTime.Now.Date.AddDays(cid - 3).AddHours(11) && m.MatchStartDate < DateTime.Now.Date.AddDays(cid - 2).AddHours(11))));
                    }
                    return ServiceOperationResult<IEnumerable<FootballMatch>>.Success(tempList);
                }
                else
                {
                    tempList = tempList.Where(m => m.IsMatchFaved == 1);
                    return ServiceOperationResult<IEnumerable<FootballMatch>>.Success(tempList);
                }
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};LoadInPlayFootballMatchList Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<FootballMatch>>.Fail(errMessage);
            }
        }

        [WebMethod(EnableSession=true)]
        public ServiceOperationResult<IEnumerable<string>> AddYourInPlay(string id, int faved, int matchId, int language)
        {
            try
            {
                if (base.CurrentUser == null)
                {
                    base.InitializeCulture((LanguageEnum)language);
                    return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("Pleaseloginfirst"));
                }
                else
                {
                    IList<string> idList = new List<string>();
                    idList.Add(id);
                    YourInPlayManager.ManageYourInPlay(base.CurrentUser.UserId, matchId, 1 - faved);
                    return ServiceOperationResult<IEnumerable<string>>.Success(idList);
                }
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};AddYourInPlay Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<string>>.Fail(errMessage);
            }
        }
    }
}
