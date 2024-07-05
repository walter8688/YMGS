using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;

namespace YMGS.Business.Search
{
    public class SearchManager
    {
        public static IList<SearchObject> GetAutoCompleteSearchedList(string SearchKey,string lan)
        {
            List<SearchObject> searchResult = new List<SearchObject>();
            //1. 匹配赛事区域
            var eventZoneList = (new SearchEventZone(lan)).Search(SearchKey);
           searchResult.AddRange(eventZoneList);
            //foreach (var obj in eventZoneList)
            //    searchResult.Add(obj);
            //2. 匹配赛事
            var eventList = (new SearchEvent(lan)).Search(SearchKey);
            searchResult.AddRange(eventList);
            //foreach (var obj in eventList)
            //    searchResult.Add(obj);
            //3. 匹配赛季
            var eventDateList = (new SearchEventDate(lan)).Search(SearchKey);
            searchResult.AddRange(eventDateList);

            //4. 匹配比赛
            var matchList = (new SearchMatch(lan)).Search(SearchKey);
            searchResult.AddRange(matchList);
            //foreach (var obj in matchList)
            //    searchResult.Add(obj);
            //5. 娱乐赛
            var ChampEntList = (new SearchChampEnt(lan)).Search(SearchKey);
            searchResult.AddRange(ChampEntList);
            // 处理搜索结果
            SearchObject totalObj = null;
            if (searchResult.Count > 0)
                totalObj = new SearchObject() { id = "", label = string.Format("View all results({0})", searchResult.Count), value ="" };
            else
                totalObj = new SearchObject() { id = "KeyWords", label = "No results found", value = "No results found" };
            searchResult.Add(totalObj);
            return searchResult;
        }
    }
}
