using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.Entity;
using YMGS.Business.GameMarket;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Football.Model;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for BetService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BetService : ServiceBase
    {
        protected virtual bool IsCheckSecurity
        {
            get
            {
                return true;
            }
        }

        [WebMethod(EnableSession = true)]
        public ServiceOperationResult<IEnumerable<string>> PlaceBet(int language,IEnumerable<BetInfoObject> backInfo, IEnumerable<BetInfoObject> layInfo)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                if (base.CurrentUser == null)
                {
                    return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("loginfirst"));
                }
                else
                {
                    MatchMarcketInfo tempMarketInfo = null;
                    decimal totalAmount = 0;
                    HashSet<MatchMarcketInfo> backMarketInfo = new HashSet<MatchMarcketInfo>();
                    HashSet<MatchMarcketInfo> layMarketInfo = new HashSet<MatchMarcketInfo>();
                    if (backInfo != null)
                    {
                        foreach (var item in backInfo)
                        {
                            totalAmount += item.matchAmounts;
                            tempMarketInfo = new MatchMarcketInfo();
                            tempMarketInfo.MATCHTYPE = item.matchType;
                            tempMarketInfo.MATCH_ID = item.matchId;
                            tempMarketInfo.MATCH_NAME = item.matchName;
                            tempMarketInfo.MARKET_ID = item.marketId;
                            tempMarketInfo.MARKET_NAME = item.marketName;
                            tempMarketInfo.MARKET_TMP_ID = item.marketTmpId;
                            tempMarketInfo.MARKET_TMP_NAME = item.marketTmpName;
                            tempMarketInfo.odds = item.Odds.ToString();
                            tempMarketInfo.AMOUNTS = item.matchAmounts.ToString();
                            backMarketInfo.Add(tempMarketInfo);
                        }
                    }
                    if (layInfo != null)
                    {
                        foreach (var item in layInfo)
                        {
                            totalAmount += item.matchAmounts;
                            tempMarketInfo = new MatchMarcketInfo();
                            tempMarketInfo.MATCHTYPE = item.matchType;
                            tempMarketInfo.MATCH_ID = item.matchId;
                            tempMarketInfo.MATCH_NAME = item.matchName;
                            tempMarketInfo.MARKET_ID = item.marketId;
                            tempMarketInfo.MARKET_NAME = item.marketName;
                            tempMarketInfo.MARKET_TMP_ID = item.marketTmpId;
                            tempMarketInfo.MARKET_TMP_NAME = item.marketTmpName;
                            tempMarketInfo.odds = item.Odds.ToString();
                            tempMarketInfo.AMOUNTS = item.matchAmounts.ToString();
                            layMarketInfo.Add(tempMarketInfo);
                        }
                    }
                    string returnValue = MatchManager.PlaceBet(totalAmount, base.CurrentUser.UserId, backMarketInfo, layMarketInfo);
                    if (string.IsNullOrEmpty(returnValue))
                    {
                        IList<string> strList = new List<string>();
                        strList.Add(LangManager.GetString("betSeccess"));
                        return ServiceOperationResult<IEnumerable<string>>.Success(strList);
                    }
                    else
                    {
                        returnValue = returnValue.Replace("'", " ");
                        string[] results = returnValue.Split(new string[] { "," }, StringSplitOptions.None);
                        if (results[0] == "999")
                        {
                            string exceptionstring = LangManager.GetString("pleasecancelitem") + results[1];
                            return ServiceOperationResult<IEnumerable<string>>.Fail(exceptionstring);
                        }
                        else
                        {
                            return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("betFail"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};PlaceBet Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<string>>.Fail(errMessage);
            }
        }
    }
}
