using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;
using YMGS.Data.Common;

namespace YMGS.DataAccess.SystemSetting
{
    public class SqlCacheDA:DaBase
    {
        /// <summary>
        /// 设置缓存数据变更通知
        /// </summary>
        /// <param name="cacheData"></param>
        public static void SetSqlCacheChangeNotification(SqlCacheDataTypeEnum cacheData)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Cache_Type_Id",ParameterType=DbType.Int32,ParameterValue=(int)cacheData}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_up_cache_object", parameters);
        }

        public static DsCacheObject QueryCachedObject()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DsCacheObject>("pr_get_cache_object", null);
        }
    }
}
