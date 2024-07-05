using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.Business.Search
{
    public class SearchEvent : AbstractSearchObject
    {
        public SearchEvent(string lan)
        {
            if (lan.Trim().ToLower() == "1")
                this.LanguageMark = Data.Common.LanguageEnum.Chinese;
            else
                this.LanguageMark = Data.Common.LanguageEnum.English;
        }
        public override object SearchData
        {
            get
            {
                return (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            }
        }

        public override IList<Data.Entity.SearchObject> Search(string SearchKey)
        {
            if (SearchData == null)
                return null;
            var searchResult = ((DSEventTeamList)SearchData)._DSEventTeamList.Where(m => this.LanguageMark == LanguageEnum.Chinese ? m.EVENT_NAME.ToUpper().Contains(SearchKey.ToUpper()) : m.EVENT_NAME_EN.ToUpper().Contains(SearchKey.ToUpper()) || string.IsNullOrEmpty(SearchKey));
            SearchObject obj = null;
            IList<SearchObject> searchedData = new List<SearchObject>();
            foreach (DSEventTeamList.DSEventTeamListRow row in searchResult)
            {
                obj = new SearchObject() { id = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}", row.EventItem_ID.ToString(), row.EVENTZONE_ID.ToString(), row.EVENT_ID.ToString()), label = this.LanguageMark == LanguageEnum.Chinese ? row.EVENT_NAME : row.EVENT_NAME_EN, value = this.LanguageMark == LanguageEnum.Chinese ? row.EVENT_NAME : row.EVENT_NAME_EN };
                searchedData.Add(obj);
            }
            return searchedData;
        }
    }

    public class SearchEventDate : AbstractSearchObject
    {
        public SearchEventDate(string lan)
        {
            if (lan.Trim().ToLower() == "1")
                this.LanguageMark = Data.Common.LanguageEnum.Chinese;
            else
                this.LanguageMark = Data.Common.LanguageEnum.English;
        }
        public override object SearchData
        {
            get
            {
                return (new CachedEvent()).QueryCachedData<DSEventTeamList>();
            }
        }

        public override IList<Data.Entity.SearchObject> Search(string SearchKey)
        {
            if (SearchData == null)
                return null; 
            var objTemp5 = (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            var ds = (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            var data = ds.Match_List.Where(m=>m.STARTDATE.ToString().ToUpper().Contains(SearchKey.ToUpper())|| string.IsNullOrEmpty(SearchKey));
           
            var champdata=objTemp5.ChampEventList.Where(t=>t.Champ_Event_Type ==1 && t.Champ_Event_Status==1 && (this.LanguageMark ==  LanguageEnum.Chinese ? t.Champ_Event_Name.ToUpper().Contains(SearchKey.ToUpper()) : t.CHAMP_EVENT_NAME_EN.ToUpper().Contains(SearchKey.ToUpper())));
                 
            //var searchResult = ((DSEventTeamList)SearchData)._DSEventTeamList.Where(m => this.LanguageMark == LanguageEnum.Chinese ? m.EVENT_NAME.ToUpper().Contains(SearchKey.ToUpper()) : m.EVENT_NAME_EN.ToUpper().Contains(SearchKey.ToUpper()) || string.IsNullOrEmpty(SearchKey));
            SearchObject obj = null;
            IList<SearchObject> searchedData = new List<SearchObject>();
            DSEventTeamList.DSEventTeamListDataTable dt=((DSEventTeamList)SearchData)._DSEventTeamList;
            foreach (DSMatchAndMarket.Match_ListRow row in data)
            {
                DSEventTeamList.DSEventTeamListRow result =dt.Where(s => s.EVENT_ID.ToString() == row.EVENT_ID).First();

                obj = new SearchObject() { id = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}", result.EventItem_ID.ToString(), result.EVENTZONE_ID.ToString(), row.EVENT_ID, row.STARTDATE.ToString("yyyy-MM-dd")), label = this.LanguageMark == LanguageEnum.Chinese ? row.EVENT_NAME + "|" + row.STARTDATE.ToString("yyyy-MM-dd") : row.EVENT_NAME_EN + "|" + row.STARTDATE.ToString("yyyy-MM-dd"), value = this.LanguageMark == LanguageEnum.Chinese ? row.EVENT_NAME + "|" + row.STARTDATE.ToString("yyyy-MM-dd") : row.EVENT_NAME_EN + "|" + row.STARTDATE.ToString("yyyy-MM-dd") };
                searchedData.Add(obj);
            }


            foreach (DSChampionEventAndMarket.ChampEventListRow row in champdata)
            {
                IEnumerable<DSEventTeamList.DSEventTeamListRow> list= dt.Where(s => s.EVENT_ID == row.Event_ID);
                if (list.GetEnumerator().Current != null || list.Count() > 0)
                {
                    DSEventTeamList.DSEventTeamListRow result = list.First();

                    obj = new SearchObject() { id = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}", result.EventItem_ID.ToString(), result.EVENTZONE_ID.ToString(), row.Event_ID.ToString(), row.Champ_Event_ID.ToString()), label = this.LanguageMark == LanguageEnum.Chinese ? "体育|冠军|" + row.Champ_Event_Name : "Sprots|Champion|" + row.CHAMP_EVENT_NAME_EN, value = "" };
                    searchedData.Add(obj);
                }
            }
            return searchedData;
        }
    }

    public class SearchChampEnt : AbstractSearchObject
    {
        public SearchChampEnt(string lan)
        {
            if (lan.Trim().ToLower() == "1")
                this.LanguageMark = Data.Common.LanguageEnum.Chinese;
            else
                this.LanguageMark = Data.Common.LanguageEnum.English;
        }
        public override object SearchData
        {
            get
            {
                return (new CachedChampionMatch()).QueryCachedData<DSChampionEventAndMarket>();
            }
        }

        public override IList<Data.Entity.SearchObject> Search(string SearchKey)
        {
            if (SearchData == null)
                return null;
            var champdata = ((DSChampionEventAndMarket)SearchData).ChampEventList.Where(t => (t.Champ_Event_Type == 2 && t.Champ_Event_Status == 1 && (this.LanguageMark == LanguageEnum.Chinese ? t.Champ_Event_Name.ToUpper().Contains(SearchKey.ToUpper()) : t.CHAMP_EVENT_NAME_EN.ToUpper().Contains(SearchKey.ToUpper()))) || string.IsNullOrEmpty(SearchKey));

            SearchObject obj = null;
            IList<SearchObject> searchedData = new List<SearchObject>();
            foreach (DSChampionEventAndMarket.ChampEventListRow row in champdata)
            {
                obj = new SearchObject() { id = string.Format("?PageId=3&ChampEventId={0}", row.Champ_Event_ID.ToString()), label = this.LanguageMark == LanguageEnum.Chinese ? "娱乐|冠军|" + row.Champ_Event_Name : "Ent|Champion|" + row.CHAMP_EVENT_NAME_EN, value = "" };
                searchedData.Add(obj);
            }
            return searchedData;
        }
    }
}
