using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;

namespace YMGS.Business.Cache
{
    public abstract class AbstractCachedObject:ICachedObject
    {
        private static object _LockObject = new object();

        public virtual string CachedKey
        {
            get;
            set;
        }

        public virtual void Refresh()
        {
            throw new NotImplementedException();
        }

        public T QueryCachedData<T>() where T : System.Data.DataSet, new()
        {
            lock (_LockObject)
            {
                if (!CacheHelper.ContainCacheKey(CachedKey))
                {
                    Refresh();
                }
                var dsTemp = CacheHelper.GetCacheData<T>(CachedKey);
                return dsTemp;
            }
        }

    }
}
