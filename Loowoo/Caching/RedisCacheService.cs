using Loowoo.Common;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loowoo.Caching
{
    public class RedisCacheService : ICacheService
    {
        private ConnectionMultiplexer redis;
        public RedisCacheService()
        {
            var conn = AppSettings.Current["Redis"];
            redis = ConnectionMultiplexer.Connect(conn ?? "127.0.0.1");
        }

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            var db = redis.GetDatabase();
            db.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }

        public T Get<T>(string key)
        {
            var db = redis.GetDatabase();
            var value = db.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value.ToString()) : default(T);
        }

        public void HSet(string hashId, string key, object value)
        {
            var db = redis.GetDatabase();
            db.HashSet(hashId, key, JsonConvert.SerializeObject(value));
        }

        public T HGet<T>(string hashId, string key)
        {
            var db = redis.GetDatabase();
            var value = db.HashGet(hashId, key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value.ToString()) : default(T);
        }

        public List<T> HGetAll<T>(string hashId)
        {
            var db = redis.GetDatabase();
            return db.HashGetAll(hashId).Select(kv => JsonConvert.DeserializeObject<T>(kv.Value)).ToList();
        }

        public void HSetAll<T>(string hashId, Dictionary<string, T> dict)
        {
            var db = redis.GetDatabase();
            var fiels = new List<HashEntry>();
            foreach (var kv in dict)
            {
                fiels.Add(new HashEntry(kv.Key, JsonConvert.SerializeObject(kv.Value)));
            }
            db.HashSetAsync(hashId, fiels.ToArray());
        }

        public void Remove(string key)
        {
            var db = redis.GetDatabase();
            db.KeyDelete(key);
        }

        public void Clear()
        {
            try
            {
                var endpoints = redis.GetEndPoints();
                var server = redis.GetServer(endpoints.FirstOrDefault());
                server.FlushDatabaseAsync();
            }
            catch { }
        }


        public bool Exists(string key)
        {
            var db = redis.GetDatabase();
            return db.KeyExists(key);
        }



        public void HRemove(string hashId, string key)
        {
            var db = redis.GetDatabase();
            db.HashDeleteAsync(hashId, key);
        }

        public void QPush(string key, object value)
        {
            var db = redis.GetDatabase();
            db.ListLeftPush(key, value.ToJson());
        }

        public T QPop<T>(string key)
        {
            var db = redis.GetDatabase();
            var json = db.ListRightPop(key);
            if (json.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }
    }
}
