using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace YMGS.Framework
{
    public class CacheHelper
    {
        public static bool ContainCacheKey(string strCacheKey)
        {
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            if (cacheManager.Contains(strCacheKey))
                return true;
            else
                return false;
        }

        public static bool CacheData<T>(string strCacheKey, T cacheDta)
        {
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            if (cacheManager.Contains(strCacheKey))
            {
                LogHelper.LogError("this cache key exist!");
                return false;
            }
            else
            {
                cacheManager.Add(strCacheKey, cacheDta);
                return true;
            }
        }

        public static bool ClearCacheData(string strCacheKey)
        {
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            if (cacheManager.Contains(strCacheKey))
            {
                cacheManager.Remove(strCacheKey);
                return true;
            }
            else
                return false;
        }

        public static bool UpdateCacheData<T>(string strCacheKey, T cacheDta)
        {
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            if (cacheManager.Contains(strCacheKey))
            {
                cacheManager.Remove(strCacheKey);
                cacheManager.Add(strCacheKey, cacheDta);
                return true;
            }
            else
            {
                LogHelper.LogError(string.Format("this cache key {0} doesn't exist!", strCacheKey));
                return false;
            }
        }


        public static T GetCacheData<T>(string strCacheKey)
        {
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            if (!cacheManager.Contains(strCacheKey))
            {
                LogHelper.LogError("this cache key does not exist!");
                return default(T);
            }
            else
            {
                return (T)cacheManager.GetData(strCacheKey);
            }
        }
    }
}
