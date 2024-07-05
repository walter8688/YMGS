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
    public class SearchMatch : AbstractSearchObject
    {
        public SearchMatch(string lan)
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
                return (new CachedMatch()).QueryCachedData<DSMatchAndMarket>();
            }
        }

        public override IList<Data.Entity.SearchObject> Search(string SearchKey)
        {
            if (SearchData == null)
                return null;
            var searchResult = ((DSMatchAndMarket)SearchData).Match_List.Where(m => this.LanguageMark == LanguageEnum.Chinese ? m.MATCH_NAME.ToUpper().Contains(SearchKey.ToUpper()) : m.MATCH_NAME_EN.ToUpper().Contains(SearchKey.ToUpper()) || string.IsNullOrEmpty(SearchKey));
            SearchObject obj = null;
            IList<SearchObject> searchedData = new List<SearchObject>();
            DSEventTeamList.DSEventTeamListDataTable dt = ((new CachedEvent()).QueryCachedData<DSEventTeamList>())._DSEventTeamList;
            foreach (DSMatchAndMarket.Match_ListRow row in searchResult)
            {
                IEnumerable<DSEventTeamList.DSEventTeamListRow> list = dt.Where(s => s.EVENT_ID.ToString() == row.EVENT_ID);
                if (list.GetEnumerator().Current != null || list.Count() > 0)
                {
                    DSEventTeamList.DSEventTeamListRow result = dt.Where(s => s.EVENT_ID.ToString() == row.EVENT_ID).First();

                    obj = new SearchObject()
                    {
                        id = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}&EventId={2}&MatchStartDate={3}&MatchId={4}", result.EventItem_ID.ToString(), result.EVENTZONE_ID.ToString(), row.EVENT_ID.ToString(), row.STARTDATE.ToString("yyyy-MM-dd"), row.MATCH_ID.ToString()),
                        label = string.Format("{0} | {1}", this.LanguageMark == LanguageEnum.Chinese ? row.MATCH_NAME : row.MATCH_NAME_EN, row.STARTDATE),
                        value = string.Format("{0} | {1}", row.MATCH_NAME, row.STARTDATE)
                    };
                    searchedData.Add(obj);
                }
            }
            return searchedData;
        }
    }
}
