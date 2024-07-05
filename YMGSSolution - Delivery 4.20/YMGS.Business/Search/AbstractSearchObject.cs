using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.Business.Search
{
    public class AbstractSearchObject
    {
        //public AbstractSearchObject()
        //{ }
        //public AbstractSearchObject(string lan)
        //{
        //    if (lan.Trim().ToLower() == "1")
        //        this.LanguageMark = Data.Common.LanguageEnum.Chinese;
        //    else
        //        this.LanguageMark = Data.Common.LanguageEnum.English;
        //}
        public virtual object SearchData
        {
            get;
            set;
        }

        public virtual LanguageEnum LanguageMark
        {
            get;
            set;
        }

        public virtual IList<SearchObject> Search(string SearchKey)
        {
            throw new NotImplementedException();
        } 
    }
}
