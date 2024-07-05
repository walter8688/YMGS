using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Business.Cache;
using YMGS.Data.DataBase;
using System.Data;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.Business.Search
{
    public class SearchEventZone:AbstractSearchObject
    {
        public SearchEventZone(string lan)
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
                return (new CachedEventZone()).QueryCachedData<DSEventZone>();
            }
        }

        public override IList<SearchObject> Search(string SearchKey)
        {
            if (SearchData == null)
                return null;

            var seachResult = ((DSEventZone)SearchData).TB_EVENT_ZONE.Where(m => (this.LanguageMark == LanguageEnum.Chinese ? m.EVENTZONE_NAME.ToUpper().Contains(SearchKey.ToUpper()) : m.EVENTZONE_NAME_EN.ToUpper().Contains(SearchKey.ToUpper()) || string.IsNullOrEmpty(SearchKey)));
            
            SearchObject obj = null;
            IList<SearchObject> searchedData = new List<SearchObject>();
            foreach (DSEventZone.TB_EVENT_ZONERow row in seachResult)
            {
                //obj = new SearchObject() { id = string.Format("?Ent=0&item={0}&zone={1}", row.EVENTITEM_ID.ToString(), row.EVENTZONE_ID.ToString()), label = this.LanguageMark == LanguageEnum.Chinese ? row.EVENTZONE_NAME : row.EVENTZONE_NAME_EN, value = this.LanguageMark == LanguageEnum.Chinese ? row.EVENTZONE_NAME : row.EVENTZONE_NAME_EN };
                obj = new SearchObject() { id = string.Format("?PageId=0&EventItemId={0}&EventZoneId={1}", row.EVENTITEM_ID.ToString(), row.EVENTZONE_ID.ToString()), label = this.LanguageMark == LanguageEnum.Chinese ? row.EVENTZONE_NAME : row.EVENTZONE_NAME_EN, value = this.LanguageMark == LanguageEnum.Chinese ? row.EVENTZONE_NAME : row.EVENTZONE_NAME_EN };
                searchedData.Add(obj);
            }
            return searchedData;
        }
    }
}
