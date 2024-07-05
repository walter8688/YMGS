using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YMGS.Data.DataBase;
using YMGS.Data.Presentation;
using YMGS.Business.AssistManage;
using YMGS.Framework;

namespace YMGS.Business.Cache
{
    public class CachedNotice : AbstractCachedObject
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        public override string CachedKey
        {
            get
            {
                return "CachedNotice";
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            DSNOTICE.TB_AD_NOTICERow tempRow = new DSNOTICE().TB_AD_NOTICE.NewTB_AD_NOTICERow();
            tempRow.PID = 0;
            tempRow.TITLE = "";
            tempRow.ISV = 1;
            var dsEvent = NoticeManager.QueryNotice(tempRow, 0);
            CacheHelper.ClearCacheData(CachedKey);
            CacheHelper.CacheData(CachedKey, dsEvent);
        }
    }
}
