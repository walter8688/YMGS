using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Business.Navigator;
using YMGS.Data.Common;

namespace YMGS.Trade.Web.Common
{
    public class DefaultPageHelper
    {
        public static IList<NavigatorSearchObject> InitNavigatorSearchList(DefaultPageQueryStringObject defaultPageQueryStringObj)
        {
            var navigatorSearchList = new List<NavigatorSearchObject>();
            
            if (defaultPageQueryStringObj.PageId == null || defaultPageQueryStringObj.EventItemId == null)
            {
                navigatorSearchList.AddRange(new NavigatorSearchObject[]{
                    new NavigatorSearchObject(){NavigatorTypeId = (int)LeftNavigatorTypeEnum.Sports,NavigatorId = "0"},
                    //new NavigatorSearchObject(){NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventItem,NavigatorId = "1"}
                });
                return navigatorSearchList;
            }

            navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Sports, NavigatorId = "0" });
            if (defaultPageQueryStringObj.EventItemId != null)
            {
                navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventItem, NavigatorId = defaultPageQueryStringObj.EventItemId });
            }
            if (defaultPageQueryStringObj.EventZoneId != null)
            {
                navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.EventZone, NavigatorId = defaultPageQueryStringObj.EventZoneId });
            }
            if (defaultPageQueryStringObj.EventId != null)
            {
                navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Event, NavigatorId = defaultPageQueryStringObj.EventId });
            }
            //如果是冠军则Break掉
            if (defaultPageQueryStringObj.ChampionEventId != null)
            {
                return navigatorSearchList;
            }
            if (defaultPageQueryStringObj.MatchStartDate != null)
            {
                navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.MatchStartDate, NavigatorId = string.Format("{0}@{1}", defaultPageQueryStringObj.EventId, defaultPageQueryStringObj.MatchStartDate) });
            }
            if (defaultPageQueryStringObj.MatchId != null)
            {
                navigatorSearchList.Add(new NavigatorSearchObject() { NavigatorTypeId = (int)LeftNavigatorTypeEnum.Match, NavigatorId = defaultPageQueryStringObj.MatchId });
            }
            return navigatorSearchList;
        }
    }

    public class DefaultPageQueryStringObject
    {
        public string PageId { get; set; }
        public string EventItemId { get; set; }
        public string EventZoneId { get; set; }
        public string EventId { get; set; }
        public string MatchStartDate { get; set; }
        public string ChampionEventId { get; set; }
        public string MatchId { get; set; }
        public string MarketTmpId { get; set; }
        public string BetTypeId { get; set; }
        public string MarketTmpType { get; set; }
        public string OrderNO { get; set; }
    }
}