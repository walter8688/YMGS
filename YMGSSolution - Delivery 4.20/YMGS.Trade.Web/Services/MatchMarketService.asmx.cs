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
using YMGS.Trade.Web.Football.Common;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for MatchMarketService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MatchMarketService : ServiceBase
    {
        [WebMethod(EnableSession = false)]
        public ServiceOperationResult<Top1DataSource> GetTop1MatchMarketDetailsList(int Language, int Match_ID, MarketBetTypeOrderInfo BetBO)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)Language);

                FTCenterTopBL matchMarketBL = new FTCenterTopBL(Match_ID, (LanguageEnum)Language);

                IList<MarketBetTypeOrderInfo> marketBetTypeLst = new FootballCommonFunction().GetMarketBetTypeOrderInfoByMatchID(Match_ID);
                BetBO = marketBetTypeLst.Where(e => e.OrdNo == BetBO.OrdNo).FirstOrDefault();
                var matchMarketList = matchMarketBL.GetWebServiceTop1Data(Match_ID, BetBO);

                Top1DataSource top1Data = new Top1DataSource();
                top1Data.matchSource = matchMarketList;
                top1Data.betOrder = BetBO;
                return ServiceOperationResult<Top1DataSource>.Success(top1Data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};Load Match Market failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<Top1DataSource>.Fail("Load Match Market failed:" + ex.Message);
            }
        }


        [WebMethod(EnableSession = false)]
        public ServiceOperationResult<Top3DataSource> GetTop3MatchMarketDetalsList(int Language, int Match_ID, MatchTop3Parameter Param, bool isAll)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)Language);

                FTCenterTopBL matchMarketBL = new FTCenterTopBL(Match_ID, (LanguageEnum)Language);
                var matchMarketObject = matchMarketBL.GetWebServiceTop3DataAllAndSummary(Match_ID, Param, isAll);
                string controlTitleName = matchMarketBL.GetTop3ControlTitle(Match_ID, Param);

                Top3DataSource top3Data = new Top3DataSource();
                top3Data.matchSource = matchMarketObject;
                top3Data.titleName = controlTitleName;

                return ServiceOperationResult<Top3DataSource>.Success(top3Data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};Load Match Market failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<Top3DataSource>.Fail("Load Match Market failed:" + ex.Message);
            }
        }

        [WebMethod(EnableSession = false)]
        public ServiceOperationResult<FTMarketsObj> GetTop3ChampMatchMarketDetalsList(int Language, int ChampEventID)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)Language);

                ChampMatchBL champBL = new ChampMatchBL((LanguageEnum)Language);
                var champLst = champBL.GetChampMatchMarketInfoByEventID(ChampEventID);
                return ServiceOperationResult<FTMarketsObj>.Success(champLst);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};Load Match Market failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<FTMarketsObj>.Fail("Load Match Market failed:" + ex.Message);
            }
        }

        [WebMethod(EnableSession = false)]
        public ServiceOperationResult<FootballMatch> GetMatchRealTitleInfo(int Language, int Match_ID)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)Language);

                InPlayFootballGameListBL bl = new InPlayFootballGameListBL((LanguageEnum)Language);
                var matchRealBO = bl.GetSingleMatchRealInfo(Match_ID);

                return ServiceOperationResult<FootballMatch>.Success(matchRealBO);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("DateTime:{0};Load Match Real Info failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message));
                return ServiceOperationResult<FootballMatch>.Fail("Load Match Real Info failed:" + ex.Message);
            }
        }
    }
}
