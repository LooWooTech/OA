using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Caching
{
    public interface ICacheService
    {
        void Set(string key, object value, TimeSpan? expiry = null);

        T Get<T>(string key);

        void HSet(string hashId, string key, object value);

        T HGet<T>(string hashId, string key);

        List<T> HGetAll<T>(string hashId);

        void HSetAll<T>(string hashId, Dictionary<string, T> dict);

        void HRemove(string hashId, string key);

        bool Exists(string key);

        void Remove(string key);

        void Clear();

        void QPush(string key, object value);

        T QPop<T>(string key);
    }
}
