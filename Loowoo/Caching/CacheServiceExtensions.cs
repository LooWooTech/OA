using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Caching
{
    public static class CacheServiceExtensions
    {
        public static T GetOrSet<T>(this ICacheService cache, string key, Func<T> getValueFunc, TimeSpan? expiry = null)
        {
            var val = cache.Get<T>(key);
            if (val == null)
            {
                val = getValueFunc();
                cache.Set(key, val, expiry);
            }
            return val;
        }

        public static T HGetOrSet<T>(this ICacheService cache, string hashId, string key, Func<T> getValueFunc)
        {
            var val = cache.HGet<T>(hashId, key);
            if (val == null)
            {
                val = getValueFunc();
                cache.HSet(hashId, key, val);
            }
            return val;
        }

    }
}
