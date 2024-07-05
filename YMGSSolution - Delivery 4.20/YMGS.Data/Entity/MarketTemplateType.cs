using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    /// <summary>
    /// 市场模板类型
    /// </summary>
    [Serializable]
    public class MarketTemplateType
    {
        public int MarketTmplateTypeId
        {
            get;
            set;
        }

        public string MarketTmplateTypeName
        {
            get;
            set;
        }
    }
}
