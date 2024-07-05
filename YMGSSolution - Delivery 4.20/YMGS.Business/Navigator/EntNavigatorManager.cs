using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;

namespace YMGS.Business.Navigator
{
    public class EntNavigatorManager
    {
        public LanguageEnum Language { get; set; }

        public EntNavigatorManager() { }
        public EntNavigatorManager(LanguageEnum Language) { this.Language = Language; }

        public IList<NavigatorObject> GetEntNavigators()
        {
            var Champds = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            var data = (from s in Champds.ChampEventList
                        where s.Champ_Event_Type == 2 && s.Champ_Event_Status == 1
                        orderby s.Champ_Event_StartDate,s.Champ_Event_Name
                        select new { s.Champ_Event_ID, Champ_Event_Name = Language == LanguageEnum.Chinese ? s.Champ_Event_Name : s.CHAMP_EVENT_NAME_EN });
            IList<NavigatorObject> navigatorObjList = new List<NavigatorObject>();
            NavigatorObject navigatorObj;
            foreach (var item in data)
            {
                navigatorObj = new NavigatorObject()
                {
                    NavigatorId = item.Champ_Event_ID.ToString(),
                    NavigatorName = item.Champ_Event_Name,
                    NavigatorLinkAddress = string.Format(@"Default.aspx?PageId=3&ChampionEventId={0}", item.Champ_Event_ID)
                };
                navigatorObjList.Add(navigatorObj);
            }
            return navigatorObjList;
        }
    }
}
