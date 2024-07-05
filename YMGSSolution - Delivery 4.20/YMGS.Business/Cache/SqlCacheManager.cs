using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemSetting;
using YMGS.Business.EventManage;

namespace YMGS.Business.Cache
{
    /// <summary>
    /// 缓存对象接口
    /// </summary>
    public interface ICachedObject
    {
        void Refresh();

        T QueryCachedData<T>() where T:DataSet,new();
    }

    /// <summary>
    /// Sql缓存管理器
    /// </summary>
    public class SqlCacheManager
    {
        private const string _CacheSql = "SELECT CACHE_TYPE_ID,CACHE_TYPE_DESC,CHANGE_TIME FROM dbo.TB_CACHE_OBJECT";
        private static IDictionary<Int32, ICachedObject> _CachedObjectList = new Dictionary<Int32, ICachedObject>();
        private static DsCacheObject _CacheObjectInfo;

        static SqlCacheManager()
        {
            _CachedObjectList.Clear();

            //增加需要缓存并且自动刷新的缓存对象
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.EventItem, new CachedEventItem());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.EventZone, new CachedEventZone());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.Event, new CachedEvent());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.MatchAndMarket, new CachedMatch());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ChampionAndMarket, new CachedChampionMatch());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ADWords, new CachedADWords());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ADPic, new CachedADPic());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ADNotice, new CachedNotice());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ADTopRace, new CachedTopRace());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.OddsCompare, new CachedOddsCompare());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.HelpList, new CachedHelp());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ExchangeBack, new CachedExchangeBack());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.ExchangeLay, new CachedExchangeLay());
            _CachedObjectList.Add((Int32)SqlCacheDataTypeEnum.YourInPlay, new CachedYourInPlay());
            _CacheObjectInfo = SqlCacheDA.QueryCachedObject();
        }

        /// <summary>
        /// 初始化SqlDependency object
        /// </summary>
        public static void InitSqlCacheManager()
        {
            using (SqlConnection sqlConnection = new SqlConnection(DomainManager.GetLFrameworkDbConfigInfo().DBConnectionString))
            {
                SqlCommand command = new SqlCommand(_CacheSql, sqlConnection);
                command.CommandType = CommandType.Text;
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += delegate
                {
                    //更新已经被变更的缓存中的数据
                    LoadCachedData();
                    //重新初始化
                    InitSqlCacheManager();
                };

                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 判断数据是否发生了变更，如果有变化就重新加载缓存中的数据
        /// </summary>
        private static void LoadCachedData()
        {
            try
            {
                DsCacheObject curCacheObjectDS = SqlCacheDA.QueryCachedObject();
                foreach (DsCacheObject.TB_CACHE_OBJECTRow curRow in curCacheObjectDS.TB_CACHE_OBJECT)
                {
                    bool bChanged = false;
                    var matchRow = _CacheObjectInfo.TB_CACHE_OBJECT.Where(r => r.CACHE_TYPE_ID == curRow.CACHE_TYPE_ID).FirstOrDefault();
                    if (matchRow == null)
                        bChanged = true;
                    else if (matchRow.CHANGE_TIME.CompareTo(curRow.CHANGE_TIME) < 0)
                        bChanged = true;
                    else
                        bChanged = false;

                    if (bChanged)
                    {
                        if (_CachedObjectList.ContainsKey(curRow.CACHE_TYPE_ID))
                        {
                            try
                            {
                                var cachedObject = _CachedObjectList[curRow.CACHE_TYPE_ID];
                                cachedObject.Refresh();
                            }
                            catch (Exception ex)
                            {
                                LogHelper.LogError(ex.Message);                                    
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("刷新缓存数据时发生错误，错误信息如下:" + ex.Message);
            }
        }
    }
}
